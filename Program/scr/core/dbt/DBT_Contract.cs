using Microsoft.Data.SqlClient;

namespace Program.scr.core.dbt
{
    public class DBT_Contract
    {
        public int ID_Contract;
        public int ID_Client;
        public int ID_Tariff;
        public int ID_Employee;
        public DateTime SigningDate;
        public string Status;


        public static List<DBT_Contract> GetAll()
        {
            var objs = new List<DBT_Contract>();
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT * FROM Contract";
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var obj = new DBT_Contract();

                                obj.ID_Contract = reader.GetInt32(0);
                                obj.ID_Client = reader.GetInt32(1);
                                obj.ID_Tariff = reader.GetInt32(2);
                                obj.ID_Employee = reader.GetInt32(3);
                                obj.SigningDate = DateTime.Parse(reader.GetValue(4).ToString());
                                obj.Status = reader.GetString(5);

                                objs.Add(obj);
                            }
                        }
                    }
                }
            }
            catch { objs = null; }
            return objs;
        }

        public static DBT_Contract GetById(int id)
        {
            var obj = new DBT_Contract();
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT * FROM Contract WHERE ID_Contract = @id";
                        query.Parameters.AddWithValue("@id", id);
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                obj.ID_Contract = reader.GetInt32(0);
                                obj.ID_Client = reader.GetInt32(1);
                                obj.ID_Tariff = reader.GetInt32(2);
                                obj.ID_Employee = reader.GetInt32(3);
                                obj.SigningDate = DateTime.Parse(reader.GetValue(4).ToString());
                                obj.Status = reader.GetString(5);
                            }
                        }
                    }
                }
            }
            catch { obj = null; }
            return obj;
        }

        public static int Create(DBT_Contract obj)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "INSERT INTO Contract VALUES (@ID_Client, @ID_Tariff, @ID_Employee, @SigningDate, @Status);";
                        query.Parameters.AddWithValue("@ID_Client", obj.ID_Client);
                        query.Parameters.AddWithValue("@ID_Tariff", obj.ID_Tariff);
                        query.Parameters.AddWithValue("@ID_Employee", obj.ID_Employee);
                        query.Parameters.AddWithValue("@SigningDate", obj.SigningDate);
                        query.Parameters.AddWithValue("@Status", obj.Status);
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
                        query.CommandText = "SELECT MAX(ID_Contract) FROM Contract;";
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

        public static int Edit(DBT_Contract obj)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "UPDATE Contract SET ID_Client = @ID_Client, ID_Tariff = @ID_Tariff, ID_Employee = @ID_Employee, SigningDate = @SigningDate, Status = @Status WHERE ID_Contract = @id;";
                        query.Parameters.AddWithValue("@ID_Client", obj.ID_Client);
                        query.Parameters.AddWithValue("@ID_Tariff", obj.ID_Tariff);
                        query.Parameters.AddWithValue("@ID_Employee", obj.ID_Employee);
                        query.Parameters.AddWithValue("@SigningDate", obj.SigningDate);
                        query.Parameters.AddWithValue("@Status", obj.Status);
                        query.Parameters.AddWithValue("@id", obj.ID_Contract);
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
                        query.CommandText = "DELETE FROM Contract WHERE ID_Contract = @id;";
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
