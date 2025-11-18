using Microsoft.Data.SqlClient;

namespace Program.scr.core.dbt
{
    public class DBT_Sale
    {
        public int ID_Sale;
        public int? ID_Client;
        public int ID_Employee;
        public int ID_Product;
        public DateTime SaleDate;
        public decimal TotalAmount;
        public string? Comment;


        public static List<DBT_Sale> GetAll()
        {
            var objs = new List<DBT_Sale>();
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT * FROM Sale";
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var obj = new DBT_Sale();

                                obj.ID_Sale = reader.GetInt32(0);
                                if (reader.IsDBNull(1)) obj.ID_Client = null;
                                else obj.ID_Client = reader.GetInt32(1);
                                obj.ID_Employee = reader.GetInt32(2);
                                obj.ID_Product = reader.GetInt32(3);
                                obj.SaleDate = DateTime.Parse(reader.GetValue(4).ToString());
                                obj.TotalAmount = reader.GetDecimal(5);
                                if (reader.IsDBNull(6)) obj.Comment = string.Empty;
                                else obj.Comment = reader.GetString(6);

                                objs.Add(obj);
                            }
                        }
                    }
                }
            }
            catch { objs = null; }
            return objs;
        }

        public static DBT_Sale GetById(int id)
        {
            var obj = new DBT_Sale();
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT * FROM Sale WHERE ID_Sale = @id";
                        query.Parameters.AddWithValue("@id", id);
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                obj.ID_Sale = reader.GetInt32(0);
                                if (reader.IsDBNull(1)) obj.ID_Client = null;
                                else obj.ID_Client = reader.GetInt32(1);
                                obj.ID_Employee = reader.GetInt32(2);
                                obj.ID_Product = reader.GetInt32(3);
                                obj.SaleDate = DateTime.Parse(reader.GetValue(4).ToString());
                                obj.TotalAmount = reader.GetDecimal(5);
                                if (reader.IsDBNull(6)) obj.Comment = string.Empty;
                                else obj.Comment = reader.GetString(6);
                            }
                        }
                    }
                }
            }
            catch { obj = null; }
            return obj;
        }

        public static int Create(DBT_Sale obj)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "INSERT INTO Sale VALUES (@ID_Client, @ID_Employee, @ID_Product, @SaleDate, @TotalAmount, @Comment);";
                        query.Parameters.AddWithValue("@ID_Client", obj.ID_Client);
                        query.Parameters.AddWithValue("@ID_Employee", obj.ID_Employee);
                        query.Parameters.AddWithValue("@ID_Product", obj.ID_Product);
                        query.Parameters.AddWithValue("@SaleDate", obj.SaleDate);
                        query.Parameters.AddWithValue("@TotalAmount", obj.TotalAmount);
                        query.Parameters.AddWithValue("@Comment", obj.Comment);
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
                        query.CommandText = "SELECT MAX(ID_Sale) FROM Sale;";
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

        public static int Edit(DBT_Sale obj)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "UPDATE Sale SET ID_Client = @ID_Client, ID_Employee = @ID_Employee, ID_Product = @ID_Product, SaleDate = @SaleDate, TotalAmount = @TotalAmount, Comment = @Comment WHERE ID_Sale = @id;";
                        query.Parameters.AddWithValue("@ID_Client", obj.ID_Client);
                        query.Parameters.AddWithValue("@ID_Employee", obj.ID_Employee);
                        query.Parameters.AddWithValue("@ID_Product", obj.ID_Product);
                        query.Parameters.AddWithValue("@SaleDate", obj.SaleDate);
                        query.Parameters.AddWithValue("@TotalAmount", obj.TotalAmount);
                        query.Parameters.AddWithValue("@Comment", obj.Comment);
                        query.Parameters.AddWithValue("@id", obj.ID_Sale);
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
                        query.CommandText = "DELETE FROM Sale WHERE ID_Sale = @id;";
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
