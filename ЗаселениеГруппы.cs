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
        int номерЗаселения_КвЗГ = 0;
        Dictionary<string, Control> controlsMapping_КвЗГ;
        DataTable номерSource_КвЗГ;

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

            sql = "SELECT Название, НГ FROM `ГостиничныйКомплекс`";
            ComboBoxDataForFill Гостиница = new ComboBoxDataForFill(sql, "Название", "НГ");
            LoadCombo(Гостиница, кбНГ);

            int НГ = (int)кбНГ.SelectedValue;

            sql = "SELECT * FROM `Корпус` WHERE НГ=@НГ";
            ComboBoxDataForFill Корпус = new ComboBoxDataForFill(sql, "НК", "НК");
            Корпус.paramsForSQLQuery.Add(new MySqlParameter("@НГ", MySqlDbType.Int32) { Value = НГ });
            LoadCombo(Корпус, кбНК);

            //int НК = Convert.ToInt32(кбНК.Text);
            int НК = (int)кбНК.SelectedValue;
            sql = "SELECT * FROM `ЭтажиИКорпусы` WHERE НК=@НК";
            ComboBoxDataForFill Этаж = new ComboBoxDataForFill(sql, "НЭ", "НЭ");
            Этаж.paramsForSQLQuery.Add(new MySqlParameter("@НК", MySqlDbType.Int32) { Value = НК });
            LoadCombo(Этаж, кбНЭ);

            sql = "SELECT DISTINCT Вместимость FROM `Комната`";
            ComboBoxDataForFill Вместимость = new ComboBoxDataForFill(sql, "Вместимость", "Вместимость");
            LoadCombo(Вместимость, кбВместимостьКомнаты);

            int НЭ = (int)кбНЭ.SelectedValue;
            int ВместимостьКомнаты = (int)кбВместимостьКомнаты.SelectedValue;
            sql = "SELECT * FROM `Комната` WHERE НК=@НК AND НЭ=@НЭ AND Вместимость=@Вместимость AND Доступность='Да'";
            ComboBoxDataForFill Комната = new ComboBoxDataForFill(sql, "НКомнаты", "НЭ");
            Комната.paramsForSQLQuery.Add(new MySqlParameter("@НК", MySqlDbType.Int32) { Value = НК });
            Комната.paramsForSQLQuery.Add(new MySqlParameter("@НЭ", MySqlDbType.Int32) { Value = НЭ });
            Комната.paramsForSQLQuery.Add(new MySqlParameter("@Вместимость", MySqlDbType.Int32) { Value = ВместимостьКомнаты });
            LoadCombo(Комната, кбНКомнаты);

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

                    sql = $"SELECT * FROM КомнатыВЗаселенииГруппы";
                    ComboBoxDataForFill НомерЗаселения_КвЗГ = new ComboBoxDataForFill(sql, "НЗаселенияГруппы", "НЗаселенияГруппы");
                    LoadCombo(НомерЗаселения_КвЗГ, кбНЗаселенияГруппы);

                    if (кбНЗаселенияГруппы.SelectedValue != null)
                    {
                        номерЗаселения_КвЗГ = (int)кбНЗаселенияГруппы.SelectedValue;
                        номерSource_КвЗГ = кбНЗаселенияГруппы.DataSource as DataTable;
                        controlsMapping_КвЗГ = new Dictionary<string, Control>
                        {
                            { "НГ", кбНДоговора },
                            { "НК", кбНОрг },
                            { "НЭ", кбНГр },
                            { "НКомнаты", кбНС }
                        };

                        SetControlsFromDataRow(номерЗаселения_КвЗГ, номерSource_КвЗГ, controlsMapping_КвЗГ);

                        try
                        {
                            // Получение выбранного значения
                            // Запрос данных из базы данных
                            var data = await _repo.GetZaselenieDetails(номерЗаселения);
                            // Привязка данных к DataGridView
                            dataGridView1.DataSource = data;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Комнаты в заселении №{кбНЗаселенияГруппы.Text} отсутствуют. Данные не были загружены в таблицу", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        НомерЗаселения.sql = $"SELECT * FROM ЗаселениеГруппы";
                        LoadCombo(НомерЗаселения, кбНЗаселенияГруппы);
                    }
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

        private async void кбНЗаселенияГруппы_SelectionChangeCommitted(object sender, EventArgs e)
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

            try
            {
                // Получение выбранного значения
                // Запрос данных из базы данных
                var data = await _repo.GetZaselenieDetails(номерЗаселения);
                // Привязка данных к DataGridView
                dataGridView1.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

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

        private void кбНГ_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int НГ = (int)кбНГ.SelectedValue;

            sql = "SELECT * FROM `Корпус` WHERE НГ=@НГ";
            ComboBoxDataForFill Корпус = new ComboBoxDataForFill(sql, "НК", "НК");
            Корпус.paramsForSQLQuery.Add(new MySqlParameter("@НГ", MySqlDbType.Int32) { Value = НГ });
            LoadCombo(Корпус, кбНК);

            //int НК = Convert.ToInt32(кбНК.Text);
            int НК = (int)кбНК.SelectedValue;
            sql = "SELECT * FROM `ЭтажиИКорпусы` WHERE НК=@НК";
            ComboBoxDataForFill Этаж = new ComboBoxDataForFill(sql, "НЭ", "НЭ");
            Этаж.paramsForSQLQuery.Add(new MySqlParameter("@НК", MySqlDbType.Int32) { Value = НК });
            LoadCombo(Этаж, кбНЭ);

            sql = "SELECT DISTINCT Вместимость FROM `Комната`";
            ComboBoxDataForFill Вместимость = new ComboBoxDataForFill(sql, "Вместимость", "Вместимость");
            LoadCombo(Вместимость, кбВместимостьКомнаты);

            int НЭ = (int)кбНЭ.SelectedValue;
            int ВместимостьКомнаты = (int)кбВместимостьКомнаты.SelectedValue;
            sql = "SELECT * FROM `Комната` WHERE НК=@НК AND НЭ=@НЭ AND Вместимость=@Вместимость AND Доступность='Да'";
            ComboBoxDataForFill Комната = new ComboBoxDataForFill(sql, "НКомнаты", "НЭ");
            Комната.paramsForSQLQuery.Add(new MySqlParameter("@НК", MySqlDbType.Int32) { Value = НК });
            Комната.paramsForSQLQuery.Add(new MySqlParameter("@НЭ", MySqlDbType.Int32) { Value = НЭ });
            Комната.paramsForSQLQuery.Add(new MySqlParameter("@Вместимость", MySqlDbType.Int32) { Value = ВместимостьКомнаты });
            LoadCombo(Комната, кбНКомнаты);

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

        private void кбНК_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int? НК = (int?)(кбНК.SelectedValue ?? 0);

            sql = "SELECT * FROM `ЭтажиИКорпусы` WHERE НК=@НК";
            ComboBoxDataForFill Этаж = new ComboBoxDataForFill(sql, "НЭ", "НЭ");
            Этаж.paramsForSQLQuery.Add(new MySqlParameter("@НК", MySqlDbType.Int32) { Value = НК });
            LoadCombo(Этаж, кбНЭ);

            sql = "SELECT DISTINCT Вместимость FROM `Комната`";
            ComboBoxDataForFill Вместимость = new ComboBoxDataForFill(sql, "Вместимость", "Вместимость");
            LoadCombo(Вместимость, кбВместимостьКомнаты);

            int НЭ = (int)кбНЭ.SelectedValue;
            int ВместимостьКомнаты = (int)кбВместимостьКомнаты.SelectedValue;
            sql = "SELECT * FROM `Комната` WHERE НК=@НК AND НЭ=@НЭ AND Вместимость=@Вместимость AND Доступность='Да'";
            ComboBoxDataForFill Комната = new ComboBoxDataForFill(sql, "НКомнаты", "НЭ");
            Комната.paramsForSQLQuery.Add(new MySqlParameter("@НК", MySqlDbType.Int32) { Value = НК });
            Комната.paramsForSQLQuery.Add(new MySqlParameter("@НЭ", MySqlDbType.Int32) { Value = НЭ });
            Комната.paramsForSQLQuery.Add(new MySqlParameter("@Вместимость", MySqlDbType.Int32) { Value = ВместимостьКомнаты });
            LoadCombo(Комната, кбНКомнаты);

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
            LoadCombo(Вместимость, кбВместимостьКомнаты);

            int ВместимостьКомнаты = (int)кбВместимостьКомнаты.SelectedValue;
            sql = "SELECT * FROM `Комната` WHERE НК=@НК AND НЭ=@НЭ AND Вместимость=@Вместимость AND Доступность='Да'";
            ComboBoxDataForFill Комната = new ComboBoxDataForFill(sql, "НКомнаты", "НЭ");
            Комната.paramsForSQLQuery.Add(new MySqlParameter("@НК", MySqlDbType.Int32) { Value = НК });
            Комната.paramsForSQLQuery.Add(new MySqlParameter("@НЭ", MySqlDbType.Int32) { Value = НЭ });
            Комната.paramsForSQLQuery.Add(new MySqlParameter("@Вместимость", MySqlDbType.Int32) { Value = ВместимостьКомнаты });
            LoadCombo(Комната, кбНКомнаты);

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
            LoadCombo(Комната, кбНКомнаты);

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

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
