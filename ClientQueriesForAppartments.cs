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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Client
{
    public partial class ClientQueriesForAppartments : Form
    {
        MySqlConnection con = new MySqlConnection("server=localhost;user id=root;database=hotel");
        MySqlCommand cmd;
        MySqlDataAdapter da;
        DataTable dt;
        string sql;



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

            public ComboBoxDataForFill (string Sql, string displayMember, string valueMember)
            {
                sql = Sql;
                DisplayMember = displayMember;
                ValueMember = valueMember;
                dataSource = new DataTable();
                paramsForSQLQuery = new List<MySqlParameter>();
            }
        }

        public ClientQueriesForAppartments()
        {
            InitializeComponent();
        }

        private void ClientQueriesForAppartments_Load(object sender, EventArgs e)
        {
            sql = "SELECT Название, НГ FROM `ГостиничныйКомплекс`";
            ComboBoxDataForFill Гостиница = new ComboBoxDataForFill(sql, "Название", "НГ");
            LoadCombo(Гостиница);
            кбНГ.DataSource = Гостиница.dataSource;
            кбНГ.DisplayMember = Гостиница.DisplayMember;
            кбНГ.ValueMember = Гостиница.ValueMember;

            sql = "SELECT * FROM `Корпус` WHERE НГ=@НГ";
            ComboBoxDataForFill Корпус = new ComboBoxDataForFill(sql, "НК", "НК");
            int НомерГостиницы = Convert.ToInt32(кбНГ.SelectedValue);
            Корпус.paramsForSQLQuery.Add(new MySqlParameter("@НГ", MySqlDbType.Int32) { Value = НомерГостиницы });
            LoadCombo(Корпус);
            кбНК.DataSource = Корпус.dataSource;
            кбНК.DisplayMember = Корпус.DisplayMember;
            кбНК.ValueMember = Корпус.ValueMember;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void кбНГ_TextChanged(object sender, EventArgs e)
        {
            //sql = "SELECT * FROM `Корпус` WHERE НГ=@НГ";
            //ComboBoxDataForFill Корпус = new ComboBoxDataForFill(sql, "НК", "НК");
            //int НомерГостиницы = Convert.ToInt32(кбНГ.SelectedValue);
            //Корпус.paramsForSQLQuery.Add(new MySqlParameter("@НГ", MySqlDbType.Int32) { Value = НомерГостиницы });
            //LoadCombo(Корпус);
            //кбНК.DataSource = Корпус.dataSource;
            //кбНК.DisplayMember = Корпус.DisplayMember;
            //кбНК.ValueMember = Корпус.ValueMember;
        }
    }
}
