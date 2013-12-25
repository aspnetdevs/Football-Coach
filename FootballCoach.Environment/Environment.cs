using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;

namespace FootballCoach.Environment
{
    public static class GameEnvironment
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["FootballCoachConnectionString"].ConnectionString;
        public static SqlConnection GetSqlConnection()
        {
            return new SqlConnection(connectionString);
        }
        public static bool IsAuthenticated
        {
            get;
            set;
        }
    }
}
