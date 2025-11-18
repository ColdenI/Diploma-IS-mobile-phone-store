using Microsoft.Data.SqlClient;
using System.Data;

namespace Program.scr.core.dbt
{
    public class DBT_Client
    {
        public int ID_Client;
        public string FullName;
        public string Phone;
        public string Email;
        public string Address;
        public DateTime RegistrationDate;

        public static DataTable GetAllTable()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT * FROM Client";
                        using (var adapter = new SqlDataAdapter(query))
                        {
                            // Заполняем DataTable данными из результата запроса
                            adapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex) // Рекомендуется ловить конкретное исключение и, возможно, логировать
            {
                // В случае ошибки возвращается пустая таблица
                // Можно добавить логирование ошибки ex.Message
                dataTable = new DataTable();
            }
            return dataTable;
        }

        public static List<DBT_Client> GetAll()
        {
            var objs = new List<DBT_Client>();
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT * FROM Client";
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var obj = new DBT_Client();

                                obj.ID_Client = reader.GetInt32(0);
                                obj.FullName = reader.GetString(1);
                                obj.Phone = reader.GetString(2);
                                obj.Email = reader.GetString(3);
                                obj.Address = reader.GetString(4);
                                obj.RegistrationDate = DateTime.Parse(reader.GetValue(5).ToString());

                                objs.Add(obj);
                            }
                        }
                    }
                }
            }
            catch { objs = null; }
            return objs;
        }

        public static DBT_Client GetById(int id)
        {
            var obj = new DBT_Client();
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT * FROM Client WHERE ID_Client = @id";
                        query.Parameters.AddWithValue("@id", id);
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                obj.ID_Client = reader.GetInt32(0);
                                obj.FullName = reader.GetString(1);
                                obj.Phone = reader.GetString(2);
                                obj.Email = reader.GetString(3);
                                obj.Address = reader.GetString(4);
                                obj.RegistrationDate = DateTime.Parse(reader.GetValue(5).ToString());
                            }
                        }
                    }
                }
            }
            catch { obj = null; }
            return obj;
        }

        public static int Create(DBT_Client obj)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "INSERT INTO Client VALUES (@FullName, @Phone, @Email, @Address, @RegistrationDate);";
                        query.Parameters.AddWithValue("@FullName", obj.FullName);
                        query.Parameters.AddWithValue("@Phone", obj.Phone);
                        query.Parameters.AddWithValue("@Email", obj.Email);
                        query.Parameters.AddWithValue("@Address", obj.Address);
                        query.Parameters.AddWithValue("@RegistrationDate", obj.RegistrationDate);
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
                        query.CommandText = "SELECT MAX(ID_Client) FROM Client;";
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

        public static int Edit(DBT_Client obj)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "UPDATE Client SET FullName = @FullName, Phone = @Phone, Email = @Email, Address = @Address, RegistrationDate = @RegistrationDate WHERE ID_Client = @id;";
                        query.Parameters.AddWithValue("@FullName", obj.FullName);
                        query.Parameters.AddWithValue("@Phone", obj.Phone);
                        query.Parameters.AddWithValue("@Email", obj.Email);
                        query.Parameters.AddWithValue("@Address", obj.Address);
                        query.Parameters.AddWithValue("@RegistrationDate", obj.RegistrationDate);
                        query.Parameters.AddWithValue("@id", obj.ID_Client);
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
                        query.CommandText = "DELETE FROM Client WHERE ID_Client = @id;";
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
