using Client.Interfaces;
using Client.Models;
using Client.Services;
using Client.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class ЗаселениеКлиента : Form
    {
        MySqlConnection con = new MySqlConnection("server=localhost;user id=root;database=hotel");
        MySqlCommand cmd;
        MySqlDataAdapter da;
        DataTable dt;
        private IZaselenieClienta _repo;
        string sql;
        int номерЗаявки = 0;

        public ЗаселениеКлиента()
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

        private void ЗаселениеКлиента_Load(object sender, EventArgs e)
        {
            _repo = new RZaselenieClienta();
            radioButton1.Checked = true;

            this.BackColor = System.Drawing.Color.White;
        }

        private async void кбНЗаявки_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var selectedValue = кбНЗаявки.SelectedValue;
            кбСтатусЗаявки.Text = "";

            sql = $"SELECT НКл, ФИО FROM Клиент ";
            ComboBoxDataForFill Клиент = new ComboBoxDataForFill(sql, "ФИО", "НКл");
            LoadCombo(Клиент);
            кбНКл.DataSource = Клиент.dataSource;
            кбНКл.DisplayMember = Клиент.DisplayMember;
            кбНКл.ValueMember = Клиент.ValueMember;

            sql = $"SELECT НС, ФИО FROM Сотрудник ";
            ComboBoxDataForFill Сотрудник = new ComboBoxDataForFill(sql, "ФИО", "НС");
            LoadCombo(Сотрудник);
            кбНС.DataSource = Сотрудник.dataSource;
            кбНС.DisplayMember = Сотрудник.DisplayMember;
            кбНС.ValueMember = Сотрудник.ValueMember;

            if (selectedValue != null & radioButton1.Checked)
            {
                var номерSource = кбНЗаявки.DataSource as DataTable;

                if (номерSource != null)
                {
                    // Находим строку с выбранным `НГр`
                    var selectedRow = номерSource.Rows
                        .Cast<DataRow>()
                        .FirstOrDefault(row => row["НЗаявки"].Equals(selectedValue));

                    if (selectedRow != null)
                    {
                        // Устанавливаем соответствующее значение `НОрг` в кбНОрг
                        var relatedНКл = selectedRow["НКл"];
                        if (relatedНКл != DBNull.Value)
                        {
                            кбНКл.SelectedValue = relatedНКл;
                        }
                    }
                }
            }
            else if (!radioButton1.Checked)
            {
                if (selectedValue != null)
                {
                    var номерSource = кбНЗаявки.DataSource as DataTable;

                    if (номерSource != null)
                    {
                        // Находим строку с выбранным `НГр`
                        var selectedRow = номерSource.Rows
                            .Cast<DataRow>()
                            .FirstOrDefault(row => row["НЗаявки"].Equals(selectedValue));

                        if (selectedRow != null)
                        {
                            // Устанавливаем соответствующее значение `НОрг` в кбНОрг
                            var relatedНКл = selectedRow["НКл"];
                            if (relatedНКл != DBNull.Value)
                            {
                                кбНКл.SelectedValue = relatedНКл;
                            }

                            var номерСотрудника = selectedRow["НС"];
                            if (номерСотрудника != DBNull.Value)
                            {
                                кбНС.SelectedValue = номерСотрудника;
                            }

                            var решениеПоЗаявке = selectedRow["СтатусЗаявки"];
                            if (решениеПоЗаявке != DBNull.Value)
                            {
                                кбСтатусЗаявки.SelectedItem = решениеПоЗаявке.ToString();
                            }
                        }
                    }
                }
            }

            try
            {
                int selectedId = (int)кбНЗаявки.SelectedValue;
                var data = await _repo.GetZayavkaDetails(selectedId, (int)кбНКл.SelectedValue);
                dataGridView1.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                button1.Enabled = true;
                button2.Enabled = false;
                кбСтатусЗаявки.Text = "";
                // Инициализация ComboBox
                sql = "SELECT * FROM ЗаявкаНаЗаселениеКлиента WHERE Статус = 'Ожидание'";
                ComboBoxDataForFill ЗаявкаКлиентаНаЗаселение = new ComboBoxDataForFill(sql, "НЗаявки", "НЗаявки");
                LoadCombo(ЗаявкаКлиентаНаЗаселение);
                кбНЗаявки.DataSource = ЗаявкаКлиентаНаЗаселение.dataSource;
                кбНЗаявки.DisplayMember = ЗаявкаКлиентаНаЗаселение.DisplayMember;
                кбНЗаявки.ValueMember = ЗаявкаКлиентаНаЗаселение.ValueMember;

                // Сохранение выбранного значения
                номерЗаявки = (int)кбНЗаявки.SelectedValue;

                var selectedValue = кбНЗаявки.SelectedValue;

                if (selectedValue != null)
                {
                    sql = $"SELECT НКл, ФИО FROM Клиент ";
                    ComboBoxDataForFill Клиент = new ComboBoxDataForFill(sql, "ФИО", "НКл");
                    LoadCombo(Клиент);
                    кбНКл.DataSource = Клиент.dataSource;
                    кбНКл.DisplayMember = Клиент.DisplayMember;
                    кбНКл.ValueMember = Клиент.ValueMember;
                    кбНКл.Enabled = false;

                    sql = $"SELECT НС, ФИО FROM Сотрудник WHERE НС = {lbWhoLogged.Text}";
                    ComboBoxDataForFill Сотрудник = new ComboBoxDataForFill(sql, "ФИО", "НС");
                    LoadCombo(Сотрудник);
                    кбНС.DataSource = Сотрудник.dataSource;
                    кбНС.DisplayMember = Сотрудник.DisplayMember;
                    кбНС.ValueMember = Сотрудник.ValueMember;
                    кбНС.Enabled = false;

                    var номерSource = кбНЗаявки.DataSource as DataTable;

                    if (номерSource != null)
                    {
                        // Находим строку с выбранным `НГр`
                        var selectedRow = номерSource.Rows
                            .Cast<DataRow>()
                            .FirstOrDefault(row => row["НЗаявки"].Equals(номерЗаявки));

                        if (selectedRow != null)
                        {
                            // Устанавливаем соответствующее значение `НОрг` в кбНОрг
                            var relatedНКл = selectedRow["НКл"];
                            if (relatedНКл != DBNull.Value)
                            {
                                кбНКл.SelectedValue = relatedНКл;
                            }
                        }
                    }
                }
            }
            else
            {
                button1.Enabled = false;
                button2.Enabled = true;

                sql = "SELECT * FROM ЗаселениеКлиента";
                ComboBoxDataForFill ЗаявкаКлиентаНаЗаселение = new ComboBoxDataForFill(sql, "НЗаявки", "НЗаявки");
                LoadCombo(ЗаявкаКлиентаНаЗаселение);
                кбНЗаявки.DataSource = ЗаявкаКлиентаНаЗаселение.dataSource;
                кбНЗаявки.DisplayMember = ЗаявкаКлиентаНаЗаселение.DisplayMember;
                кбНЗаявки.ValueMember = ЗаявкаКлиентаНаЗаселение.ValueMember;

                var selectedValue = кбНЗаявки.SelectedValue;

                if (selectedValue != null)
                {
                    sql = $"SELECT НКл, ФИО FROM Клиент ";
                    ComboBoxDataForFill Клиент = new ComboBoxDataForFill(sql, "ФИО", "НКл");
                    LoadCombo(Клиент);
                    кбНКл.DataSource = Клиент.dataSource;
                    кбНКл.DisplayMember = Клиент.DisplayMember;
                    кбНКл.ValueMember = Клиент.ValueMember;
                    кбНКл.Enabled = false;

                    sql = $"SELECT НС, ФИО FROM Сотрудник ";
                    ComboBoxDataForFill Сотрудник = new ComboBoxDataForFill(sql, "ФИО", "НС");
                    LoadCombo(Сотрудник);
                    кбНС.DataSource = Сотрудник.dataSource;
                    кбНС.DisplayMember = Сотрудник.DisplayMember;
                    кбНС.ValueMember = Сотрудник.ValueMember;
                    кбНС.Enabled = true;

                    var номерSource = кбНЗаявки.DataSource as DataTable;

                    if (номерSource != null)
                    {
                        // Находим строку с выбранным `НГр`
                        var selectedRow = номерSource.Rows
                            .Cast<DataRow>()
                            .FirstOrDefault(row => row["НЗаявки"].Equals(selectedValue));

                        if (selectedRow != null)
                        {
                            // Устанавливаем соответствующее значение `НОрг` в кбНОрг
                            var relatedНКл = selectedRow["НКл"];
                            if (relatedНКл != DBNull.Value)
                            {
                                кбНКл.SelectedValue = relatedНКл;
                            }

                            var номерСотрудника = selectedRow["НС"];
                            if (номерСотрудника != DBNull.Value)
                            {
                                кбНС.SelectedValue = номерСотрудника;
                            }

                            var решениеПоЗаявке = selectedRow["СтатусЗаявки"];
                            if (решениеПоЗаявке != DBNull.Value)
                            {
                                кбСтатусЗаявки.SelectedItem = решениеПоЗаявке.ToString();
                            }
                        }
                    }
                }
            }
            try
            {
                int selectedId = (int)кбНЗаявки.SelectedValue;
                var data = await _repo.GetZayavkaDetails(selectedId, (int)кбНКл.SelectedValue);
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
                номерЗаявки = (int)кбНЗаявки.SelectedValue;

                ZaselenieClienta current = new ZaselenieClienta(номерЗаявки, (int)кбНКл.SelectedValue, (int)кбНС.SelectedValue, кбСтатусЗаявки.Text);

                Result<int> result;
                result = await _repo.Add(current);

                if (result)
                {
                    MessageBox.Show($"Заявка №{номерЗаявки} клиента {кбНКл.Text} рассмотрена и ей выдан статус {кбСтатусЗаявки.Text}!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (!result)
                {

                    MessageBox.Show($"Ошибка в проведении заявки: " + result.Error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                номерЗаявки = (int)кбНЗаявки.SelectedValue;
                ZaselenieClienta current = new ZaselenieClienta(номерЗаявки, (int)кбНКл.SelectedValue, (int)кбНС.SelectedValue, кбСтатусЗаявки.Text);

                Result<int> result;
                result = await _repo.Update(current, номерЗаявки, (int)кбНКл.SelectedValue);

                if (result)
                {
                    MessageBox.Show($"Сведения заявки №{номерЗаявки} клиента {кбНКл.Text} успешно изменены!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (!result)
                {
                    MessageBox.Show("Ошибка обновления данных: "+result.Error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("Вы не заполнили все поля формы!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
