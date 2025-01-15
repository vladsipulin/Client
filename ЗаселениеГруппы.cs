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
    public partial class ЗаселениеГруппы : Form
    {
        MySqlConnection con = new MySqlConnection("server=localhost;user id=root;database=hotel");
        MySqlCommand cmd;
        MySqlDataAdapter da;
        DataTable dt;
        private IZGroup _repo;
        string sql;
        int номерЗаселения = 0;
        bool isDataLoadingNow = false;

        //новые поля
        int selectedValue;
        Dictionary<string, Control> controlsMapping;
        DataTable номерSource;

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

        public ЗаселениеГруппы()
        {
            InitializeComponent();
        }

        private void ЗаселениеГруппы_Load(object sender, EventArgs e)
        {
            _repo = new RZGroup();

            checkBox1.Checked = true;
            controlsMapping = new Dictionary<string, Control>
                    {
                        { "НДоговора", кбНДоговора },
                        { "НОрг", кбНОрг },
                        { "НГр", кбНГр },
                        { "НС", кбНС },
                        { "ДатаОплаты", тбДатаОплаты },
                        { "ДатаЗаселения", тбДатаЗаселения },
                        { "ДатаВыезда", тбДатаВыезда },
                        { "СтоимостьОплаты", тбСтоимостьОплаты },
                        { "Статус", кбСтатус }
                    };

            this.BackColor = System.Drawing.Color.White;
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

                            if (selectedRow.Table.Columns.Contains(columnName))
                            {
                                var value = selectedRow[columnName];

                                if (value != DBNull.Value)
                                {
                                    switch (control)
                                    {
                                        case ComboBox comboBox when columnName == "Статус":
                                            if (comboBox.Items.Contains(value))
                                            {
                                                comboBox.SelectedItem = value;
                                            }
                                            else
                                            {
                                                comboBox.Text = "<Статус не найден>";
                                            }
                                            break;

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
        }

        private async void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                button1.Enabled = true;
                button2.Enabled = false;

                кбВыборСтатуса.Enabled = false;
                кбНЗаселенияГруппы.Enabled = false;

                // Загружаем список договоров
                sql = "SELECT * FROM ДоговорСОрганизацией";
                ComboBoxDataForFill ДоговорСОрганизацией = new ComboBoxDataForFill(sql, "НДоговора", "НДоговора");
                LoadCombo(ДоговорСОрганизацией, кбНДоговора);

                // Загружаем список организаций
                sql = "SELECT НОрг, Наименование FROM Организация";
                ComboBoxDataForFill Организации = new ComboBoxDataForFill(sql, "Наименование", "НОрг");
                LoadCombo(Организации, кбНОрг);

                try
                {
                    sql = "SELECT НОрг, Наименование FROM Организация WHERE НОрг = @НОрг";
                    ComboBoxDataForFill Организация = new ComboBoxDataForFill(sql, "Наименование", "НОрг");
                    Организация.paramsForSQLQuery.Add(new MySqlParameter("@НОрг", MySqlDbType.Int32)
                    {
                        Value = (int)кбНОрг.SelectedValue
                    });
                    LoadCombo(Организация, кбНОрг);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке организации: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                try
                {
                    sql = "SELECT НГр FROM Группа WHERE НОрг = @НОрг";
                    ComboBoxDataForFill Группа = new ComboBoxDataForFill(sql, "НГр", "НГр");

                    Группа.paramsForSQLQuery.Add(new MySqlParameter("@НОрг", MySqlDbType.Int32)
                    {
                        Value = (int)кбНОрг.SelectedValue
                    });

                    LoadCombo(Группа, кбНГр);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке групп: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                sql = $"SELECT НС, ФИО FROM Сотрудник";
                ComboBoxDataForFill Сотрудник = new ComboBoxDataForFill(sql, "ФИО", "НС");
                LoadCombo(Сотрудник, кбНС);
                кбНС.Enabled = false;
            }
            else
            {
                button1.Enabled = false;
                button2.Enabled = true;
                кбВыборСтатуса.Enabled = true;
                кбНЗаселенияГруппы.Enabled = true;

                try
                {

                    sql = $"SELECT * FROM ЗаселениеГруппы";
                    ComboBoxDataForFill НомерЗаселения = new ComboBoxDataForFill(sql, "НЗаселенияГруппы", "НЗаселенияГруппы");
                    LoadCombo(НомерЗаселения, кбНЗаселенияГруппы);

                    номерЗаселения = (int)кбНЗаселенияГруппы.SelectedValue;
                    номерSource = кбНЗаселенияГруппы.DataSource as DataTable;

                    // Загружаем связанные НГр
                    sql = "SELECT * FROM ДоговорСОрганизацией WHERE НДоговора IN (SELECT НДоговора FROM ЗаселениеГруппы WHERE НЗаселенияГруппы = @НЗаселенияГруппы)";
                    ComboBoxDataForFill Договор = new ComboBoxDataForFill(sql, "НДоговора", "НДоговора");
                    Договор.paramsForSQLQuery.Add(new MySqlParameter("@НЗаселенияГруппы", MySqlDbType.Int32) { Value = номерЗаселения });
                    LoadCombo(Договор, кбНДоговора);

                    int номерДоговора = (int)кбНДоговора.SelectedValue;

                    // Загружаем связанные НОрг
                    sql = "SELECT НОрг, Наименование FROM Организация WHERE НОрг IN (SELECT НОрг FROM ДоговорСОрганизацией WHERE НДоговора = @НДоговора)";
                    ComboBoxDataForFill Организации = new ComboBoxDataForFill(sql, "Наименование", "НОрг");
                    Организации.paramsForSQLQuery.Add(new MySqlParameter("@НДоговора", MySqlDbType.Int32) { Value = номерДоговора });
                    LoadCombo(Организации, кбНОрг);

                    int номерОрганизации = (int)кбНОрг.SelectedValue;

                    // Загружаем связанные НГр
                    sql = "SELECT НГр FROM Группа WHERE НОрг = @НОрг";
                    ComboBoxDataForFill Группы = new ComboBoxDataForFill(sql, "НГр", "НГр");
                    Группы.paramsForSQLQuery.Add(new MySqlParameter("@НОрг", MySqlDbType.Int32) { Value = номерОрганизации });
                    LoadCombo(Группы, кбНГр);

                    sql = "SELECT НС, ФИО FROM Сотрудник WHERE НС IN (SELECT НС FROM ЗаселениеГруппы WHERE НЗаселенияГруппы = @НЗаселенияГруппы)";
                    ComboBoxDataForFill Сотрудник = new ComboBoxDataForFill(sql, "ФИО", "НС");
                    Сотрудник.paramsForSQLQuery.Add(new MySqlParameter("@НЗаселенияГруппы", MySqlDbType.Int32) { Value = номерЗаселения });
                    LoadCombo(Сотрудник, кбНС);

                    isDataLoadingNow = true;
                    SetControlsFromDataRow(номерЗаселения, номерSource, controlsMapping);
                    isDataLoadingNow = false;
                }
                catch
                {
                    MessageBox.Show("Записи в таблице 'Заселение группы' отсутствуют. Начните работу с добавления новой записи", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    checkBox1.Checked = true;
                }
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                bool isUnique = false;

                Random rand = new Random();
                while (!isUnique)
                {
                    номерЗаселения = rand.Next(10000, 99999); // Генерация случайного числа от 10000 до 99999

                    // Проверка уникальности номера заявки
                    var existing = await _repo.FindExistingNumZaselenie(номерЗаселения);
                    if (existing == 0)
                    {
                        // Если номера заявки еще нет в базе данных, то он уникален
                        isUnique = true;
                    }
                }

                DateTime date1 = тбДатаЗаселения.Value.Date;
                DateTime date2 = тбДатаВыезда.Value.Date;

                if (date2 < date1)
                {
                    MessageBox.Show("Ошибка: дата заселения позже даты выезда", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    ZGroup current = new ZGroup(номерЗаселения, (int)кбНДоговора.SelectedValue, (int)кбНОрг.SelectedValue, (int)кбНГр.SelectedValue, (int)кбНС.SelectedValue,
                                            Convert.ToDateTime(тбДатаОплаты.Text), Convert.ToDateTime(тбДатаЗаселения.Text),
                                            Convert.ToDateTime(тбДатаВыезда.Text), float.Parse(тбСтоимостьОплаты.Text), кбСтатус.Text);

                    Result<int> result;
                    result = await _repo.Add(current);

                    if (result)
                    {
                        MessageBox.Show($"Заселение №{номерЗаселения} группы №{кбНГр.Text}\nрассмотрено сотрудником '{кбНС.Text}'\nи ему выдан статус '{кбСтатус.Text}'", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    if (!result)
                    {

                        MessageBox.Show($"Ошибка заселения группы: " + result.Error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Вы не заполнили все поля формы!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void тбДатаЗаселения_ValueChanged(object sender, EventArgs e)
        {

        }

        private void тбДатаВыезда_ValueChanged(object sender, EventArgs e)
        {

        }

        private void кбНДоговора_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int номерДоговора = (int)кбНДоговора.SelectedValue;

            // Загружаем связанные НОрг
            string sql = "SELECT НОрг, Наименование FROM Организация WHERE НОрг IN (SELECT НОрг FROM ДоговорСОрганизацией WHERE НДоговора = @НДоговора)";
            ComboBoxDataForFill Организации = new ComboBoxDataForFill(sql, "Наименование", "НОрг");
            Организации.paramsForSQLQuery.Add(new MySqlParameter("@НДоговора", MySqlDbType.Int32) { Value = номерДоговора });
            LoadCombo(Организации, кбНОрг);

            кбНОрг.SelectedIndex = 0;
            int номерОрганизации = (int)кбНОрг.SelectedValue;

            // Загружаем связанные НГр
            sql = "SELECT НГр FROM Группа WHERE НОрг = @НОрг";
            ComboBoxDataForFill Группы = new ComboBoxDataForFill(sql, "НГр", "НГр");
            Группы.paramsForSQLQuery.Add(new MySqlParameter("@НОрг", MySqlDbType.Int32) { Value = номерОрганизации });
            LoadCombo(Группы, кбНГр);

            isDataLoadingNow = true;
            SetControlsFromDataRow(номерДоговора, номерSource, controlsMapping);
            isDataLoadingNow = false;

        }

        private void кбНЗаселенияГруппы_SelectionChangeCommitted(object sender, EventArgs e)
        {
            номерЗаселения = (int)кбНЗаселенияГруппы.SelectedValue;
            //номерSource = кбНЗаселенияГруппы.DataSource as DataTable;

            // Загружаем связанные НГр
            sql = "SELECT * FROM ДоговорСОрганизацией WHERE НДоговора IN (SELECT НДоговора FROM ЗаселениеГруппы WHERE НЗаселенияГруппы = @НЗаселенияГруппы)";
            ComboBoxDataForFill Договор = new ComboBoxDataForFill(sql, "НДоговора", "НДоговора");
            Договор.paramsForSQLQuery.Add(new MySqlParameter("@НЗаселенияГруппы", MySqlDbType.Int32) { Value = номерЗаселения });
            LoadCombo(Договор, кбНДоговора);

            int номерДоговора = (int)кбНДоговора.SelectedValue;

            // Загружаем связанные НОрг
            sql = "SELECT НОрг, Наименование FROM Организация WHERE НОрг IN (SELECT НОрг FROM ДоговорСОрганизацией WHERE НДоговора = @НДоговора)";
            ComboBoxDataForFill Организации = new ComboBoxDataForFill(sql, "Наименование", "НОрг");
            Организации.paramsForSQLQuery.Add(new MySqlParameter("@НДоговора", MySqlDbType.Int32) { Value = номерДоговора });
            LoadCombo(Организации, кбНОрг);

            int номерОрганизации = (int)кбНОрг.SelectedValue;

            // Загружаем связанные НГр
            sql = "SELECT НГр FROM Группа WHERE НОрг = @НОрг";
            ComboBoxDataForFill Группы = new ComboBoxDataForFill(sql, "НГр", "НГр");
            Группы.paramsForSQLQuery.Add(new MySqlParameter("@НОрг", MySqlDbType.Int32) { Value = номерОрганизации });
            LoadCombo(Группы, кбНГр);

            sql = "SELECT НС, ФИО FROM Сотрудник WHERE НС IN (SELECT НС FROM ЗаселениеГруппы WHERE НЗаселенияГруппы = @НЗаселенияГруппы)";
            ComboBoxDataForFill Сотрудник = new ComboBoxDataForFill(sql, "ФИО", "НС");
            Сотрудник.paramsForSQLQuery.Add(new MySqlParameter("@НЗаселенияГруппы", MySqlDbType.Int32) { Value = номерЗаселения });
            LoadCombo(Сотрудник, кбНС);

            isDataLoadingNow = true;
            SetControlsFromDataRow(номерЗаселения, номерSource, controlsMapping);
            isDataLoadingNow = false;
        }

        string previousItem = String.Empty;

        private void кбВыборСтатуса_SelectedIndexChanged(object sender, EventArgs e)
        {
            sql = $"SELECT * FROM ЗаселениеГруппы WHERE Статус = @Статус";
            ComboBoxDataForFill НомерЗаселения = new ComboBoxDataForFill(sql, "НЗаселенияГруппы", "НЗаселенияГруппы");
            НомерЗаселения.paramsForSQLQuery.Add(new MySqlParameter("@Статус", MySqlDbType.VarChar, 255) { Value = кбВыборСтатуса.Text });
            LoadCombo(НомерЗаселения, кбНЗаселенияГруппы);

            if (кбНЗаселенияГруппы.SelectedValue != null)
            {
                номерЗаселения = (int)кбНЗаселенияГруппы.SelectedValue;
                номерSource = кбНЗаселенияГруппы.DataSource as DataTable;

                // Загружаем связанные НГр
                sql = "SELECT * FROM ДоговорСОрганизацией WHERE НДоговора IN (SELECT НДоговора FROM ЗаселениеГруппы WHERE НЗаселенияГруппы = @НЗаселенияГруппы)";
                ComboBoxDataForFill Договор = new ComboBoxDataForFill(sql, "НДоговора", "НДоговора");
                Договор.paramsForSQLQuery.Add(new MySqlParameter("@НЗаселенияГруппы", MySqlDbType.Int32) { Value = номерЗаселения });
                LoadCombo(Договор, кбНДоговора);

                int номерДоговора = (int)кбНДоговора.SelectedValue;

                // Загружаем связанные НОрг
                sql = "SELECT НОрг, Наименование FROM Организация WHERE НОрг IN (SELECT НОрг FROM ДоговорСОрганизацией WHERE НДоговора = @НДоговора)";
                ComboBoxDataForFill Организации = new ComboBoxDataForFill(sql, "Наименование", "НОрг");
                Организации.paramsForSQLQuery.Add(new MySqlParameter("@НДоговора", MySqlDbType.Int32) { Value = номерДоговора });
                LoadCombo(Организации, кбНОрг);

                int номерОрганизации = (int)кбНОрг.SelectedValue;

                // Загружаем связанные НГр
                sql = "SELECT НГр FROM Группа WHERE НОрг = @НОрг";
                ComboBoxDataForFill Группы = new ComboBoxDataForFill(sql, "НГр", "НГр");
                Группы.paramsForSQLQuery.Add(new MySqlParameter("@НОрг", MySqlDbType.Int32) { Value = номерОрганизации });
                LoadCombo(Группы, кбНГр);

                sql = "SELECT НС, ФИО FROM Сотрудник WHERE НС IN (SELECT НС FROM ЗаселениеГруппы WHERE НЗаселенияГруппы = @НЗаселенияГруппы)";
                ComboBoxDataForFill Сотрудник = new ComboBoxDataForFill(sql, "ФИО", "НС");
                Сотрудник.paramsForSQLQuery.Add(new MySqlParameter("@НЗаселенияГруппы", MySqlDbType.Int32) { Value = номерЗаселения });
                LoadCombo(Сотрудник, кбНС);

                isDataLoadingNow = true;
                SetControlsFromDataRow(номерЗаселения, номерSource, controlsMapping);
                isDataLoadingNow = false;
            }
            else
            {
                MessageBox.Show("По выбранному фильтру данные не найдены", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                НомерЗаселения.paramsForSQLQuery.Clear();
                НомерЗаселения.paramsForSQLQuery.Add(new MySqlParameter("@Статус", MySqlDbType.VarChar, 255) { Value = previousItem });
                LoadCombo(НомерЗаселения, кбНЗаселенияГруппы);
                кбВыборСтатуса.Text = previousItem;
            }
        }

        private void кбВыборСтатуса_SelectionChangeCommitted(object sender, EventArgs e)
        {
            previousItem = кбВыборСтатуса.Text;
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                номерЗаселения = (int)кбНЗаселенияГруппы.SelectedValue;

                DateTime date1 = тбДатаЗаселения.Value.Date;
                DateTime date2 = тбДатаВыезда.Value.Date;

                if (date2 < date1)
                {
                    MessageBox.Show("Ошибка: дата заселения позже даты выезда", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    ZGroup current = new ZGroup(номерЗаселения, (int)кбНДоговора.SelectedValue, (int)кбНОрг.SelectedValue, (int)кбНГр.SelectedValue, (int)кбНС.SelectedValue,
                                            Convert.ToDateTime(тбДатаОплаты.Text), Convert.ToDateTime(тбДатаЗаселения.Text),
                                            Convert.ToDateTime(тбДатаВыезда.Text), float.Parse(тбСтоимостьОплаты.Text), кбСтатус.Text);

                    Result<int> result;
                    result = await _repo.Update(current, номерЗаселения);

                    if (result)
                    {
                        MessageBox.Show($"Сведения заселения №{номерЗаселения} группы №{кбНГр.Text} успешно изменены!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        кбВыборСтатуса.Text = кбСтатус.Text;
                        кбВыборСтатуса_SelectedIndexChanged(sender, e);
                    }
                    if (!result)
                    {
                        MessageBox.Show("Ошибка обновления данных: " + result.Error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Вы не заполнили все поля формы!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
