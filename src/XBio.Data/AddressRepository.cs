
using System;
using System.Data;
using System.Data.SqlClient;

using XBio.Core.Models;
using XBio.Core.Dtos;

namespace XBio.Data
{
    public class AddressRepository : RepositoryBase
    {
        public Address Get(int id)
        {
            Address address = null;

            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = Queries.AddressGet;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("AddressId", SqlDbType.Int) { Value = id });
                conn.Open();

                using (var rdr = cmd.ExecuteReader())
                {
                    if (!rdr.HasRows)
                        return null;

                    rdr.Read();

                    address = new Address
                    {
                        Addr1 = rdr.GetString(0),
                        Addr2 = rdr.IsDBNull(1) ? string.Empty : rdr.GetString(1),
                        PostalId = rdr.GetInt32(2),
                        Name = rdr.IsDBNull(3) ? string.Empty : rdr.GetString(3)
                    };

                }
            }

            return address;
        }

        public AddressDto GetAddressDtoByPersonId(int personId)
        {
            AddressDto address = null;

            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = Queries.AddressDtoGetByPersonId;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("PersonId", SqlDbType.Int) { Value = personId });
                conn.Open();

                using (var rdr = cmd.ExecuteReader())
                {
                    if (!rdr.HasRows)
                        return null;

                    rdr.Read();

                    address = new AddressDto
                    {
                        Addr1 = rdr.GetString(0),
                        Addr2 = rdr.IsDBNull(1) ? string.Empty : rdr.GetString(1),
                        City = rdr.IsDBNull(2) ? string.Empty : rdr.GetString(2),
                        State = rdr.IsDBNull(3) ? string.Empty : rdr.GetString(3),
                        Postal = rdr.IsDBNull(4) ? string.Empty : rdr.GetString(4)
                    };

                }
            }

            return address;
        }
    }
}