
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using XBio.Core.Interfaces;
using XBio.Core.Models;

namespace XBio.Data
{
    public class PositionRepository : RepositoryBase
    {
        public Position Get(int id)
        {
            throw new NotImplementedException();
        }

        public int Save(IPosition position)
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

            return positionId;

        }

        private void Save(int positionId, IPositionDetail detail)
        {

        }
    }
}