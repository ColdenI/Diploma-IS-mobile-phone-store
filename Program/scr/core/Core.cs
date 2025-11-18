using Microsoft.Data.SqlClient;
using Program.scr.core.dbt;
using System.Text;

namespace Program.scr.core
{
    public class Core
    {
        public static DBT_Auth ThisUser;
        public static DBT_Employee ThisEmployee;

        public static string[] Roles = {"Admin", "HeadManager", "Manager", "Not" };

        public class Analytics
        {
            public static async Task<string> GenerateAnalyticsHtmlAsync(int selectedSalonId)
            {
                // Пример строки подключения. ЗАМЕНИТЕ НА СВОЮ!
                string connectionString = SQL._sqlConnectStr;

                var htmlBuilder = new StringBuilder();
                htmlBuilder.AppendLine("<html><body>");

                // Текущий и предыдущий месяцы
                DateTime currentDate = DateTime.Today;
                DateTime currentMonthStart = new DateTime(currentDate.Year, currentDate.Month, 1);
                DateTime currentMonthEnd = currentMonthStart.AddMonths(1).AddDays(-1);

                DateTime previousMonthStart = currentMonthStart.AddMonths(-1);
                DateTime previousMonthEnd = currentMonthStart.AddDays(-1);

                try
                {
                    using (var connection = new SqlConnection(connectionString))
                    {
                        await connection.OpenAsync();

                        // --- Заголовок ---
                        htmlBuilder.AppendLine($"<h2 style='color: #2E86AB;'>Аналитика для салона на: {DBT_Salon.GetById(selectedSalonId).Address}</h2>");
                        htmlBuilder.AppendLine($"<p style='color: #5E6C84; font-style: italic;'>Периоды: {previousMonthStart:MMMM yyyy} vs {currentMonthStart:MMMM yyyy}</p>");

                        // --- 1. Анализ клиентов ---
                        htmlBuilder.AppendLine("<h3 style='color: #F18F01;'>Анализ клиентов</h3>");

                        // Запросы для подсчета новых клиентов
                        string newClientsQuery = @"
                SELECT COUNT(*) FROM Client c
                INNER JOIN Sale s ON c.ID_Client = s.ID_Client
                INNER JOIN Employee e ON s.ID_Employee = e.ID_Employee
                WHERE e.ID_Salon = @SalonId AND s.SaleDate >= @PeriodStart AND s.SaleDate <= @PeriodEnd
                AND NOT EXISTS (
                    SELECT 1 FROM Sale prev_s
                    INNER JOIN Employee prev_e ON prev_s.ID_Employee = prev_e.ID_Employee
                    WHERE prev_e.ID_Salon = @SalonId AND prev_s.ID_Client = c.ID_Client
                    AND prev_s.SaleDate < @PeriodStart
                )";

                        int newClientsCurrent = await GetCountAsync(connection, newClientsQuery, selectedSalonId, currentMonthStart, currentMonthEnd);
                        int newClientsPrevious = await GetCountAsync(connection, newClientsQuery, selectedSalonId, previousMonthStart, previousMonthEnd);

                        int clientDiff = newClientsCurrent - newClientsPrevious;
                        string clientDiffSign = clientDiff >= 0 ? "+" : "";
                        string clientColor = clientDiff >= 0 ? "#009B77" : "#DD4132";

                        htmlBuilder.AppendLine($"<p>Новые клиенты в текущем месяце: <strong style='color: #2E86AB;'>{newClientsCurrent}</strong></p>");
                        htmlBuilder.AppendLine($"<p>Новые клиенты в предыдущем месяце: <strong style='color: #5E6C84;'>{newClientsPrevious}</strong></p>");
                        htmlBuilder.AppendLine($"<p>Изменение: <strong style='color: {clientColor};'>{clientDiffSign}{clientDiff}</strong> " +
                                              (clientDiff > 0 ? "<span style='color: #009B77;'>(Рост)</span>" : clientDiff < 0 ? "<span style='color: #DD4132;'>(Снижение)</span>" : "<span style='color: #5E6C84;'>(Без изменений)</span>") + "</p>");

                        // --- 2. Анализ продаж ---
                        htmlBuilder.AppendLine("<h3 style='color: #F18F01;'>Анализ продаж</h3>");

                        // Запросы для подсчета продаж и суммы
                        string salesQuery = @"
                SELECT COUNT(*), ISNULL(SUM(TotalAmount), 0) FROM Sale s
                INNER JOIN Employee e ON s.ID_Employee = e.ID_Employee
                WHERE e.ID_Salon = @SalonId AND s.SaleDate >= @PeriodStart AND s.SaleDate <= @PeriodEnd";

                        var (salesCurrent, sumCurrent) = await GetSalesDataAsync(connection, salesQuery, selectedSalonId, currentMonthStart, currentMonthEnd);
                        var (salesPrevious, sumPrevious) = await GetSalesDataAsync(connection, salesQuery, selectedSalonId, previousMonthStart, previousMonthEnd);

                        int salesDiff = salesCurrent - salesPrevious;
                        string salesDiffSign = salesDiff >= 0 ? "+" : "";
                        string salesColor = salesDiff >= 0 ? "#009B77" : "#DD4132";

                        decimal sumDiff = sumCurrent - sumPrevious;
                        string sumDiffSign = sumDiff >= 0 ? "+" : "";
                        string sumColor = sumDiff >= 0 ? "#009B77" : "#DD4132";

                        htmlBuilder.AppendLine($"<p>Продаж в текущем месяце: <strong style='color: #2E86AB;'>{salesCurrent}</strong></p>");
                        htmlBuilder.AppendLine($"<p>Продаж в предыдущем месяце: <strong style='color: #5E6C84;'>{salesPrevious}</strong></p>");
                        htmlBuilder.AppendLine($"<p>Изменение количества продаж: <strong style='color: {salesColor};'>{salesDiffSign}{salesDiff}</strong> " +
                                              (salesDiff > 0 ? "<span style='color: #009B77;'>(Рост)</span>" : salesDiff < 0 ? "<span style='color: #DD4132;'>(Снижение)</span>" : "<span style='color: #5E6C84;'>(Без изменений)</span>") + "</p>");

                        htmlBuilder.AppendLine($"<p>Общая сумма продаж в текущем месяце: <strong style='color: #2E86AB;'>{sumCurrent:C}</strong></p>");
                        htmlBuilder.AppendLine($"<p>Общая сумма продаж в предыдущем месяце: <strong style='color: #5E6C84;'>{sumPrevious:C}</strong></p>");
                        htmlBuilder.AppendLine($"<p>Изменение общей суммы: <strong style='color: {sumColor};'>{sumDiffSign}{sumDiff:C}</strong> " +
                                              (sumDiff > 0 ? "<span style='color: #009B77;'>(Рост)</span>" : sumDiff < 0 ? "<span style='color: #DD4132;'>(Снижение)</span>" : "<span style='color: #5E6C84;'>(Без изменений)</span>") + "</p>");

                        // --- 3. Средний чек ---
                        htmlBuilder.AppendLine("<h3 style='color: #F18F01;'>Средний чек</h3>");

                        decimal avgCurrent = salesCurrent > 0 ? sumCurrent / salesCurrent : 0;
                        decimal avgPrevious = salesPrevious > 0 ? sumPrevious / salesPrevious : 0;

                        decimal avgDiff = avgCurrent - avgPrevious;
                        string avgDiffSign = avgDiff >= 0 ? "+" : "";
                        string avgColor = avgDiff >= 0 ? "#009B77" : "#DD4132";

                        htmlBuilder.AppendLine($"<p>Средний чек в текущем месяце: <strong style='color: #2E86AB;'>{avgCurrent:C}</strong></p>");
                        htmlBuilder.AppendLine($"<p>Средний чек в предыдущем месяце: <strong style='color: #5E6C84;'>{avgPrevious:C}</strong></p>");
                        htmlBuilder.AppendLine($"<p>Изменение среднего чека: <strong style='color: {avgColor};'>{avgDiffSign}{avgDiff:C}</strong> " +
                                              (avgDiff > 0 ? "<span style='color: #009B77;'>(Рост)</span>" : avgDiff < 0 ? "<span style='color: #DD4132;'>(Снижение)</span>" : "<span style='color: #5E6C84;'>(Без изменений)</span>") + "</p>");

                        // --- 4. Топ-3 проданных продуктов ---
                        htmlBuilder.AppendLine("<h3 style='color: #F18F01;'>Топ-3 проданных продуктов</h3>");

                        string topProductsQuery = @"
                SELECT TOP 3 p.Name, COUNT(s.ID_Sale) AS SalesCount, SUM(s.TotalAmount) AS TotalSales
                FROM Sale s
                INNER JOIN Employee e ON s.ID_Employee = e.ID_Employee
                INNER JOIN Product p ON s.ID_Product = p.ID_Product
                WHERE e.ID_Salon = @SalonId AND s.SaleDate >= @PeriodStart AND s.SaleDate <= @PeriodEnd
                GROUP BY p.ID_Product, p.Name
                ORDER BY SalesCount DESC";

                        htmlBuilder.AppendLine("<ul>");
                        using (var cmd = new SqlCommand(topProductsQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@SalonId", selectedSalonId);
                            cmd.Parameters.AddWithValue("@PeriodStart", currentMonthStart);
                            cmd.Parameters.AddWithValue("@PeriodEnd", currentMonthEnd);

                            using (var reader = await cmd.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    string productName = reader.GetString(0);
                                    int salesCount = reader.GetInt32(1);
                                    decimal totalSales = reader.GetDecimal(2);
                                    htmlBuilder.AppendLine($"<li><strong>{productName}</strong> - Продаж: {salesCount}, На сумму: {totalSales:C}</li>");
                                }
                            }
                        }
                        htmlBuilder.AppendLine("</ul>");

                        // --- 5. Топ-3 активных сотрудников ---
                        htmlBuilder.AppendLine("<h3 style='color: #F18F01;'>Топ-3 активных сотрудников (по количеству продаж)</h3>");

                        string topEmployeesQuery = @"
                SELECT TOP 3 e.FullName, COUNT(s.ID_Sale) AS SalesCount, SUM(s.TotalAmount) AS TotalSales
                FROM Sale s
                INNER JOIN Employee e ON s.ID_Employee = e.ID_Employee
                WHERE e.ID_Salon = @SalonId AND s.SaleDate >= @PeriodStart AND s.SaleDate <= @PeriodEnd
                GROUP BY e.ID_Employee, e.FullName
                ORDER BY SalesCount DESC";

                        htmlBuilder.AppendLine("<ul>");
                        using (var cmd = new SqlCommand(topEmployeesQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@SalonId", selectedSalonId);
                            cmd.Parameters.AddWithValue("@PeriodStart", currentMonthStart);
                            cmd.Parameters.AddWithValue("@PeriodEnd", currentMonthEnd);

                            using (var reader = await cmd.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    string empName = reader.GetString(0);
                                    int empSalesCount = reader.GetInt32(1);
                                    decimal empTotalSales = reader.GetDecimal(2);
                                    htmlBuilder.AppendLine($"<li><strong>{empName}</strong> - Продаж: {empSalesCount}, На сумму: {empTotalSales:C}</li>");
                                }
                            }
                        }
                        htmlBuilder.AppendLine("</ul>");

                        // --- Заключение ---
                        string conclusionColor = (sumDiff >= 0 && salesDiff >= 0) ? "#009B77" : "#DD4132";
                        string conclusionText = (sumDiff >= 0 && salesDiff >= 0) ? "Положительная динамика!" : "Требуется внимание.";
                        htmlBuilder.AppendLine($"<p style='color: {conclusionColor}; font-weight: bold; font-size: 1.1em;'>Вывод: {conclusionText}</p>");

                    } // using connection
                }
                catch (Exception ex)
                {
                    // Обработка ошибки
                    htmlBuilder.AppendLine($"<p style='color: #DD4132; font-weight: bold;'>Ошибка при выполнении анализа: {ex.Message}</p>");
                }

                htmlBuilder.AppendLine("</body></html>");
                return htmlBuilder.ToString();
            }

            // Вспомогательный метод для получения количества
            private static async Task<int> GetCountAsync(SqlConnection connection, string query, int salonId, DateTime start, DateTime end)
            {
                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@SalonId", salonId);
                    cmd.Parameters.AddWithValue("@PeriodStart", start);
                    cmd.Parameters.AddWithValue("@PeriodEnd", end);
                    var result = await cmd.ExecuteScalarAsync();
                    return result != null ? Convert.ToInt32(result) : 0;
                }
            }

            // Вспомогательный метод для получения количества продаж и суммы
            private static async Task<(int count, decimal sum)> GetSalesDataAsync(SqlConnection connection, string query, int salonId, DateTime start, DateTime end)
            {
                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@SalonId", salonId);
                    cmd.Parameters.AddWithValue("@PeriodStart", start);
                    cmd.Parameters.AddWithValue("@PeriodEnd", end);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            int count = reader.GetInt32(0);
                            decimal sum = reader.GetDecimal(1);
                            return (count, sum);
                        }
                    }
                }
                return (0, 0);
            }
        }
    }
}
