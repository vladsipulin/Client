using Client.Interfaces;
using Client.Models;
using Client.Services;
using Client.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class ReqOnServiceForm : Form
    {
        private readonly List<ServiceData> selectedServices;
        private readonly Dictionary<int, int> serviceQuantities = new Dictionary<int, int>(); // ID услуги -> Количество
        private readonly System.Windows.Forms.Timer inactivityTimer = new System.Windows.Forms.Timer();
        private IReqOnService _repo;
        private DataTable servicesData;

        public ReqOnServiceForm(List<ServiceData> selectedServices, string userId)
        {
            InitializeComponent();
            this.selectedServices = selectedServices ?? new List<ServiceData>();
            тбНКл.Text = userId;
            lbWhoLogged.Text = userId;

            // Настройка таймера неактивности (1 минута)
            inactivityTimer.Interval = 60000; // 1 минута
            inactivityTimer.Tick += InactivityTimer_Tick;
            inactivityTimer.Start();

            // Сбрасываем таймер при взаимодействии с формой
            MouseMove += ResetInactivityTimer;
            KeyPress += ResetInactivityTimer;
            MouseClick += ResetInactivityTimer; // Для сенсорного ввода
        }

        private void ResetInactivityTimer(object sender, EventArgs e)
        {
            inactivityTimer.Stop();
            inactivityTimer.Start();
        }

        private void InactivityTimer_Tick(object sender, EventArgs e)
        {
            inactivityTimer.Stop();
            this.Close();
        }

        private void ReqOnService_Load(object sender, EventArgs e)
        {
            _repo = new RReqOnService();
            this.BackColor = Color.White;

            тбНКл.Enabled = false;
            тбДатаЗаявки.Enabled = false;
            тбСумма.ReadOnly = true;

            // Настройка DataGridView
            SetupDataGridView();

            // Загрузка данных из переданного списка
            LoadSelectedServices();

            // Установка текущей даты
            тбДатаЗаявки.Value = DateTime.Now;

            // Пересчёт суммы
            UpdateTotalSum();

            // По Уставу компании срок оплаты составляет не более двух суток с момента оплаты
            тбСрокОплаты.Enabled = false;
            тбСрокОплаты.Value = DateTime.Now.AddDays(2); 
        }

        private void SetupDataGridView()
        {
            dgvSelectedServices.Columns.Clear();
            dgvSelectedServices.AutoGenerateColumns = false;

            // Колонка для ID
            DataGridViewTextBoxColumn idColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "НСл",
                Name = "НСл",
                HeaderText = "Номер услуги",
                Width = 60,
                ReadOnly = true,
                HeaderCell = { Style = { Font = new Font("Segoe UI", 12F) } }
            };

            // Колонка для наименования
            DataGridViewTextBoxColumn nameColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Наименование",
                Name = "Наименование",
                HeaderText = "Наименование",
                Width = 280,
                ReadOnly = true,
                HeaderCell = { Style = { Font = new Font("Segoe UI", 12F) } }
            };

            // Колонка для цены
            DataGridViewTextBoxColumn priceColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Цена",
                Name = "Цена",
                HeaderText = "Цена, руб.",
                Width = 120,
                ReadOnly = true,
                HeaderCell = { Style = { Font = new Font("Segoe UI", 12F) } }
            };

            // Колонка для количества (только отображение)
            DataGridViewTextBoxColumn quantityColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Количество",
                Name = "Количество",
                HeaderText = "Количество",
                Width = 100,
                ReadOnly = true,
                HeaderCell = { Style = { Font = new Font("Segoe UI", 12F) } }
            };

            // Кнопка "+"
            DataGridViewButtonColumn plusButton = new DataGridViewButtonColumn
            {
                HeaderText = "",
                Text = "+",
                Name = "PlusButton",
                Width = 50,
                UseColumnTextForButtonValue = true,
                HeaderCell = { Style = { Font = new Font("Segoe UI", 12F) } }
            };

            // Кнопка "−"
            DataGridViewButtonColumn minusButton = new DataGridViewButtonColumn
            {
                HeaderText = "",
                Text = "−",
                Name = "MinusButton",
                Width = 50,
                UseColumnTextForButtonValue = true,
                HeaderCell = { Style = { Font = new Font("Segoe UI", 12F) } }
            };

            dgvSelectedServices.Columns.AddRange(new DataGridViewColumn[] { idColumn, nameColumn, priceColumn, quantityColumn, plusButton, minusButton });

            // Обработчик нажатий на кнопки
            dgvSelectedServices.CellContentClick += DgvSelectedServices_CellContentClick;
        }

        private void LoadSelectedServices()
        {
            try
            {
                servicesData = new DataTable();
                servicesData.Columns.Add("НСл", typeof(int));
                servicesData.Columns.Add("Наименование", typeof(string));
                servicesData.Columns.Add("Цена", typeof(double));
                servicesData.Columns.Add("Количество", typeof(int));

                // Заполняем DataTable из переданных данных
                foreach (var service in selectedServices)
                {
                    servicesData.Rows.Add(service.Id, service.Name, service.Price, 1);
                    serviceQuantities[service.Id] = 1; // Начальное количество
                }

                dgvSelectedServices.DataSource = servicesData;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки услуг: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvSelectedServices_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                int serviceId = Convert.ToInt32(dgvSelectedServices.Rows[e.RowIndex].Cells["НСл"].Value);
                DataRow row = servicesData.AsEnumerable().First(r => Convert.ToInt32(r["НСл"]) == serviceId);
                var service = selectedServices.First(s => s.Id == serviceId);

                int currentQuantity = serviceQuantities.ContainsKey(serviceId) ? serviceQuantities[serviceId] : 1;
                int maxQuantity = service.Quantity; // Максимальное количество из переданных данных

                if (e.ColumnIndex == dgvSelectedServices.Columns["PlusButton"].Index) // Кнопка "+"
                {
                    if (currentQuantity < maxQuantity)
                    {
                        currentQuantity++;
                        serviceQuantities[serviceId] = currentQuantity;
                        row["Количество"] = currentQuantity;
                    }
                    else
                    {
                        MessageBox.Show($"Максимальное количество для услуги {row["Наименование"]}: {maxQuantity}.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else if (e.ColumnIndex == dgvSelectedServices.Columns["MinusButton"].Index) // Кнопка "−"
                {
                    if (currentQuantity > 1)
                    {
                        currentQuantity--;
                        serviceQuantities[serviceId] = currentQuantity;
                        row["Количество"] = currentQuantity;
                    }
                }

                dgvSelectedServices.Refresh(); // Обновляем отображение
                UpdateTotalSum();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при изменении количества: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateTotalSum()
        {
            try
            {
                float totalSum = 0;
                foreach (DataRow row in servicesData.Rows)
                {
                    int serviceId = Convert.ToInt32(row["НСл"]);
                    float price = Convert.ToSingle(row["Цена"]);
                    int quantity = serviceQuantities.ContainsKey(serviceId) ? serviceQuantities[serviceId] : 1;
                    totalSum += price * quantity;
                }
                тбСумма.Text = totalSum.ToString("F2");
            }
            catch (Exception ex)
            {
                тбСумма.Text = "Ошибка";
                MessageBox.Show($"Ошибка пересчёта суммы: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static async void SendEmail(string clientmail, string subject, string body)
        {
            await Task.Run(() =>
            {
                try
                {
                    MailAddress from = new MailAddress("vladsipulin@mail.ru", "База отдыха «Обуховка»");
                    MailAddress to = new MailAddress(clientmail);
                    MailMessage m = new MailMessage(from, to);
                    m.Subject = subject;
                    m.Body = body;
                    SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);
                    smtp.Credentials = new NetworkCredential("vladsipulin@mail.ru", "Dx0i5QtBtp1EmzPXE76A");
                    smtp.EnableSsl = true;
                    smtp.Send(m);
                    //MessageBox.Show("На вашу электронную почту " + clientmail + " отправлены реквизиты для оплаты заявки на услуги", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    MessageBox.Show("Ошибка при попытке отправки письма", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });
        }

        private async Task<string> GetClientEmailAsync(int clientId)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["MySqlConn"].ConnectionString;
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string query = "SELECT Email FROM Клиент WHERE НКл = @clientId";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@clientId", clientId);
                        var result = await command.ExecuteScalarAsync();
                        return result?.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении email клиента: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private bool CheckOnlineCashServiceAvailability()
        {
            return false; // Пока не реализован способ оплаты через сервис онлайн оплаты
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(lbWhoLogged.Text, out int clientId))
                {
                    MessageBox.Show("Некорректный ID клиента.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Random rand = new Random();
                int номерЗаявки = 0; // Инициализация по умолчанию
                bool isUnique = false;

                while (!isUnique)
                {
                    номерЗаявки = rand.Next(10000, 99999);
                    var existing = await _repo.GetIdByRequest(номерЗаявки);
                    if (existing == 0)
                    {
                        isUnique = true;
                    }
                }

                // Формируем содержание заказа
                string servicesSummary = string.Join("\n", servicesData.AsEnumerable().Select(row =>
                {
                    int serviceId = Convert.ToInt32(row["НСл"]);
                    int quantity = serviceQuantities.ContainsKey(serviceId) ? serviceQuantities[serviceId] : 1;
                    return $"Услуга: {row["Наименование"]}, Количество: {quantity}, Сумма: {Convert.ToSingle(row["Цена"]) * quantity:F2} руб.";
                }));

                // Отправка письма, если выбран онлайн-платёж
                if (RADIOBTN_ONLINEPAY.Checked)
                {
                    string clientEmail = await GetClientEmailAsync(clientId);
                    if (string.IsNullOrWhiteSpace(clientEmail))
                    {
                        MessageBox.Show("Не удалось получить email клиента.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string subject = $"Заявка #{номерЗаявки} - Реквизиты для оплаты";
                    string body = $@"Уважаемый клиент,

Ваша заявка #{номерЗаявки} успешно оформлена. Ниже приведено содержание заказа и реквизиты для оплаты.

**Содержание заказа:**
{servicesSummary}

**Общая сумма:** {тбСумма.Text} руб.

**Реквизиты для оплаты:**
Получатель: Общество с ограниченной ответственностью «Ивановка»
Банк: Филиал ПАО Банк ВТБ в г. Воронеже
Расчетный счет: 4070281020625000210
Корреспондентский счет: 30101810100000000835
БИК: 042007835
ИНН: 3128066522
КПП: 312801001
ОГРН: 1083128002198

Пожалуйста, произведите оплату до {тбСрокОплаты.Value:dd.MM.yyyy}. После оплаты сохраните подтверждение.

С уважением,
База отдыха «Обуховка»";

                    SendEmail(clientEmail, subject, body);
                }

                // Упаковываем данные с каждой строки dataGridView (servicesData) в объект класса ReqOnService
                // и вносим их в БД, создав объект класса Result 
                foreach (DataRow row in servicesData.Rows)
                {
                    int serviceId = Convert.ToInt32(row["НСл"]);
                    int quantity = serviceQuantities.ContainsKey(serviceId) ? serviceQuantities[serviceId] : 1;
                    float price = Convert.ToSingle(row["Цена"]);
                    float sum = price * quantity;

                    // Устанавливаем ПокупкаСовершена: true для онлайн-платежа при доступной кассе, иначе false
                    bool покупкаСовершена = RADIOBTN_ONLINEPAY.Checked;

                    ReqOnService request = new ReqOnService(
                        номерЗаявки,
                        serviceId,
                        тбСрокОплаты.Value,
                        clientId,
                        quantity,
                        sum,
                        тбДатаЗаявки.Value,
                        покупкаСовершена
                    );

                    Result<int> result = await _repo.Add(request);
                    if (!result)
                    {
                        MessageBox.Show($"Ошибка при создании заявки для услуги {row["Наименование"]}: {result.Error}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // Формируем итоговые данные для сообщения
                if (RADIOBTN_ONLINEPAY.Checked)
                {
                    bool isCashRegisterAvailable = CheckOnlineCashServiceAvailability();
                    if (isCashRegisterAvailable)
                    {
                        MessageBox.Show($"Заявка #{номерЗаявки} успешно создана!\n\nИтоговые данные:\n{servicesSummary}\n\nОбщая сумма: {тбСумма.Text} руб.\n\nОплата подтверждена через онлайн-кассу.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"Заявка #{номерЗаявки} успешно создана!\n\nИтоговые данные:\n{servicesSummary}\n\nОбщая сумма: {тбСумма.Text} руб.\n\nОнлайн-касса недоступна. Реквизиты для оплаты отправлены на ваш email.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else // RADIOBTN_CASHPAY.Checked
                {
                    MessageBox.Show($"Заявка #{номерЗаявки} успешно создана!\n\nИтоговые данные:\n{servicesSummary}\n\nОбщая сумма: {тбСумма.Text} руб.\n\nПожалуйста, подойдите к портье для оплаты. Помните про срок оплаты, иначе будет начислен штраф в соответствии с Уставом.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании заявки: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RADIOBTN_ONLINEPAY_CheckedChanged(object sender, EventArgs e)
        {
        }
    }
}