
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

using XBio.Core.Models;

namespace XBio.Data
{
    public class SkillRepository : RepositoryBase
    {
        public List<Skill> GetSkills(int personId)
        {
            var values = new List<Skill>();

            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = Queries.SkillsGetByPersonId;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("PersonId", SqlDbType.Int) { Value = personId });
                conn.Open();

                using (var rdr = cmd.ExecuteReader())
                {
                    if (!rdr.HasRows)
                        return null;

                    while (rdr.Read())
                    {
                        var skill = new Skill
                        {
                            Id = rdr.GetInt32(0),
                            PersonId = personId,
                            TechnologyId = rdr.GetInt32(2),
                            FirstUsedYear = rdr.GetInt32(3),
                            LastUsedYear = rdr.GetInt32(4),
                            NumYearsUsed = rdr.IsDBNull(5) ? int.MinValue : rdr.GetDecimal(5),
                            Created = rdr.GetDateTime(6)
                        };
                        values.Add(skill);
                    }
                }
            }

            return values;
        }

        public void Save(IEnumerable<Skill> skills)
        {
            if (skills == null)
                return;

            skills.ToList().ForEach(Save);
        }

        private void Save(Skill skill)
        {
            if (skill.Deleted.HasValue)
            {
                Delete(skill.Id);
                return;
            }

            var paramList = new List<SqlParameter>
            {
                new SqlParameter("Id", SqlDbType.Int) {Value = skill.Id},
                new SqlParameter("PersonId", SqlDbType.Int) {Value = skill.PersonId},
                new SqlParameter("TechnologyId", SqlDbType.Int) {Value = skill.TechnologyId},
                new SqlParameter("FirstUsedYear", SqlDbType.Int) {Value = skill.FirstUsedYear},
                new SqlParameter("LastUsedYear", SqlDbType.Int) {Value = skill.LastUsedYear},
                new SqlParameter("NumYearsUsed", SqlDbType.Decimal) {Value = skill.NumYearsUsed}
            };

            skill.Id = ExecuteIdentity(Queries.SkillSave, paramList);

        }

        private void Delete(int skillId)
        {
            var sqlStatement = "DELETE FROM [Skill] WHERE [Id] = @Id";
            var paramList = new List<SqlParameter>
            {
                new SqlParameter("Id", SqlDbType.Int) {Value = skillId}
            };

            ExecuteInLineSql(sqlStatement, paramList);
        }
    }
}
