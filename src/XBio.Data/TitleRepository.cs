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
    public class TitleRepository : RepositoryBase
    {
        public IEnumerable<KvpItem> GetLookups()
        {
            var positions = new List<KvpItem>();

            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT [Id], [Value] FROM [Title] ORDER BY [Value]";
                cmd.CommandType = CommandType.Text;
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
    }
}
