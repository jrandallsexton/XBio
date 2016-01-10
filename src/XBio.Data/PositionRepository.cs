
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

using XBio.Core.Dtos;
using XBio.Core.Interfaces;
using XBio.Core.Models;

namespace XBio.Data
{
    public class PositionRepository : RepositoryBase
    {

        public IEnumerable<KvpItem> GetPositionsByPersonId(int personId)
        {
            var positions = new List<KvpItem>();

            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = Queries.PositionsLookupByPersonId;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("PersonId", SqlDbType.Int) { Value = personId });
                conn.Open();

                using (var rdr = cmd.ExecuteReader())
                {
                    if (!rdr.HasRows)
                        return null;

                    while (rdr.Read())
                    {
                        positions.Add(new KvpItem(rdr.GetInt32(0), rdr.GetString(1)));
                    }
                }
            }

            return positions;
        }

        public IEnumerable<PositionDto> GetPositionDtos(int personId)
        {
            var positions = new List<PositionDto>();

            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = Queries.PositionDtosGetByPersonId;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("PersonId", SqlDbType.Int) { Value = personId });
                conn.Open();

                using (var rdr = cmd.ExecuteReader())
                {
                    if (!rdr.HasRows)
                        return null;

                    while (rdr.Read())
                    {
                        var positionId = rdr.GetInt32(0);
                        var position = new PositionDto
                        {
                            Company = rdr.GetString(1),
                            Title = rdr.GetString(2),
                            Telecommute = rdr.GetInt32(3) == 1 ? true : false,
                            Summary = rdr.IsDBNull(4) ? string.Empty : rdr.GetString(4),
                            StartDate = rdr.GetDateTime(5),
                            EndDate = rdr.IsDBNull(6) ? DateTime.MaxValue : rdr.GetDateTime(6),
                            City = rdr.IsDBNull(7) ? string.Empty : rdr.GetString(7),
                            State = rdr.IsDBNull(8) ? string.Empty : rdr.GetString(8),
                            Details = GetDetailDtos(positionId)
                        };
                        positions.Add(position);
                    }
                }
            }

            return positions;
        }

        public Position Get(int id)
        {
            Position position = null;

            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = Queries.PositionGet;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("PositionId", SqlDbType.Int) {Value = id});
                conn.Open();

                using (var rdr = cmd.ExecuteReader())
                {
                    if (!rdr.HasRows)
                        return null;

                    rdr.Read();

                    position = new Position
                    {
                        Id = rdr.GetInt32(0),
                        PersonId = rdr.GetInt32(1),
                        CompanyId = rdr.GetInt32(2),
                        TitleId = rdr.GetInt32(3),
                        StartDate = rdr.GetDateTime(4),
                        EndDate = rdr.IsDBNull(5) ? DateTime.MinValue : rdr.GetDateTime(5),
                        Summary = rdr.IsDBNull(6) ? string.Empty : rdr.GetString(6)
                    };

                    if (!rdr.IsDBNull(5))
                        position.EndDate = rdr.GetDateTime(5);

                    if (!rdr.NextResult())
                        return position;

                    position.Details = GetDetails(rdr);
                }
            }

            return position;
        }

        public void Save(Position position)
        {

            var paramList = new List<SqlParameter>
            {
                new SqlParameter("Id", SqlDbType.Int) {Value = position.Id},
                new SqlParameter("PersonId", SqlDbType.Int) {Value = position.PersonId},
                new SqlParameter("CompanyId", SqlDbType.Int) {Value = position.CompanyId},
                new SqlParameter("TitleId", SqlDbType.Int) {Value = position.TitleId},
                new SqlParameter("StartDate", SqlDbType.Date) {Value = position.StartDate},
                new SqlParameter("Summary", SqlDbType.NVarChar) {Value = position.Summary}
            };

            string queryName;
            if (position.EndDate.HasValue)
            {
                queryName = Queries.PositionSave;
                paramList.Add(new SqlParameter("EndDate", SqlDbType.Date) {Value = position.EndDate});
            }
            else
            {
                queryName = Queries.PositionSaveCurrent;
            }

            var positionId = ExecuteIdentity(queryName, paramList);

            if (position.Details.Any())
                position.Details.ToList().ForEach(x => { Save(positionId, x); });

            position.Id = positionId;

        }

        public void Delete(int positionId)
        {
            var sqlStatement = "DELETE FROM [PositionDetail] WHERE [PositionId] = @Id; DELETE FROM [Position] WHERE [Id] = @Id";
            var paramList = new List<SqlParameter>
            {
                new SqlParameter("Id", SqlDbType.Int) {Value = positionId}
            };

            ExecuteInLineSql(sqlStatement, paramList);
        }

        private List<PositionDetail> GetDetails(IDataReader rdr)
        {
            var details = new List<PositionDetail>();

            while (rdr.Read())
            {
                details.Add(new PositionDetail
                {
                    Id = rdr.GetInt32(0),
                    PositionId = rdr.GetInt32(1),
                    Title = rdr.IsDBNull(2) ? string.Empty : rdr.GetString(2),
                    Value = rdr.GetString(3),
                    Order = rdr.GetInt32(4)
                });
            }

            return details;
        }

        private IEnumerable<PositionDetailDto> GetDetailDtos(int positionId)
        {
            var values = new List<PositionDetailDto>();

            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = Queries.PositionDetailDtosGetByPositionId;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("PositionId", SqlDbType.Int) { Value = positionId });
                conn.Open();

                using (var rdr = cmd.ExecuteReader())
                {
                    if (!rdr.HasRows)
                        return null;

                    while (rdr.Read())
                    {

                        var position = new PositionDetailDto
                        {
                            Title = rdr.IsDBNull(0) ? string.Empty : rdr.GetString(0),
                            Value = rdr.GetString(1),
                            Order = rdr.GetInt32(2)
                        };
                        values.Add(position);
                    }
                }
            }

            return values;
        }

        private void Save(int positionId, IPositionDetail detail)
        {
            if (detail.Deleted.HasValue)
            {
                DeletePositionDetail(detail.Id);
                return;
            }

            detail.PositionId = positionId;
            var paramList = new List<SqlParameter>
            {
                new SqlParameter("Id", SqlDbType.Int) {Value = detail.Id},
                new SqlParameter("PositionId", SqlDbType.Int) {Value = detail.PositionId},
                new SqlParameter("Title", SqlDbType.NVarChar) {Value = string.IsNullOrEmpty(detail.Title) ? "[]" : detail.Title},
                new SqlParameter("Value", SqlDbType.NVarChar) {Value = detail.Value},
                new SqlParameter("Order", SqlDbType.Int) {Value = detail.Order}
            };

            detail.Id = ExecuteIdentity(Queries.PositionDetailSave, paramList);
        }

        private void DeletePositionDetail(int id)
        {
            var sqlStatement = "DELETE FROM [PositionDetail] WHERE [Id] = @Id";
            var paramList = new List<SqlParameter>
            {
                new SqlParameter("Id", SqlDbType.Int) {Value = id}
            };

            ExecuteInLineSql(sqlStatement, paramList);
        }
    }
}