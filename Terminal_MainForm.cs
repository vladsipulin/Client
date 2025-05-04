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
using Client.Utils;

namespace Client
{
    // Класс для хранения данных об услуге
    public partial class Terminal_MainForm : Form
    {
        private readonly List<int> selectedServiceIds = new List<int>(); // Список ID выбранных услуг
        private readonly List<ServiceData> services = new List<ServiceData>(); // Список всех услуг
        private int selectedRating = 0; // Для хранения выбранной клиентом оценки работы сервиса 

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
            BTN_OPEN_RETURNPAYMENT.Visible = false;
            await LoadServicesAsync();
        }

        private void BTN_OPEN_CART_Click(object sender, EventArgs e)
        {
            if (selectedServiceIds.Count == 0)
            {
                MessageBox.Show("Ни одна услуга не выбрана", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            BTN_OPEN_RETURNPAYMENT.Visible = true;
            BTN_OPEN_CALLBACK_FORM.Visible = true;

            selectedServiceIds.Clear();
            selectedRating = 0; // Сбрасываем оценку
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

                                services.Add(new ServiceData(id, name, price, quantity)); // Сохраняем данные услуги

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

        private void BTN_OPEN_CALLBACK_FORM_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Visible = true;
            BTN_RETURN.Visible = true;
            panel1.Visible = false;
            BTN_OPEN_SERVICEBOOK.Visible = false;
            BTN_OPEN_RETURNPAYMENT.Visible = false;
            BTN_OPEN_CALLBACK_FORM.Visible = false;
            BTN_OPEN_CART.Visible = false;

            flowLayoutPanel1.Controls.Clear();
            CreateFeedbackForm();
        }

        private void CreateFeedbackForm()
        {
            // Панель для формы обратной связи
            Panel feedbackPanel = new Panel
            {
                Size = new Size(600, 250),
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(10),
                BackColor = Color.White
            };

            // Надпись "Номер клиента"
            Label lblClientId = new Label
            {
                Location = new Point(10, 20),
                Size = new Size(200, 30),
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Text = "Номер клиента:"
            };

            // Поле для ввода номера клиента
            TextBox txtClientId = new TextBox
            {
                Location = new Point(220, 20),
                Size = new Size(350, 30),
                Font = new Font("Segoe UI", 12F),
                ReadOnly = true,
                Text = keyLbl.Text // Предзаполняем номером клиента
            };

            // Надпись "Дата"
            Label lblDate = new Label
            {
                Location = new Point(10, 60),
                Size = new Size(200, 30),
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Text = "Дата:"
            };

            // Поле для отображения текущей даты
            Label txtDate = new Label
            {
                Location = new Point(220, 60),
                Size = new Size(350, 30),
                Font = new Font("Segoe UI", 12F),
                Text = DateTime.Now.ToString("yyyy-MM-dd HH-MM-s")
            };

            // Надпись "Оценка"
            Label lblRating = new Label
            {
                Location = new Point(10, 100),
                Size = new Size(200, 30),
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Text = "Оценка:"
            };

            // Панель для звездочек
            Panel starPanel = new Panel
            {
                Location = new Point(220, 100),
                Size = new Size(350, 50)
            };

            // Создаем 5 звездочек
            for (int i = 1; i <= 5; i++)
            {
                Label star = new Label
                {
                    Text = "★",
                    Font = new Font("Segoe UI", 24F),
                    ForeColor = Color.Gray,
                    Location = new Point((i - 1) * 60, 0),
                    Size = new Size(50, 50),
                    Tag = i,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                star.Click += (s, e) =>
                {
                    int rating = (int)((Label)s).Tag;
                    selectedRating = rating;
                    foreach (Label lbl in starPanel.Controls.OfType<Label>())
                    {
                        lbl.ForeColor = (int)lbl.Tag <= rating ? Color.Gold : Color.Gray;
                    }
                };
                starPanel.Controls.Add(star);
            }

            // Кнопка "Отправить отзыв"
            Button btnSubmit = new Button
            {
                Location = new Point(220, 170),
                Size = new Size(350, 60),
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Text = "Отправить отзыв",
                BackColor = Color.White
            };
            btnSubmit.Click += async (s, e) =>
            {
                await SubmitFeedbackAsync(txtClientId.Text, selectedRating);
            };

            // Добавляем элементы на панель
            feedbackPanel.Controls.AddRange(new Control[] { lblClientId, txtClientId, lblDate, txtDate, lblRating, starPanel, btnSubmit });
            flowLayoutPanel1.Controls.Add(feedbackPanel);
        }

        private async Task SubmitFeedbackAsync(string clientId, int rating)
        {
            if (string.IsNullOrWhiteSpace(clientId) || !int.TryParse(clientId, out int nKl))
            {
                MessageBox.Show("Введите корректный номер клиента.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (rating < 1 || rating > 5)
            {
                MessageBox.Show("Выберите оценку от 1 до 5.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["MySqlConn"].ConnectionString;
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string query = "INSERT INTO ОтзывКлиентаНаТерминал (НКл, Дата, Оценка) VALUES (@НКл, @Дата, @Оценка)";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@НКл", nKl);
                        command.Parameters.AddWithValue("@Дата", DateTime.Now);
                        command.Parameters.AddWithValue("@Оценка", rating);
                        await command.ExecuteNonQueryAsync();
                    }
                }
                MessageBox.Show("Отзыв успешно отправлен.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BTN_RETURN_Click(null, null); // Возвращаемся назад
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при отправке отзыва: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}