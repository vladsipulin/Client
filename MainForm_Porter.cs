using ClosedXML.Excel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class MainForm_Porter : Form
    {
        Form currentForm = null;
        int thisFormWidth = 0;

        public MainForm_Porter()
        {
            InitializeComponent();
        }

        private MySqlConnection GetConnection()
        {
            var cs = ConfigurationManager.ConnectionStrings["MySqlConn"].ToString();
            var builder = new MySqlConnectionStringBuilder(cs);
            //чтоб избежать проблем с русским языком
            builder.CharacterSet = "utf8";
            return new MySqlConnection(builder.ConnectionString);
        }

        private void MainForm_Porter_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            thisFormWidth = this.Width;
            this.DoubleBuffered = true;
            label1.BackColor = Color.Gainsboro;
            label5.BackColor = Color.Gainsboro;
            label6.BackColor = Color.Gainsboro;
            keyLbl.BackColor = Color.Gainsboro;
            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Закрываем и удаляем текущую форму, если она существует
            if (currentForm != null)
            {
                currentForm.Close();
                panel1.Controls.Remove(currentForm);
                currentForm.Dispose();
                currentForm = null;
            }

            Form newForm = null;

            // Создаем новую форму в зависимости от выбора
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    var договорForm = new Договор();
                    договорForm.lbWhoLogged.Text = keyLbl.Text;
                    newForm = договорForm;
                    break;
                case 1:
                    var заявкиНаУслугиForm = new УчетПокупокУслугКлиентов();
                    заявкиНаУслугиForm.lbWhoLogged.Text = keyLbl.Text;
                    newForm = заявкиНаУслугиForm;
                    break;
                case 2:
                    var возвратForm = new ReturnPaymentForm();
                    возвратForm.lbWhoLogged.Text = keyLbl.Text;
                    newForm = возвратForm;
                    break;
                case 3:
                    GenerateExcelReport();
                    return;
                case 4:
                    GenerateTopServicesReport();
                    return;
            }

            // Настраиваем и отображаем новую форму
            if (newForm != null)
            {

                newForm.TopLevel = false;
                newForm.FormBorderStyle = FormBorderStyle.None;
                newForm.Dock = DockStyle.Fill;

                AdjustParentFormSize(newForm);

                panel1.Controls.Add(newForm);
                newForm.Show();
                currentForm = newForm; 
            }
        }

        // Метод для адаптивной настройки размера родительской формы
        private void AdjustParentFormSize(Form newForm)
        {
            // Учитываем размер панели и дополнительные элементы (например, comboBox1)
            int extraHeight = 0;

            // Учитываем высоту и отступы для comboBox1 (или других элементов управления)
            if (comboBox1 != null)
            {
                extraHeight += comboBox1.Height + comboBox1.Top + 10; 
            }

            int newHeight = newForm.Height + extraHeight;

            this.ClientSize = new Size(thisFormWidth, newHeight);
        }

        private void GenerateExcelReport()
        {
            // Путь к шаблону Excel
            string templatePath = @"C:\Users\user\Desktop\4 курс ВУЗ\Управление данными\WinForms\Client\bin\Debug\net6.0-windows\Templates\1.xlsx";
            if (!File.Exists(templatePath))
            {
                MessageBox.Show("Шаблон Excel не найден по пути: " + templatePath, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (var workbook = new XLWorkbook(templatePath))
                {
                    var worksheet = workbook.Worksheet("Отчет по отзывам");

                    // Получаем данные из БД
                    DataTable dt = GetFeedbackData();

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Нет данных в базе.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    // Средняя оценка за всё время
                    double averageRating = dt.AsEnumerable()
                        .Average(row => row.Field<int>("Оценка"));
                    worksheet.Cell("E5").Value = averageRating;
                    worksheet.Cell("E5").Style.NumberFormat.Format = "0.00";

                    // Находим самый свежий отзыв
                    DateTime latestDate = dt.AsEnumerable()
                        .Select(row => row.Field<DateTime>("Дата"))
                        .Max();

                    // Определяем начало текущего месяца для самого свежего отзыва
                    DateTime currentMonthStart = new DateTime(latestDate.Year, latestDate.Month, 1);

                    // Обновляем заголовки месяцев (C7:I7)
                    CultureInfo russianCulture = new CultureInfo("ru-RU");
                    worksheet.Cell(7, 3).Value = currentMonthStart.ToString("MMMM yyyy", russianCulture); // Текущий
                    worksheet.Cell(7, 4).Value = currentMonthStart.AddMonths(-1).ToString("MMMM yyyy", russianCulture); // Прошлый
                    worksheet.Cell(7, 5).Value = currentMonthStart.AddMonths(-2).ToString("MMMM yyyy", russianCulture); // Два месяца назад
                    worksheet.Cell(7, 6).Value = currentMonthStart.AddMonths(-3).ToString("MMMM yyyy", russianCulture); // Три месяца назад
                    worksheet.Cell(7, 7).Value = currentMonthStart.AddMonths(-4).ToString("MMMM yyyy", russianCulture); // Четыре месяца назад
                    worksheet.Cell(7, 8).Value = currentMonthStart.AddMonths(-5).ToString("MMMM yyyy", russianCulture); // Пять месяцев назад
                    worksheet.Cell(7, 9).Value = currentMonthStart.AddMonths(-6).ToString("MMMM yyyy", russianCulture); // Шесть месяцев назад

                    // Подключаемся к базе данных для выполнения SQL-запросов
                    using (MySqlConnection conn = GetConnection())
                    {
                        conn.Open();

                        // Заполняем таблицу данными (оценки 1–5 за каждый месяц)
                        for (int rating = 1; rating <= 5; rating++)
                        {
                            int row = rating + 8; // Строки 9–13 соответствуют оценкам 1–5
                            worksheet.Cell(row, 2).Value = rating; // Устанавливаем оценку в столбце B

                            // Подсчитываем количество оценок для каждого месяца с помощью SQL
                            for (int monthOffset = 0; monthOffset <= 6; monthOffset++)
                            {
                                DateTime monthStart = currentMonthStart.AddMonths(-monthOffset);
                                DateTime monthEnd = monthStart.AddMonths(1).AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59);

                                // SQL-запрос для подсчёта количества отзывов
                                string query = @"
                                                SELECT COUNT(*) 
                                                FROM ОтзывКлиентаНаТерминал 
                                                WHERE НКл = @NKl 
                                                AND Оценка = @Rating 
                                                AND Дата BETWEEN @MonthStart AND @MonthEnd";

                                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                                {
                                    cmd.Parameters.AddWithValue("@NKl", 1); // Все записи с НКл = 1
                                    cmd.Parameters.AddWithValue("@Rating", rating);
                                    cmd.Parameters.AddWithValue("@MonthStart", monthStart);
                                    cmd.Parameters.AddWithValue("@MonthEnd", monthEnd);

                                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                                    // Записываем количество в соответствующую ячейку (столбцы C–I)
                                    worksheet.Cell(row, 3 + monthOffset).Value = count;

                                    // Отладочная информация: выводим диапазоны дат
                                    System.Diagnostics.Debug.WriteLine($"Месяц: {monthStart:MMMM yyyy}, Диапазон: {monthStart:yyyy-MM-dd} - {monthEnd:yyyy-MM-dd}, Оценка: {rating}, Количество: {count}");
                                }
                            }

                            // Столбец J (Сумма Оценок) не трогаем, предполагается, что там уже есть формулы
                        }

                        conn.Close();
                    }

                    // Задаём ширину столбцов B–J (2–10) для умещения на A4
                    worksheet.Column(2).Width = 12;  // B: "Период, мес." и оценки 1–5
                    worksheet.Column(3).Width = 14;  // C: Текущий (например, "май 2025")
                    worksheet.Column(4).Width = 14;  // D: Прошлый
                    worksheet.Column(5).Width = 14;  // E: Два месяца назад
                    worksheet.Column(6).Width = 14;  // F: Три месяца назад
                    worksheet.Column(7).Width = 14;  // G: Четыре месяца назад
                    worksheet.Column(8).Width = 14;  // H: Пять месяцев назад
                    worksheet.Column(9).Width = 14;  // I: Шесть месяцев назад
                    worksheet.Column(10).Width = 12; // J: Сумма Оценок

                    // Настраиваем параметры страницы для печати на A4
                    worksheet.PageSetup.PageOrientation = XLPageOrientation.Landscape;
                    worksheet.PageSetup.PaperSize = XLPaperSize.A4Paper;
                    worksheet.PageSetup.Margins.Left = 0.7;
                    worksheet.PageSetup.Margins.Right = 0.7;
                    worksheet.PageSetup.Margins.Top = 0.7;
                    worksheet.PageSetup.Margins.Bottom = 0.7;
                    worksheet.PageSetup.FitToPages(1, 1);
                    worksheet.PageSetup.Scale = 70;

                    // Параметры файла и путь
                    string fileName = $"Отчет_Отзывы_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.xlsx";
                    string reportsDirectory = Path.Combine(Environment.CurrentDirectory, "Reports");
                    // Создаём папку, если её нет
                    Directory.CreateDirectory(reportsDirectory);
                    string filePath = Path.Combine(reportsDirectory, fileName);
                    // Сохраняем файл
                    workbook.SaveAs(filePath);

                    // Открываем файл
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = filePath,
                        UseShellExecute = true
                    });
                }
            }
            catch (System.IO.IOException ex) when (ex.Message.Contains("being used by another process"))
            {
                MessageBox.Show("Файл шаблона Excel открыт и доступ к нему невозможен. Пожалуйста, закройте файл и повторите попытку.",
                    "Ошибка доступа к файлу", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при создании отчёта: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void GenerateTopServicesReport()
        {
            // Путь к шаблону Excel
            string templatePath = @"C:\Users\user\Desktop\4 курс ВУЗ\Управление данными\WinForms\Client\bin\Debug\net6.0-windows\Templates\2.xlsx";
            if (!File.Exists(templatePath))
            {
                MessageBox.Show("Шаблон Excel не найден по пути: " + templatePath, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (var workbook = new XLWorkbook(templatePath))
                {
                    var worksheet = workbook.Worksheet("Отчет об услугах");

                    // Подключаемся к базе данных
                    using (MySqlConnection conn = GetConnection())
                    {
                        conn.Open();

                        // SQL-запрос для получения топ-5 услуг
                        string query = @"SELECT 
                                            z.НУслуги,
                                            u.Наименование AS НаименованиеУслуги,
                                            COUNT(CASE WHEN z.ПокупкаСовершена = TRUE THEN 1 END) AS КоличествоЗаказов,
                                            SUM(CASE WHEN z.ПокупкаСовершена = TRUE THEN z.Сумма ELSE 0 END) AS СуммаЗаказов,
                                            COUNT(v.НЗаявки) AS КоличествоВозвратов
                                        FROM ЗаявкаНаУслугу z
                                        LEFT JOIN Услуга u ON z.НУслуги = u.НУслуги
                                        LEFT JOIN ВозвратСредств v ON z.НЗаявки = v.НЗаявки AND z.НУслуги = v.НУслуги AND z.НКл = v.НКл
                                        GROUP BY z.НУслуги, u.Наименование
                                        ORDER BY КоличествоЗаказов DESC
                                        LIMIT 5";

                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                int row = 7; // Начинаем с строки 7 (B7:F7)

                                while (reader.Read() && row <= 11) // Заполняем до строки 11 (топ-5)
                                {
                                    // C: Наименование услуги
                                    worksheet.Cell(row, 3).Value = reader["НаименованиеУслуги"].ToString();

                                    // D: Количество заказов
                                    worksheet.Cell(row, 4).Value = Convert.ToInt32(reader["КоличествоЗаказов"]);

                                    // E: Сумма всех заказов, руб.
                                    worksheet.Cell(row, 5).Value = Convert.ToDouble(reader["СуммаЗаказов"]);
                                    worksheet.Cell(row, 5).Style.NumberFormat.Format = "#,##0.00";

                                    // F: Количество возвратов
                                    worksheet.Cell(row, 6).Value = Convert.ToInt32(reader["КоличествоВозвратов"]);

                                    row++;
                                }

                                // Если записей меньше 5, заполняем оставшиеся строки нулями
                                while (row <= 11)
                                {
                                    worksheet.Cell(row, 3).Value = "-";
                                    worksheet.Cell(row, 4).Value = 0;
                                    worksheet.Cell(row, 5).Value = 0;
                                    worksheet.Cell(row, 5).Style.NumberFormat.Format = "#,##0.00";
                                    worksheet.Cell(row, 6).Value = 0;
                                    row++;
                                }
                            }
                        }

                        conn.Close();
                    }

                    // Задаём ширину столбцов B–F (2–6) для читаемости
                    worksheet.Column(2).Width = 12;  // B: Место по популярности
                    worksheet.Column(3).Width = 20;  // C: Наименование услуги
                    worksheet.Column(4).Width = 14;  // D: Количество заказов
                    worksheet.Column(5).Width = 14;  // E: Сумма всех заказов
                    worksheet.Column(6).Width = 14;  // F: Количество возвратов

                    // Настраиваем параметры страницы для печати на A4
                    worksheet.PageSetup.PageOrientation = XLPageOrientation.Landscape;
                    worksheet.PageSetup.PaperSize = XLPaperSize.A4Paper;
                    worksheet.PageSetup.Margins.Left = 0.7;
                    worksheet.PageSetup.Margins.Right = 0.7;
                    worksheet.PageSetup.Margins.Top = 0.7;
                    worksheet.PageSetup.Margins.Bottom = 0.7;
                    worksheet.PageSetup.FitToPages(1, 1);
                    worksheet.PageSetup.Scale = 70;

                    // Сохраняем файл
                    string reportsDirectory = Path.Combine(Environment.CurrentDirectory, "Reports");
                    Directory.CreateDirectory(reportsDirectory); // Создаём папку, если её нет
                    string fileName = $"Топ_Услуг_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.xlsx";
                    string filePath = Path.Combine(reportsDirectory, fileName);
                    workbook.SaveAs(filePath);

                    // Открываем файл
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = filePath,
                        UseShellExecute = true
                    });
                }
            }
            catch (System.IO.IOException ex) when (ex.Message.Contains("being used by another process"))
            {
                MessageBox.Show("Файл шаблона Excel открыт и доступ к нему невозможен. Пожалуйста, закройте файл и повторите попытку.",
                    "Ошибка доступа к файлу", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при создании отчёта: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private DataTable GetFeedbackData()
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = "SELECT Дата, Оценка FROM ОтзывКлиентаНаТерминал";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }
}
