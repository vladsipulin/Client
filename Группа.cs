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
    public partial class Группа : Form
    {
        MySqlConnection con = new MySqlConnection("server=localhost;user id=root;database=hotel");
        MySqlCommand cmd;
        MySqlDataAdapter da;
        DataTable dt;
        private IGroup _repo;
        string sql;
        int номерГруппы = 0;

        public Группа()
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

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Random rand = new Random();

                bool isUnique = false;

                while (!isUnique)
                {
                    номерГруппы = rand.Next(10000, 99999); // Генерация случайного числа от 10000 до 99999

                    // Проверка уникальности 
                    var existing = await _repo.FindExistingNumOfGroup(номерГруппы);
                    if (existing == 0)
                    {
                        // Если номера еще нет в базе данных, то он уникален
                        isUnique = true;
                    }
                }

                Group current = new Group(номерГруппы, (int)кбНОрг.SelectedValue, Convert.ToDateTime(тбДатаРегистрации.Text), (int)тбЧисленностьГруппы.Value);

                Result<int> result;
                result = await _repo.Add(current);

                if (result)
                {
                    MessageBox.Show($"Группа №{номерГруппы} для организации {кбНОрг.Text} успешно создана!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (!result)
                {

                    MessageBox.Show($"Группа №{номерГруппы} для организации {кбНОрг.Text} не была создана" + result.Error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("Вы не заполнили все поля формы!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void Группа_Load(object sender, EventArgs e)
        {
            _repo = new RGroup();

            this.BackColor = System.Drawing.Color.White;
            тбДатаРегистрации.Enabled = false;
            checkBox1.Checked = true;

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                номерГруппы = (int)кбНГр.SelectedValue;
                Group current = new Group(номерГруппы, (int)кбНОрг.SelectedValue, Convert.ToDateTime(тбДатаРегистрации.Text), (int)тбЧисленностьГруппы.Value);

                Result<int> result;
                result = await _repo.Update(current, номерГруппы);

                if (result)
                {
                    MessageBox.Show($"Сведения группы №{номерГруппы} для организации {кбНОрг.Text} успешно изменены!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                номерГруппы = (int)кбНГр.SelectedValue;
                Group current = new Group(номерГруппы, (int)кбНОрг.SelectedValue, Convert.ToDateTime(тбДатаРегистрации.Text), (int)тбЧисленностьГруппы.Value);

                Result<int> result;
                result = await _repo.Remove(номерГруппы);

                if (result)
                {
                    MessageBox.Show($"Сведения группы №{номерГруппы} для организации {кбНОрг.Text} успешно удалены!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private async void кбНОрг_SelectionChangeCommitted(object sender, EventArgs e)
        {
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

        private async void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                кбНГр.Text = "";
                кбНГр.Enabled = false;
                button1.Enabled = true;
                button2.Enabled = false;
                button3.Enabled = false;

                sql = $"SELECT * FROM Организация";
                ComboBoxDataForFill Организация = new ComboBoxDataForFill(sql, "Наименование", "НОрг");
                LoadCombo(Организация);
                кбНОрг.DataSource = Организация.dataSource;
                кбНОрг.DisplayMember = Организация.DisplayMember;
                кбНОрг.ValueMember = Организация.ValueMember;
            }
            else
            {
                кбНГр.Enabled = true;
                button1.Enabled = false;
                button2.Enabled = true;
                button3.Enabled = true;

                // Инициализация ComboBox для выбора группы
                sql = "SELECT * FROM Группа";
                ComboBoxDataForFill группа = new ComboBoxDataForFill(sql, "НГр", "НГр");
                LoadCombo(группа);

                кбНГр.DataSource = группа.dataSource;
                кбНГр.DisplayMember = группа.DisplayMember;
                кбНГр.ValueMember = группа.ValueMember;

                // Сохранение выбранного значения
                номерГруппы = (int)кбНГр.SelectedValue;

                // Обработчик изменения выбранного значения
                кбНГр.SelectedValueChanged += (sender, e) =>
                {
                    // Получаем выбранное значение из кбНГр
                    var selectedValue = кбНГр.SelectedValue;

                    if (selectedValue != null)
                    {
                        // Формируем SQL-запрос для получения всех организаций
                        sql = "SELECT НОрг, Наименование FROM Организация";
                        ComboBoxDataForFill организации = new ComboBoxDataForFill(sql, "Наименование", "НОрг");
                        LoadCombo(организации);

                        кбНОрг.DataSource = организации.dataSource;
                        кбНОрг.DisplayMember = организации.DisplayMember;
                        кбНОрг.ValueMember = организации.ValueMember;

                        // Получаем `НОрг`, `ДатаРегистрации` и `ЧисленностьГруппы` из dataSource `кбГр`
                        var группаSource = кбНГр.DataSource as DataTable;

                        if (группаSource != null)
                        {
                            // Находим строку с выбранным `НГр`
                            var selectedRow = группаSource.Rows
                                .Cast<DataRow>()
                                .FirstOrDefault(row => row["НГр"].Equals(selectedValue));

                            if (selectedRow != null)
                            {
                                // Устанавливаем соответствующее значение `НОрг` в кбНОрг
                                var relatedНОрг = selectedRow["НОрг"];
                                if (relatedНОрг != DBNull.Value)
                                {
                                    кбНОрг.SelectedValue = relatedНОрг;
                                }

                                // Устанавливаем значение для dateTimePicker1
                                var датаРегистрации = selectedRow["ДатаРегистрации"];
                                if (датаРегистрации != DBNull.Value)
                                {
                                    тбДатаРегистрации.Value = Convert.ToDateTime(датаРегистрации);
                                }
                                else
                                    тбДатаРегистрации.Value = DateTime.Now;

                                // Устанавливаем значение для numericUpDown
                                var численностьГруппы = selectedRow["ЧисленностьГруппы"];
                                if (численностьГруппы != DBNull.Value)
                                {
                                    тбЧисленностьГруппы.Value = Convert.ToDecimal(численностьГруппы);
                                }
                            }
                        }
                    }
                };

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

        private async void кбНГр_SelectionChangeCommitted(object sender, EventArgs e)
        {
            // Инициализация ComboBox для выбора группы
            int текущийНГр = (int)кбНГр.SelectedValue;

            // Обработчик изменения выбранного значения
            // Обработчик изменения выбранного значения

            // Получаем выбранное значение из кбНГр
            var selectedValue = кбНГр.SelectedValue;

            if (selectedValue != null)
            {
                // Формируем SQL-запрос для получения всех организаций
                sql = "SELECT НОрг, Наименование FROM Организация";
                ComboBoxDataForFill организации = new ComboBoxDataForFill(sql, "Наименование", "НОрг");
                LoadCombo(организации);

                кбНОрг.DataSource = организации.dataSource;
                кбНОрг.DisplayMember = организации.DisplayMember;
                кбНОрг.ValueMember = организации.ValueMember;

                // Получаем `НОрг`, связанный с выбранным `НГр` из dataSource `кбНГр`
                var группаSource = кбНГр.DataSource as DataTable;

                if (группаSource != null)
                {
                    //Находим строку с выбранным `НГр`
                    var selectedRow = группаSource.Rows
                        .Cast<DataRow>()
                        .FirstOrDefault(row => row["НГр"].Equals(текущийНГр));

                    if (selectedRow != null)
                    {
                        // Устанавливаем соответствующее значение `НОрг` в кбНОрг
                        var relatedНОрг = selectedRow["НОрг"];
                        if (relatedНОрг != DBNull.Value)
                        {
                            кбНОрг.SelectedValue = relatedНОрг;
                        }

                        // Устанавливаем значение для dateTimePicker1
                        var датаРегистрации = selectedRow["ДатаРегистрации"];
                        if (датаРегистрации != DBNull.Value)
                        {
                            тбДатаРегистрации.Value = Convert.ToDateTime(датаРегистрации);
                        }
                        else
                            тбДатаРегистрации.Value = DateTime.Now;

                        // Устанавливаем значение для numericUpDown
                        var численностьГруппы = selectedRow["ЧисленностьГруппы"];
                        if (численностьГруппы != DBNull.Value)
                        {
                            тбЧисленностьГруппы.Value = Convert.ToDecimal(численностьГруппы);
                        }
                    }
                }
            }

            try
            {
                // Получение выбранного значения
                int selectedId = (int)кбНОрг.SelectedValue;
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
    }
}
