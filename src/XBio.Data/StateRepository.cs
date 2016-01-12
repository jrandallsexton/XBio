using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBio.Core.Dtos;

namespace XBio.Data
{
    public class StateRepository : RepositoryBase
    {
        public IEnumerable<KvpItem> GetStates(int countryId)
        {
            var values = new List<KvpItem>();

            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT [Id], [Name] FROM [State] WHERE [CountryId] = @CountryId ORDER BY [Name]";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("CountryId", SqlDbType.Int) { Value = countryId });
                conn.Open();

                using (var rdr = cmd.ExecuteReader())
                {
                    if (!rdr.HasRows)
                        return null;

                    while (rdr.Read())
                    {
                        values.Add(new KvpItem(rdr.GetInt32(0), rdr.GetString(1)));
                    }
                }
            }

            return values;
        }

        public int GetIdByAbbreviation(int countryId, string abbreviation)
        {
            var sqlStatement = "SELECT [Id] FROM [State] WHERE [CountryId] = @CountryId AND [Abbreviation] = @Abbrev";
            var paramList = new List<SqlParameter>()
            {
                new SqlParameter("@CountryId", countryId),
                new SqlParameter("@Abbrev", abbreviation)
            };
            return base.ExecuteScalar<int>(sqlStatement, paramList);
        }
    }
}