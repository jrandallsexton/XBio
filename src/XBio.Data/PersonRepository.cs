using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBio.Core.Dtos;
using XBio.Core.Interfaces;
using XBio.Core.Models;

namespace XBio.Data
{
    public class PersonRepository : RepositoryBase
    {
        public IPerson Get(int id)
        {
            throw new NotImplementedException();
        }

        public PersonDto GetPerson(int id)
        {
            PersonDto person = null;

            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = Queries.PersonDtoGet;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("PersonId", SqlDbType.Int) { Value = id });
                conn.Open();

                using (var rdr = cmd.ExecuteReader())
                {
                    if (!rdr.HasRows)
                        return null;

                    rdr.Read();

                    person = new PersonDto
                    {
                        Id = id,
                        LastName = rdr.GetString(0),
                        FirstName = rdr.GetString(1),
                        MiddleName = rdr.GetString(2),
                        Display = rdr.GetString(3)
                    };

                }

                person.Address = new AddressRepository().GetAddressDtoByPersonId(id);
                person.Positions = new PositionRepository().GetPositionDtos(id);
            }

            return person;
        }

        private List<Person> LoadFromReader(IDataReader rdr)
        {
            throw new NotImplementedException();
        }
    }
}