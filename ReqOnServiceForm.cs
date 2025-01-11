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
    public partial class ReqOnServiceForm : Form
    {

        MySqlConnection con = new MySqlConnection("server=localhost;user id=root;database=hotel");
        MySqlCommand cmd;
        MySqlDataAdapter da;
        DataTable dt;
        private IReqOnService _repo;
        string sql;
        int clientId;
        string username;

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
        private MySqlConnection GetConnection()
        {
            var cs = ConfigurationManager.ConnectionStrings["MySqlConn"].ToString();
            var builder = new MySqlConnectionStringBuilder(cs);
            //чтоб избежать проблем с русским языком
            builder.CharacterSet = "utf8";
            return new MySqlConnection(builder.ConnectionString);
        }

        public ReqOnServiceForm()
        {
            InitializeComponent();
        }

        private void ReqOnService_Load(object sender, EventArgs e)
        {
            _repo = new RReqOnService();

            this.BackColor = System.Drawing.Color.White;

            тбЦенаСлужбы.Enabled = false;
            тбНКл.Enabled = false;
            тбДатаЗаявки.Enabled = false;

            sql = "SELECT * FROM СлужбаБыта";
            ComboBoxDataForFill СлужбаБыта = new ComboBoxDataForFill(sql, "Наименование", "НСл");
            LoadCombo(СлужбаБыта);
            кбНСл.DataSource = СлужбаБыта.dataSource;
            кбНСл.DisplayMember = СлужбаБыта.DisplayMember;
            кбНСл.ValueMember = СлужбаБыта.ValueMember;

            тбНКл.Text = lbWhoLogged.Text;
            if (кбНСл.DataSource is DataTable dataTable)
            {
                float price;
                foreach (DataRow row in dataTable.Rows)
                {
                    int? drНСл = Convert.ToInt32(row["НСл"]);
                    if (drНСл == Convert.ToInt32(кбНСл.SelectedValue))
                    {
                        if (row["Цена"] != DBNull.Value)
                        {
                            price = (float)row["Цена"];
                            тбЦенаСлужбы.Text = price.ToString();
                            break;
                        }
                        else
                            price = 0;
                    }
                }
            }
            else
            {
                MessageBox.Show("Ошибка вывода цены комнаты за одну ночь: DataSource не является DataTable.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void кбНСл_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (кбНСл.DataSource is DataTable dataTable)
            {
                float price;
                foreach (DataRow row in dataTable.Rows)
                {
                    int? drНСл = Convert.ToInt32(row["НСл"]);
                    if (drНСл == Convert.ToInt32(кбНСл.SelectedValue))
                    {
                        if (row["Цена"] != DBNull.Value)
                        {
                            price = (float)row["Цена"];
                            тбЦенаСлужбы.Text = price.ToString();
                            break;
                        }
                        else
                            price = 0;
                    }
                }
            }
            else
            {
                MessageBox.Show("Ошибка вывода цены комнаты за одну ночь: DataSource не является DataTable.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                float summ = float.Parse(тбЦенаСлужбы.Text) * float.Parse(тбКолво.Text);
                тбСумма.Text = summ.ToString();
            }
            catch
            { тбСумма.Text = "null"; }

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int clientId = Convert.ToInt16(lbWhoLogged.Text);
                //SetClientIdByUsername(username);

                Random rand = new Random();
                int номерЗаявки=0;
                bool isUnique = false;

                while (!isUnique)
                {
                    номерЗаявки = rand.Next(10000, 99999); // Генерация случайного числа от 10000 до 99999

                    // Проверка уникальности номера заявки
                    var existing = await _repo.GetIdByRequest(номерЗаявки);
                    if (existing == 0)
                    {
                        // Если номера заявки еще нет в базе данных, то он уникален
                        isUnique = true;
                    }
                }

                ReqOnService current = new ReqOnService(номерЗаявки, (int)кбНСл.SelectedValue, Convert.ToDateTime(тбСрокОплаты.Text),
                                                        Convert.ToInt32(тбНКл.Text), Convert.ToInt32(тбКолво.Text), 
                                                        float.Parse(тбСумма.Text), Convert.ToDateTime(тбДатаЗаявки.Text));

                Result<int> result;
                result = await _repo.Add(current);

                if (result)
                {
                    MessageBox.Show("Заявка успешно создана!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (!result)
                {
                    MessageBox.Show("Вы уже имеете эту заявку: "+result.Error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("Вы не заполнили все поля формы!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
