using Client.Interfaces;
using Client.Models;
using Client.Services;
using Client.Utils;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
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
    public partial class УчетПокупокУслугКлиентов : Form
    {
        MySqlConnection con = new MySqlConnection("server=localhost;user id=root;database=hotel");
        MySqlCommand cmd;
        MySqlDataAdapter da;
        DataTable dt;
        private IUchetZayavokUslug _repo;
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
            if (selectedValue != null)
            {
                if (номерSource != null)
                {
                    var selectedRow = номерSource.Rows
                        .Cast<DataRow>()
                        .FirstOrDefault(row => row[0].Equals(selectedValue));

                    if (selectedRow != null)
                    {
                        // Словарь сопоставления столбцов и соответствующих элементов управления

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
            кбНС.Enabled = false;
            тбСтоимостьОплаты.Enabled = false;
            тбШтраф.Enabled = false;

            controlsMapping = new Dictionary<string, Control>
                    {
                        { "НЗаявки", кбНЗаявки },
                        { "НКл", кбНКл },
                        { "НС", кбНС },
                        { "ДатаОплаты", тбДатаОплаты },
                        { "РазмерШтрафа", тбШтраф },
                        { "СуммаКОплате", тбСтоимостьОплаты }
                    };

            sql = "SELECT * FROM `заявканауслугу` WHERE ПокупкаСовершена=0";
            ComboBoxDataForFill ЗаявкаНаУслугу = new ComboBoxDataForFill(sql, "НЗаявки", "НЗаявки");
            LoadCombo(ЗаявкаНаУслугу, кбНЗаявки);

            int? НЗаявки = (int?)(кбНЗаявки.SelectedValue ?? 0);

            if (НЗаявки == 0)
            {
                MessageBox.Show("Данные о заявках клиентов на услуги, требующих рассмотрения, отсутствуют.\nПереход на рассмотренные заявки.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                firstLoading = false;
                checkBox1.Checked = false;
                return;
            }

            sql = "SELECT НКл FROM `заявканауслугу` WHERE НЗаявки=@НЗаявки";
            ComboBoxDataForFill Клиент = new ComboBoxDataForFill(sql, "НКл", "НКл");
            Клиент.paramsForSQLQuery.Add(new MySqlParameter("@НЗаявки", MySqlDbType.Int32) { Value = НЗаявки });
            LoadCombo(Клиент, кбНКл);

            int? НКл = (int?)(кбНКл.SelectedValue ?? 0);

            if (НКл == 0)
            {
                MessageBox.Show("Данные о клиенте, заказавшем услугу, остутствуют. Продолжение невозможно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Прекращаем выполнение
            }

            sql = "SELECT НКл, ФИО FROM Клиент WHERE НКл=@НКл";
            Клиент.sql = sql;
            Клиент.DisplayMember = "ФИО";
            Клиент.ValueMember = "НКл";
            Клиент.paramsForSQLQuery.Clear();
            Клиент.paramsForSQLQuery.Add(new MySqlParameter("@НКл", MySqlDbType.Int32) { Value = НКл });
            LoadCombo(Клиент, кбНКл);

            sql = "SELECT НС, ФИО FROM Сотрудник WHERE НС=@НС";
            ComboBoxDataForFill Сотрудник = new ComboBoxDataForFill(sql, "ФИО", "НС");
            Сотрудник.paramsForSQLQuery.Add(new MySqlParameter("@НС", MySqlDbType.Int32) { Value = Convert.ToInt16(lbWhoLogged.Text) });
            LoadCombo(Сотрудник, кбНС);

            try
            {
                var data = await _repo.GetByRequest((int)кбНЗаявки.SelectedValue, (int)кбНКл.SelectedValue);
                dataGridView1.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            firstLoading = false;
        }

        private async void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                button1.Enabled = true;
                button2.Enabled = false;
                button3.Enabled = false;
                тбШтраф.Text = "";
                тбСтоимостьОплаты.Text = "";

                if (!firstLoading)
                {
                    sql = "SELECT * FROM `заявканауслугу` WHERE ПокупкаСовершена=0";
                    ComboBoxDataForFill ЗаявкаНаУслугу = new ComboBoxDataForFill(sql, "НЗаявки", "НЗаявки");
                    LoadCombo(ЗаявкаНаУслугу, кбНЗаявки);

                    int? НЗаявки = (int?)(кбНЗаявки.SelectedValue ?? 0);

                    if (НЗаявки == 0)
                    {
                        MessageBox.Show("Данные о заявках клиентов на услуги, требующих рассмотрения, отсутствуют.\nПереход на рассмотренные заявки.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        checkBox1.Checked = false;
                        return; // Прекращаем выполнение
                    }

                    sql = "SELECT НКл FROM `заявканауслугу` WHERE НЗаявки=@НЗаявки";
                    ComboBoxDataForFill Клиент = new ComboBoxDataForFill(sql, "НКл", "НКл");
                    Клиент.paramsForSQLQuery.Add(new MySqlParameter("@НЗаявки", MySqlDbType.Int32) { Value = НЗаявки });
                    LoadCombo(Клиент, кбНКл);

                    int? НКл = (int?)(кбНКл.SelectedValue ?? 0);

                    if (НКл == 0)
                    {
                        MessageBox.Show("Данные о клиенте, заказавшем услугу, остутствуют. Продолжение невозможно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Прекращаем выполнение
                    }

                    sql = "SELECT НКл, ФИО FROM Клиент WHERE НКл=@НКл";
                    Клиент.sql = sql;
                    Клиент.DisplayMember = "ФИО";
                    Клиент.ValueMember = "НКл";
                    Клиент.paramsForSQLQuery.Clear();
                    Клиент.paramsForSQLQuery.Add(new MySqlParameter("@НКл", MySqlDbType.Int32) { Value = НКл });
                    LoadCombo(Клиент, кбНКл);

                    sql = "SELECT НС, ФИО FROM Сотрудник WHERE НС=@НС";
                    ComboBoxDataForFill Сотрудник = new ComboBoxDataForFill(sql, "ФИО", "НС");
                    Сотрудник.paramsForSQLQuery.Add(new MySqlParameter("@НС", MySqlDbType.Int32) { Value = Convert.ToInt16(lbWhoLogged.Text) });
                    LoadCombo(Сотрудник, кбНС);

                    try
                    {
                        var data = await _repo.GetByRequest((int)кбНЗаявки.SelectedValue, (int)кбНКл.SelectedValue);
                        dataGridView1.DataSource = data;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                button1.Enabled = false;
                button2.Enabled = true;
                button3.Enabled = true;

                sql = "SELECT * FROM `учетпокупокуслуг`";
                ComboBoxDataForFill ЗаявкаНаУслугу = new ComboBoxDataForFill(sql, "НЗаявки", "НЗаявки");
                LoadCombo(ЗаявкаНаУслугу, кбНЗаявки);

                int? НЗаявки = (int?)(кбНЗаявки.SelectedValue ?? 0);

                if (НЗаявки == 0)
                {
                    MessageBox.Show("Данные о заявках клиентов на услуги отсутствуют", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Прекращаем выполнение
                }

                sql = "SELECT НКл FROM `учетпокупокуслуг` WHERE НЗаявки=@НЗаявки";
                ComboBoxDataForFill Клиент = new ComboBoxDataForFill(sql, "НКл", "НКл");
                Клиент.paramsForSQLQuery.Add(new MySqlParameter("@НЗаявки", MySqlDbType.Int32) { Value = НЗаявки });
                LoadCombo(Клиент, кбНКл);

                int? НКл = (int?)(кбНКл.SelectedValue ?? 0);

                if (НКл == 0)
                {
                    MessageBox.Show("Данные о клиенте, заказавшем услугу, остутствуют. Продолжение невозможно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Прекращаем выполнение
                }

                sql = "SELECT НКл, ФИО FROM Клиент WHERE НКл=@НКл";
                Клиент.sql = sql;
                Клиент.DisplayMember = "ФИО";
                Клиент.ValueMember = "НКл";
                Клиент.paramsForSQLQuery.Clear();
                Клиент.paramsForSQLQuery.Add(new MySqlParameter("@НКл", MySqlDbType.Int32) { Value = НКл });
                LoadCombo(Клиент, кбНКл);

                sql = "SELECT НС FROM `учетпокупокуслуг` WHERE НЗаявки=@НЗаявки";
                ComboBoxDataForFill Сотрудник = new ComboBoxDataForFill(sql, "НС", "НС");
                Сотрудник.paramsForSQLQuery.Add(new MySqlParameter("@НЗаявки", MySqlDbType.Int32) { Value = НЗаявки });
                LoadCombo(Сотрудник, кбНС);

                int НС = (int)кбНС.SelectedValue;

                sql = "SELECT НС, ФИО FROM Сотрудник WHERE НС=@НС";
                Сотрудник.sql = sql;
                Сотрудник.DisplayMember = "ФИО";
                Сотрудник.ValueMember = "НС";
                Сотрудник.paramsForSQLQuery.Clear();
                Сотрудник.paramsForSQLQuery.Add(new MySqlParameter("@НС", MySqlDbType.Int32) { Value = НС });
                LoadCombo(Сотрудник, кбНС);

                SetControlsFromDataRow((int)кбНЗаявки.SelectedValue, кбНЗаявки.DataSource as DataTable, controlsMapping);

                try
                {
                    var data = await _repo.GetByRequest((int)кбНЗаявки.SelectedValue, (int)кбНКл.SelectedValue);
                    dataGridView1.DataSource = data;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int clientId = Convert.ToInt16(lbWhoLogged.Text);

                UchetZayavokUslug current = new UchetZayavokUslug((int)кбНЗаявки.SelectedValue, (int)кбНКл.SelectedValue, (int)кбНС.SelectedValue,
                                                                    тбДатаОплаты.Value, float.Parse(тбШтраф.Text), float.Parse(тбСтоимостьОплаты.Text));

                Result<int> result;
                result = await _repo.Add(current);

                if (result)
                {
                    MessageBox.Show($"Покупка по заявке №{кбНЗаявки.SelectedValue} успешно добавлена!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (!result)
                {

                    MessageBox.Show($"Вы уже добавляли покупку по заявке №{кбНЗаявки.SelectedValue}\n" + result.Error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("Вы не заполнили все поля формы!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void кбНЗаявки_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int НЗаявки = (int)кбНЗаявки.SelectedValue;
            sql = "SELECT НКл FROM `заявканауслугу` WHERE НЗаявки=@НЗаявки";
            ComboBoxDataForFill Клиент = new ComboBoxDataForFill(sql, "НКл", "НКл");
            Клиент.paramsForSQLQuery.Add(new MySqlParameter("@НЗаявки", MySqlDbType.Int32) { Value = НЗаявки });
            LoadCombo(Клиент, кбНКл);

            int? НКл = (int?)(кбНКл.SelectedValue ?? 0);

            if (НКл == 0)
            {
                MessageBox.Show("Данные о клиенте, заказавшем услугу, остутствуют. Продолжение невозможно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Прекращаем выполнение
            }

            sql = "SELECT НКл, ФИО FROM Клиент WHERE НКл=@НКл";
            Клиент.sql = sql;
            Клиент.DisplayMember = "ФИО";
            Клиент.ValueMember = "НКл";
            Клиент.paramsForSQLQuery.Clear();
            Клиент.paramsForSQLQuery.Add(new MySqlParameter("@НКл", MySqlDbType.Int32) { Value = НКл });
            LoadCombo(Клиент, кбНКл);

            if (checkBox1.Checked)
            {
                sql = "SELECT НС, ФИО FROM Сотрудник WHERE НС=@НС";
                ComboBoxDataForFill Сотрудник = new ComboBoxDataForFill(sql, "ФИО", "НС");
                Сотрудник.paramsForSQLQuery.Add(new MySqlParameter("@НС", MySqlDbType.Int32) { Value = Convert.ToInt16(lbWhoLogged.Text) });
                LoadCombo(Сотрудник, кбНС);
            }
            else
            {
                sql = "SELECT НС FROM `учетпокупокуслуг` WHERE НЗаявки=@НЗаявки";
                ComboBoxDataForFill Сотрудник = new ComboBoxDataForFill(sql, "НС", "НС");
                Сотрудник.paramsForSQLQuery.Add(new MySqlParameter("@НЗаявки", MySqlDbType.Int32) { Value = НЗаявки });
                LoadCombo(Сотрудник, кбНС);

                int НС = (int)кбНС.SelectedValue;

                sql = "SELECT НС, ФИО FROM Сотрудник WHERE НС=@НС";
                Сотрудник.sql = sql;
                Сотрудник.DisplayMember = "ФИО";
                Сотрудник.ValueMember = "НС";
                Сотрудник.paramsForSQLQuery.Clear();
                Сотрудник.paramsForSQLQuery.Add(new MySqlParameter("@НС", MySqlDbType.Int32) { Value = НС });
                LoadCombo(Сотрудник, кбНС);

                SetControlsFromDataRow((int)кбНЗаявки.SelectedValue, кбНЗаявки.DataSource as DataTable, controlsMapping);
            }
            try
            {
                var data = await _repo.GetByRequest((int)кбНЗаявки.SelectedValue, (int)кбНКл.SelectedValue);
                dataGridView1.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void тбДатаОплаты_ValueChanged(object sender, EventArgs e)
        {
            if (!firstLoadingOfDate || firstTimeOnForm)
            {
                firstLoadingOfDate = false;
                firstTimeOnForm = false;

                sql = "SELECT * FROM `заявканауслугу`";
                ComboBoxDataForFill ЗаявкаНаУслугу = new ComboBoxDataForFill(sql, "НЗаявки", "НЗаявки");
                LoadCombo(ЗаявкаНаУслугу, кбНЗаявкиКлон);

                кбНЗаявкиКлон.SelectedValue = кбНЗаявки.SelectedValue;

                ВыбратьСрокОплатыИзЗаявки((int)кбНЗаявкиКлон.SelectedValue, кбНЗаявкиКлон.DataSource as DataTable);

                int diff = (тбДатаОплаты.Value - тбСрокОплаты.Value).Days;
                float штрафТекущий = string.IsNullOrWhiteSpace(тбШтраф.Text) ? 0 : float.Parse(тбШтраф.Text);
                float текущаяСтоимость = string.IsNullOrWhiteSpace(тбСтоимостьОплаты.Text) ? 0 : float.Parse(тбСтоимостьОплаты.Text) - штрафТекущий;
                float новыйШтраф = 0;

                if (diff < 0)
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
            if (selectedValue != null)
            {
                if (rowSource != null)
                {
                    var selectedRow = rowSource.Rows
                        .Cast<DataRow>()
                        .FirstOrDefault(row => row[0].Equals(selectedValue));

                    if (selectedRow != null)
                    {
                        // Словарь сопоставления столбцов и соответствующих элементов управления

                        var columnName = "СрокОплаты";

                        if (selectedRow.Table.Columns.Contains(columnName))
                        {
                            var value = selectedRow[columnName];

                            тбСрокОплаты.Value = (DateTime)value;
                        }
                    }
                }
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                UchetZayavokUslug current = new UchetZayavokUslug((int)кбНЗаявки.SelectedValue, (int)кбНКл.SelectedValue, (int)кбНС.SelectedValue,
                                                                    тбДатаОплаты.Value, float.Parse(тбШтраф.Text), float.Parse(тбСтоимостьОплаты.Text));

                Result<int> result;
                result = await _repo.Update(current);

                if (result)
                {
                    MessageBox.Show($"Сведения покупки по заявке №{кбНЗаявки.SelectedValue} успешно изменены!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (!result)
                {
                    MessageBox.Show("Ошибка обновления данных: " + result.Error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                UchetZayavokUslug current = new UchetZayavokUslug((int)кбНЗаявки.SelectedValue, (int)кбНКл.SelectedValue, (int)кбНС.SelectedValue,
                                                                    тбДатаОплаты.Value, float.Parse(тбШтраф.Text), float.Parse(тбСтоимостьОплаты.Text));

                Result<int> result;
                result = await _repo.Remove(current);

                if (result)
                {
                    MessageBox.Show($"Сведения покупки по заявке №{кбНЗаявки.SelectedValue} успешно удалены!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    sql = "SELECT * FROM `учетпокупокуслуг`";
                    ComboBoxDataForFill ЗаявкаНаУслугу = new ComboBoxDataForFill(sql, "НЗаявки", "НЗаявки");
                    LoadCombo(ЗаявкаНаУслугу, кбНЗаявки);

                }
                if (!result)
                {
                    MessageBox.Show("Ошибка удаления данных: " + result.Error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("Вы не заполнили все поля формы!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
