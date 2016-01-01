﻿
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
            var values = new List<KvpItem>
            {
                new KvpItem(2, "Sr. Developer - Bechtel Corporation"),
                new KvpItem(1, "Programmer/Analyst - DPRA, Inc.")
            };
            return values;
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
                            StartDate = rdr.GetDateTime(4),
                            EndDate = rdr.IsDBNull(5) ? DateTime.MaxValue : rdr.GetDateTime(5),
                            City = rdr.IsDBNull(6) ? string.Empty : rdr.GetString(6),
                            State = rdr.IsDBNull(7) ? string.Empty : rdr.GetString(7),
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
                        StartDate = rdr.GetDateTime(4)
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

        public void Save(IPosition position)
        {

            var paramList = new List<SqlParameter>
            {
                new SqlParameter("Id", SqlDbType.Int) {Value = position.Id},
                new SqlParameter("PersonId", SqlDbType.Int) {Value = position.PersonId},
                new SqlParameter("CompanyId", SqlDbType.Int) {Value = position.CompanyId},
                new SqlParameter("TitleId", SqlDbType.Int) {Value = position.TitleId},
                new SqlParameter("StartDate", SqlDbType.Date) {Value = position.StartDate},
                new SqlParameter("EndDate", SqlDbType.Date) {Value = position.EndDate}
            };

            var positionId = ExecuteIdentity(Queries.PositionSave, paramList);

            if (position.Details.Any())
                position.Details.ToList().ForEach(x => { Save(positionId, x); });

            position.Id = positionId;

        }

        private List<IPositionDetail> GetDetails(IDataReader rdr)
        {
            var details = new List<IPositionDetail>();

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
    }
}