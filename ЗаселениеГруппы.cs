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
using System.Reflection;
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

        private IZGroupRoom _repo2;
        int номерЗаселения_КвЗГ = 0;
        Dictionary<string, Control> controlsMapping_КвЗГ;
        DataTable номерSource_КвЗГ;

        int selectedValue;
        Dictionary<string, Control> controlsMapping;
        DataTable номерSource;

        int index = 0;

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
            _repo2 = new RZGroupRoom();

            checkBox1.Checked = true;
            кбНЗГ.Visible = false;
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
            SetupDataGridView();

            sql = "SELECT Название, НГ FROM `ГостиничныйКомплекс`";
            ComboBoxDataForFill Гостиница = new ComboBoxDataForFill(sql, "Название", "НГ");
            LoadCombo(Гостиница, кбНГ);

            int? НГ = (int?)(кбНГ.SelectedValue ?? 0);

            if (НГ == 0)
            {
                MessageBox.Show("Данные для гостиницы отсутствуют. Продолжение невозможно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Прекращаем выполнение
            }

            sql = "SELECT * FROM `Корпус` WHERE НГ=@НГ";
            ComboBoxDataForFill Корпус = new ComboBoxDataForFill(sql, "НК", "НК");
            Корпус.paramsForSQLQuery.Add(new MySqlParameter("@НГ", MySqlDbType.Int32) { Value = НГ });
            LoadCombo(Корпус, кбНК);

            int? НК = (int?)(кбНК.SelectedValue ?? 0);

            if (НК == 0)
            {
                MessageBox.Show("Данные для корпуса отсутствуют. Продолжение невозможно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Прекращаем выполнение
            }

            sql = "SELECT * FROM `ЭтажиИКорпусы` WHERE НК=@НК";
            ComboBoxDataForFill Этаж = new ComboBoxDataForFill(sql, "НЭ", "НЭ");
            Этаж.paramsForSQLQuery.Add(new MySqlParameter("@НК", MySqlDbType.Int32) { Value = НК });
            LoadCombo(Этаж, кбНЭ);

            sql = "SELECT DISTINCT Вместимость FROM `Комната`";
            ComboBoxDataForFill Вместимость = new ComboBoxDataForFill(sql, "Вместимость", "Вместимость");
            LoadCombo(Вместимость, кбВместимостьКомнаты);

            int? НЭ = (int?)(кбНЭ.SelectedValue ?? 0);

            if (НЭ == 0)
            {
                MessageBox.Show("Данные для этажей отсутствуют. Продолжение невозможно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Прекращаем выполнение
            }

            int? ВместимостьКомнаты = ((int?)(кбВместимостьКомнаты.SelectedValue) ?? 0);

            if (ВместимостьКомнаты == 0)
            {
                MessageBox.Show("Данные для вместимости комнат отсутствуют. Продолжение невозможно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Прекращаем выполнение
            }


            sql = "SELECT * FROM `Комната` WHERE НК=@НК AND НЭ=@НЭ AND Вместимость=@Вместимость AND Доступность='Да'";
            ComboBoxDataForFill Комната = new ComboBoxDataForFill(sql, "НКомнаты", "НКомнаты");
            Комната.paramsForSQLQuery.Add(new MySqlParameter("@НК", MySqlDbType.Int32) { Value = НК });
            Комната.paramsForSQLQuery.Add(new MySqlParameter("@НЭ", MySqlDbType.Int32) { Value = НЭ });
            Комната.paramsForSQLQuery.Add(new MySqlParameter("@Вместимость", MySqlDbType.Int32) { Value = ВместимостьКомнаты });
            LoadCombo(Комната, кбНКомнаты);

            int? НКомнаты = (int?)(кбНКомнаты.SelectedValue ?? 0);

            if (НКомнаты == 0)
            {
                MessageBox.Show("Данные для комнат отсутствуют. Продолжение невозможно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Прекращаем выполнение
            }

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

                //нижняя половина формы
                dataGridView1.Enabled = false;
                кбНГ.Enabled = false;
                кбНК.Enabled = false;
                кбНЭ.Enabled = false;
                кбНКомнаты.Enabled = false;
                кбВместимостьКомнаты.Enabled = false;
                tbRoomPrice.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;

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

                //нижняя половина формы
                dataGridView1.Enabled = true;
                кбНГ.Enabled = true;
                кбНК.Enabled = true;
                кбНЭ.Enabled = true;
                кбНКомнаты.Enabled = true;
                кбВместимостьКомнаты.Enabled = true;
                tbRoomPrice.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;

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

                    SetControlsFromDataRow(номерЗаселения, номерSource, controlsMapping);

                    // Работаем со второй частью формы - комнаты в заселении группы
                    sql = $"SELECT * FROM КомнатыВЗаселенииГруппы";
                    ComboBoxDataForFill НомерЗаселения_КвЗГ = new ComboBoxDataForFill(sql, "НЗаселенияГруппы", "НЗаселенияГруппы");
                    LoadCombo(НомерЗаселения_КвЗГ, кбНЗГ);

                    if (кбНЗГ.SelectedValue != null)
                    {
                        bool найденоСоответствие = false;
                        foreach (var item in кбНЗГ.Items)
                        {
                            DataRowView row = item as DataRowView;
                            if (row != null && row["НЗаселенияГруппы"].ToString() == кбНЗаселенияГруппы.SelectedValue.ToString())
                            {
                                найденоСоответствие = true;
                                break;
                            }
                        }

                        if (найденоСоответствие)
                        {
                            номерЗаселения_КвЗГ = (int)кбНЗаселенияГруппы.SelectedValue;
                            номерSource_КвЗГ = кбНЗГ.DataSource as DataTable;
                            controlsMapping_КвЗГ = new Dictionary<string, Control>
                            {
                                { "НГ", кбНГ },
                                { "НК", кбНК },
                                { "НЭ", кбНЭ },
                                { "НКомнаты", кбНКомнаты }
                            };

                            LoadDataIntoDataGridView();
                        }
                        else
                        {
                            MessageBox.Show($"Комнаты в этом заселении группы отсутствуют. Данные не были загружены в таблицу", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Комнаты в этом заселении группы отсутствуют. Данные не были загружены в таблицу", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        sql = $"SELECT * FROM ЗаселениеГруппы";
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


            SetControlsFromDataRow(номерДоговора, номерSource, controlsMapping);

        }

        private async void кбНЗаселенияГруппы_SelectionChangeCommitted(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
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

            SetControlsFromDataRow(номерЗаселения, номерSource, controlsMapping);

            //Работаем со второй частью формы 

            sql = $"SELECT * FROM КомнатыВЗаселенииГруппы";
            ComboBoxDataForFill НомерЗаселения_КвЗГ = new ComboBoxDataForFill(sql, "НЗаселенияГруппы", "НЗаселенияГруппы");
            LoadCombo(НомерЗаселения_КвЗГ, кбНЗГ);

            if (кбНЗГ.SelectedValue != null)
            {
                bool найденоСоответствие = false;
                foreach (var item in кбНЗГ.Items)
                {
                    DataRowView row = item as DataRowView;
                    if (row != null && row["НЗаселенияГруппы"].ToString() == кбНЗаселенияГруппы.SelectedValue.ToString())
                    {
                        найденоСоответствие = true;
                        break;
                    }
                }

                if (найденоСоответствие)
                {
                    номерЗаселения_КвЗГ = (int)кбНЗаселенияГруппы.SelectedValue;
                    номерSource_КвЗГ = кбНЗГ.DataSource as DataTable;
                    controlsMapping_КвЗГ = new Dictionary<string, Control>
                    {
                        { "НГ", кбНГ },
                        { "НК", кбНК },
                        { "НЭ", кбНЭ },
                        { "НКомнаты", кбНКомнаты }
                    };

                    LoadDataIntoDataGridView();
                }
                else
                {
                    MessageBox.Show($"Комнаты в этом заселении группы отсутствуют. Данные не были загружены в таблицу", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show($"Комнаты в этом заселении группы отсутствуют. Данные не были загружены в таблицу", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                sql = $"SELECT * FROM ЗаселениеГруппы";
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

                SetControlsFromDataRow(номерЗаселения, номерSource, controlsMapping);

                sql = $"SELECT * FROM КомнатыВЗаселенииГруппы";
                ComboBoxDataForFill НомерЗаселения_КвЗГ = new ComboBoxDataForFill(sql, "НЗаселенияГруппы", "НЗаселенияГруппы");
                LoadCombo(НомерЗаселения_КвЗГ, кбНЗГ);

                if (кбНЗГ.SelectedValue != null)
                {
                    bool найденоСоответствие = false;
                    foreach (var item in кбНЗГ.Items)
                    {
                        DataRowView row = item as DataRowView;
                        if (row != null && row["НЗаселенияГруппы"].ToString() == кбНЗаселенияГруппы.SelectedValue.ToString())
                        {
                            найденоСоответствие = true;
                            break;
                        }
                    }

                    if (найденоСоответствие)
                    {
                        номерЗаселения_КвЗГ = (int)кбНЗаселенияГруппы.SelectedValue;
                        номерSource_КвЗГ = кбНЗГ.DataSource as DataTable;
                        controlsMapping_КвЗГ = new Dictionary<string, Control>
                    {
                        { "НГ", кбНГ },
                        { "НК", кбНК },
                        { "НЭ", кбНЭ },
                        { "НКомнаты", кбНКомнаты }
                    };

                        LoadDataIntoDataGridView();
                    }
                    else
                    {
                        MessageBox.Show($"Комнаты в этом заселении группы отсутствуют. Данные не были загружены в таблицу", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show($"Комнаты в этом заселении группы отсутствуют. Данные не были загружены в таблицу", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    sql = $"SELECT * FROM ЗаселениеГруппы";
                }
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
            dataGridView1.DataSource = null;
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
            int? НГ = (int?)(кбНГ.SelectedValue ?? 0);

            if (НГ == 0)
            {
                MessageBox.Show("Данные для гостиницы отсутствуют. Продолжение невозможно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Прекращаем выполнение
            }

            sql = "SELECT * FROM `Корпус` WHERE НГ=@НГ";
            ComboBoxDataForFill Корпус = new ComboBoxDataForFill(sql, "НК", "НК");
            Корпус.paramsForSQLQuery.Add(new MySqlParameter("@НГ", MySqlDbType.Int32) { Value = НГ });
            LoadCombo(Корпус, кбНК);

            int? НК = (int?)(кбНК.SelectedValue ?? 0);

            if (НК == 0)
            {
                MessageBox.Show("Данные для корпуса отсутствуют. Продолжение невозможно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Прекращаем выполнение
            }

            sql = "SELECT * FROM `ЭтажиИКорпусы` WHERE НК=@НК";
            ComboBoxDataForFill Этаж = new ComboBoxDataForFill(sql, "НЭ", "НЭ");
            Этаж.paramsForSQLQuery.Add(new MySqlParameter("@НК", MySqlDbType.Int32) { Value = НК });
            LoadCombo(Этаж, кбНЭ);

            sql = "SELECT DISTINCT Вместимость FROM `Комната`";
            ComboBoxDataForFill Вместимость = new ComboBoxDataForFill(sql, "Вместимость", "Вместимость");
            LoadCombo(Вместимость, кбВместимостьКомнаты);

            int? НЭ = (int?)(кбНЭ.SelectedValue ?? 0);

            if (НЭ == 0)
            {
                MessageBox.Show("Данные для этажей отсутствуют. Продолжение невозможно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Прекращаем выполнение
            }

            int? ВместимостьКомнаты = (int?)(кбВместимостьКомнаты.SelectedValue ?? 0);

            if (ВместимостьКомнаты == 0)
            {
                MessageBox.Show("Данные для вместимости комнат отсутствуют. Продолжение невозможно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Прекращаем выполнение
            }

            sql = "SELECT * FROM `Комната` WHERE НК=@НК AND НЭ=@НЭ AND Вместимость=@Вместимость AND Доступность='Да'";
            ComboBoxDataForFill Комната = new ComboBoxDataForFill(sql, "НКомнаты", "НКомнаты");
            Комната.paramsForSQLQuery.Add(new MySqlParameter("@НК", MySqlDbType.Int32) { Value = НК });
            Комната.paramsForSQLQuery.Add(new MySqlParameter("@НЭ", MySqlDbType.Int32) { Value = НЭ });
            Комната.paramsForSQLQuery.Add(new MySqlParameter("@Вместимость", MySqlDbType.Int32) { Value = ВместимостьКомнаты });
            LoadCombo(Комната, кбНКомнаты);

            int? НКомнаты = (int?)(кбНКомнаты.SelectedValue ?? 0);

            if (НКомнаты == 0)
            {
                MessageBox.Show("Данные для комнат отсутствуют. Продолжение невозможно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Прекращаем выполнение
            }

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

            if (НК == 0)
            {
                MessageBox.Show("Данные для корпуса отсутствуют. Продолжение невозможно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Прекращаем выполнение
            }

            sql = "SELECT * FROM `ЭтажиИКорпусы` WHERE НК=@НК";
            ComboBoxDataForFill Этаж = new ComboBoxDataForFill(sql, "НЭ", "НЭ");
            Этаж.paramsForSQLQuery.Add(new MySqlParameter("@НК", MySqlDbType.Int32) { Value = НК });
            LoadCombo(Этаж, кбНЭ);

            sql = "SELECT DISTINCT Вместимость FROM `Комната`";
            ComboBoxDataForFill Вместимость = new ComboBoxDataForFill(sql, "Вместимость", "Вместимость");
            LoadCombo(Вместимость, кбВместимостьКомнаты);

            int? НЭ = (int?)(кбНЭ.SelectedValue ?? 0);

            if (НЭ == 0)
            {
                MessageBox.Show("Данные для этажей отсутствуют. Продолжение невозможно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Прекращаем выполнение
            }

            int? ВместимостьКомнаты = (int?)(кбВместимостьКомнаты.SelectedValue ?? 0);

            if (ВместимостьКомнаты == 0)
            {
                MessageBox.Show("Данные для вместимости комнат отсутствуют. Продолжение невозможно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Прекращаем выполнение
            }

            sql = "SELECT * FROM `Комната` WHERE НК=@НК AND НЭ=@НЭ AND Вместимость=@Вместимость AND Доступность='Да'";
            ComboBoxDataForFill Комната = new ComboBoxDataForFill(sql, "НКомнаты", "НКомнаты");
            Комната.paramsForSQLQuery.Add(new MySqlParameter("@НК", MySqlDbType.Int32) { Value = НК });
            Комната.paramsForSQLQuery.Add(new MySqlParameter("@НЭ", MySqlDbType.Int32) { Value = НЭ });
            Комната.paramsForSQLQuery.Add(new MySqlParameter("@Вместимость", MySqlDbType.Int32) { Value = ВместимостьКомнаты });
            LoadCombo(Комната, кбНКомнаты);

            int? НКомнаты = (int?)(кбНКомнаты.SelectedValue ?? 0);

            if (НКомнаты == 0)
            {
                MessageBox.Show("Данные для комнат отсутствуют. Продолжение невозможно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Прекращаем выполнение
            }

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

            if (НК == 0)
            {
                MessageBox.Show("Данные для корпуса отсутствуют. Продолжение невозможно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Прекращаем выполнение
            }

            int? НЭ = (int?)(кбНЭ.SelectedValue ?? 0);

            if (НЭ == 0)
            {
                MessageBox.Show("Данные для этажей отсутствуют. Продолжение невозможно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Прекращаем выполнение
            }

            sql = "SELECT DISTINCT Вместимость FROM `Комната`";
            ComboBoxDataForFill Вместимость = new ComboBoxDataForFill(sql, "Вместимость", "Вместимость");
            LoadCombo(Вместимость, кбВместимостьКомнаты);

            int? ВместимостьКомнаты = (int?)(кбВместимостьКомнаты.SelectedValue ?? 0);

            if (ВместимостьКомнаты == 0)
            {
                MessageBox.Show("Данные для вместимости комнат отсутствуют. Продолжение невозможно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Прекращаем выполнение
            }

            sql = "SELECT * FROM `Комната` WHERE НК=@НК AND НЭ=@НЭ AND Вместимость=@Вместимость AND Доступность='Да'";
            ComboBoxDataForFill Комната = new ComboBoxDataForFill(sql, "НКомнаты", "НКомнаты");
            Комната.paramsForSQLQuery.Add(new MySqlParameter("@НК", MySqlDbType.Int32) { Value = НК });
            Комната.paramsForSQLQuery.Add(new MySqlParameter("@НЭ", MySqlDbType.Int32) { Value = НЭ });
            Комната.paramsForSQLQuery.Add(new MySqlParameter("@Вместимость", MySqlDbType.Int32) { Value = ВместимостьКомнаты });
            LoadCombo(Комната, кбНКомнаты);

            int? НКомнаты = (int?)(кбНКомнаты.SelectedValue ?? 0);

            if (НКомнаты == 0)
            {
                MessageBox.Show("Данные для комнат отсутствуют. Продолжение невозможно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Прекращаем выполнение
            }

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

            if (НК == 0)
            {
                MessageBox.Show("Данные для корпуса отсутствуют. Продолжение невозможно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Прекращаем выполнение
            }

            int? НЭ = (int?)(кбНЭ.SelectedValue ?? 0);

            if (НЭ == 0)
            {
                MessageBox.Show("Данные для этажей отсутствуют. Продолжение невозможно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Прекращаем выполнение
            }

            int? ВместимостьКомнаты = (int?)(кбВместимостьКомнаты.SelectedValue ?? 0);

            if (ВместимостьКомнаты == 0)
            {
                MessageBox.Show("Данные для вместимости комнат отсутствуют. Продолжение невозможно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Прекращаем выполнение
            }

            sql = "SELECT * FROM `Комната` WHERE НК=@НК AND НЭ=@НЭ AND Вместимость=@Вместимость AND Доступность='Да'";
            ComboBoxDataForFill Комната = new ComboBoxDataForFill(sql, "НКомнаты", "НЭ");
            Комната.paramsForSQLQuery.Add(new MySqlParameter("@НК", MySqlDbType.Int32) { Value = НК });
            Комната.paramsForSQLQuery.Add(new MySqlParameter("@НЭ", MySqlDbType.Int32) { Value = НЭ });
            Комната.paramsForSQLQuery.Add(new MySqlParameter("@Вместимость", MySqlDbType.Int32) { Value = ВместимостьКомнаты });
            LoadCombo(Комната, кбНКомнаты);

            int? НКомнаты = (int?)(кбНКомнаты.SelectedValue ?? 0);

            if (НКомнаты == 0)
            {
                MessageBox.Show("Данные для комнат отсутствуют. Продолжение невозможно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Прекращаем выполнение
            }

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

        private async void button3_Click(object sender, EventArgs e)
        {
            try
            {

                if (кбНЗаселенияГруппы.SelectedValue != null)
                {
                    кбНЗГ.SelectedValue = кбНЗаселенияГруппы.SelectedValue;
                    номерЗаселения_КвЗГ = (int)кбНЗаселенияГруппы.SelectedValue;
                    ZGroupRoom current = new ZGroupRoom((int)кбНЗаселенияГруппы.SelectedValue, (int)кбНГ.SelectedValue, Convert.ToInt32(кбНК.SelectedValue),
                                                    Convert.ToInt32(кбНЭ.SelectedValue), Convert.ToInt32(кбНКомнаты.Text));

                    Result<int> result;
                    result = await _repo2.Add(current);

                    if (result)
                    {
                        MessageBox.Show($"Комната №{кбНКомнаты.Text} добавлена\n" +
                            $"в заселение № {кбНЗаселенияГруппы.SelectedValue} группы №{кбНГр.Text}\n", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //обновление выводимой итоговой стоимости заказа
                        UpdateComboBoxes();

                        //перерисовка таблицы
                        try
                        {
                            var data = await _repo2.GetByRequest((int)кбНЗаселенияГруппы.SelectedValue);
                            dataGridView1.DataSource = data;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    if (!result)
                    {
                        MessageBox.Show($"Ошибка бронирования комнаты: " + result.Error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка: сначала создайте запись заселения группы перед бронированием комнат!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("Вы не заполнили все поля формы!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (кбНЗГ.SelectedValue != null)
                {

                    ZGroupRoom current = new ZGroupRoom((int)кбНЗаселенияГруппы.SelectedValue, (int)кбНГ.SelectedValue, Convert.ToInt32(кбНК.SelectedValue),
                                                                            Convert.ToInt32(кбНЭ.SelectedValue), Convert.ToInt32(кбНКомнаты.Text));

                    Result<int> result;
                    DataGridViewRow selectedRow = dataGridView1.CurrentRow;
                    int НЗГ_dgv = (int)selectedRow.Cells["НЗаселенияГруппы"].Value;
                    int НГ_dgv = (int)selectedRow.Cells["НГ"].Value;
                    int НК_dgv = (int)selectedRow.Cells["НК"].Value;
                    int НЭ_dgv = (int)selectedRow.Cells["НЭ"].Value;
                    int НКомнаты_dgv = (int)selectedRow.Cells["НКомнаты"].Value;

                    result = await _repo2.Update(current, НЗГ_dgv, НГ_dgv, НК_dgv, НЭ_dgv, НКомнаты_dgv);

                    if (result)
                    {
                        MessageBox.Show($"Комната №{кбНКомнаты.Text} в заселении №{кбНЗаселенияГруппы.SelectedValue} группы №{кбНГр.Text} успешно изменена!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //обновление выводимой итоговой стоимости заказа
                        UpdateComboBoxes();

                        //перерисовка таблицы
                        try
                        {
                            var data = await _repo2.GetByRequest(номерЗаселения_КвЗГ);
                            dataGridView1.DataSource = data;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    if (!result)
                    {
                        MessageBox.Show("Ошибка обновления данных: " + result.Error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {
                    MessageBox.Show("Ошибка: отсутствует запись о бронировании комнаты по номеру заселения группы!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("Вы не заполнили все поля формы!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (кбНЗГ.SelectedValue != null)
                {
                    ZGroupRoom current = new ZGroupRoom((int)кбНЗаселенияГруппы.SelectedValue, (int)кбНГ.SelectedValue, Convert.ToInt32(кбНК.SelectedValue),
                                                        Convert.ToInt32(кбНЭ.SelectedValue), Convert.ToInt32(кбНКомнаты.Text));

                    Result<int> result;
                    result = await _repo2.Remove(current);

                    if (result)
                    {
                        MessageBox.Show($"Комната №{кбНКомнаты.Text} из заселения №{кбНЗаселенияГруппы.SelectedValue} группы №{кбНГр.SelectedValue}\nуспешно убрана!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //обновление выводимой итоговой стоимости заказа
                        UpdateComboBoxes();

                        //перерисовка таблицы
                        try
                        {
                            var data = await _repo2.GetByRequest(номерЗаселения_КвЗГ);
                            dataGridView1.DataSource = data;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    if (!result)
                    {
                        MessageBox.Show(result.Error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка: отсутствует запись о бронировании комнаты по номеру заселения группы!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("Вы не заполнили все поля формы!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        async void LoadDataIntoDataGridView()
        {
            sql = "SELECT * FROM `Корпус`";
            ComboBoxDataForFill Корпус = new ComboBoxDataForFill(sql, "НК", "НК");
            LoadCombo(Корпус, кбНК);

            sql = "SELECT * FROM `ЭтажиИКорпусы`";
            ComboBoxDataForFill Этаж = new ComboBoxDataForFill(sql, "НЭ", "НЭ");
            LoadCombo(Этаж, кбНЭ);

            sql = "SELECT * FROM `Комната`";
            ComboBoxDataForFill Комната = new ComboBoxDataForFill(sql, "НКомнаты", "НКомнаты");
            LoadCombo(Комната, кбНКомнаты);

            sql = "SELECT DISTINCT НКомнаты, Вместимость FROM `Комната`";
            ComboBoxDataForFill Вместимость = new ComboBoxDataForFill(sql, "Вместимость", "НКомнаты");
            LoadCombo(Вместимость, кбВместимостьКомнаты);

            SetControlsFromDataRow(номерЗаселения_КвЗГ, номерSource_КвЗГ, controlsMapping_КвЗГ);


            int? НГ = (int?)(кбНГ.SelectedValue ?? 0);

            if (НГ == 0)
            {
                MessageBox.Show("Данные для гостиницы отсутствуют. Продолжение невозможно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Прекращаем выполнение
            }

            int? НК = (int?)(кбНК.SelectedValue ?? 0);

            if (НК == 0)
            {
                MessageBox.Show("Данные для корпуса отсутствуют. Продолжение невозможно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Прекращаем выполнение
            }

            int? НЭ = (int?)(кбНЭ.SelectedValue ?? 0);

            if (НЭ == 0)
            {
                MessageBox.Show("Данные для этажей отсутствуют. Продолжение невозможно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Прекращаем выполнение
            }

            int? ВместимостьКомнаты = (int?)(кбВместимостьКомнаты.SelectedValue ?? 0);

            if (ВместимостьКомнаты == 0)
            {
                MessageBox.Show("Данные для вместимости комнат отсутствуют. Продолжение невозможно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Прекращаем выполнение
            }

            int? НКомнаты = (int?)(кбНКомнаты.SelectedValue ?? 0);

            if (НКомнаты == 0)
            {
                MessageBox.Show("Данные для комнат отсутствуют. Продолжение невозможно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Прекращаем выполнение
            }

            sql = "SELECT * FROM `Корпус` WHERE НГ=@НГ";
            Корпус.sql = sql;
            Корпус.paramsForSQLQuery.Add(new MySqlParameter("@НГ", MySqlDbType.Int32) { Value = НГ });
            LoadCombo(Корпус, кбНК);

            кбНК.SelectedValue = НК;

            НК = (int?)(кбНК.SelectedValue ?? 0);

            if (НК == 0)
            {
                MessageBox.Show("Данные для корпуса отсутствуют. Продолжение невозможно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Прекращаем выполнение
            }


            sql = "SELECT * FROM `ЭтажиИКорпусы` WHERE НК=@НК";
            Этаж.sql = sql;
            Этаж.paramsForSQLQuery.Add(new MySqlParameter("@НК", MySqlDbType.Int32) { Value = НК });
            LoadCombo(Этаж, кбНЭ);

            кбНЭ.SelectedValue = НЭ;

            НЭ = (int?)(кбНЭ.SelectedValue ?? 0);

            if (НЭ == 0)
            {
                MessageBox.Show("Данные для этажей отсутствуют. Продолжение невозможно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Прекращаем выполнение
            }

            sql = "SELECT * FROM `Комната` WHERE НК=@НК AND НЭ=@НЭ";
            Комната.sql = sql;
            Комната.paramsForSQLQuery.Add(new MySqlParameter("@НК", MySqlDbType.Int32) { Value = НК });
            Комната.paramsForSQLQuery.Add(new MySqlParameter("@НЭ", MySqlDbType.Int32) { Value = НЭ });
            LoadCombo(Комната, кбНКомнаты);

            кбНКомнаты.SelectedValue = НКомнаты;

            НКомнаты = (int?)(кбНКомнаты.SelectedValue ?? 0);

            if (НКомнаты == 0)
            {
                MessageBox.Show("Данные для комнат отсутствуют. Продолжение невозможно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Прекращаем выполнение
            }

            sql = "SELECT DISTINCT НКомнаты, Вместимость FROM `Комната` WHERE НК=@НК AND НЭ=@НЭ";
            Вместимость.sql = sql;
            Вместимость.paramsForSQLQuery.Add(new MySqlParameter("@НКомнаты", MySqlDbType.Int32) { Value = НКомнаты });
            Вместимость.paramsForSQLQuery.Add(new MySqlParameter("@НК", MySqlDbType.Int32) { Value = НК });
            Вместимость.paramsForSQLQuery.Add(new MySqlParameter("@НЭ", MySqlDbType.Int32) { Value = НЭ });
            LoadCombo(Вместимость, кбВместимостьКомнаты);

            кбВместимостьКомнаты.SelectedValue = НКомнаты;

            ВместимостьКомнаты = (int?)(кбВместимостьКомнаты.SelectedValue ?? 0);

            if (ВместимостьКомнаты == 0)
            {
                MessageBox.Show("Данные для вместимости комнат отсутствуют. Продолжение невозможно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Прекращаем выполнение
            }

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

            try
            {
                // Получение выбранного значения
                // Запрос данных из базы данных
                var data = await _repo2.GetByRequest(номерЗаселения_КвЗГ);
                // Привязка данных к DataGridView
                dataGridView1.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Убедимся, что клик произошёл не на заголовке столбца или строке
            if (e.RowIndex >= 0)
            {
                // Устанавливаем выделение всей строки
                dataGridView1.Rows[e.RowIndex].Selected = true;
            }
        }

        // Настройка DataGridView для выделения всей строки
        private void SetupDataGridView()
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false; // Опционально, если нужно выделять только одну строку
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                return;
            }

            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

            int? НГ = selectedRow.Cells["НГ"].Value as int?;

            if (НГ == null || НГ == 0)
            {
                MessageBox.Show("Данные для гостиницы отсутствуют. Продолжение невозможно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            кбНГ.SelectedValue = НГ;

            sql = "SELECT * FROM `Корпус` WHERE НГ=@НГ";
            ComboBoxDataForFill Корпус = new ComboBoxDataForFill(sql, "НК", "НК");
            Корпус.paramsForSQLQuery.Add(new MySqlParameter("@НГ", MySqlDbType.Int32) { Value = НГ });
            LoadCombo(Корпус, кбНК);

            int? НК = selectedRow.Cells["НК"].Value as int?;

            if (НК == null || НК == 0)
            {
                MessageBox.Show("Данные для корпуса отсутствуют. Продолжение невозможно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            кбНК.SelectedValue = НК;

            sql = "SELECT * FROM `ЭтажиИКорпусы` WHERE НК=@НК";
            ComboBoxDataForFill Этаж = new ComboBoxDataForFill(sql, "НЭ", "НЭ");
            Этаж.paramsForSQLQuery.Add(new MySqlParameter("@НК", MySqlDbType.Int32) { Value = НК });
            LoadCombo(Этаж, кбНЭ);

            sql = "SELECT DISTINCT НКомнаты, Вместимость FROM `Комната`";
            ComboBoxDataForFill Вместимость = new ComboBoxDataForFill(sql, "Вместимость", "НКомнаты");
            LoadCombo(Вместимость, кбВместимостьКомнаты);

            int? НЭ = selectedRow.Cells["НЭ"].Value as int?;

            if (НЭ == null || НЭ == 0)
            {
                MessageBox.Show("Данные для этажей отсутствуют. Продолжение невозможно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            кбНЭ.SelectedValue = НЭ;

            sql = "SELECT * FROM `Комната` WHERE НК=@НК AND НЭ=@НЭ";
            ComboBoxDataForFill Комната = new ComboBoxDataForFill(sql, "НКомнаты", "НКомнаты");
            Комната.paramsForSQLQuery.Add(new MySqlParameter("@НК", MySqlDbType.Int32) { Value = НК });
            Комната.paramsForSQLQuery.Add(new MySqlParameter("@НЭ", MySqlDbType.Int32) { Value = НЭ });
            LoadCombo(Комната, кбНКомнаты);

            int? НКомнаты = selectedRow.Cells["НКомнаты"].Value as int?;

            if (НКомнаты == null || НКомнаты == 0)
            {
                MessageBox.Show("Данные для комнат отсутствуют. Продолжение невозможно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            кбНКомнаты.SelectedValue = НКомнаты;

            sql = "SELECT DISTINCT НКомнаты, Вместимость FROM `Комната` WHERE НК=@НК AND НЭ=@НЭ";
            Вместимость.sql = sql;
            Вместимость.paramsForSQLQuery.Add(new MySqlParameter("@НКомнаты", MySqlDbType.Int32) { Value = НКомнаты });
            Вместимость.paramsForSQLQuery.Add(new MySqlParameter("@НК", MySqlDbType.Int32) { Value = НК });
            Вместимость.paramsForSQLQuery.Add(new MySqlParameter("@НЭ", MySqlDbType.Int32) { Value = НЭ });
            LoadCombo(Вместимость, кбВместимостьКомнаты);

            if (кбВместимостьКомнаты.Text.Equals(""))
            {
                MessageBox.Show("Данные для вместимости комнат отсутствуют. Продолжение невозможно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Прекращаем выполнение
            }

            кбВместимостьКомнаты.SelectedValue = НКомнаты;

            if (кбНКомнаты.DataSource is DataTable dataTable)
            {
                float pricePerNight = 0;
                foreach (DataRow row in dataTable.Rows)
                {
                    int? drНКомнаты = row["НКомнаты"] as int?;
                    int? drНК = row["НК"] as int?;
                    int? drНЭ = row["НЭ"] as int?;
                    if (drНКомнаты == НКомнаты && drНК == НК && drНЭ == НЭ)
                    {
                        pricePerNight = row["Цена1Ночь"] != DBNull.Value ? Convert.ToSingle(row["Цена1Ночь"]) : 0;
                        tbRoomPrice.Text = pricePerNight.ToString();
                        break;
                    }
                }

                if (pricePerNight == 0)
                {
                    MessageBox.Show("Цена комнаты за одну ночь не найдена.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Ошибка вывода цены комнаты за одну ночь: DataSource не является DataTable.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateComboBoxes()
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

            SetControlsFromDataRow(номерЗаселения, номерSource, controlsMapping);
        }
    }
}
