using Client.Interfaces;
using Client.Services;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Client
{
    public partial class ReturnPaymentForm : Form
    {
        MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConn"].ToString());
        MySqlCommand cmd;
        MySqlDataAdapter da;
        DataTable dt;
        IReturnPayment _repo;
        string sql;
        Dictionary<string, Control> controlsMapping;

        public ReturnPaymentForm()
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

        private async void ReturnPaymentForm_Load(object sender, EventArgs e)
        {
            _repo = new ReturnPaymentService();

            controlsMapping = new Dictionary<string, Control>
            {
                { "НЗаявки", кбНЗаявки },
                { "НКл", кбНКл },
                { "НС", кбНС },
                { "ДатаОплаты", тбДатаВозврата },
                { "СпособВозврата", кбСпособВозврата },
                { "СуммаВозврата", тбСуммаВозврата }
            };

            // Загружаем клиентов
            sql = "SELECT DISTINCT НКл, ФИО FROM Клиент WHERE НКл IN (SELECT НКл FROM ВозвратСредств WHERE НС IS NULL)";
            ComboBoxDataForFill Клиент = new ComboBoxDataForFill(sql, "ФИО", "НКл");
            LoadCombo(Клиент, кбНКл);

            int? НКл = (int?)(кбНКл.SelectedValue ?? 0);

            if (НКл == 0)
            {
                MessageBox.Show("Клиенты с неподтвержденными заявками отсутствуют.\nПереход на подтвержденные заявки", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            sql = "SELECT DISTINCT НЗаявки FROM ВозвратСредств WHERE НКл=@НКл";
            ComboBoxDataForFill ЗаявкаНаУслугу = new ComboBoxDataForFill(sql, "НЗаявки", "НЗаявки");
            ЗаявкаНаУслугу.paramsForSQLQuery.Add(new MySqlParameter("@НКл", MySqlDbType.Int32) { Value = НКл });
            LoadCombo(ЗаявкаНаУслугу, кбНЗаявки);

            int? НЗаявки = (int?)(кбНЗаявки.SelectedValue ?? 0);

            if (НЗаявки == 0)
            {
                MessageBox.Show("Заявки, требующие проведения возврата, отсутствуют", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            sql = "SELECT НС, ФИО FROM Портье WHERE НС=@НС";
            ComboBoxDataForFill Портье = new ComboBoxDataForFill(sql, "ФИО", "НС");
            Портье.paramsForSQLQuery.Add(new MySqlParameter("@НС", MySqlDbType.Int32) { Value = Convert.ToInt16(lbWhoLogged.Text) });
            LoadCombo(Портье, кбНС);

            GetSumByPickingOrderNum();

            await LoadServicesByRequest(НЗаявки.Value);

            тбДатаВозврата.Value = DateTime.Today;
        }

        void GetSumByPickingOrderNum()
        {
            sql = "SELECT НЗаявки, ДатаВозврата, SUM(СуммаВозврата) AS 'СуммаВозврата' " +
                  "FROM ВозвратСредств WHERE НЗаявки=@НЗаявки GROUP BY НЗаявки";
            ComboBoxDataForFill ЗаявкаНаУслугу = new ComboBoxDataForFill(sql, "НЗаявки", "НЗаявки");
            ЗаявкаНаУслугу.paramsForSQLQuery.Add(new MySqlParameter("@НЗаявки", MySqlDbType.Int32) { Value = кбНЗаявки.SelectedValue });
            LoadCombo(ЗаявкаНаУслугу, кбНЗаявкиКлон);

            кбНЗаявкиКлон.SelectedValue = кбНЗаявки.SelectedValue;
            int selectedValue = (int)кбНЗаявкиКлон.SelectedValue;
            DataTable rowSource = (DataTable)кбНЗаявкиКлон.DataSource;

            if (selectedValue != null && rowSource != null)
            {
                var selectedRow = rowSource.Rows
                    .Cast<DataRow>()
                    .FirstOrDefault(row => row[0].Equals(selectedValue));

                if (selectedRow != null)
                {
                    var columnName = "СуммаВозврата";
                    if (selectedRow.Table.Columns.Contains(columnName))
                    {
                        var value = selectedRow[columnName];
                        тбСуммаВозврата.Text = value.ToString();
                    }
                }
            }
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

        private void BTN_CREATENEW_Click(object sender, EventArgs e)
        {

        }

        private async void кбНКл_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int НКл = (int)кбНКл.SelectedValue;

            sql = "SELECT DISTINCT НЗаявки FROM ВозвратСредств WHERE НКл=@НКл";
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

            SetControlsFromDataRow(НЗаявки, кбНЗаявки.DataSource as DataTable, controlsMapping);
            await LoadServicesByRequest(НЗаявки.Value);
            GetSumByPickingOrderNum();
        }

        private async void кбНЗаявки_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int НЗаявки = (int)кбНЗаявки.SelectedValue;

            SetControlsFromDataRow(НЗаявки, кбНЗаявки.DataSource as DataTable, controlsMapping);
            await LoadServicesByRequest(НЗаявки);
            GetSumByPickingOrderNum();
        }
    }
}
