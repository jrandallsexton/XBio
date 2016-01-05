using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBio.Core.Dtos;
using XBio.Core.Models;

namespace XBio.Data
{
    public class TechnologyRepository : RepositoryBase
    {
        public IEnumerable<Select2Item> GetTechnologies()
        {
            var values = new List<Select2Item>();

            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT [Id], [Name] FROM [Technology] ORDER BY [Name]";
                cmd.CommandType = CommandType.Text;
                conn.Open();

                using (var rdr = cmd.ExecuteReader())
                {
                    if (!rdr.HasRows)
                        return null;

                    while (rdr.Read())
                    {
                        values.Add(new Select2Item(rdr.GetInt32(0), rdr.GetString(1)));
                    }
                }
            }

            return values;
        }

        public IEnumerable<KvpItem> GetTechnologiesByType(int technologyTypeId)
        {
            var values = new List<KvpItem>();

            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT [Id], [Name] FROM [Technology] WHERE [TechnologyTypeId] = @TechnologyTypeId ORDER BY [Name]";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("TechnologyTypeId", SqlDbType.Int) { Value = technologyTypeId });
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

        public void Save(Technology technology)
        {
            throw new NotImplementedException();
        }
    }
}