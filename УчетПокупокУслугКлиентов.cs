using Client.Interfaces;
using Client.Models;
using Client.Services;
using Client.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Client
{
    public partial class УчетПокупокУслугКлиентов : Form
    {
        MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConn"].ToString());
        MySqlCommand cmd;
        MySqlDataAdapter da;
        DataTable dt;
        IUchetZayavokUslug _repo;
        string sql;
        Dictionary<string, Control> controlsMapping;
        bool firstLoading = true;
        bool firstLoadingOfDate = true;
        bool firstTimeOnForm = true;

        public УчетПокупокУслугКлиентов()
        {
            InitializeComponent();
        }

        private void LoadCombo(ComboBoxDataForFill obj, ComboBox sender)
        {
            try
            {
                con.Open();
                cmd = new MySqlCommand();
                cmd.Connection = con;
                cmd.CommandText = obj.sql;
                foreach (MySqlParameter e in obj.paramsForSQLQuery)
                {
                    cmd.Parameters.Add(e);
                }
                da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                dt = new DataTable();
                da.Fill(dt);

                obj.dataSource = dt;
                sender.DataSource = obj.dataSource;
                sender.DisplayMember = obj.DisplayMember;
                sender.ValueMember = obj.ValueMember;
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

        private void SetControlsFromDataRow(object selectedValue, DataTable номерSource, Dictionary<string, Control> controlsMapping)
        {
            if (selectedValue != null && номерSource != null)
            {
                var selectedRow = номерSource.Rows
                    .Cast<DataRow>()
                    .FirstOrDefault(row => row[0].Equals(selectedValue));

                if (selectedRow != null)
                {
                    foreach (var entry in controlsMapping)
                    {
                        var columnName = entry.Key;
                        var control = entry.Value;

                        if (columnName.Equals("НС"))
                            continue;

                        if (selectedRow.Table.Columns.Contains(columnName))
                        {
                            var value = selectedRow[columnName];

                            if (value != DBNull.Value)
                            {
                                switch (control)
                                {
                                    case ComboBox comboBox:
                                        comboBox.SelectedValue = value;
                                        break;

                                    case DateTimePicker dateTimePicker:
                                        dateTimePicker.Text = value.ToString();
                                        firstLoadingOfDate = false;
                                        break;

                                    case TextBox textBox:
                                        textBox.Text = value.ToString();
                                        break;
                                }
                            }
                        }
                    }
                }
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

        private async void УчетПокупокУслугКлиентов_Load(object sender, EventArgs e)
        {
            _repo = new RUchetZayavokUslug();

            checkBox1.Checked = true;
            checkBox1.Visible = false;
            кбНС.Enabled = false;
            тбСтоимостьОплаты.ReadOnly = true;
            тбШтраф.ReadOnly = true;
            BTN_UPDATECURRENT.Visible = false;

            controlsMapping = new Dictionary<string, Control>
            {
                { "НЗаявки", кбНЗаявки },
                { "НКл", кбНКл },
                { "НС", кбНС },
                { "ДатаОплаты", тбДатаОплаты },
                { "РазмерШтрафа", тбШтраф },
                { "СуммаКОплате", тбСтоимостьОплаты }
            };

            // Загружаем клиентов
            sql = "SELECT DISTINCT НКл, ФИО FROM Клиент WHERE НКл IN (SELECT НКл FROM ЗаявкаНаУслугу WHERE ПокупкаСовершена=0)";
            ComboBoxDataForFill Клиент = new ComboBoxDataForFill(sql, "ФИО", "НКл");
            LoadCombo(Клиент, кбНКл);

            int? НКл = (int?)(кбНКл.SelectedValue ?? 0);

            if (НКл == 0)
            {
                MessageBox.Show("Клиенты с неподтвержденными заявками отсутствуют.\nПереход на подтвержденные заявки.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                firstLoading = false;
                checkBox1.Checked = false;
                return;
            }

            sql = "SELECT DISTINCT НЗаявки FROM ЗаявкаНаУслугу WHERE НКл=@НКл AND ПокупкаСовершена=0";
            ComboBoxDataForFill ЗаявкаНаУслугу = new ComboBoxDataForFill(sql, "НЗаявки", "НЗаявки");
            ЗаявкаНаУслугу.paramsForSQLQuery.Add(new MySqlParameter("@НКл", MySqlDbType.Int32) { Value = НКл });
            LoadCombo(ЗаявкаНаУслугу, кбНЗаявки);

            int? НЗаявки = (int?)(кбНЗаявки.SelectedValue ?? 0);

            if (НЗаявки == 0)
            {
                MessageBox.Show("Заявки, требующие подтверждения покупки, отсутствуют", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            sql = "SELECT НС, ФИО FROM Портье WHERE НС=@НС";
            ComboBoxDataForFill Портье = new ComboBoxDataForFill(sql, "ФИО", "НС");
            Портье.paramsForSQLQuery.Add(new MySqlParameter("@НС", MySqlDbType.Int32) { Value = Convert.ToInt32(lbWhoLogged.Text) });
            LoadCombo(Портье, кбНС);


            await LoadServicesByRequest(НЗаявки.Value);

            firstLoading = false;
            тбДатаОплаты.Value = DateTime.Today;
        }

        private async Task LoadServicesByRequest(int НЗаявки)
        {
            try
            {
                sql = @"SELECT z.НУслуги, u.Наименование, SUM(z.Количество_Ед) AS Количество_Ед, u.Цена, SUM(z.Сумма) AS Сумма
                        FROM ЗаявкаНаУслугу z
                        JOIN Услуга u ON z.НУслуги = u.НУслуги
                        WHERE z.НЗаявки = @НЗаявки
                        GROUP BY z.НУслуги, u.Наименование, u.Цена";
                using (var cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@НЗаявки", НЗаявки);
                    da = new MySqlDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;

                    // Настройка столбцов
                    dataGridView1.Columns["НУслуги"].Visible = false;
                    dataGridView1.Columns["Наименование"].HeaderText = "Наименование услуги";
                    dataGridView1.Columns["Количество_Ед"].HeaderText = "Количество";
                    dataGridView1.Columns["Цена"].HeaderText = "Цена за ед., руб";
                    dataGridView1.Columns["Сумма"].HeaderText = "Общая сумма, руб";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки услуг: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                BTN_CREATENEW.Enabled = true;
                BTN_UPDATECURRENT.Enabled = false;
                тбШтраф.Text = "";
                тбСтоимостьОплаты.Text = "";

                //if (!firstLoading)
                //{
                // Загружаем клиентов
                sql = "SELECT DISTINCT НКл, ФИО FROM Клиент WHERE НКл IN (SELECT НКл FROM ЗаявкаНаУслугу WHERE ПокупкаСовершена=0)";
                ComboBoxDataForFill Клиент = new ComboBoxDataForFill(sql, "ФИО", "НКл");
                LoadCombo(Клиент, кбНКл);

                int? НКл = (int?)(кбНКл.SelectedValue ?? 0);

                if (НКл == 0)
                {
                    MessageBox.Show("Клиенты с неподтвержденными заявками отсутствуют.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    checkBox1.Checked = false;
                    return;
                }

                // Загружаем заявки клиента
                sql = "SELECT DISTINCT НЗаявки FROM ЗаявкаНаУслугу WHERE НКл=@НКл AND ПокупкаСовершена=0";
                ComboBoxDataForFill ЗаявкаНаУслугу = new ComboBoxDataForFill(sql, "НЗаявки", "НЗаявки");
                ЗаявкаНаУслугу.paramsForSQLQuery.Add(new MySqlParameter("@НКл", MySqlDbType.Int32) { Value = НКл });
                LoadCombo(ЗаявкаНаУслугу, кбНЗаявки);

                int? НЗаявки = (int?)(кбНЗаявки.SelectedValue ?? 0);

                if (НЗаявки == 0)
                {
                    MessageBox.Show("Заявки, требующие подтверждения покупки, отсутствуют", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }

                // Загружаем портье
                sql = "SELECT НС, ФИО FROM Портье WHERE НС=@НС";
                ComboBoxDataForFill Портье = new ComboBoxDataForFill(sql, "ФИО", "НС");
                Портье.paramsForSQLQuery.Add(new MySqlParameter("@НС", MySqlDbType.Int32) { Value = Convert.ToInt32(lbWhoLogged.Text) });
                LoadCombo(Портье, кбНС);

                await LoadServicesByRequest(НЗаявки.Value);
                //}
            }
            else
            {
                BTN_CREATENEW.Enabled = false;
                BTN_UPDATECURRENT.Enabled = true;

                // Загружаем клиентов с подтвержденными заявками
                sql = "SELECT DISTINCT НКл, ФИО FROM Клиент WHERE НКл IN (SELECT НКл FROM ЗаявкаНаУслугу WHERE ПокупкаСовершена=1)";
                ComboBoxDataForFill Клиент = new ComboBoxDataForFill(sql, "ФИО", "НКл");
                LoadCombo(Клиент, кбНКл);

                int? НКл = (int?)(кбНКл.SelectedValue ?? 0);

                if (НКл == 0)
                {
                    MessageBox.Show("Клиенты с подтвержденными заявками отсутствуют.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Загружаем подтвержденные заявки клиента
                sql = "SELECT DISTINCT НЗаявки, ДатаОплаты, РазмерШтрафа, СуммаКОплате, НС FROM ЗаявкаНаУслугу WHERE НКл=@НКл AND ПокупкаСовершена=1";
                ComboBoxDataForFill ЗаявкаНаУслугу = new ComboBoxDataForFill(sql, "НЗаявки", "НЗаявки");
                ЗаявкаНаУслугу.paramsForSQLQuery.Add(new MySqlParameter("@НКл", MySqlDbType.Int32) { Value = НКл });
                LoadCombo(ЗаявкаНаУслугу, кбНЗаявки);

                int? НЗаявки = (int?)(кбНЗаявки.SelectedValue ?? 0);

                if (НЗаявки == 0)
                {
                    MessageBox.Show("Подтвержденные заявки для выбранного клиента отсутствуют.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Загружаем портье
                sql = "SELECT НС, ФИО FROM Портье WHERE НС IN (SELECT НС FROM ЗаявкаНаУслугу WHERE НЗаявки=@НЗаявки)";
                ComboBoxDataForFill Портье = new ComboBoxDataForFill(sql, "ФИО", "НС");
                Портье.paramsForSQLQuery.Add(new MySqlParameter("@НЗаявки", MySqlDbType.Int32) { Value = НЗаявки });
                LoadCombo(Портье, кбНС);

                SetControlsFromDataRow(НЗаявки, кбНЗаявки.DataSource as DataTable, controlsMapping);

                await LoadServicesByRequest(НЗаявки.Value);
            }
        }

        private async void кбНКл_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int НКл = (int)кбНКл.SelectedValue;

            // Загружаем заявки клиента
            sql = checkBox1.Checked
                ? "SELECT DISTINCT НЗаявки FROM ЗаявкаНаУслугу WHERE НКл=@НКл AND ПокупкаСовершена=0"
                : "SELECT DISTINCT НЗаявки, ДатаОплаты, РазмерШтрафа, СуммаКОплате, НС FROM ЗаявкаНаУслугу WHERE НКл=@НКл AND ПокупкаСовершена=1";
            ComboBoxDataForFill ЗаявкаНаУслугу = new ComboBoxDataForFill(sql, "НЗаявки", "НЗаявки");
            ЗаявкаНаУслугу.paramsForSQLQuery.Add(new MySqlParameter("@НКл", MySqlDbType.Int32) { Value = НКл });
            LoadCombo(ЗаявкаНаУслугу, кбНЗаявки);

            int? НЗаявки = (int?)(кбНЗаявки.SelectedValue ?? 0);

            if (НЗаявки == 0)
            {
                MessageBox.Show("Заявки для выбранного клиента отсутствуют.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataGridView1.DataSource = null;
                return;
            }

            if (!checkBox1.Checked)
            {
                SetControlsFromDataRow(НЗаявки, кбНЗаявки.DataSource as DataTable, controlsMapping);
            }

            await LoadServicesByRequest(НЗаявки.Value);
        }

        private async void кбНЗаявки_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int НЗаявки = (int)кбНЗаявки.SelectedValue;
            sql = "SELECT НС, ФИО FROM Портье WHERE НС=@НС";
            ComboBoxDataForFill Портье = new ComboBoxDataForFill(sql, "ФИО", "НС");
            Портье.paramsForSQLQuery.Add(new MySqlParameter("@НС", MySqlDbType.Int32) { Value = Convert.ToInt16(lbWhoLogged.Text) });
            LoadCombo(Портье, кбНС);
            SetControlsFromDataRow(НЗаявки, кбНЗаявки.DataSource as DataTable, controlsMapping);
            await LoadServicesByRequest(НЗаявки);
            тбДатаОплаты_ValueChanged(тбДатаОплаты.Value, null);
        }

        private async void тбДатаОплаты_ValueChanged(object sender, EventArgs e)
        {
            if (!firstLoadingOfDate || firstTimeOnForm)
            {
                firstLoadingOfDate = false;
                firstTimeOnForm = false;

                sql = "SELECT НЗаявки, СрокОплаты, SUM(Сумма) AS 'Сумма' FROM ЗаявкаНаУслугу WHERE НЗаявки=@НЗаявки GROUP BY НЗаявки";
                ComboBoxDataForFill ЗаявкаНаУслугу = new ComboBoxDataForFill(sql, "НЗаявки", "НЗаявки");
                ЗаявкаНаУслугу.paramsForSQLQuery.Add(new MySqlParameter("@НЗаявки", MySqlDbType.Int32) { Value = кбНЗаявки.SelectedValue });
                LoadCombo(ЗаявкаНаУслугу, кбНЗаявкиКлон);

                кбНЗаявкиКлон.SelectedValue = кбНЗаявки.SelectedValue;

                ВыбратьСрокОплатыИзЗаявки((int)кбНЗаявкиКлон.SelectedValue, кбНЗаявкиКлон.DataSource as DataTable);

                int diff = (тбДатаОплаты.Value.Date - тбСрокОплаты.Value.Date).Days;

                float штрафТекущий = string.IsNullOrWhiteSpace(тбШтраф.Text) ? 0 : float.Parse(тбШтраф.Text);
                float текущаяСтоимость = string.IsNullOrWhiteSpace(тбСтоимостьОплаты.Text) ? 0 : float.Parse(тбСтоимостьОплаты.Text);
                float новыйШтраф = 0;

                if (diff <= 0)
                {
                    штрафТекущий = 0;
                    тбШтраф.Text = "0";
                    тбСтоимостьОплаты.Text = текущаяСтоимость.ToString("F2");
                }
                else
                {
                    новыйШтраф = diff * 50;
                    тбШтраф.Text = новыйШтраф.ToString();
                    тбСтоимостьОплаты.Text = (новыйШтраф + текущаяСтоимость).ToString("F2");
                }
            }
        }

        void ВыбратьСрокОплатыИзЗаявки(int selectedValue, DataTable rowSource)
        {
            if (selectedValue != null && rowSource != null)
            {
                var selectedRow = rowSource.Rows
                    .Cast<DataRow>()
                    .FirstOrDefault(row => row[0].Equals(selectedValue));

                if (selectedRow != null)
                {
                    var columnName = "СрокОплаты";
                    if (selectedRow.Table.Columns.Contains(columnName))
                    {
                        var value = selectedRow[columnName];
                        тбСрокОплаты.Value = (DateTime)value;

                        // Устанавливаем начальную сумму
                        columnName = "Сумма";
                        if (selectedRow.Table.Columns.Contains(columnName))
                        {
                            var сумма = selectedRow[columnName];
                            тбСтоимостьОплаты.Text = сумма.ToString();
                        }
                    }
                }
            }
        }

        private async void BTN_CREATENEW_Click(object sender, EventArgs e)
        {
            try
            {
                UchetZayavokUslug current = new UchetZayavokUslug(
                    (int)кбНЗаявки.SelectedValue,
                    (int)кбНКл.SelectedValue,
                    (int)кбНС.SelectedValue,
                    тбДатаОплаты.Value,
                    float.Parse(тбШтраф.Text),
                    float.Parse(тбСтоимостьОплаты.Text),
                    true);

                Result<int> result = await _repo.Update(current);

                if (result)
                {
                    MessageBox.Show($"Покупка по заявке №{кбНЗаявки.SelectedValue} подтверждена", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    sql = "SELECT DISTINCT НЗаявки FROM ЗаявкаНаУслугу WHERE НКл=@НКл AND ПокупкаСовершена=0";
                    ComboBoxDataForFill ЗаявкаНаУслугу = new ComboBoxDataForFill(sql, "НЗаявки", "НЗаявки");
                    ЗаявкаНаУслугу.paramsForSQLQuery.Add(new MySqlParameter("@НКл", MySqlDbType.Int32) { Value = Convert.ToInt16(кбНКл.Text) });
                    LoadCombo(ЗаявкаНаУслугу, кбНЗаявки);

                    int? НЗаявки = (int?)(кбНЗаявки.SelectedValue ?? 0);

                    if (НЗаявки == 0)
                    {
                        MessageBox.Show("Заявки, требующие подтверждения покупки, отсутствуют", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show($"Ошибка: {result.Error}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("Вы не заполнили все поля формы!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BTN_UPDATECURRENT_Click(object sender, EventArgs e)
        {

        }
    }
}