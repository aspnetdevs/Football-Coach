using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballCoach.Environment;

namespace FootballCoach.DbEngine
{
    public static class DbHelper
    {
        private static SqlConnection sqlConnection = GameEnvironment.GetSqlConnection();

        public static void CreateProcess(string dbProcessName, string name, string userLogin, byte[] data, IDictionary<string, string> processParameters = null)
        {
            if (GameEnvironment.IsAuthenticated)
            {
                DataRow userData;
                if (IsUser(userLogin, out userData))
                {
                    StringBuilder stringParameters = new StringBuilder();

                    Dictionary<string, object> parameters = new Dictionary<string, object>();
                    parameters.Add("@name", name);
                    parameters.Add("@user", new Guid(userData["Id"].ToString()));
                    parameters.Add("@data", data);
                    if (processParameters != null)
                    {
                        foreach (var item in processParameters)
                        {
                            string parameterName = "@" + item.Key;
                            parameters.Add(parameterName, item.Value);
                            stringParameters.Append(", " + parameterName);
                        }
                    }
                    DbHelper.ChangeData("insert into " + dbProcessName + " values (NewId(), GETDATE(),GETDATE(), @name, @data, @user" + stringParameters.ToString() + ")", parameters);
                }
            }
        }
        public static bool IsUserPassword(string login, string password)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@login", login);
            var select = DbHelper.SelectData("select Login, Password from [User] where Login = @login", parameters);
            return select.Rows[0]["Password"].ToString() == password;
        }
        public static bool IsUser(string login)
        {
            return GetUserByLogin(login) != null ? true : false;
        }
        public static bool IsUser(string login, out DataRow userData)
        {
            userData = GetUserByLogin(login);
            return userData != null ? true : false;
        }

        public static void CreateUser(string login, string password)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@login", login);
            parameters.Add("@password", password);
            DbHelper.ChangeData("insert into [User] values (NewId(), GETDATE(), @login, @password)", parameters);
        }
        private static DataRow GetUserByLogin(string login)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@login", login);
            var select = DbHelper.SelectData("select * from [User] where Login = @login", parameters);
            return select.Rows.Count != 0 ? select.Rows[0] : null;
        }
        private static DataTable SelectData(string query, IDictionary<string, object> parameters = null)
        {
            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);
                }
            }
            sqlConnection.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            sqlConnection.Close();
            return dt;
        }
        private static int ChangeData(string query, IDictionary<string, object> parameters)
        {
            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            foreach (var parameter in parameters)
            {
                cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);
            }
            sqlConnection.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            sqlConnection.Close();
            return rowsAffected;
        }
    }
}
