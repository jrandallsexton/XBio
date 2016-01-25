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
    public class CompanyRepository : RepositoryBase
    {

        public Company Get(int id)
        {
            Company company = null;

            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = Queries.CompanyGet;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("CompanyId", SqlDbType.Int) { Value = id });
                conn.Open();

                using (var rdr = cmd.ExecuteReader())
                {
                    if (!rdr.HasRows)
                        return null;

                    rdr.Read();

                    company = new Company
                    {
                        Id = rdr.GetInt32(0),
                        Name = rdr.GetString(1),
                        Url = rdr.IsDBNull(2) ? string.Empty : rdr.GetString(2)
                    };

                    if (!rdr.IsDBNull(3))
                    {
                        var addrId = rdr.GetInt32(3);
                        company.HQ = new AddressRepository().Get(addrId);
                    }                        

                }
            }

            return company;
        }

        public IEnumerable<KvpItem> GetLookups()
        {
            var values = new List<KvpItem>();

            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT [Id], [Name] FROM [Company] ORDER BY [Name]";
                cmd.CommandType = CommandType.Text;
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

        public IEnumerable<Select2Item> Search(string searchTerm)
        {
            var values = new List<Select2Item>();

            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = string.Format("SELECT [Id], [Name] FROM [Company] WHERE [Name] LIKE '%{0}%' ORDER BY [Name]", searchTerm);
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
    }
}
