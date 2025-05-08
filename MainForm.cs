using Client.Utils;
using ClosedXML.Excel;
using Microsoft.VisualBasic.Logging;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using Org.BouncyCastle.Tls.Crypto;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Client
{
    public partial class MainForm : Form
    {
        DataTable table = new DataTable();
        MySqlDataAdapter adapter = new MySqlDataAdapter();
        MySqlDataAdapter da;
        DataTable dt;
        int numOfColumn;
        int[] oldNumOfRow;
        bool resultExists = false;

        public MainForm()
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
        class ComboBoxDataForFill
        {
            public string sql { get; set; }
            public string DisplayMember { get; set; }
            public string ValueMember { get; set; }
            public DataTable dataSource { get; set; }
            public List<MySqlParameter> paramsForSQLQuery { get; set; }

            public ComboBoxDataForFill(string Sql, string displayMember, string valueMember)
            {
                sql = Sql;
                DisplayMember = displayMember;
                ValueMember = valueMember;
                dataSource = new DataTable();
                paramsForSQLQuery = new List<MySqlParameter>();
            }
        }

        private void LoadCombo(ComboBoxDataForFill obj)
        {
            using (var con = GetConnection())
            {
                try
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = obj.sql;
                    //cmd.Parameters.Add(new MySqlParameter("@НОрг", MySqlDbType.Int32)
                    //{ Value = objOfTable.НОрг });
                    foreach (MySqlParameter e in obj.paramsForSQLQuery)
                    {
                        cmd.Parameters.Add(e);
                    }
                    da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    dt = new DataTable();
                    da.Fill(dt);

                    obj.dataSource = dt;
                    //comboBox2.DataSource = dt;
                    //comboBox2.DisplayMember = DisplayMember;
                    //comboBox2.ValueMember = ValueMember;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private string GetUserFriendlyErrorMessage(MySqlException ex)
        {
            var message = String.Empty;
            switch (ex.Number)
            {
                case 0:
                    if (ex.InnerException.Message.Contains("Unknown"))
                    {
                        message = "Неверное название схемы или таблицы.";
                    }
                    else if (ex.InnerException.Message.Contains("Access"))
                    {
                        message = "Неверное имя или пароль доступа.";
                    }
                    else
                    {
                        message = ex.Message;
                    }
                    break;
                case 1042:
                    message = "Сервер по указанному адресу не доступен." +
                        "\nОшибка ожидания.";
                    break;
                case 1045:
                    message = "Неверное имя пользователя или пароль, " +
                        "\nпожалуйста, попробуйте еще раз.";
                    break;
                default:
                    message = ex.Message;
                    break;
            }
            return message;
        }

        private void SelectFrom(string tableName, bool userIsClient = false)
        {
            string message = string.Empty;
            try
            {
                using (var con = GetConnection())
                {
                    using (var cmd = con.CreateCommand())
                    {
                        string query = String.Empty;
                        if (!userIsClient)
                        {
                            // Формируем запрос динамически
                            query = $"SELECT * FROM {tableName};";
                        }
                        else
                        {
                            if (tableName.Equals("отзывклиентаназаселение")
                                || tableName.Equals("учетпокупокуслуг")
                                || tableName.Equals("отзывклиентанауслугу"))
                            {
                                query = $"SELECT * FROM {tableName} WHERE НКл = {keyLbl.Text};";
                            }
                            else
                            {
                                query = $"SELECT * FROM {tableName};";
                            }

                        }

                        // Выполняем запрос
                        cmd.CommandText = query;
                        // No need to open and dispose the connection here
                        table = new DataTable();
                        adapter = new MySqlDataAdapter(cmd);
                        MySqlCommandBuilder commandBuilder = new MySqlCommandBuilder(adapter);
                        adapter.InsertCommand = commandBuilder.GetInsertCommand();
                        adapter.UpdateCommand = commandBuilder.GetUpdateCommand();
                        adapter.DeleteCommand = commandBuilder.GetDeleteCommand();
                        adapter.Fill(table);

                        // Привязка данных к DataGridView
                        dataGridView1.DataSource = table;
                        dataGridView1.AutoResizeColumnHeadersHeight();
                        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    }
                }
            }
            catch (MySqlException ex)
            {
                message = GetUserFriendlyErrorMessage(ex);
                MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            Клиенты clients = new Клиенты();
            clients.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Организация org = new Организация();
            org.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Должность position = new Должность();
            position.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ClientQueriesForAppartments queryForApps = new ClientQueriesForAppartments();
            queryForApps.lbWhoLogged.Text = keyLbl.Text;
            queryForApps.ShowDialog();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
            if (Equals(lbWhoLogged.Text, "Клиент:"))
            {
                updateButton.Visible = false;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.AllowUserToDeleteRows = false;
                dataGridView1.ReadOnly = true;
                //начало 'бизнес-формы'
                comboBox1.Items.Clear();
                comboBox1.Items.Add("Заявка на службу быта");
                comboBox1.Items.Add("Профиль клиента");
                comboBox1.Items.Add("Статус заявки на заселение");
                comboBox1.Items.Add("Забронировать номер в гостиинце");
                comboBox1.Items.Add("Отзыв на заселение");
                comboBox1.Items.Add("Отзыв на услугу");
                //конец 'бизнес-формы'
                button1.Visible = false;
                button2.Visible = false;
                button3.Visible = false;
                button4.Visible = false;
                button5.Visible = false;
                button6.Visible = false;
                button7.Visible = false;
                button8.Visible = false;
                clientsButton.Visible = false;
                показатьГостиничныеКомплексы.Visible = false;
                показатьЗаселениеКлиента.Visible = false;
                показатьЗаявкиКлиентов.Visible = false;
                //button3.Location = new Point(38, 109);
                string sql = "SELECT TABLE_NAME AS 'id', TABLE_COMMENT AS 'Таблица' FROM INFORMATION_SCHEMA.TABLES " +
                    "WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_SCHEMA='hotel' " +
                    "AND TABLE_NAME IN('субъект','гостиничныйкомплекс', 'отзывклиентаназаселение', 'учетпокупокуслуг', 'отзывклиентанауслугу', 'корпус', 'этажиикорпусы','комната', 'службабыта') " +
                    "ORDER BY TABLE_COMMENT ASC;" +
                    "";
                ComboBoxDataForFill ИменаТаблиц = new ComboBoxDataForFill(sql, "Таблица", "id");
                LoadCombo(ИменаТаблиц);
                кбТаблицыБД.DataSource = ИменаТаблиц.dataSource;
                кбТаблицыБД.DisplayMember = ИменаТаблиц.DisplayMember;
                кбТаблицыБД.ValueMember = ИменаТаблиц.ValueMember;

                string tableName = (string)кбТаблицыБД.SelectedValue;
                SelectFrom(tableName, true);

                string таблица = (string)кбТаблицыБД.SelectedValue;
                sql = "SELECT COLUMN_NAME AS 'Столбец' FROM INFORMATION_SCHEMA.COlUMNS WHERE TABLE_SCHEMA='hotel' AND TABLE_NAME=@Таблица";
                ComboBoxDataForFill Столбец = new ComboBoxDataForFill(sql, "Столбец", "Столбец");
                Столбец.paramsForSQLQuery.Add(new MySqlParameter("@Таблица", MySqlDbType.VarChar, 255) { Value = таблица });
                LoadCombo(Столбец);
                кбСтолбцыТаблицы.DataSource = Столбец.dataSource;
                кбСтолбцыТаблицы.DisplayMember = Столбец.DisplayMember;
                кбСтолбцыТаблицы.ValueMember = Столбец.ValueMember;
                /*
                if (tableName.Equals("заселениеклиента"))
                {
                    string query = $"SELECT f.НГ AS 'Номер гостиницы', f.НК AS 'Номер корпуса', f.НЭ AS 'Этаж', f.НКомнаты AS 'Комната', s.НКл AS 'Номер клиента', f.СтатусЗаявки" +
                        $"FROM `заселениеклиента` AS f INNER JOIN `заявканазаселениеклиента` AS s ON f.НГ = s.НГ AND f.НК=s.НК AND f.НЭ=s.НЭ AND f.НКомнаты=s.НКомнаты " +
                        $"WHERE s.НКл = {keyLbl.Text};";
                    SelectFrom(tableName, true, query);
                }
                else if (tableName.Equals("учетпокупокуслуг"))
                {
                    string query = $"SELECT f.НЗаявки AS 'Номер заявки', f.НСл AS 'Номер службы быта', f.СрокОплаты AS 'Срок оплаты', f.СуммаВЗаявке AS 'Сумма в заявке', s.НКл AS 'Номер клиента', f.ДатаОплаты, f.РазмерШтрафа, f.СуммаКОплате" +
                        $"FROM `учетпокупокуслуг` AS f INNER JOIN `заявканауслугу` AS s " +
                        $"ON f.НЗаявки = s.НЗаявки AND f.НСл=s.НСл AND f.СрокОплаты=s.СрокОплаты AND f.СуммаВЗаявке=s.Сумма" +
                        $"WHERE s.НКл = {keyLbl.Text};";
                    SelectFrom(tableName, true, query);
                }
                else
                {
                    SelectFrom(tableName, true);

                    string таблица = (string)кбТаблицыБД.SelectedValue;
                    sql = "SELECT COLUMN_NAME AS 'Столбец' FROM INFORMATION_SCHEMA.COlUMNS WHERE TABLE_SCHEMA='hotel' AND TABLE_NAME=@Таблица";
                    ComboBoxDataForFill Столбец = new ComboBoxDataForFill(sql, "Столбец", "Столбец");
                    Столбец.paramsForSQLQuery.Add(new MySqlParameter("@Таблица", MySqlDbType.VarChar, 255) { Value = таблица });
                    LoadCombo(Столбец);
                    кбСтолбцыТаблицы.DataSource = Столбец.dataSource;
                    кбСтолбцыТаблицы.DisplayMember = Столбец.DisplayMember;
                    кбСтолбцыТаблицы.ValueMember = Столбец.ValueMember;
                }
                */
            }
            else
            {
                button1.Visible = false;
                button2.Visible = false;
                clientsButton.Visible = false;
                button3.Visible = false;
                button4.Visible = false;
                button5.Visible = false;
                button6.Visible = false;
                button7.Visible = false;
                button8.Visible = false;

                показатьГостиничныеКомплексы.Visible = false;
                показатьЗаселениеКлиента.Visible = false;
                показатьЗаявкиКлиентов.Visible = false;

                panel1.BackColor = Color.Moccasin;
                label1.BackColor = Color.Moccasin;
                label2.BackColor = Color.Moccasin;
                label5.BackColor = Color.Moccasin;
                label6.BackColor = Color.Moccasin;
                keyLbl.BackColor = Color.Moccasin;
                label1.ForeColor = Color.Black;
                label2.ForeColor = Color.Black;
                label4.ForeColor = Color.Black;
                label5.ForeColor = Color.Black;
                label6.ForeColor = Color.Black;
                keyLbl.ForeColor = Color.Black;

                string sql = "SELECT TABLE_NAME AS 'id', TABLE_COMMENT AS 'Таблица' FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_SCHEMA='Obuhovka'";
                ComboBoxDataForFill ИменаТаблиц = new ComboBoxDataForFill(sql, "Таблица", "id");
                LoadCombo(ИменаТаблиц);
                кбТаблицыБД.DataSource = ИменаТаблиц.dataSource;
                кбТаблицыБД.DisplayMember = ИменаТаблиц.DisplayMember;
                кбТаблицыБД.ValueMember = ИменаТаблиц.ValueMember;

                string tableName = (string)кбТаблицыБД.SelectedValue;
                SelectFrom(tableName);

                string таблица = (string)кбТаблицыБД.SelectedValue;
                sql = "SELECT COLUMN_NAME AS 'Столбец' FROM INFORMATION_SCHEMA.COlUMNS WHERE TABLE_SCHEMA='Obuhovka' AND TABLE_NAME=@Таблица";
                ComboBoxDataForFill Столбец = new ComboBoxDataForFill(sql, "Столбец", "Столбец");
                Столбец.paramsForSQLQuery.Add(new MySqlParameter("@Таблица", MySqlDbType.VarChar, 255) { Value = таблица });
                LoadCombo(Столбец);
                кбСтолбцыТаблицы.DataSource = Столбец.dataSource;
                кбСтолбцыТаблицы.DisplayMember = Столбец.DisplayMember;
                кбСтолбцыТаблицы.ValueMember = Столбец.ValueMember;
            }
        }

        private void показатьГостиничныеКомплексы_Click(object sender, EventArgs e)
        {
            string tableName = "ГостиничныйКомплекс";
            SelectFrom(tableName);
        }

        private void показатьЗаявкиКлиентов_Click(object sender, EventArgs e)
        {
            string tableName = "ЗаявкаНаЗаселениеКлиента";
            SelectFrom(tableName);
        }

        private void показатьЗаселениеКлиента_Click(object sender, EventArgs e)
        {
            string tableName = "ЗаселениеКлиента";
            SelectFrom(tableName);
        }


        private void updateButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Точно ли вы хотите продолжить?",
                                      "Подтверждение действия",
                                      MessageBoxButtons.YesNo,
                                      MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string message = string.Empty;
                using (var con = GetConnection())
                {
                    try
                    {
                        // Assign the new connection to each command
                        adapter.SelectCommand.Connection = con;
                        adapter.InsertCommand.Connection = con;
                        adapter.UpdateCommand.Connection = con;
                        adapter.DeleteCommand.Connection = con;

                        con.Open(); // Open the connection
                        adapter.Update(table); // Perform the update
                    }
                    catch (MySqlException ex)
                    {
                        message = GetUserFriendlyErrorMessage(ex);
                        MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        // The 'using' statement ensures the connection is closed and disposed
                    }
                }
            }
            else
            {
                MessageBox.Show("Операция отменена.");
            }
        }

        private void кбТаблицыБД_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string tableName = (string)кбТаблицыБД.SelectedValue;
            if (Equals(lbWhoLogged.Text, "Клиент:"))
                SelectFrom(tableName, true);
            else
                SelectFrom(tableName);

            string sql = "SELECT COLUMN_NAME AS 'Столбец' FROM INFORMATION_SCHEMA.COlUMNS WHERE TABLE_SCHEMA='Obuhovka' AND TABLE_NAME=@Таблица";
            ComboBoxDataForFill Столбец = new ComboBoxDataForFill(sql, "Столбец", "Столбец");
            Столбец.paramsForSQLQuery.Add(new MySqlParameter("@Таблица", MySqlDbType.VarChar, 255) { Value = tableName });
            LoadCombo(Столбец);
            кбСтолбцыТаблицы.DataSource = Столбец.dataSource;
            кбСтолбцыТаблицы.DisplayMember = Столбец.DisplayMember;
            кбСтолбцыТаблицы.ValueMember = Столбец.ValueMember;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string? searchValue = tbSearch.Text;
            if (searchValue is "")
            {
                MessageBox.Show("Введите значение в поле Поиск", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int j = 0;
                if (oldNumOfRow is not null & resultExists)
                {
                    for (int i = 0; i < oldNumOfRow.Length; i++)
                        dataGridView1.Rows[oldNumOfRow[i]].Selected = false;
                }
                else
                    oldNumOfRow = new int[dataGridView1.Rows.Count];

                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                numOfColumn = кбСтолбцыТаблицы.SelectedIndex;

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[numOfColumn].Value is not null)
                    {
                        if (row.Cells[numOfColumn].Value.ToString().IndexOf(searchValue, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            row.Selected = true;
                            oldNumOfRow[j++] = row.Index;
                            resultExists = true;
                        }
                    }
                }
                if (!resultExists)
                    MessageBox.Show($"Введенное значение не найдено – столбец {кбСтолбцыТаблицы.Text}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            UserProfile userProfile = new UserProfile();
            userProfile.keyLbl.Text = keyLbl.Text;
            userProfile.ShowDialog();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            ReqStatusApps rsa = new ReqStatusApps();
            rsa.keyLbl.Text = keyLbl.Text;
            rsa.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //ReqOnServiceForm rsf = new ReqOnServiceForm();
            //rsf.lbWhoLogged.Text = keyLbl.Text;
            //rsf.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ReviewService rsf = new ReviewService();
            rsf.lbWhoLogged.Text = keyLbl.Text;
            rsf.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ReviewRoom rsf = new ReviewRoom();
            rsf.lbWhoLogged.Text = keyLbl.Text;
            rsf.ShowDialog();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Equals(lbWhoLogged.Text, "Клиент:"))
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    RegistrationForm rgf = new RegistrationForm();
                    rgf.ShowDialog();
                }
                else if (comboBox1.SelectedIndex == 1)
                {
                    Договор obj = new Договор();
                    obj.lbWhoLogged.Text = keyLbl.Text;
                    obj.ShowDialog();
                }
                else if (comboBox1.SelectedIndex == 2)
                {
                    УчетПокупокУслугКлиентов obj = new УчетПокупокУслугКлиентов();
                    obj.lbWhoLogged.Text = keyLbl.Text;
                    obj.ShowDialog();
                }
                else if (comboBox1.SelectedIndex == 3)
                {
                    ReturnPaymentForm obj = new ReturnPaymentForm();
                    obj.lbWhoLogged.Text = keyLbl.Text;
                    obj.ShowDialog();
                }
                else if (comboBox1.SelectedIndex == 4)
                {
                    GenerateExcelReport();
                }
            }
            else
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    RegistrationForm rgf = new RegistrationForm();
                    rgf.ShowDialog();
                }
                else if (comboBox1.SelectedIndex == 1)
                {
                    UserProfile userProfile = new UserProfile();
                    userProfile.keyLbl.Text = keyLbl.Text;
                    userProfile.ShowDialog();
                }
                else if (comboBox1.SelectedIndex == 2)
                {
                    ReqStatusApps rsa = new ReqStatusApps();
                    rsa.keyLbl.Text = keyLbl.Text;
                    rsa.ShowDialog();
                }
                else if (comboBox1.SelectedIndex == 3)
                {
                    ClientQueriesForAppartments queryForApps = new ClientQueriesForAppartments();
                    queryForApps.lbWhoLogged.Text = keyLbl.Text;
                    queryForApps.ShowDialog();
                }
                else if (comboBox1.SelectedIndex == 4)
                {
                    ReviewRoom rsf = new ReviewRoom();
                    rsf.lbWhoLogged.Text = keyLbl.Text;
                    rsf.ShowDialog();
                }
                else if (comboBox1.SelectedIndex == 5)
                {
                    ReviewService rsf = new ReviewService();
                    rsf.lbWhoLogged.Text = keyLbl.Text;
                    rsf.ShowDialog();
                }
            }

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
                MessageBox.Show("Файл шаблона Excel открыт. Пожалуйста, закройте файл и повторите попытку.",
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
