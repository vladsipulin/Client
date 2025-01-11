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
    public partial class ReviewRoom : Form
    {
        MySqlConnection con = new MySqlConnection("server=localhost;user id=root;database=hotel");
        MySqlCommand cmd;
        MySqlDataAdapter da;
        DataTable dt;
        private IReview _repo;
        string sql;

        public ReviewRoom()
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

        private async void ReviewRoom_Load(object sender, EventArgs e)
        {
            _repo = new RReviewRoom();

            this.BackColor = System.Drawing.Color.White;

            sql = $"SELECT * FROM ЗаселениеКлиента WHERE НКл = {lbWhoLogged.Text}";
            ComboBoxDataForFill СлужбаБыта = new ComboBoxDataForFill(sql, "НЗаявки", "НЗаявки");
            LoadCombo(СлужбаБыта);
            кбНЗаявки.DataSource = СлужбаБыта.dataSource;
            кбНЗаявки.DisplayMember = СлужбаБыта.DisplayMember;
            кбНЗаявки.ValueMember = СлужбаБыта.ValueMember;

            if (кбНЗаявки.SelectedValue is null)
            {
                MessageBox.Show($"Вы не можете оставить отзыв, пока ваша заявка не будет рассмотрена сотрудником", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }

            try
            {
                // Получение выбранного значения
                int selectedId = Convert.ToInt32(кбНЗаявки.SelectedValue);
                int idUser = Convert.ToInt32(lbWhoLogged.Text);

                // Запрос данных из базы данных
                var data = await _repo.GetByRequest(selectedId, idUser);

                // Привязка данных к DataGridView
                dataGridView1.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void кбНЗаявки_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                // Получение выбранного значения
                int selectedId = Convert.ToInt32(кбНЗаявки.SelectedValue);
                int idUser = Convert.ToInt32(lbWhoLogged.Text);

                // Запрос данных из базы данных
                var data = await _repo.GetByRequest(selectedId, idUser);

                // Привязка данных к DataGridView
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
                int clientId = Convert.ToInt16(lbWhoLogged.Text);

                Review current = new Review((int)кбНЗаявки.SelectedValue, clientId, (int)тбОценка.Value);

                Result<int> result;
                result = await _repo.Add(current);

                if (result)
                {
                    MessageBox.Show($"Отзыв на услугу по заявке {кбНЗаявки.SelectedValue} успешно добавлен!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (!result)
                {

                    MessageBox.Show($"Вы уже имеете отзыв на заявку {кбНЗаявки.SelectedValue}: " + result.Error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                int clientId = Convert.ToInt16(lbWhoLogged.Text);

                Review current = new Review((int)кбНЗаявки.SelectedValue, clientId, (int)тбОценка.Value);

                Result<int> result;
                result = await _repo.Update(current, (int)кбНЗаявки.SelectedValue, clientId);

                if (result)
                {
                    MessageBox.Show($"Отзыв на услугу по заявке {кбНЗаявки.SelectedValue} успешно изменен!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                int clientId = Convert.ToInt16(lbWhoLogged.Text);

                Review current = new Review((int)кбНЗаявки.SelectedValue, clientId, (int)тбОценка.Value);

                Result<int> result;
                result = await _repo.Remove((int)кбНЗаявки.SelectedValue, clientId);

                if (result)
                {
                    MessageBox.Show($"Отзыв на услугу по заявке {кбНЗаявки.SelectedValue} успешно удален!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}
