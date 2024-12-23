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
    }
}
