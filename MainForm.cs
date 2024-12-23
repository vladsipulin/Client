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
        Клиенты clients;
        Организация org;

        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clients.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            org.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            clients = new Клиенты();
            org = new Организация();
        }
    }
}
