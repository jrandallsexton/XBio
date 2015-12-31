
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
                new KvpItem(0, "Sr. Developer - Bechtel Corporation"),
                new KvpItem(1, "Sr. Developer - SERVPRO Industries")
            };
            return values;
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