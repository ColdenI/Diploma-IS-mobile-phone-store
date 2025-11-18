using Microsoft.Data.SqlClient;

namespace Program.scr.core.dbt
{
    public class DBT_Salon
    {
        public int ID_Salon;
        public string Address;
        public string Phone;
        public string Manager;
        public string Region;


        public static List<DBT_Salon> GetAll()
        {
            var objs = new List<DBT_Salon>();
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT * FROM Salon";
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var obj = new DBT_Salon();

                                obj.ID_Salon = reader.GetInt32(0);
                                obj.Address = reader.GetString(1);
                                obj.Phone = reader.GetString(2);
                                obj.Manager = reader.GetString(3);
                                obj.Region = reader.GetString(4);

                                objs.Add(obj);
                            }
                        }
                    }
                }
            }
            catch { objs = null; }
            return objs;
        }

        public static DBT_Salon GetById(int id)
        {
            var obj = new DBT_Salon();
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT * FROM Salon WHERE ID_Salon = @id";
                        query.Parameters.AddWithValue("@id", id);
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                obj.ID_Salon = reader.GetInt32(0);
                                obj.Address = reader.GetString(1);
                                obj.Phone = reader.GetString(2);
                                obj.Manager = reader.GetString(3);
                                obj.Region = reader.GetString(4);
                            }
                        }
                    }
                }
            }
            catch { obj = null; }
            return obj;
        }

        public static int Create(DBT_Salon obj)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "INSERT INTO Salon VALUES (@Address, @Phone, @Manager, @Region);";
                        query.Parameters.AddWithValue("@Address", obj.Address);
                        query.Parameters.AddWithValue("@Phone", obj.Phone);
                        query.Parameters.AddWithValue("@Manager", obj.Manager);
                        query.Parameters.AddWithValue("@Region", obj.Region);
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
                        query.CommandText = "SELECT MAX(ID_Salon) FROM Salon;";
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

        public static int Edit(DBT_Salon obj)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "UPDATE Salon SET Address = @Address, Phone = @Phone, Manager = @Manager, Region = @Region WHERE ID_Salon = @id;";
                        query.Parameters.AddWithValue("@Address", obj.Address);
                        query.Parameters.AddWithValue("@Phone", obj.Phone);
                        query.Parameters.AddWithValue("@Manager", obj.Manager);
                        query.Parameters.AddWithValue("@Region", obj.Region);
                        query.Parameters.AddWithValue("@id", obj.ID_Salon);
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
                        query.CommandText = "DELETE FROM Salon WHERE ID_Salon = @id;";
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
