using Client.Interfaces;
using Client.Models;
using Client.Services;
using Client.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Договор : Form
    {
        MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConn"].ConnectionString);
        MySqlCommand cmd;
        MySqlDataAdapter da;
        DataTable dt;
        private IDogovor _repo;
        string sql;
        int номерДоговора = 0;
        bool loadingData = false;

        public Договор()
        {
            InitializeComponent();
        }

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

        private void Договор_Load(object sender, EventArgs e)
        {
            _repo = new RDogovor();

            this.BackColor = System.Drawing.Color.White;
            checkBox1.Checked = true;
            кбНС.Enabled = false;
        }

        private async void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            sql = $"SELECT НС, ФИО FROM Портье ";
            ComboBoxDataForFill Сотрудник = new ComboBoxDataForFill(sql, "ФИО", "НС");
            LoadCombo(Сотрудник);
            кбНС.DataSource = Сотрудник.dataSource;
            кбНС.DisplayMember = Сотрудник.DisplayMember;
            кбНС.ValueMember = Сотрудник.ValueMember;

            if (checkBox1.Checked)
            {
                кбНДоговора.Text = "";
                button1.Enabled = true;
                кбНС.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;

                кбНС.SelectedValue = Convert.ToInt32(lbWhoLogged.Text);

                sql = $"SELECT * FROM Организация";
                ComboBoxDataForFill Организация = new ComboBoxDataForFill(sql, "Наименование", "НОрг");
                LoadCombo(Организация);
                кбНОрг.DataSource = Организация.dataSource;
                кбНОрг.DisplayMember = Организация.DisplayMember;
                кбНОрг.ValueMember = Организация.ValueMember;

                // Инициализация ComboBox
                sql = "SELECT * FROM ТипДоговора";
                ComboBoxDataForFill Договор = new ComboBoxDataForFill(sql, "Наименование", "НТипаДоговора");
                LoadCombo(Договор);

                кбНДоговора.DataSource = Договор.dataSource;
                кбНДоговора.DisplayMember = Договор.DisplayMember;
                кбНДоговора.ValueMember = Договор.ValueMember;
            }
            else
            {
                button1.Enabled = false;
                кбНС.Enabled = false;
                button2.Enabled = true;
                button3.Enabled = true;

                // Инициализация ComboBox для организаций (кбНОрг)
                string sqlOrg = "SELECT DISTINCT Организация.НОрг, Организация.Наименование " +
                                "FROM Организация " +
                                "INNER JOIN ДоговорСОрганизацией ON Организация.НОрг = ДоговорСОрганизацией.НОрг";
                ComboBoxDataForFill Организация = new ComboBoxDataForFill(sqlOrg, "Наименование", "НОрг");
                LoadCombo(Организация);

                кбНОрг.DataSource = Организация.dataSource;
                кбНОрг.DisplayMember = Организация.DisplayMember;
                кбНОрг.ValueMember = Организация.ValueMember;

                // comboBox с именем TEMP, который хранит данные таблицы ДоговорСОрганизацией
                // так как иначе с кбНОрг и кбНДоговора не получилось сделать
                sql = "SELECT * FROM ДоговорСОрганизацией";
                ComboBoxDataForFill ДСО = new ComboBoxDataForFill(sql, "НТипаДоговора", "НТипаДоговора");
                LoadCombo(ДСО);

                CMBX_TEMP.DataSource = ДСО.dataSource;
                CMBX_TEMP.DisplayMember = ДСО.DisplayMember;
                CMBX_TEMP.ValueMember = ДСО.ValueMember;

                var selectedOrgValue = кбНОрг.SelectedValue;

                loadingData = true;
                if (selectedOrgValue != null)
                {
                    var номерSource = CMBX_TEMP.DataSource as DataTable;

                    if (номерSource != null)
                    {
                        // Находим строку с выбранным `НТипаДоговора`
                        var selectedRow = номерSource.Rows
                            .Cast<DataRow>()
                            .FirstOrDefault(row => row["НТипаДоговора"].Equals(кбНДоговора.SelectedValue));

                        if (selectedRow != null)
                        {
                            // Устанавливаем значение `НС` в кбНС
                            var номерСотрудника = selectedRow["НС"];
                            if (номерСотрудника != DBNull.Value)
                            {
                                кбНС.SelectedValue = номерСотрудника;
                            }
                            else
                            {
                                кбНС.SelectedValue = null;
                            }

                            // Устанавливаем значение для dateTimePicker
                            var датаНачала = selectedRow["ДатаНачала"];
                            if (датаНачала != DBNull.Value)
                            {
                                тбДатаНачала.Value = Convert.ToDateTime(датаНачала);
                            }
                            else
                            {
                                тбДатаНачала.Value = DateTime.Today;
                            }

                            var датаОкончания = selectedRow["ДатаОкончания"];
                            if (датаОкончания != DBNull.Value)
                            {
                                тбДатаОкончания.Value = Convert.ToDateTime(датаОкончания);
                            }
                            else
                            {
                                тбДатаОкончания.Value = DateTime.Today;
                            }
                        }
                    }

                }
                loadingData = false;
                // Сохранение выбранного значения
                if (кбНДоговора.SelectedValue != null)
                {
                    номерДоговора = (int)кбНДоговора.SelectedValue;
                }
                else
                {
                    номерДоговора = 0; // Или другое значение по умолчанию
                }
            }
            try
            {
                // Получение выбранного значения
                int selectedId = Convert.ToInt32(кбНОрг.SelectedValue);

                // Запрос данных из базы данных
                var data = await _repo.GetOrgDetails(selectedId);

                // Привязка данных к DataGridView
                dataGridView1.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void кбНДоговора_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                int selectedId = (int)кбНОрг.SelectedValue;
                var data = await _repo.GetOrgDetails(selectedId);
                dataGridView1.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void кбНОрг_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
            {
                loadingData = true;
                var selectedValue = кбНОрг.SelectedValue;

                var номерSource = CMBX_TEMP.DataSource as DataTable;

                if (номерSource != null & selectedValue != null)
                {
                    var selectedRow = номерSource.Rows
                        .Cast<DataRow>()
                        .FirstOrDefault(row => row["НОрг"].Equals(selectedValue));

                    if (selectedRow != null)
                    {
                        var relatedНОрг = selectedRow["НТипаДоговора"];
                        if (relatedНОрг != DBNull.Value)
                        {
                            кбНДоговора.SelectedValue = relatedНОрг;
                        }

                        var номерСотрудника = selectedRow["НС"];
                        if (номерСотрудника != DBNull.Value)
                        {
                            кбНС.SelectedValue = номерСотрудника;
                        }

                        var датаНачала = selectedRow["ДатаНачала"];
                        if (датаНачала != DBNull.Value)
                        {
                            тбДатаНачала.Value = Convert.ToDateTime(датаНачала);
                        }

                        var датаОкончания = selectedRow["ДатаОкончания"];
                        if (датаОкончания != DBNull.Value)
                        {
                            тбДатаОкончания.Value = Convert.ToDateTime(датаОкончания);
                        }
                    }
                }
                loadingData = false;
            }
            try
            {
                int selectedId = Convert.ToInt32(кбНОрг.SelectedValue);
                var data = await _repo.GetOrgDetails(selectedId);
                dataGridView1.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                номерДоговора = (int)кбНДоговора.SelectedValue;

                Dogovor current = new Dogovor(номерДоговора, (int)кбНОрг.SelectedValue, 0, (int)кбНС.SelectedValue,
                                                 Convert.ToDateTime(тбДатаНачала.Text), Convert.ToDateTime(тбДатаОкончания.Text));

                Result<int> result;
                result = await _repo.Add(current);

                if (result)
                {
                    MessageBox.Show($"Договор №{номерДоговора} с организацией '{кбНОрг.Text}' успешно создан!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    sql = "SELECT * FROM ДоговорСОрганизацией";
                    ComboBoxDataForFill ДСО = new ComboBoxDataForFill(sql, "НТипаДоговора", "НТипаДоговора");
                    LoadCombo(ДСО);
                    CMBX_TEMP.DataSource = ДСО.dataSource;
                }
                if (!result)
                {

                    MessageBox.Show($"Договор №{номерДоговора} с организацией '{кбНОрг.Text}' не был создан" + result.Error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("Вы не заполнили все поля формы!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                номерДоговора = (int)кбНДоговора.SelectedValue;
                Dogovor current = new Dogovor(номерДоговора, (int)кбНОрг.SelectedValue, 0, (int)кбНС.SelectedValue,
                                                 Convert.ToDateTime(тбДатаНачала.Text), Convert.ToDateTime(тбДатаОкончания.Text));

                Result<int> result;
                result = await _repo.Update(current, номерДоговора, (int)кбНОрг.SelectedValue);

                if (result)
                {
                    MessageBox.Show($"Сведения договора №{номерДоговора} с организацией '{кбНОрг.Text}' успешно изменены!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    sql = "SELECT * FROM ДоговорСОрганизацией";
                    ComboBoxDataForFill ДСО = new ComboBoxDataForFill(sql, "НТипаДоговора", "НТипаДоговора");
                    LoadCombo(ДСО);
                    CMBX_TEMP.DataSource = ДСО.dataSource;
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

        private async void button3_Click(object sender, EventArgs e)
        {
            try
            {
                номерДоговора = (int)кбНДоговора.SelectedValue;
                Dogovor current = new Dogovor(номерДоговора, (int)кбНОрг.SelectedValue, 0, (int)кбНС.SelectedValue,
                                                Convert.ToDateTime(тбДатаНачала.Text), Convert.ToDateTime(тбДатаОкончания.Text));

                Result<int> result;
                result = await _repo.Remove(номерДоговора, (int)кбНОрг.SelectedValue);

                if (result)
                {
                    MessageBox.Show($"Договор №{номерДоговора} с организацией '{кбНОрг.Text}' успешно расторгнут!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    sql = "SELECT * FROM ДоговорСОрганизацией";
                    ComboBoxDataForFill ДСО = new ComboBoxDataForFill(sql, "НТипаДоговора", "НТипаДоговора");
                    LoadCombo(ДСО);
                    CMBX_TEMP.DataSource = ДСО.dataSource;
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

        private void тбДатаНачала_ValueChanged(object sender, EventArgs e)
        {
            if (!loadingData)
            {
                DateTime date1 = тбДатаНачала.Value.Date;
                DateTime date2 = тбДатаОкончания.Value.Date;
                if (date2 < date1)
                {
                    MessageBox.Show("Ошибка: дата начала позже даты окончания", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                return;
        }

        private void тбДатаОкончания_ValueChanged(object sender, EventArgs e)
        {
            if (!loadingData)
            {
                DateTime date1 = тбДатаНачала.Value.Date;
                DateTime date2 = тбДатаОкончания.Value.Date;
                if (date2 < date1)
                {
                    MessageBox.Show("Ошибка: дата окончания раньше даты начала", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                return;
        }
    }
}
