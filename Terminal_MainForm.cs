using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Client.Utils;
using Mysqlx.Crud;

namespace Client
{
    public partial class Terminal_MainForm : Form
    {
        private readonly List<int> selectedServiceIds = new List<int>(); // Список ID выбранных услуг
        private readonly List<ServiceData> services = new List<ServiceData>(); // Список всех услуг
        private readonly List<RequestData> requestOrderedServices = new List<RequestData>(); // Список заказов клиента для формы возврата средств
        private int selectedRating = 0; // Для хранения выбранной клиентом оценки работы сервиса 
        private int selectedOrderId = -1; // ID выбранного заказа
        private bool isFeedbackFormDisplayed = false; // Флаг для определения отображения feedbackPanel
        private int lastPanelWidth = 0; // Последняя ширина mainPanel для оптимизации
        private System.Windows.Forms.Timer resizeTimer; // Таймер для дебouncing события SizeChanged

        public Terminal_MainForm()
        {
            InitializeComponent();

            // Инициализация таймера для дебouncing
            resizeTimer = new System.Windows.Forms.Timer
            {
                Interval = 100 // Задержка 100 мс
            };
            resizeTimer.Tick += (s, e) =>
            {
                resizeTimer.Stop();
                CenterPanelsInMainPanel();
                PanelCentered();
            };
        }

        private void Terminal_MainForm_Load(object sender, EventArgs e)
        {
            // Перемещаем кнопки в mainPanel
            mainPanel.Controls.Add(BTN_RETURN);
            mainPanel.Controls.Add(BTN_OPEN_CART);
            BTN_RETURN.Location = new Point(10, 10);
            BTN_OPEN_CART.Location = new Point(mainPanel.Width - BTN_OPEN_CART.Width - 10, 10);
            BTN_RETURN.BringToFront();
            BTN_OPEN_CART.BringToFront();

            mainPanel.Visible = false;
            BTN_OPEN_CART.Visible = false;
            BTN_RETURN.Visible = false;
            PanelCentered();
        }

        private void Terminal_MainForm_SizeChanged(object sender, EventArgs e)
        {
            PanelCentered();
        }

        private async void BTN_OPEN_SERVICEBOOK_Click(object sender, EventArgs e)
        {
            mainPanel.Visible = true;
            BTN_OPEN_CART.Visible = true;
            BTN_RETURN.Visible = true;
            panel1.Visible = false;
            BTN_OPEN_SERVICEBOOK.Visible = false;
            BTN_OPEN_RETURNPAYMENT.Visible = false;
            BTN_OPEN_CALLBACK_FORM.Visible = false;
            BTN_LEAVE.Visible = false;
            mainPanel.Controls.Clear();

            // Восстанавливаем кнопки
            mainPanel.Controls.Add(BTN_RETURN);
            mainPanel.Controls.Add(BTN_OPEN_CART);
            BTN_RETURN.Location = new Point(10, 10);
            BTN_OPEN_CART.Location = new Point(mainPanel.Width - BTN_OPEN_CART.Width - 10, 10);
            BTN_RETURN.BringToFront();
            BTN_OPEN_CART.BringToFront();

            isFeedbackFormDisplayed = false;
            await LoadServicesAsync();
        }

        private void BTN_OPEN_CALLBACK_FORM_Click(object sender, EventArgs e)
        {
            mainPanel.Visible = true;
            BTN_RETURN.Visible = true;
            panel1.Visible = false;
            BTN_OPEN_SERVICEBOOK.Visible = false;
            BTN_OPEN_RETURNPAYMENT.Visible = false;
            BTN_OPEN_CALLBACK_FORM.Visible = false;
            BTN_LEAVE.Visible = false;
            BTN_OPEN_CART.Visible = false;

            mainPanel.Controls.Clear();

            // Восстанавливаем кнопку "Назад"
            mainPanel.Controls.Add(BTN_RETURN);
            BTN_RETURN.Location = new Point(10, 10);
            BTN_RETURN.BringToFront();

            isFeedbackFormDisplayed = true;
            CreateFeedbackForm();
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
            mainPanel.Visible = false;
            BTN_OPEN_CART.Visible = false;
            BTN_RETURN.Visible = false;
            panel1.Visible = true;
            BTN_OPEN_SERVICEBOOK.Visible = true;
            BTN_OPEN_RETURNPAYMENT.Visible = true;
            BTN_OPEN_CALLBACK_FORM.Visible = true;
            BTN_LEAVE.Visible = true;

            selectedServiceIds.Clear();
            selectedRating = 0; // Сбрасываем оценку
            foreach (Panel card in mainPanel.Controls.OfType<Panel>())
            {
                card.BackColor = Color.WhiteSmoke;
            }
            isFeedbackFormDisplayed = false;
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
                mainPanel.Controls.Clear();
                services.Clear(); // Очищаем список услуг

                // Восстанавливаем кнопки
                mainPanel.Controls.Add(BTN_RETURN);
                mainPanel.Controls.Add(BTN_OPEN_CART);
                BTN_RETURN.Location = new Point(10, 10);
                BTN_OPEN_CART.Location = new Point(mainPanel.Width - BTN_OPEN_CART.Width - 10, 10);
                BTN_RETURN.BringToFront();
                BTN_OPEN_CART.BringToFront();

                string connectionString = ConfigurationManager.ConnectionStrings["MySqlConn"].ConnectionString;
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string query = "SELECT НУслуги, Наименование, Цена, Количество FROM Услуга";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                        {
                            List<Panel> cards = new List<Panel>();
                            while (await reader.ReadAsync())
                            {
                                int id = reader.GetInt32(0);
                                string name = reader.GetString(1);
                                double price = reader.GetDouble(2);
                                int quantity = reader.GetInt32(3);

                                services.Add(new ServiceData(id, name, price, quantity)); // Сохраняем данные услуги

                                Panel card = CreateServiceCard(id, name, price, quantity);
                                cards.Add(card);
                            }
                            // Добавляем карточки в mainPanel после расчета позиций
                            PlaceServiceCards(cards);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PlaceServiceCards(List<Panel> cards)
        {
            mainPanel.SuspendLayout(); // Отключаем перерисовку

            // Сохраняем кнопки
            var buttons = mainPanel.Controls.OfType<Button>().ToList();
            mainPanel.Controls.Clear();
            foreach (var button in buttons)
            {
                mainPanel.Controls.Add(button);
            }
            BTN_RETURN.Location = new Point(10, 10);
            BTN_OPEN_CART.Location = new Point(mainPanel.Width - BTN_OPEN_CART.Width - 10, 10);
            BTN_RETURN.BringToFront();
            BTN_OPEN_CART.BringToFront();

            int cardWidth = 418; // Ширина карточки
            int margin = 10; // Отступ между карточками
            int topMargin = 80; // Отступ сверху для учета кнопок
            int panelWidth = mainPanel.Width - SystemInformation.VerticalScrollBarWidth; // Учитываем полосу прокрутки

            // Проверяем, изменилась ли ширина достаточно для пересчета столбцов
            int columns = Math.Max(1, panelWidth / (cardWidth + 2 * margin));
            if (Math.Abs(panelWidth - lastPanelWidth) < (cardWidth + 2 * margin) / 2 && mainPanel.Controls.OfType<Panel>().Count() == cards.Count)
            {
                // Незначительное изменение ширины, просто центрируем существующие карточки
                int columnWidth = panelWidth / columns;
                int[] yPositions = new int[columns];
                for (int i = 0; i < columns; i++)
                {
                    yPositions[i] = topMargin;
                }

                int index = 0;
                foreach (Panel card in mainPanel.Controls.OfType<Panel>())
                {
                    int minY = yPositions.Min();
                    int columnIndex = Array.IndexOf(yPositions, minY);
                    int xPosition = columnIndex * columnWidth + (columnWidth - cardWidth) / 2;
                    card.Location = new Point(Math.Max(margin, xPosition), minY);
                    yPositions[columnIndex] += card.Height + margin;
                    index++;
                }
            }
            else
            {
                // Значительное изменение ширины или новое количество карточек, пересчитываем
                int columnWidth = panelWidth / columns;
                int[] yPositions = new int[columns];
                for (int i = 0; i < columns; i++)
                {
                    yPositions[i] = topMargin;
                }

                for (int i = 0; i < cards.Count; i++)
                {
                    int minY = yPositions.Min();
                    int columnIndex = Array.IndexOf(yPositions, minY);
                    Panel card = cards[i];
                    int xPosition = columnIndex * columnWidth + (columnWidth - cardWidth) / 2;
                    card.Location = new Point(Math.Max(margin, xPosition), minY);
                    mainPanel.Controls.Add(card);
                    yPositions[columnIndex] += card.Height + margin;
                }
                lastPanelWidth = panelWidth; // Обновляем последнюю ширину
            }

            mainPanel.ResumeLayout(); // Включаем перерисовку
        }

        private Panel CreateServiceCard(int id, string name, double price, int quantity)
        {
            Panel card = new Panel
            {
                Size = new Size(418, 100),
                BorderStyle = BorderStyle.FixedSingle,
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

        private void CreateFeedbackForm()
        {
            mainPanel.SuspendLayout();

            // Панель для формы обратной связи
            Panel feedbackPanel = new Panel
            {
                Size = new Size(600, 250),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };

            // Устанавливаем начальную позицию по центру экрана
            feedbackPanel.Location = new Point(
                Math.Max(0, (mainPanel.Width - 600) / 2),
                Math.Max(0, (mainPanel.Height - 250) / 2)
            );

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
                Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
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
            mainPanel.Controls.Add(feedbackPanel);
            feedbackPanel.BringToFront(); // Убедимся, что feedbackPanel поверх карточек, но ниже кнопок

            mainPanel.ResumeLayout();
        }

        private void CenterPanelsInMainPanel()
        {
            mainPanel.SuspendLayout();

            if (isFeedbackFormDisplayed)
            {
                // Центрируем feedbackPanel по центру экрана
                Panel feedbackPanel = mainPanel.Controls.OfType<Panel>().FirstOrDefault();
                if (feedbackPanel != null)
                {
                    feedbackPanel.Location = new Point(
                        Math.Max(0, (mainPanel.Width - feedbackPanel.Width) / 2),
                        Math.Max(0, (mainPanel.Height - feedbackPanel.Height) / 2)
                    );
                    feedbackPanel.BringToFront();
                    BTN_RETURN.BringToFront(); // Кнопка "Назад" выше feedbackPanel
                }
            }
            else
            {
                // Перестраиваем карточки услуг в столбцы
                List<Panel> cards = mainPanel.Controls.OfType<Panel>().ToList();
                if (cards.Count > 0)
                {
                    PlaceServiceCards(cards);
                }
            }

            mainPanel.ResumeLayout();
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

        private void Terminal_MainForm_Resize(object sender, EventArgs e)
        {
            resizeTimer.Stop();
            resizeTimer.Start();
        }

        private void BTN_LEAVE_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BTN_OPEN_RETURNPAYMENT_Click(object sender, EventArgs e)
        {
            mainPanel.Visible = true;
            BTN_RETURN.Visible = true;
            panel1.Visible = false;
            BTN_OPEN_SERVICEBOOK.Visible = false;
            BTN_OPEN_RETURNPAYMENT.Visible = false;
            BTN_OPEN_CALLBACK_FORM.Visible = false;
            BTN_LEAVE.Visible = false;
            BTN_OPEN_CART.Visible = false;

            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(BTN_RETURN);
            BTN_RETURN.Location = new Point(10, 10);
            BTN_RETURN.BringToFront();

            isFeedbackFormDisplayed = false;
            LoadOrdersAndCreateReturnFormAsync();
        }

        private Panel CreateOrderCard(RequestData request)
        {
            bool isOldRequest = (DateTime.Now - request.RequestDate).TotalDays > 10;
            int baseHeight = 100; // Базовая высота карточки (как в CreateServiceCard)
            int serviceHeight = 20; // Высота строки для каждой услуги
            int totalHeight = baseHeight + request.Services.Count * serviceHeight; // Динамическая высота

            Panel card = new Panel
            {
                Size = new Size(418, totalHeight), // Ширина как в CreateServiceCard
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = isOldRequest ? Color.FromArgb(135, 206, 250) : Color.White, // Светло-синий для заявок старше 10 дней
                Tag = new { RequestId = request.RequestId, IsOld = isOldRequest }
            };

            if (!isOldRequest)
            {
                card.Click += (s, e) =>
                {
                    Panel clickedCard = (Panel)s;
                    dynamic tag = clickedCard.Tag;
                    int clickedRequestId = tag.RequestId;

                    if (selectedOrderId == clickedRequestId)
                    {
                        selectedOrderId = -1;
                        clickedCard.BackColor = Color.White;
                    }
                    else
                    {
                        selectedOrderId = clickedRequestId;
                        foreach (Panel p in mainPanel.Controls.OfType<Panel>().Where(p => p.Tag != null && ((dynamic)p.Tag).RequestId != clickedRequestId))
                        {
                            dynamic pTag = p.Tag;
                            p.BackColor = pTag.IsOld ? Color.FromArgb(135, 206, 250) : Color.White;
                        }
                        clickedCard.BackColor = Color.FromArgb(144, 238, 144); // Светло-зелёный для выбранной карточки
                    }
                };
            }

            Label lblId = new Label
            {
                Location = new Point(10, 10),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Text = $"Номер заявки: {request.RequestId}"
            };

            Label lblTotalPrice = new Label
            {
                Location = new Point(10, 30),
                AutoSize = true,
                Font = new Font("Segoe UI", 9F),
                Text = $"Сумма: {request.TotalPrice} руб."
            };

            Label lblRequestDate = new Label
            {
                Location = new Point(10, 50),
                AutoSize = true,
                Font = new Font("Segoe UI", 9F),
                Text = $"Дата заявки: {request.RequestDate:yyyy-MM-dd}"
            };

            // Отображение списка услуг
            Label lblServicesHeader = new Label
            {
                Location = new Point(10, 70),
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Text = "Услуги:"
            };

            int yOffset = 90;
            foreach (var service in request.Services)
            {
                Label lblService = new Label
                {
                    Location = new Point(10, yOffset),
                    Size = new Size(398, 20), // Учитываем ширину карточки минус отступы
                    Font = new Font("Segoe UI", 9F),
                    Text = $"{service.Key} (кол-во: {service.Value})"
                };
                card.Controls.Add(lblService);
                yOffset += serviceHeight;
            }

            card.Controls.AddRange(new Control[] { lblId, lblTotalPrice, lblRequestDate, lblServicesHeader });
            return card;
        }

        private void PlaceOrderCards(List<Panel> cards)
        {
            mainPanel.SuspendLayout(); // Отключаем перерисовку

            // Сохраняем кнопки
            var buttons = mainPanel.Controls.OfType<Button>().ToList();
            mainPanel.Controls.Clear();
            foreach (var button in buttons)
            {
                mainPanel.Controls.Add(button);
            }
            BTN_RETURN.Location = new Point(10, 10);
            // Устанавливаем расположение кнопки "Подтвердить выбор", если она есть
            foreach (var button in buttons)
            {
                if (button.Text == "Подтвердить выбор")
                {
                    button.Location = new Point(mainPanel.Width - button.Width - 10, 10);
                }
                button.BringToFront();
            }

            int cardWidth = 418; // Ширина карточки (как в PlaceServiceCards)
            int margin = 10; // Отступ между карточками
            int topMargin = 80; // Отступ сверху для учета кнопок
            int panelWidth = mainPanel.Width - SystemInformation.VerticalScrollBarWidth; // Учитываем полосу прокрутки

            // Проверяем, изменилась ли ширина достаточно для пересчета столбцов
            int columns = Math.Max(1, panelWidth / (cardWidth + 2 * margin));
            if (Math.Abs(panelWidth - lastPanelWidth) < (cardWidth + 2 * margin) / 2 && mainPanel.Controls.OfType<Panel>().Count() == cards.Count)
            {
                // Незначительное изменение ширины, просто центрируем существующие карточки
                int columnWidth = panelWidth / columns;
                int[] yPositions = new int[columns];
                for (int i = 0; i < columns; i++)
                {
                    yPositions[i] = topMargin;
                }

                int index = 0;
                foreach (Panel card in mainPanel.Controls.OfType<Panel>())
                {
                    int minY = yPositions.Min();
                    int columnIndex = Array.IndexOf(yPositions, minY);
                    int xPosition = columnIndex * columnWidth + (columnWidth - cardWidth) / 2;
                    card.Location = new Point(Math.Max(margin, xPosition), minY);
                    yPositions[columnIndex] += card.Height + margin;
                    index++;
                }
            }
            else
            {
                // Значительное изменение ширины или новое количество карточек, пересчитываем
                int columnWidth = panelWidth / columns;
                int[] yPositions = new int[columns];
                for (int i = 0; i < columns; i++)
                {
                    yPositions[i] = topMargin;
                }

                for (int i = 0; i < cards.Count; i++)
                {
                    int minY = yPositions.Min();
                    int columnIndex = Array.IndexOf(yPositions, minY);
                    Panel card = cards[i];
                    int xPosition = columnIndex * columnWidth + (columnWidth - cardWidth) / 2;
                    card.Location = new Point(Math.Max(margin, xPosition), minY);
                    mainPanel.Controls.Add(card);
                    yPositions[columnIndex] += card.Height + margin;
                }
                lastPanelWidth = panelWidth; // Обновляем последнюю ширину
            }

            mainPanel.ResumeLayout(); // Включаем перерисовку
        }

        private void CreateReturnForm()
        {
            Panel returnPanel = new Panel
            {
                Size = new Size(600, 300),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Location = new Point(Math.Max(0, (mainPanel.Width - 600) / 2), mainPanel.Height - 350)
            };

            Label lblReturnMethod = new Label
            {
                Location = new Point(10, 20),
                Size = new Size(200, 30),
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Text = "Способ возврата:"
            };

            ComboBox cmbReturnMethod = new ComboBox
            {
                Location = new Point(220, 20),
                Size = new Size(350, 30),
                Font = new Font("Segoe UI", 12F),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbReturnMethod.Items.AddRange(new string[] { "Электронный", "Наличные"});

            Label lblReturnDate = new Label
            {
                Location = new Point(10, 60),
                Size = new Size(200, 30),
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Text = "Дата возврата:"
            };

            DateTimePicker dtpReturnDate = new DateTimePicker
            {
                Location = new Point(220, 60),
                Size = new Size(350, 30),
                Font = new Font("Segoe UI", 12F),
                Format = DateTimePickerFormat.Short,
                MinDate = DateTime.Today
            };

            Button btnSubmitReturn = new Button
            {
                Location = new Point(220, 100),
                Size = new Size(350, 60),
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Text = "Создать заявку",
                BackColor = Color.White
            };
            btnSubmitReturn.Click += async (s, e) =>
            {
                await SubmitReturnRequestAsync(cmbReturnMethod.SelectedItem?.ToString(), dtpReturnDate.Value);
            };

            returnPanel.Controls.AddRange(new Control[] { lblReturnMethod, cmbReturnMethod, lblReturnDate, dtpReturnDate, btnSubmitReturn });
            mainPanel.Controls.Add(returnPanel);
            returnPanel.BringToFront();
            BTN_RETURN.BringToFront();
        }

        private async Task SubmitReturnRequestAsync(string returnMethod, DateTime returnDate)
        {
            if (selectedOrderId == -1)
            {
                MessageBox.Show("Выберите заявку для возврата.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(returnMethod) || (returnMethod != "Электронный" && returnMethod != "Наличные"))
            {
                MessageBox.Show("Выберите корректный способ возврата: 'Электронный' или 'Наличные'.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Находим данные выбранной заявки
                RequestData selectedRequest = requestOrderedServices.FirstOrDefault(o => o.RequestId == selectedOrderId);
                if (selectedRequest == null)
                {
                    MessageBox.Show("Выбранная заявка не найдена.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Получаем все НУслуги для данной заявки
                string connectionString = ConfigurationManager.ConnectionStrings["MySqlConn"].ConnectionString;
                List<(int ServiceId, int Quantity)> services = new List<(int, int)>();
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    string query = @"
                SELECT НУслуги, Количество_Ед
                FROM ЗаявкаНаУслугу
                WHERE НЗаявки = @НЗаявки AND НКл = @НКл";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@НЗаявки", selectedOrderId);
                        command.Parameters.AddWithValue("@НКл", int.Parse(keyLbl.Text));
                        using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                int serviceId = reader.GetInt32(0);
                                int quantity = reader.GetInt32(1);
                                services.Add((serviceId, quantity));
                            }
                        }
                    }

                    if (services.Count == 0)
                    {
                        MessageBox.Show("Не удалось найти услуги для данной заявки.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Рассчитываем сумму возврата для каждой услуги (пропорционально)
                    float totalRefundAmount = (float)selectedRequest.TotalPrice;
                    float totalQuantity = services.Sum(s => s.Quantity);
                    foreach (var service in services)
                    {
                        float serviceRefundAmount = totalRefundAmount * service.Quantity / totalQuantity;

                        string insertQuery = @"
                    INSERT INTO ВозвратСредств (НЗаявки, НУслуги, НКл, СуммаВозврата, ДатаВозврата, НС, СпособВозврата)
                    VALUES (@НЗаявки, @НУслуги, @НКл, @СуммаВозврата, @ДатаВозврата, @НС, @СпособВозврата)";
                        using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
                        {
                            command.Parameters.AddWithValue("@НЗаявки", selectedOrderId);
                            command.Parameters.AddWithValue("@НУслуги", service.ServiceId);
                            command.Parameters.AddWithValue("@НКл", int.Parse(keyLbl.Text));
                            command.Parameters.AddWithValue("@СуммаВозврата", serviceRefundAmount);
                            command.Parameters.AddWithValue("@ДатаВозврата", returnDate);
                            command.Parameters.AddWithValue("@СпособВозврата", returnMethod);

                            if (returnMethod == "Электронный")
                            {
                                command.Parameters.AddWithValue("@НС", 0);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@НС", DBNull.Value);
                            }

                            await command.ExecuteNonQueryAsync();
                        }
                    }
                }

                MessageBox.Show("Ваша заявка на возврат средств была оформлена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BTN_RETURN_Click(null, null); // Возвращаемся назад
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании заявки: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadOrdersAndCreateReturnFormAsync()
        {
            try
            {
                requestOrderedServices.Clear();
                mainPanel.Controls.Clear();
                mainPanel.Controls.Add(BTN_RETURN);
                BTN_RETURN.Location = new Point(10, 10);
                BTN_RETURN.BringToFront();

                // Загрузка заявок клиента
                string connectionString = ConfigurationManager.ConnectionStrings["MySqlConn"].ConnectionString;
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string query = @"
                SELECT z.НЗаявки, u.Наименование, z.Количество_Ед, z.Сумма, z.ДатаЗаявки
                FROM ЗаявкаНаУслугу z
                JOIN Услуга u ON z.НУслуги = u.НУслуги
                WHERE z.НКл = @НКл";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@НКл", int.Parse(keyLbl.Text));
                        using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                        {
                            // Группировка данных по НЗаявки
                            Dictionary<int, RequestData> requestsDict = new Dictionary<int, RequestData>();
                            while (await reader.ReadAsync())
                            {
                                int requestId = reader.GetInt32(0); // НЗаявки
                                string serviceName = reader.GetString(1); // Наименование
                                int quantity = reader.GetInt32(2); // Количество_Ед
                                double totalPrice = reader.GetDouble(3); // Сумма
                                DateTime requestDate = reader.GetDateTime(4); // ДатаЗаявки

                                if (!requestsDict.ContainsKey(requestId))
                                {
                                    requestsDict[requestId] = new RequestData(requestId, totalPrice, requestDate);
                                }
                                requestsDict[requestId].AddService(serviceName, quantity);
                            }

                            requestOrderedServices.AddRange(requestsDict.Values);
                        }
                    }
                }

                // Создание карточек
                List<Panel> cards = new List<Panel>();
                foreach (var request in requestOrderedServices)
                {
                    Panel card = CreateOrderCard(request);
                    cards.Add(card);
                }

                // Размещение карточек
                PlaceOrderCards(cards);

                // Добавление кнопки "Подтвердить выбор"
                Button btnConfirmSelection = new Button
                {
                    Location = new Point(mainPanel.Width - 180 - 10, 10), // Как BTN_OPEN_CART в PlaceServiceCards
                    Size = new Size(180, 40),
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    Text = "Подтвердить выбор",
                    BackColor = Color.White
                };
                btnConfirmSelection.Click += (s, e) =>
                {
                    if (selectedOrderId == -1)
                    {
                        MessageBox.Show("Выберите заявку для возврата.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Проверяем, не старше ли заявка 10 дней
                    var selectedRequest = requestOrderedServices.FirstOrDefault(r => r.RequestId == selectedOrderId);
                    if (selectedRequest != null && (DateTime.Now - selectedRequest.RequestDate).TotalDays > 10)
                    {
                        MessageBox.Show("Возврат для заявок старше 10 дней невозможен.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Перерисовываем форму с данными выбранной заявки
                    ShowSelectedRequestForm(selectedRequest);
                };

                mainPanel.Controls.Add(btnConfirmSelection);
                btnConfirmSelection.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки заявок: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowSelectedRequestForm(RequestData selectedRequest)
        {
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(BTN_RETURN);
            BTN_RETURN.Location = new Point(10, 10);
            BTN_RETURN.BringToFront();

            // Панель с данными заявки
            Panel requestPanel = new Panel
            {
                Size = new Size(600, 400),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Location = new Point((mainPanel.Width - 600) / 2, 50)
            };

            Label lblId = new Label
            {
                Location = new Point(10, 20),
                AutoSize = true,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Text = $"Номер заявки: {selectedRequest.RequestId}"
            };

            Label lblTotalPrice = new Label
            {
                Location = new Point(10, 50),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F),
                Text = $"Сумма: {selectedRequest.TotalPrice} руб."
            };

            Label lblRequestDate = new Label
            {
                Location = new Point(10, 80),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F),
                Text = $"Дата заявки: {selectedRequest.RequestDate:yyyy-MM-dd}"
            };

            Label lblServicesHeader = new Label
            {
                Location = new Point(10, 110),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Text = "Услуги:"
            };

            int yOffset = 130;
            int serviceHeight = 20;
            foreach (var service in selectedRequest.Services)
            {
                Label lblService = new Label
                {
                    Location = new Point(10, yOffset),
                    Size = new Size(580, 20),
                    Font = new Font("Segoe UI", 10F),
                    Text = $"{service.Key} (кол-во: {service.Value})"
                };
                requestPanel.Controls.Add(lblService);
                yOffset += serviceHeight;
            }

            // Поля для возврата
            Label lblReturnMethod = new Label
            {
                Location = new Point(10, yOffset + 20),
                Size = new Size(200, 30),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Text = "Способ возврата:"
            };

            ComboBox cmbReturnMethod = new ComboBox
            {
                Location = new Point(220, yOffset + 20),
                Size = new Size(350, 30),
                Font = new Font("Segoe UI", 10F),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbReturnMethod.Items.AddRange(new string[] { "Электронный", "Наличные" });

            Label lblReturnDate = new Label
            {
                Location = new Point(10, yOffset + 60),
                Size = new Size(200, 30),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Text = "Дата возврата:"
            };

            DateTimePicker dtpReturnDate = new DateTimePicker
            {
                Location = new Point(220, yOffset + 60),
                Size = new Size(350, 30),
                Font = new Font("Segoe UI", 10F),
                Format = DateTimePickerFormat.Short,
                MinDate = DateTime.Today
            };

            Button btnSubmitReturn = new Button
            {
                Location = new Point(220, yOffset + 100),
                Size = new Size(350, 40),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Text = "Создать заявку",
                BackColor = Color.White
            };
            btnSubmitReturn.Click += async (s, e) =>
            {
                await SubmitReturnRequestAsync(cmbReturnMethod.SelectedItem?.ToString(), dtpReturnDate.Value);
            };

            requestPanel.Controls.AddRange(new Control[] { lblId, lblTotalPrice, lblRequestDate, lblServicesHeader, lblReturnMethod, cmbReturnMethod, lblReturnDate, dtpReturnDate, btnSubmitReturn });
            mainPanel.Controls.Add(requestPanel);
            requestPanel.BringToFront();
            BTN_RETURN.BringToFront();
        }
    }
}