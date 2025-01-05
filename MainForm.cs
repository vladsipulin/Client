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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Клиенты clients = new Клиенты();
            clients.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Организация org = new Организация();
            org.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Должность position = new Должность();
            position.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ClientQueriesForAppartments queryForApps = new ClientQueriesForAppartments();
            queryForApps.Show();
            queryForApps.lbWhoLogged.Text = keyLbl.Text;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (Equals(keyLbl.Text,"Клиент"))
            {
                button1.Visible = false;
                button2.Visible = false;
                clientsButton.Visible = false;
                button3.Location = new Point(38, 109);
            }
        }
    }
}
