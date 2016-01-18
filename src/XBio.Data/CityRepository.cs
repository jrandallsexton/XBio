using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBio.Core.Models;

namespace XBio.Data
{
    public class CityRepository : RepositoryBase
    {
        public City Search(int stateId, string cityName)
        {
            string sql =
                "SELECT [Id], [StateId], [Name], [Lat], [Lon] FROM [City] WHERE [StateId] = @StateId AND [Name] = @Name";

            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("StateId", SqlDbType.Int) { Value = stateId });
                cmd.Parameters.Add(new SqlParameter("Name", SqlDbType.VarChar) { Value = cityName });
                conn.Open();

                using (var rdr = cmd.ExecuteReader())
                {
                    if (!rdr.HasRows)
                        return null;

                    if (rdr.Read())
                    {
                        try
                        {
                            return new City()
                            {
                                Id = rdr.GetInt32(0),
                                StateId = rdr.GetInt32(1),
                                Name = rdr.GetString(2),
                                Lat = rdr.IsDBNull(3) ? 0 : (float)rdr.GetDouble(3),
                                Lon = rdr.IsDBNull(4) ? 0 : (float)rdr.GetDouble(4)
                            };
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                    }

                }
            }

            return null;
        }

        public void Save(City city)
        {
            var paramList = new List<SqlParameter>
            {
                new SqlParameter("Id", SqlDbType.Int) {Value = city.Id},
                new SqlParameter("StateId", SqlDbType.Int) {Value = city.StateId},
                new SqlParameter("Name", SqlDbType.VarChar) {Value = city.Name},
                new SqlParameter("Lat", SqlDbType.Float) {Value = city.Lat},
                new SqlParameter("Lon", SqlDbType.Float) {Value = city.Lon}
            };

            city.Id = ExecuteIdentity(Queries.CitySave, paramList);
        }
    }
}