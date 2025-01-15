using Client.Interfaces;
using Client.Services;
using Client.Utils;
using Client.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Configuration;
using MySqlX.XDevAPI.Common;

namespace Client
{
    public partial class ClientQueriesForAppartments : Form
    {
        MySqlConnection con = new MySqlConnection("server=localhost;user id=root;database=hotel");
        MySqlCommand cmd;
        MySqlDataAdapter da;
        DataTable dt;
        private IReqClientApparts _repo;
        string sql;
        int clientId;
        string username;

        private void LoadCombo(ComboBoxDataForFill obj)
        {
            try
            {
                con.Open();
                cmd = new MySqlCommand();
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

        private MySqlConnection GetConnection()
        {
            var cs = ConfigurationManager.ConnectionStrings["MySqlConn"].ToString();
            var builder = new MySqlConnectionStringBuilder(cs);
            //чтоб избежать проблем с русским языком
            builder.CharacterSet = "utf8";
            return new MySqlConnection(builder.ConnectionString);
        }

        public ClientQueriesForAppartments()
        {
            InitializeComponent();
        }

        private void ClientQueriesForAppartments_Load(object sender, EventArgs e)
        {
            _repo = new RRequestClientApparts();
            this.BackColor = System.Drawing.Color.White;

            sql = "SELECT Название, НГ FROM `ГостиничныйКомплекс`";
            ComboBoxDataForFill Гостиница = new ComboBoxDataForFill(sql, "Название", "НГ");
            LoadCombo(Гостиница);
            кбНГ.DataSource = Гостиница.dataSource;
            кбНГ.DisplayMember = Гостиница.DisplayMember;
            кбНГ.ValueMember = Гостиница.ValueMember;

            int НГ = (int)кбНГ.SelectedValue;
            // так как при загрузке формы всегда загружается элемент из самой первой строки таблицы
            // то мы напрямую к нему обращаемся
            //DataRow row = dt.Rows[0];
            //var cells = row.ItemArray;
            //object? cell = cells[1];
            //if (cell != null )
            //{
            //НГ = (int)cell;
            sql = "SELECT * FROM `Корпус` WHERE НГ=@НГ";
            ComboBoxDataForFill Корпус = new ComboBoxDataForFill(sql, "НК", "НК");
            Корпус.paramsForSQLQuery.Add(new MySqlParameter("@НГ", MySqlDbType.Int32) { Value = НГ });
            LoadCombo(Корпус);
            кбНК.DataSource = Корпус.dataSource;
            кбНК.DisplayMember = Корпус.DisplayMember;
            кбНК.ValueMember = Корпус.ValueMember;

            //int НК = Convert.ToInt32(кбНК.Text);
            int НК = (int)кбНК.SelectedValue;
            sql = "SELECT * FROM `ЭтажиИКорпусы` WHERE НК=@НК";
            ComboBoxDataForFill Этаж = new ComboBoxDataForFill(sql, "НЭ", "НЭ");
            Этаж.paramsForSQLQuery.Add(new MySqlParameter("@НК", MySqlDbType.Int32) { Value = НК });
            LoadCombo(Этаж);
            кбНЭ.DataSource = Этаж.dataSource;
            кбНЭ.DisplayMember = Этаж.DisplayMember;
            кбНЭ.ValueMember = Этаж.ValueMember;

            sql = "SELECT DISTINCT Вместимость FROM `Комната`";
            ComboBoxDataForFill Вместимость = new ComboBoxDataForFill(sql, "Вместимость", "Вместимость");
            LoadCombo(Вместимость);
            кбВместимостьКомнаты.DataSource = Вместимость.dataSource;
            кбВместимостьКомнаты.DisplayMember = Вместимость.DisplayMember;
            кбВместимостьКомнаты.ValueMember = Вместимость.ValueMember;

            int НЭ = (int)кбНЭ.SelectedValue;
            int ВместимостьКомнаты = (int)кбВместимостьКомнаты.SelectedValue;
            sql = "SELECT * FROM `Комната` WHERE НК=@НК AND НЭ=@НЭ AND Вместимость=@Вместимость AND Доступность='Да'";
            ComboBoxDataForFill Комната = new ComboBoxDataForFill(sql, "НКомнаты", "НЭ");
            Комната.paramsForSQLQuery.Add(new MySqlParameter("@НК", MySqlDbType.Int32) { Value = НК });
            Комната.paramsForSQLQuery.Add(new MySqlParameter("@НЭ", MySqlDbType.Int32) { Value = НЭ });
            Комната.paramsForSQLQuery.Add(new MySqlParameter("@Вместимость", MySqlDbType.Int32) { Value = ВместимостьКомнаты });
            LoadCombo(Комната);
            кбНКомнаты.DataSource = Комната.dataSource;
            кбНКомнаты.DisplayMember = Комната.DisplayMember;
            кбНКомнаты.ValueMember = Комната.ValueMember;

            if (кбНКомнаты.DataSource is DataTable dataTable)
            {
                float pricePerNight;
                foreach (DataRow row in dataTable.Rows)
                {
                    int? drНКоманты = Convert.ToInt32(row["НКомнаты"]);
                    int? drНК = Convert.ToInt32(row["НК"]);
                    int? drНЭ = Convert.ToInt32(row["НЭ"]);
                    if (drНКоманты == Convert.ToInt32(кбНКомнаты.Text)
                        & drНК == Convert.ToInt32(кбНК.Text)
                        & drНЭ == Convert.ToInt32(кбНЭ.Text))
                    {
                        if (row["Цена1Ночь"] != DBNull.Value)
                        {
                            pricePerNight = (float)row["Цена1Ночь"];
                            tbRoomPrice.Text = pricePerNight.ToString();
                            break;
                        }
                        else
                            pricePerNight = 0;
                    }

                }
            }
            else
            {
                MessageBox.Show("Ошибка вывода цены комнаты за одну ночь: DataSource не является DataTable.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //}

        private void кбНГ_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //перезаполняем остальные объекты comboBox исходя из нового SelectedValue в объекте кбНГ
            int НГ = (int)кбНГ.SelectedValue;

            sql = "SELECT * FROM `Корпус` WHERE НГ=@НГ";
            ComboBoxDataForFill Корпус = new ComboBoxDataForFill(sql, "НК", "НК");
            Корпус.paramsForSQLQuery.Add(new MySqlParameter("@НГ", MySqlDbType.Int32) { Value = НГ });
            LoadCombo(Корпус);
            кбНК.DataSource = Корпус.dataSource;
            кбНК.DisplayMember = Корпус.DisplayMember;
            кбНК.ValueMember = Корпус.ValueMember;

            int? НК = (int?)(кбНК.SelectedValue ?? 0);
            sql = "SELECT * FROM `ЭтажиИКорпусы` WHERE НК=@НК";
            ComboBoxDataForFill Этаж = new ComboBoxDataForFill(sql, "НЭ", "НЭ");
            Этаж.paramsForSQLQuery.Add(new MySqlParameter("@НК", MySqlDbType.Int32) { Value = НК });
            LoadCombo(Этаж);
            кбНЭ.DataSource = Этаж.dataSource;
            кбНЭ.DisplayMember = Этаж.DisplayMember;
            кбНЭ.ValueMember = Этаж.ValueMember;

            sql = "SELECT DISTINCT Вместимость FROM `Комната`";
            ComboBoxDataForFill Вместимость = new ComboBoxDataForFill(sql, "Вместимость", "Вместимость");
            LoadCombo(Вместимость);
            кбВместимостьКомнаты.DataSource = Вместимость.dataSource;
            кбВместимостьКомнаты.DisplayMember = Вместимость.DisplayMember;
            кбВместимостьКомнаты.ValueMember = Вместимость.ValueMember;

            int? НЭ = (int?)(кбНЭ.SelectedValue ?? 0);
            int? ВместимостьКомнаты = (int?)(кбВместимостьКомнаты.SelectedValue ?? 0);
            sql = "SELECT * FROM `Комната` WHERE НК=@НК AND НЭ=@НЭ AND Вместимость=@Вместимость AND Доступность='Да'";
            ComboBoxDataForFill Комната = new ComboBoxDataForFill(sql, "НКомнаты", "НЭ");
            Комната.paramsForSQLQuery.Add(new MySqlParameter("@НК", MySqlDbType.Int32) { Value = НК });
            Комната.paramsForSQLQuery.Add(new MySqlParameter("@НЭ", MySqlDbType.Int32) { Value = НЭ });
            Комната.paramsForSQLQuery.Add(new MySqlParameter("@Вместимость", MySqlDbType.Int32) { Value = ВместимостьКомнаты });
            LoadCombo(Комната);
            кбНКомнаты.DataSource = Комната.dataSource;
            кбНКомнаты.DisplayMember = Комната.DisplayMember;
            кбНКомнаты.ValueMember = Комната.ValueMember;


            if (кбНКомнаты.DataSource is DataTable dataTable)
            {
                float pricePerNight;
                foreach (DataRow row in dataTable.Rows)
                {
                    int? drНКоманты = Convert.ToInt32(row["НКомнаты"]);
                    int? drНК = Convert.ToInt32(row["НК"]);
                    int? drНЭ = Convert.ToInt32(row["НЭ"]);
                    if (drНКоманты == Convert.ToInt32(кбНКомнаты.Text)
                        & drНК == Convert.ToInt32(кбНК.Text)
                        & drНЭ == Convert.ToInt32(кбНЭ.Text))
                    {
                        if (row["Цена1Ночь"] != DBNull.Value)
                        {
                            pricePerNight = (float)row["Цена1Ночь"];
                            tbRoomPrice.Text = pricePerNight.ToString();
                            break;
                        }
                        else
                            pricePerNight = 0;
                    }

                }
            }
            else
            {
                MessageBox.Show("Ошибка вывода цены комнаты за одну ночь: DataSource не является DataTable.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void SetClientIdByUsername(string login)
        {
            string message = String.Empty;
            try
            {
                using (var con = GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT НКл FROM Клиент WHERE Логин = @Логин";

                    cmd.Parameters.Add(new MySqlParameter("@Логин", MySqlDbType.VarChar, 255)
                    { Value = login });

                    con.Open();
                    var result = cmd.ExecuteScalar();
                    if (result != null) // Проверяем, нашлось ли значение
                    {
                        clientId = Convert.ToInt32(result); // Преобразуем результат в int
                        //MessageBox.Show($"Найден клиент с НКл: {clientId}", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Клиент с указанным логином не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                int clientId = Convert.ToInt16(lbWhoLogged.Text);
                //SetClientIdByUsername(username);

                RequestClientAppartmnts current = new RequestClientAppartmnts(Convert.ToInt32(кбНКомнаты.Text), Convert.ToInt32(кбНК.Text), Convert.ToInt32(кбНЭ.Text),
                                                                              Convert.ToInt32(кбНГ.SelectedValue), clientId,
                                           Convert.ToDateTime(dateTimePicker1.Text), Convert.ToDateTime(dateTimePicker2.Text),
                                           Convert.ToDateTime(dateTimePicker3.Text), float.Parse(tbCost.Text));

                Result<int> result;
                result = await _repo.Add(current);

                if (result)
                {
                    MessageBox.Show("Заявка успешно создана!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (!result)
                {
                    MessageBox.Show(result.Error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("Вы не заполнили все поля формы!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void кбНК_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int? НК = (int?)(кбНК.SelectedValue ?? 0);
            sql = "SELECT * FROM `ЭтажиИКорпусы` WHERE НК=@НК";
            ComboBoxDataForFill Этаж = new ComboBoxDataForFill(sql, "НЭ", "НЭ");
            Этаж.paramsForSQLQuery.Add(new MySqlParameter("@НК", MySqlDbType.Int32) { Value = НК });
            LoadCombo(Этаж);
            кбНЭ.DataSource = Этаж.dataSource;
            кбНЭ.DisplayMember = Этаж.DisplayMember;
            кбНЭ.ValueMember = Этаж.ValueMember;

            sql = "SELECT DISTINCT Вместимость FROM `Комната`";
            ComboBoxDataForFill Вместимость = new ComboBoxDataForFill(sql, "Вместимость", "Вместимость");
            LoadCombo(Вместимость);
            кбВместимостьКомнаты.DataSource = Вместимость.dataSource;
            кбВместимостьКомнаты.DisplayMember = Вместимость.DisplayMember;
            кбВместимостьКомнаты.ValueMember = Вместимость.ValueMember;

            int? НЭ = (int?)(кбНЭ.SelectedValue ?? 0);
            int? ВместимостьКомнаты = (int?)(кбВместимостьКомнаты.SelectedValue ?? 0);
            sql = "SELECT * FROM `Комната` WHERE НК=@НК AND НЭ=@НЭ AND Вместимость=@Вместимость AND Доступность='Да'";
            ComboBoxDataForFill Комната = new ComboBoxDataForFill(sql, "НКомнаты", "НЭ");
            Комната.paramsForSQLQuery.Add(new MySqlParameter("@НК", MySqlDbType.Int32) { Value = НК });
            Комната.paramsForSQLQuery.Add(new MySqlParameter("@НЭ", MySqlDbType.Int32) { Value = НЭ });
            Комната.paramsForSQLQuery.Add(new MySqlParameter("@Вместимость", MySqlDbType.Int32) { Value = ВместимостьКомнаты });
            LoadCombo(Комната);
            кбНКомнаты.DataSource = Комната.dataSource;
            кбНКомнаты.DisplayMember = Комната.DisplayMember;
            кбНКомнаты.ValueMember = Комната.ValueMember;

            if (кбНКомнаты.DataSource is DataTable dataTable)
            {
                float pricePerNight;
                foreach (DataRow row in dataTable.Rows)
                {
                    int? drНКоманты = Convert.ToInt32(row["НКомнаты"]);
                    int? drНК = Convert.ToInt32(row["НК"]);
                    int? drНЭ = Convert.ToInt32(row["НЭ"]);
                    if (drНКоманты == Convert.ToInt32(кбНКомнаты.Text)
                        & drНК == Convert.ToInt32(кбНК.Text)
                        & drНЭ == Convert.ToInt32(кбНЭ.Text))
                    {
                        if (row["Цена1Ночь"] != DBNull.Value)
                        {
                            pricePerNight = (float)row["Цена1Ночь"];
                            tbRoomPrice.Text = pricePerNight.ToString();
                            break;
                        }
                        else
                            pricePerNight = 0;
                    }

                }
            }
            else
            {
                MessageBox.Show("Ошибка вывода цены комнаты за одну ночь: DataSource не является DataTable.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void кбНЭ_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int? НК = (int?)(кбНК.SelectedValue ?? 0);
            int? НЭ = (int?)(кбНЭ.SelectedValue ?? 0);

            sql = "SELECT DISTINCT Вместимость FROM `Комната`";
            ComboBoxDataForFill Вместимость = new ComboBoxDataForFill(sql, "Вместимость", "Вместимость");
            LoadCombo(Вместимость);
            кбВместимостьКомнаты.DataSource = Вместимость.dataSource;
            кбВместимостьКомнаты.DisplayMember = Вместимость.DisplayMember;
            кбВместимостьКомнаты.ValueMember = Вместимость.ValueMember;

            int? ВместимостьКомнаты = (int?)(кбВместимостьКомнаты.SelectedValue ?? 0);
            sql = "SELECT * FROM `Комната` WHERE НК=@НК AND НЭ=@НЭ AND Вместимость=@Вместимость AND Доступность='Да'";
            ComboBoxDataForFill Комната = new ComboBoxDataForFill(sql, "НКомнаты", "НЭ");
            Комната.paramsForSQLQuery.Add(new MySqlParameter("@НК", MySqlDbType.Int32) { Value = НК });
            Комната.paramsForSQLQuery.Add(new MySqlParameter("@НЭ", MySqlDbType.Int32) { Value = НЭ });
            Комната.paramsForSQLQuery.Add(new MySqlParameter("@Вместимость", MySqlDbType.Int32) { Value = ВместимостьКомнаты });
            LoadCombo(Комната);
            кбНКомнаты.DataSource = Комната.dataSource;
            кбНКомнаты.DisplayMember = Комната.DisplayMember;
            кбНКомнаты.ValueMember = Комната.ValueMember;

            if (кбНКомнаты.DataSource is DataTable dataTable)
            {
                float pricePerNight;
                foreach (DataRow row in dataTable.Rows)
                {
                    int? drНКоманты = Convert.ToInt32(row["НКомнаты"]);
                    int? drНК = Convert.ToInt32(row["НК"]);
                    int? drНЭ = Convert.ToInt32(row["НЭ"]);
                    if (drНКоманты == Convert.ToInt32(кбНКомнаты.Text)
                        & drНК == Convert.ToInt32(кбНК.Text)
                        & drНЭ == Convert.ToInt32(кбНЭ.Text))
                    {
                        if (row["Цена1Ночь"] != DBNull.Value)
                        {
                            pricePerNight = (float)row["Цена1Ночь"];
                            tbRoomPrice.Text = pricePerNight.ToString();
                            break;
                        }
                        else
                            pricePerNight = 0;
                    }

                }
            }
            else
            {
                MessageBox.Show("Ошибка вывода цены комнаты за одну ночь: DataSource не является DataTable.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void кбНКомнаты_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (кбНКомнаты.DataSource is DataTable dataTable)
            {
                float pricePerNight;
                foreach (DataRow row in dataTable.Rows)
                {
                    int? drНКоманты = Convert.ToInt32(row["НКомнаты"]);
                    int? drНК = Convert.ToInt32(row["НК"]);
                    int? drНЭ = Convert.ToInt32(row["НЭ"]);
                    if (drНКоманты == Convert.ToInt32(кбНКомнаты.Text)
                        & drНК == Convert.ToInt32(кбНК.Text)
                        & drНЭ == Convert.ToInt32(кбНЭ.Text))
                    {
                        if (row["Цена1Ночь"] != DBNull.Value)
                        {
                            pricePerNight = (float)row["Цена1Ночь"];
                            tbRoomPrice.Text = pricePerNight.ToString();
                            break;
                        }
                        else
                            pricePerNight = 0;
                    }

                }
            }
            else
            {
                MessageBox.Show("Ошибка вывода цены комнаты за одну ночь: DataSource не является DataTable.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void кбВместимостьКомнаты_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int? НК = (int?)(кбНК.SelectedValue ?? 0);
            int? НЭ = (int?)(кбНЭ.SelectedValue ?? 0);
            int? ВместимостьКомнаты = (int?)(кбВместимостьКомнаты.SelectedValue ?? 0);
            sql = "SELECT * FROM `Комната` WHERE НК=@НК AND НЭ=@НЭ AND Вместимость=@Вместимость AND Доступность='Да'";
            ComboBoxDataForFill Комната = new ComboBoxDataForFill(sql, "НКомнаты", "НЭ");
            Комната.paramsForSQLQuery.Add(new MySqlParameter("@НК", MySqlDbType.Int32) { Value = НК });
            Комната.paramsForSQLQuery.Add(new MySqlParameter("@НЭ", MySqlDbType.Int32) { Value = НЭ });
            Комната.paramsForSQLQuery.Add(new MySqlParameter("@Вместимость", MySqlDbType.Int32) { Value = ВместимостьКомнаты });
            LoadCombo(Комната);
            кбНКомнаты.DataSource = Комната.dataSource;
            кбНКомнаты.DisplayMember = Комната.DisplayMember;
            кбНКомнаты.ValueMember = Комната.ValueMember;

            if (кбНКомнаты.DataSource is DataTable dataTable)
            {
                float pricePerNight;
                foreach (DataRow row in dataTable.Rows)
                {
                    int? drНКоманты = Convert.ToInt32(row["НКомнаты"]);
                    int? drНК = Convert.ToInt32(row["НК"]);
                    int? drНЭ = Convert.ToInt32(row["НЭ"]);
                    if (drНКоманты == Convert.ToInt32(кбНКомнаты.Text)
                        & drНК == Convert.ToInt32(кбНК.Text)
                        & drНЭ == Convert.ToInt32(кбНЭ.Text))
                    {
                        if (row["Цена1Ночь"] != DBNull.Value)
                        {
                            pricePerNight = (float)row["Цена1Ночь"];
                            tbRoomPrice.Text = pricePerNight.ToString();
                            break;
                        }
                        else
                            pricePerNight = 0;
                    }

                }
            }
            else
            {
                MessageBox.Show("Ошибка вывода цены комнаты за одну ночь: DataSource не является DataTable", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            DateTime date1 = dateTimePicker2.Value.Date;
            DateTime date2 = dateTimePicker3.Value.Date;
            int nights = (date2 - date1).Days;
            if (nights < 0)
            {
                MessageBox.Show("Ошибка: дата заселения позже даты выезда", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                float roomPrice = float.Parse(tbRoomPrice.Text);
                float cost = nights * roomPrice;
                tbCost.Text = Convert.ToString(cost);
            }
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            DateTime date1 = dateTimePicker2.Value.Date;
            DateTime date2 = dateTimePicker3.Value.Date;
            int nights = (date2 - date1).Days;
            if (nights < 0)
            {
                MessageBox.Show("Ошибка: дата выезда раньше даты заселения", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                float roomPrice = float.Parse(tbRoomPrice.Text);
                float cost = nights * roomPrice;
                tbCost.Text = Convert.ToString(cost);
            }
        }
    }
}
