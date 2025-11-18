using Microsoft.Data.SqlClient;

namespace Program.scr.core.dbt
{
    public class DBT_TariffPlan
    {
        public int ID_Tariff;
        public string Name;
        public string Description;
        public decimal Cost;
        public string? Features;


        public static List<DBT_TariffPlan> GetAll()
        {
            var objs = new List<DBT_TariffPlan>();
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT * FROM TariffPlan";
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var obj = new DBT_TariffPlan();

                                obj.ID_Tariff = reader.GetInt32(0);
                                obj.Name = reader.GetString(1);
                                obj.Description = reader.GetString(2);
                                obj.Cost = reader.GetDecimal(3);
                                if (reader.IsDBNull(4)) obj.Features = string.Empty;
                                else obj.Features = reader.GetString(4);

                                objs.Add(obj);
                            }
                        }
                    }
                }
            }
            catch { objs = null; }
            return objs;
        }

        public static DBT_TariffPlan GetById(int id)
        {
            var obj = new DBT_TariffPlan();
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT * FROM TariffPlan WHERE ID_Tariff = @id";
                        query.Parameters.AddWithValue("@id", id);
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                obj.ID_Tariff = reader.GetInt32(0);
                                obj.Name = reader.GetString(1);
                                obj.Description = reader.GetString(2);
                                obj.Cost = reader.GetDecimal(3);
                                if (reader.IsDBNull(4)) obj.Features = string.Empty;
                                else obj.Features = reader.GetString(4);
                            }
                        }
                    }
                }
            }
            catch { obj = null; }
            return obj;
        }

        public static int Create(DBT_TariffPlan obj)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "INSERT INTO TariffPlan VALUES (@Name, @Description, @Cost, @Features);";
                        query.Parameters.AddWithValue("@Name", obj.Name);
                        query.Parameters.AddWithValue("@Description", obj.Description);
                        query.Parameters.AddWithValue("@Cost", obj.Cost);
                        query.Parameters.AddWithValue("@Features", obj.Features);
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
                        query.CommandText = "SELECT MAX(ID_Tariff) FROM TariffPlan;";
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

        public static int Edit(DBT_TariffPlan obj)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "UPDATE TariffPlan SET Name = @Name, Description = @Description, Cost = @Cost, Features = @Features WHERE ID_Tariff = @id;";
                        query.Parameters.AddWithValue("@Name", obj.Name);
                        query.Parameters.AddWithValue("@Description", obj.Description);
                        query.Parameters.AddWithValue("@Cost", obj.Cost);
                        query.Parameters.AddWithValue("@Features", obj.Features);
                        query.Parameters.AddWithValue("@id", obj.ID_Tariff);
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
                        query.CommandText = "DELETE FROM TariffPlan WHERE ID_Tariff = @id;";
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
