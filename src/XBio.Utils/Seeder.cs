using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using XBio.Data;
using XBio.Core.Models;
using XBio.Service;

namespace XBio.Utils
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

        public void ImportCitiesCsv()
        {
            string FileName = @"F:\Projects\GitHub\XBio\src\data\cities.csv";
            OleDbConnection conn = new OleDbConnection
                ("Provider=Microsoft.Jet.OleDb.4.0; Data Source = " +
                 Path.GetDirectoryName(FileName) +
                 "; Extended Properties = \"Text;HDR=YES;FMT=Delimited\"");

            conn.Open();

            OleDbDataAdapter adapter = new OleDbDataAdapter
                ("SELECT * FROM " + Path.GetFileName(FileName), conn);

            DataSet ds = new DataSet("Temp");
            adapter.Fill(ds);

            conn.Close();

            var stateList = new Dictionary<string, int>();
            var service = new StateService();
            var pService = new PostalService();
            var cService = new CityService();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var zip = row[0].ToString().Trim();
                if (zip.Length < 5)
                    zip = "0" + zip;
                var state = row[1].ToString();
                var city = row[2].ToString().Trim();
                var lat = float.Parse(row[3].ToString().Trim());
                var lon = float.Parse(row[4].ToString().Trim());

                if (!stateList.ContainsKey(state))
                {
                    var stateId = service.GetIdByAbbreviation(240, state);
                    stateList.Add(state, stateId);
                    //Console.WriteLine("{0}\t{1}", state, stateId);
                }

                int test;
                if (int.TryParse(city, out test))
                {
                    Console.WriteLine("Need city name for {0}", city);
                    continue;
                }

                // do we already have a city in that state?
                var existingCity = cService.Search(stateList[state], city);
                if (existingCity == null)
                {
                    // insert the city
                    existingCity = new City()
                    {
                        Name = city,
                        Id = 0,
                        Lat = lat,
                        Lon = lon,
                        StateId = stateList[state]
                    };
                    cService.Save(existingCity);
                }

                // insert the postal
                var newPostal = new Postal()
                {
                    Id = 0,
                    Value = zip,
                    CityId = existingCity.Id
                };
                pService.Save(newPostal);
            }
        }
    }
}