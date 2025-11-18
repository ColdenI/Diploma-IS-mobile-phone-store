using Microsoft.Data.SqlClient;

namespace Program.scr.core.dbt
{
    public class DBT_Employee
    {
        public int ID_Employee;
        public string FullName;
        public string Position;
        public string Phone;
        public string Email;
        public decimal Salary;
        public DateTime HireDate;
        public int ID_Salon;


        public static List<DBT_Employee> GetAll()
        {
            var objs = new List<DBT_Employee>();
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT * FROM Employee";
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var obj = new DBT_Employee();

                                obj.ID_Employee = reader.GetInt32(0);
                                obj.FullName = reader.GetString(1);
                                obj.Position = reader.GetString(2);
                                obj.Phone = reader.GetString(3);
                                obj.Email = reader.GetString(4);
                                obj.Salary = reader.GetDecimal(5);
                                obj.HireDate = DateTime.Parse(reader.GetValue(6).ToString());
                                obj.ID_Salon = reader.GetInt32(7);

                                objs.Add(obj);
                            }
                        }
                    }
                }
            }
            catch { objs = null; }
            return objs;
        }

        public static DBT_Employee GetById(int id)
        {
            var obj = new DBT_Employee();
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT * FROM Employee WHERE ID_Employee = @id";
                        query.Parameters.AddWithValue("@id", id);
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                obj.ID_Employee = reader.GetInt32(0);
                                obj.FullName = reader.GetString(1);
                                obj.Position = reader.GetString(2);
                                obj.Phone = reader.GetString(3);
                                obj.Email = reader.GetString(4);
                                obj.Salary = reader.GetDecimal(5);
                                obj.HireDate = DateTime.Parse(reader.GetValue(6).ToString());
                                obj.ID_Salon = reader.GetInt32(7);
                            }
                        }
                    }
                }
            }
            catch { obj = null; }
            return obj;
        }

        public static int Create(DBT_Employee obj)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "INSERT INTO Employee VALUES (@FullName, @Position, @Phone, @Email, @Salary, @HireDate, @ID_Salon);";
                        query.Parameters.AddWithValue("@FullName", obj.FullName);
                        query.Parameters.AddWithValue("@Position", obj.Position);
                        query.Parameters.AddWithValue("@Phone", obj.Phone);
                        query.Parameters.AddWithValue("@Email", obj.Email);
                        query.Parameters.AddWithValue("@Salary", obj.Salary);
                        query.Parameters.AddWithValue("@HireDate", obj.HireDate);
                        query.Parameters.AddWithValue("@ID_Salon", obj.ID_Salon);
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
                        query.CommandText = "SELECT MAX(ID_Employee) FROM Employee;";
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

        public static int Edit(DBT_Employee obj)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "UPDATE Employee SET FullName = @FullName, Position = @Position, Phone = @Phone, Email = @Email, Salary = @Salary, HireDate = @HireDate, ID_Salon = @ID_Salon WHERE ID_Employee = @id;";
                        query.Parameters.AddWithValue("@FullName", obj.FullName);
                        query.Parameters.AddWithValue("@Position", obj.Position);
                        query.Parameters.AddWithValue("@Phone", obj.Phone);
                        query.Parameters.AddWithValue("@Email", obj.Email);
                        query.Parameters.AddWithValue("@Salary", obj.Salary);
                        query.Parameters.AddWithValue("@HireDate", obj.HireDate);
                        query.Parameters.AddWithValue("@ID_Salon", obj.ID_Salon);
                        query.Parameters.AddWithValue("@id", obj.ID_Employee);
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
                        query.CommandText = "DELETE FROM Employee WHERE ID_Employee = @id;";
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
