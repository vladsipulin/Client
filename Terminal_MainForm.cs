using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;
using MySql.Data.MySqlClient;

namespace Client
{
    // Класс для хранения данных об услуге
    public class ServiceData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; } 

        public ServiceData(int userId, int id, string name, double price, int quantity)
        {
            Id = id;
            Name = name;
            Price = price;
            Quantity = quantity;
        }
    }

    public partial class Terminal_MainForm : Form
    {
        private readonly List<int> selectedServiceIds = new List<int>(); // Список ID выбранных услуг
        private readonly List<ServiceData> services = new List<ServiceData>(); // Список всех услуг

        public Terminal_MainForm()
        {
            InitializeComponent();
        }

        private void Terminal_MainForm_Load(object sender, EventArgs e)
        {
            flowLayoutPanel1.Visible = false;
            BTN_OPEN_CART.Visible = false;
            BTN_RETURN.Visible = false;
            PanelCentered();
        }

        private async void BTN_OPEN_SERVICEBOOK_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Visible = true;
            BTN_OPEN_CART.Visible = true;
            BTN_RETURN.Visible = true;
            panel1.Visible = false;
            BTN_OPEN_SERVICEBOOK.Visible = false;
            BTN_OPEN_CALLBACK_FORM.Visible = false;
            await LoadServicesAsync();
        }

        private async void BTN_OPEN_CART_Click(object sender, EventArgs e)
        {
            if (selectedServiceIds.Count == 0)
            {
                MessageBox.Show("Корзина пуста.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Собираем данные выбранных услуг
                List<ServiceData> selectedServices = services
                    .Where(s => selectedServiceIds.Contains(s.Id))
                    .ToList();

                // Передаём данные в ReqOnServiceForm
                ReqOnServiceForm reqForm = new ReqOnServiceForm(selectedServices, keyLbl.Text);
                reqForm.ShowDialog();
            }
        }

        private void BTN_RETURN_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Visible = false;
            BTN_OPEN_CART.Visible = false;
            BTN_RETURN.Visible = false;
            panel1.Visible = true;
            BTN_OPEN_SERVICEBOOK.Visible = true;
            BTN_OPEN_CALLBACK_FORM.Visible = true;

            selectedServiceIds.Clear();
            foreach (Panel card in flowLayoutPanel1.Controls.OfType<Panel>())
            {
                card.BackColor = Color.WhiteSmoke;
            }
            PanelCentered();
        }

        private void PanelCentered()
        {
            int leftPadding = (ClientSize.Width - panel1.Width) / 2;
            int topPadding = (ClientSize.Height - panel1.Height) / 2;
            panel1.Location = new Point(leftPadding, topPadding);
        }

        private async Task LoadServicesAsync()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                services.Clear(); // Очищаем список услуг
                string connectionString = ConfigurationManager.ConnectionStrings["MySqlConn"].ConnectionString;
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string query = "SELECT НСл, Наименование, Цена, Количество FROM СлужбаБыта";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                int id = reader.GetInt32(0);
                                string name = reader.GetString(1);
                                double price = reader.GetDouble(2);
                                int quantity = reader.GetInt32(3);

                                // Сохраняем данные услуги
                                services.Add(new ServiceData(Convert.ToInt16(keyLbl.Text), id, name, price, quantity));

                                Panel card = CreateServiceCard(id, name, price, quantity);
                                flowLayoutPanel1.Controls.Add(card);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Panel CreateServiceCard(int id, string name, double price, int quantity)
        {
            Panel card = new Panel
            {
                Size = new Size(418, 100),
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(10),
                BackColor = Color.White,
                Tag = id
            };

            card.Click += (s, e) =>
            {
                Panel clickedCard = (Panel)s;
                int serviceId = (int)clickedCard.Tag;

                if (selectedServiceIds.Contains(serviceId))
                {
                    selectedServiceIds.Remove(serviceId);
                    clickedCard.BackColor = Color.White;
                }
                else
                {
                    selectedServiceIds.Add(serviceId);
                    clickedCard.BackColor = Color.LightGreen;
                }
            };

            Label lblId = new Label
            {
                Location = new Point(10, 10),
                AutoSize = true,
                Text = $"ID: {id}"
            };

            Label lblName = new Label
            {
                Location = new Point(10, 30),
                AutoSize = true,
                Text = $"Наименование: {name}"
            };

            Label lblPrice = new Label
            {
                Location = new Point(10, 50),
                AutoSize = true,
                Text = $"Цена: {price} руб."
            };

            Label lblQuantity = new Label
            {
                Location = new Point(10, 70),
                AutoSize = true,
                Text = $"Количество: {quantity}"
            };

            card.Controls.AddRange(new Control[] { lblId, lblName, lblPrice, lblQuantity });
            return card;
        }
    }
}