
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using XBio.Core.Models;

namespace XBio.Data
{
    public class PostalRepository : RepositoryBase
    {
        public void Save(Postal postal)
        {
            var paramList = new List<SqlParameter>
            {
                new SqlParameter("Id", SqlDbType.Int) {Value = postal.Id},
                new SqlParameter("CityId", SqlDbType.Int) {Value = postal.CityId},
                new SqlParameter("Value", SqlDbType.VarChar) {Value = postal.Value}
            };

            postal.Id = ExecuteIdentity(Queries.PostalSave, paramList);
        }
    }
}