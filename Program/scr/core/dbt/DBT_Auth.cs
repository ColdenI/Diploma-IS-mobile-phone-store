using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program.scr.core.dbt
{
    public class DBT_Auth
    {
        public int ID_Auth;
        public int ID_Employee;
        public string Login;
        public string Password;
        public string AccessLevel;


        public static List<DBT_Auth> GetAll()
        {
            var objs = new List<DBT_Auth>();
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT * FROM Auth";
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var obj = new DBT_Auth();

                                obj.ID_Auth = reader.GetInt32(0);
                                obj.ID_Employee = reader.GetInt32(1);
                                obj.Login = reader.GetString(2);
                                obj.Password = reader.GetString(3);
                                obj.AccessLevel = reader.GetString(4);

                                objs.Add(obj);
                            }
                        }
                    }
                }
            }
            catch { objs = null; }
            return objs;
        }

        public static DBT_Auth GetById(int id)
        {
            var obj = new DBT_Auth();
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT * FROM Auth WHERE ID_Auth = @id";
                        query.Parameters.AddWithValue("@id", id);
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                obj.ID_Auth = reader.GetInt32(0);
                                obj.ID_Employee = reader.GetInt32(1);
                                obj.Login = reader.GetString(2);
                                obj.Password = reader.GetString(3);
                                obj.AccessLevel = reader.GetString(4);
                            }
                        }
                    }
                }
            }
            catch { obj = null; }
            return obj;
        }

        public static DBT_Auth GetByEmployeeId(int id)
        {
            var obj = new DBT_Auth();
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT * FROM Auth WHERE ID_Employee = @id";
                        query.Parameters.AddWithValue("@id", id);
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                obj.ID_Auth = reader.GetInt32(0);
                                obj.ID_Employee = reader.GetInt32(1);
                                obj.Login = reader.GetString(2);
                                obj.Password = reader.GetString(3);
                                obj.AccessLevel = reader.GetString(4);
                            }
                        }
                    }
                }
            }
            catch { obj = null; }
            return obj;
        }

        public static int Create(DBT_Auth obj)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "INSERT INTO Auth VALUES (@ID_Employee, @Login, @Password, @AccessLevel);";
                        query.Parameters.AddWithValue("@ID_Employee", obj.ID_Employee);
                        query.Parameters.AddWithValue("@Login", obj.Login);
                        query.Parameters.AddWithValue("@Password", obj.Password);
                        query.Parameters.AddWithValue("@AccessLevel", obj.AccessLevel);
                        query.ExecuteNonQuery();
                    }
                }
            }
            catch { return -1; }
            int _id = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT MAX(ID_Auth) FROM Auth;";
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _id = reader.GetInt32(0);
                            }
                        }
                    }
                }
            }
            catch { return -1; }
            return _id;
        }

        public static int Edit(DBT_Auth obj)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "UPDATE Auth SET ID_Employee = @ID_Employee, Login = @Login, Password = @Password, AccessLevel = @AccessLevel WHERE ID_Auth = @id;";
                        query.Parameters.AddWithValue("@ID_Employee", obj.ID_Employee);
                        query.Parameters.AddWithValue("@Login", obj.Login);
                        query.Parameters.AddWithValue("@Password", obj.Password);
                        query.Parameters.AddWithValue("@AccessLevel", obj.AccessLevel);
                        query.Parameters.AddWithValue("@id", obj.ID_Auth);
                        query.ExecuteNonQuery();
                    }
                }
            }
            catch { return -1; }
            return 0;
        }

        public static int Remove(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "DELETE FROM Auth WHERE ID_Auth = @id;";
                        query.Parameters.AddWithValue("@id", id);
                        query.ExecuteNonQuery();
                    }
                }
            }
            catch { return -1; }
            return 0;
        }

    }
}
