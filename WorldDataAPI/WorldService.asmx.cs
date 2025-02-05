using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WorldDataAPI
{
    /// <summary>
    /// Summary description for WorldService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WorldService : System.Web.Services.WebService
    {

        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["WorldDB"].ConnectionString;

        [WebMethod]
        public DataTable GetAllCountries()
        {
            string query = "SELECT * FROM country";
            return ExecuteQuery(query);
        }

        [WebMethod]
        public DataTable GetCountryByCode(string countryCode)
        {
            string query = "SELECT * FROM country WHERE Code = @Code";
            return ExecuteQuery(query, new SqlParameter("@Code", countryCode));
        }

        [WebMethod]
        public DataTable GetCityByName(string cityName)
        {
            string query = "SELECT * FROM city WHERE Name LIKE @Name";
            return ExecuteQuery(query, new SqlParameter("@Name", "%" + cityName + "%"));
        }

        [WebMethod]
        public DataTable GetCitiesByCountryCode(string countryCode)
        {
            string query = "SELECT * FROM city WHERE CountryCode = @CountryCode";
            return ExecuteQuery(query, new SqlParameter("@CountryCode", countryCode));
        }

        [WebMethod]
        public DataTable GetCountriesByRegion(string region)
        {
            string query = "SELECT * FROM country WHERE Region = @Region";
            return ExecuteQuery(query, new SqlParameter("@Region", region));
        }

        [WebMethod]
        public int GetPopulationByCountry(string countryCode)
        {
            string query = "SELECT Population FROM country WHERE Code = @Code";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Code", countryCode);
                    conn.Open();
                    return (int)cmd.ExecuteScalar();
                }
            }
        }



        private DataTable ExecuteQuery(string query, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.TableName = "Result"; // Đặt tên cho DataTable
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }
    }
}
