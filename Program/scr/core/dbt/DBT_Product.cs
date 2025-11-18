using Microsoft.Data.SqlClient;

namespace Program.scr.core.dbt
{
    public class DBT_Product
    {
        public int ID_Product;
        public string Name;
        public string Type;
        public string Manufacturer;
        public string? Model;
        public decimal Price;
        public string? Warranty;


        public static List<DBT_Product> GetAll()
        {
            var objs = new List<DBT_Product>();
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT * FROM Product";
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var obj = new DBT_Product();

                                obj.ID_Product = reader.GetInt32(0);
                                obj.Name = reader.GetString(1);
                                obj.Type = reader.GetString(2);
                                obj.Manufacturer = reader.GetString(3);
                                if (reader.IsDBNull(4)) obj.Model = string.Empty;
                                else obj.Model = reader.GetString(4);
                                obj.Price = reader.GetDecimal(5);
                                if (reader.IsDBNull(6)) obj.Warranty = string.Empty;
                                else obj.Warranty = reader.GetString(6);

                                objs.Add(obj);
                            }
                        }
                    }
                }
            }
            catch { objs = null; }
            return objs;
        }

        public static DBT_Product GetById(int id)
        {
            var obj = new DBT_Product();
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT * FROM Product WHERE ID_Product = @id";
                        query.Parameters.AddWithValue("@id", id);
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                obj.ID_Product = reader.GetInt32(0);
                                obj.Name = reader.GetString(1);
                                obj.Type = reader.GetString(2);
                                obj.Manufacturer = reader.GetString(3);
                                if (reader.IsDBNull(4)) obj.Model = string.Empty;
                                else obj.Model = reader.GetString(4);
                                obj.Price = reader.GetDecimal(5);
                                if (reader.IsDBNull(6)) obj.Warranty = string.Empty;
                                else obj.Warranty = reader.GetString(6);
                            }
                        }
                    }
                }
            }
            catch { obj = null; }
            return obj;
        }

        public static int Create(DBT_Product obj)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "INSERT INTO Product VALUES (@Name, @Type, @Manufacturer, @Model, @Price, @Warranty);";
                        query.Parameters.AddWithValue("@Name", obj.Name);
                        query.Parameters.AddWithValue("@Type", obj.Type);
                        query.Parameters.AddWithValue("@Manufacturer", obj.Manufacturer);
                        query.Parameters.AddWithValue("@Model", obj.Model);
                        query.Parameters.AddWithValue("@Price", obj.Price);
                        query.Parameters.AddWithValue("@Warranty", obj.Warranty);
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
                        query.CommandText = "SELECT MAX(ID_Product) FROM Product;";
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

        public static int Edit(DBT_Product obj)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "UPDATE Product SET Name = @Name, Type = @Type, Manufacturer = @Manufacturer, Model = @Model, Price = @Price, Warranty = @Warranty WHERE ID_Product = @id;";
                        query.Parameters.AddWithValue("@Name", obj.Name);
                        query.Parameters.AddWithValue("@Type", obj.Type);
                        query.Parameters.AddWithValue("@Manufacturer", obj.Manufacturer);
                        query.Parameters.AddWithValue("@Model", obj.Model);
                        query.Parameters.AddWithValue("@Price", obj.Price);
                        query.Parameters.AddWithValue("@Warranty", obj.Warranty);
                        query.Parameters.AddWithValue("@id", obj.ID_Product);
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
                        query.CommandText = "DELETE FROM Product WHERE ID_Product = @id;";
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
