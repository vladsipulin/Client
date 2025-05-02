using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Client
{
    public partial class Terminal_MainForm : Form
    {
        private readonly List<int> selectedServiceIds = new List<int>(); // Список для корзины (ID выбранных услуг)
        public Terminal_MainForm()
        {
            InitializeComponent();
        }

        // Обработчик клика по кнопке "Заказать услугу"
        private async void BTN_OPEN_SERVICEBOOK_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Visible = true;
            BTN_OPEN_CART.Visible = true;
            BTN_RETURN.Visible = true;
            panel1.Visible = false;
            BTN_OPEN_SERVICEBOOK.Visible = false;
            BTN_OPEN_CALLBACK_FORM.Visible = false;
            await LoadServicesAsync(); // Асинхронная загрузка данных
        }

        // Обработчик загрузки формы
        private async void Terminal_MainForm_Load(object sender, EventArgs e)
        {
            flowLayoutPanel1.Visible = false; // Изначально скрываем FlowLayoutPanel
            BTN_OPEN_CART.Visible = false;
            BTN_RETURN.Visible = false;
            if (panel1.Visible)
                PanelCentered();
            await LoadServicesAsync();
        }

        // Асинхронная загрузка данных 
        private async Task LoadServicesAsync()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear(); // Очищаем существующие карточки
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

        // Метод для создания карточки услуги
        private Panel CreateServiceCard(int id, string name, double price, int quantity)
        {
            Panel card = new Panel
            {
                Size = new Size(418, 100), // Размер карточки (определен фиксировано исходя из max длины поля «Наименование»)
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(10), // Расстояние между карточками
                BackColor = Color.White,
                Tag = id // Сохраняем ID услуги в Tag
            };

            // Добавляем обработчик клика для выбора карточки
            card.Click += (s, e) =>
            {
                Panel clickedCard = (Panel)s;
                int serviceId = (int)clickedCard.Tag;

                if (selectedServiceIds.Contains(serviceId))
                {
                    // Снимаем выбор
                    selectedServiceIds.Remove(serviceId);
                    clickedCard.BackColor = Color.White;
                }
                else
                {
                    // Добавляем в корзину
                    selectedServiceIds.Add(serviceId);
                    clickedCard.BackColor = Color.LightGreen; // Визуальная индикация выбора
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

        // Обработчик клика по кнопке "Открыть корзину"
        private void BTN_OPEN_CART_Click(object sender, EventArgs e)
        {
            if (selectedServiceIds.Count == 0)
            {
                MessageBox.Show("Корзина пуста.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string selectedIds = string.Join(", ", selectedServiceIds);
                MessageBox.Show($"Выбранные услуги (ID): {selectedIds}", "Корзина", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Обработчик клика по кнопке "Назад" 
        private void BTN_RETURN_Click(object sender, EventArgs e)
        {
            // Скрываем FlowLayoutPanel и показываем BTN_OPEN_SERVICEBOOK
            flowLayoutPanel1.Visible = false;
            BTN_OPEN_CART.Visible = false;
            BTN_RETURN.Visible = false;
            panel1.Visible = true;
            BTN_OPEN_SERVICEBOOK.Visible = true;
            BTN_OPEN_CALLBACK_FORM.Visible = true;

            // Очищаем корзину и сбрасываем выбор карточек
            selectedServiceIds.Clear();
            foreach (Panel card in flowLayoutPanel1.Controls.OfType<Panel>())
            {
                card.BackColor = Color.WhiteSmoke; // Сбрасываем цвет
            }
        }

        private void PanelCentered()
        {
            int leftPadding = (this.Width - panel1.Width) / 2;
            int topPadding = (this.Height - panel1.Height) / 2;
            panel1.Location = new Point(leftPadding, topPadding);
        }
    }
}
