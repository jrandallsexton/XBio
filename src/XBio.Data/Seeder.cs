using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBio.Core.Dtos;

namespace XBio.Data
{
    public class Seeder : RepositoryBase
    {
        public void SeedCountries()
        {
            var values = new Dictionary<string, string>();
            using (var conn = new SqlConnection("Persist Security Info=False;Initial Catalog=AVREF;Data Source=DBSVR01;User Id=sa;Password=sesame1?"))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT *  FROM [AVREF].[dbo].[Country] ORDER BY COUNTRY_NAME";
                cmd.CommandType = CommandType.Text;
                conn.Open();

                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        values.Add(rdr.GetString(0), rdr.GetString(1));
                    }
                }
            }

            var sql = "INSERT INTO [Country] ([Abbreviation], [Name]) VALUES ('{0}', '{1}')";
            foreach (var kvp in values)
            {
                var temp = string.Format(sql, kvp.Key, kvp.Value.Replace("'", "''"));
                base.ExecuteInLineSql(temp, null);
            }
        }

        public void SeedStates()
        {
            var values = new Dictionary<string, string>
            {
                {"AK", "Alaska"},
                {"AL", "Alabama"},
                {"AR", "Arkansas"},
                {"AZ", "Arizona"},
                {"CA", "California"},
                {"CO", "Colorado"},
                {"CT", "Connecticut"},
                {"DC", "District Of Columbia"},
                {"DE", "Delaware"},
                {"FL", "Florida"},
                {"GA", "Georgia"},
                {"HI", "Hawaii"},
                {"IA", "Iowa"},
                {"ID", "Idaho"},
                {"IL", "Illinois"},
                {"IN", "Indiana"},
                {"KS", "Kansas"},
                {"KY", "Kentucky"},
                {"LA", "Louisiana"},
                {"MA", "Massachusetts"},
                {"MD", "Maryland"},
                {"ME", "Maine"},
                {"MI", "Michigan"},
                {"MN", "Minnesota"},
                {"MO", "Missouri"},
                {"MS", "Mississippi"},
                {"MT", "Montana"},
                {"NC", "North Carolina"},
                {"ND", "North Dakota"},
                {"NE", "Nebraska"},
                {"NH", "New Hampshire"},
                {"NJ", "New Jersey"},
                {"NM", "New Mexico"},
                {"NV", "Nevada"},
                {"NY", "New York"},
                {"OH", "Ohio"},
                {"OK", "Oklahoma"},
                {"OR", "Oregon"},
                {"PA", "Pennsylvania"},
                {"RI", "Rhode Island"},
                {"SC", "South Carolina"},
                {"SD", "South Dakota"},
                {"TN", "Tennessee"},
                {"TX", "Texas"},
                {"UT", "Utah"},
                {"VA", "Virginia"},
                {"VT", "Vermont"},
                {"WA", "Washington"},
                {"WI", "Wisconsin"},
                {"WV", "West Virginia"},
                {"WY", "Wyoming"}
            };

            const int countryId = 240;
            var sql = "INSERT INTO [State] ([Abbreviation], [Name], [CountryId]) VALUES ('{0}', '{1}', {2})";
            foreach (var kvp in values)
            {
                var temp = string.Format(sql, kvp.Key, kvp.Value, countryId);
                base.ExecuteInLineSql(temp, null);
            }

        }
    }
}
