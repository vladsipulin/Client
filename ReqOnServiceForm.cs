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
        private readonly Dictionary<int, int> serviceQuantities = new Dictionary<int, int>();
        private readonly System.Windows.Forms.Timer inactivityTimer = new System.Windows.Forms.Timer();
        private IReqOnService _repo;
        private DataTable servicesData;
        private int? selectedOrgId = null; // Номер выбранной организации
        private int? contractTypeId = null; // Номер типа договора
        private int discountPercentage = 0; // Размер скидки в процентах

        public ReqOnServiceForm(List<ServiceData> selectedServices, string userId)
        {
            InitializeComponent();
            this.selectedServices = selectedServices ?? new List<ServiceData>();
            тбНКл.Text = userId;
            lbWhoLogged.Text = userId;

            inactivityTimer.Interval = 60000;
            inactivityTimer.Tick += InactivityTimer_Tick;
            inactivityTimer.Start();

            MouseMove += ResetInactivityTimer;
            KeyPress += ResetInactivityTimer;
            MouseClick += ResetInactivityTimer;
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

        private async void ReqOnService_Load(object sender, EventArgs e)
        {
            _repo = new RReqOnService();
            this.BackColor = Color.White;

            тбНКл.Enabled = false;
            тбДатаЗаявки.Enabled = false;
            тбСумма.ReadOnly = true;
            TB_DISCOUNT.ReadOnly = true;

            SetupDataGridView();
            LoadSelectedServices();
            await LoadOrganizationsAsync();

            тбДатаЗаявки.Value = DateTime.Now;
            UpdateTotalSum();

            тбСрокОплаты.Enabled = false;
            тбСрокОплаты.Value = DateTime.Now.AddDays(2);

            // Настройка обработчиков событий
            CHKBOX_FROM_ORGANIZATION.CheckedChanged += CHKBOX_FROM_ORGANIZATION_CheckedChanged;
            CMBX_ORGANIZATION.SelectedIndexChanged += CMBX_ORGANIZATION_SelectedIndexChanged;
        }

        private async Task LoadOrganizationsAsync()
        {
            try
            {
                // Создаем объект ComboBoxDataForFill с SQL-запросом и настройками
                var comboData = new ComboBoxDataForFill(
                    sql: @"
                SELECT o.НОрг, o.Наименование
                FROM Организация o
                JOIN ДоговорСОрганизацией do ON o.НОрг = do.НОрг
                WHERE do.Действует = TRUE AND do.РасторгнутДосрочно = FALSE
                ORDER BY o.Наименование",
                    displayMember: "Наименование",
                    valueMember: "НОрг"
                );

                // Вызываем метод LoadCombo для заполнения DataTable
                LoadCombo(comboData);

                // Привязываем данные к CMBX_ORGANIZATION
                CMBX_ORGANIZATION.DataSource = comboData.dataSource;
                CMBX_ORGANIZATION.DisplayMember = comboData.DisplayMember;
                CMBX_ORGANIZATION.ValueMember = comboData.ValueMember;

                // Отключаем CHKBOX_FROM_ORGANIZATION, если нет организаций
                CHKBOX_FROM_ORGANIZATION.Enabled = CMBX_ORGANIZATION.Items.Count > 0;
                if (CMBX_ORGANIZATION.Items.Count == 0)
                {
                    CHKBOX_FROM_ORGANIZATION.Checked = false;
                    CMBX_ORGANIZATION.Enabled = false;
                    TB_DISCOUNT.Text = "Скидка: 0%";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки списка организаций: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHKBOX_FROM_ORGANIZATION.Enabled = false;
                CMBX_ORGANIZATION.Enabled = false;
                TB_DISCOUNT.Text = "Скидка: 0%";
            }
        }

        private void LoadCombo(ComboBoxDataForFill obj)
        {
            MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConn"].ConnectionString);
            MySqlCommand cmd = null;
            MySqlDataAdapter da = null;
            DataTable dt = null;

            try
            {
                con.Open();
                cmd = new MySqlCommand();
                cmd.Connection = con;
                cmd.CommandText = obj.sql;
                foreach (MySqlParameter e in obj.paramsForSQLQuery)
                {
                    cmd.Parameters.Add(e);
                }
                da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                dt = new DataTable();
                da.Fill(dt);

                obj.dataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
                cmd?.Dispose();
                da?.Dispose();
            }
        }

        class ComboBoxDataForFill
        {
            public string sql { get; set; }
            public string DisplayMember { get; set; }
            public string ValueMember { get; set; }
            public DataTable dataSource { get; set; }
            public List<MySqlParameter> paramsForSQLQuery { get; set; }

            public ComboBoxDataForFill(string sql, string displayMember, string valueMember)
            {
                this.sql = sql;
                DisplayMember = displayMember;
                ValueMember = valueMember;
                dataSource = new DataTable();
                paramsForSQLQuery = new List<MySqlParameter>();
            }
        }

        private async void CHKBOX_FROM_ORGANIZATION_CheckedChanged(object sender, EventArgs e)
        {
            CMBX_ORGANIZATION.Enabled = CHKBOX_FROM_ORGANIZATION.Checked && CMBX_ORGANIZATION.Items.Count > 0;
            if (!CHKBOX_FROM_ORGANIZATION.Checked)
            {
                CMBX_ORGANIZATION.SelectedIndex = -1;
                selectedOrgId = null;
                contractTypeId = null;
                discountPercentage = 0;
                TB_DISCOUNT.Text = "Скидка: 0%";
                UpdateTotalSum();
            }
        }

        private async void CMBX_ORGANIZATION_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CMBX_ORGANIZATION.SelectedIndex >= 0)
            {
                selectedOrgId = (int)CMBX_ORGANIZATION.SelectedValue;
                var contractInfo = await GetContractInfoAsync(selectedOrgId.Value);
                contractTypeId = contractInfo.НТипаДоговора;
                discountPercentage = contractInfo.Скидка;
                TB_DISCOUNT.Text = $"Скидка: {discountPercentage}%";
                if (contractInfo.НТипаДоговора == null)
                {
                    MessageBox.Show("Для выбранной организации нет действующего договора.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                selectedOrgId = null;
                contractTypeId = null;
                discountPercentage = 0;
                TB_DISCOUNT.Text = "Скидка: 0%";
            }
            UpdateTotalSum();
        }

        private async Task<(int? НТипаДоговора, int Скидка)> GetContractInfoAsync(int orgId)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["MySqlConn"].ConnectionString;
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string query = @"
                        SELECT do.НТипаДоговора, td.Скидка
                        FROM ДоговорСОрганизацией do
                        JOIN ТипДоговора td ON do.НТипаДоговора = td.НТипаДоговора
                        WHERE do.НОрг = @orgId AND do.Действует = TRUE AND do.РасторгнутДосрочно = FALSE
                        LIMIT 1";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@orgId", orgId);
                        using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                int нТипаДоговора = reader.GetInt32("НТипаДоговора");
                                int скидка = reader.GetInt32("Скидка");
                                return (нТипаДоговора, скидка);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении данных договора: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return (null, 0);
        }

        private void SetupDataGridView()
        {
            dgvSelectedServices.Columns.Clear();
            dgvSelectedServices.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn idColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "НУслуги",
                Name = "НУслуги",
                HeaderText = "Номер услуги",
                Width = 60,
                ReadOnly = true,
                HeaderCell = { Style = { Font = new Font("Segoe UI", 12F) } }
            };

            DataGridViewTextBoxColumn nameColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Наименование",
                Name = "Наименование",
                HeaderText = "Наименование",
                Width = 280,
                ReadOnly = true,
                HeaderCell = { Style = { Font = new Font("Segoe UI", 12F) } }
            };

            DataGridViewTextBoxColumn priceColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Цена",
                Name = "Цена",
                HeaderText = "Цена, руб.",
                Width = 120,
                ReadOnly = true,
                HeaderCell = { Style = { Font = new Font("Segoe UI", 12F) } }
            };

            DataGridViewTextBoxColumn quantityColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Количество",
                Name = "Количество",
                HeaderText = "Количество",
                Width = 100,
                ReadOnly = true,
                HeaderCell = { Style = { Font = new Font("Segoe UI", 12F) } }
            };

            DataGridViewButtonColumn plusButton = new DataGridViewButtonColumn
            {
                HeaderText = "",
                Text = "+",
                Name = "PlusButton",
                Width = 50,
                UseColumnTextForButtonValue = true,
                HeaderCell = { Style = { Font = new Font("Segoe UI", 12F) } }
            };

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
            dgvSelectedServices.CellContentClick += DgvSelectedServices_CellContentClick;
        }

        private void LoadSelectedServices()
        {
            try
            {
                servicesData = new DataTable();
                servicesData.Columns.Add("НУслуги", typeof(int));
                servicesData.Columns.Add("Наименование", typeof(string));
                servicesData.Columns.Add("Цена", typeof(double));
                servicesData.Columns.Add("Количество", typeof(int));

                foreach (var service in selectedServices)
                {
                    servicesData.Rows.Add(service.Id, service.Name, service.Price, 1);
                    serviceQuantities[service.Id] = 1;
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
                int serviceId = Convert.ToInt32(dgvSelectedServices.Rows[e.RowIndex].Cells["НУслуги"].Value);
                DataRow row = servicesData.AsEnumerable().First(r => Convert.ToInt32(r["НУслуги"]) == serviceId);
                var service = selectedServices.First(s => s.Id == serviceId);

                int currentQuantity = serviceQuantities.ContainsKey(serviceId) ? serviceQuantities[serviceId] : 1;
                int maxQuantity = service.Quantity;

                if (e.ColumnIndex == dgvSelectedServices.Columns["PlusButton"].Index)
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
                else if (e.ColumnIndex == dgvSelectedServices.Columns["MinusButton"].Index)
                {
                    if (currentQuantity > 1)
                    {
                        currentQuantity--;
                        serviceQuantities[serviceId] = currentQuantity;
                        row["Количество"] = currentQuantity;
                    }
                }

                dgvSelectedServices.Refresh();
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
                    int serviceId = Convert.ToInt32(row["НУслуги"]);
                    float price = Convert.ToSingle(row["Цена"]);
                    int quantity = serviceQuantities.ContainsKey(serviceId) ? serviceQuantities[serviceId] : 1;
                    totalSum += price * quantity;
                }
                float discountedSum = discountPercentage > 0 ? totalSum * (1 - discountPercentage / 100f) : totalSum;
                тбСумма.Text = discountedSum.ToString("F2");
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
                    smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SmtpUsername"],
                                                             ConfigurationManager.AppSettings["SmtpPassword"]);
                    smtp.EnableSsl = true;
                    smtp.Send(m);
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
            return false;
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

                if (CHKBOX_FROM_ORGANIZATION.Checked && CMBX_ORGANIZATION.SelectedIndex < 0)
                {
                    MessageBox.Show("Выберите организацию.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Random rand = new Random();
                int номерЗаявки = 0;
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

                float totalSum = 0;
                foreach (DataRow row in servicesData.Rows)
                {
                    int serviceId = Convert.ToInt32(row["НУслуги"]);
                    float price = Convert.ToSingle(row["Цена"]);
                    int quantity = serviceQuantities.ContainsKey(serviceId) ? serviceQuantities[serviceId] : 1;
                    totalSum += price * quantity;
                }
                float discountedSum = discountPercentage > 0 ? totalSum * (1 - discountPercentage / 100f) : totalSum;

                string servicesSummary = string.Join("\n", servicesData.AsEnumerable().Select(row =>
                {
                    int serviceId = Convert.ToInt32(row["НУслуги"]);
                    int quantity = serviceQuantities.ContainsKey(serviceId) ? serviceQuantities[serviceId] : 1;
                    float price = Convert.ToSingle(row["Цена"]);
                    float sum = price * quantity;
                    float discountedPrice = discountPercentage > 0 ? sum * (1 - discountPercentage / 100f) : sum;
                    return $"Услуга: {row["Наименование"]}, Количество: {quantity}, Сумма: {discountedPrice:F2} руб.";
                }));

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

                    **Общая сумма:** {discountedSum:F2} руб.{(discountPercentage > 0 ? $"\n**Скидка:** {discountPercentage}%" : "")}

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

                foreach (DataRow row in servicesData.Rows)
                {
                    int serviceId = Convert.ToInt32(row["НУслуги"]);
                    int quantity = serviceQuantities.ContainsKey(serviceId) ? serviceQuantities[serviceId] : 1;
                    float price = Convert.ToSingle(row["Цена"]);
                    float sum = price * quantity;
                    float discountedPrice = discountPercentage > 0 ? sum * (1 - discountPercentage / 100f) : sum;

                    bool покупкаСовершена = RADIOBTN_ONLINEPAY.Checked;
                    DateTime? датаОплаты = покупкаСовершена ? DateTime.Now : null;

                    ReqOnService request = new ReqOnService(
                        нЗаявки: номерЗаявки,
                        нУслуги: serviceId,
                        срокОплаты: тбСрокОплаты.Value,
                        нКл: clientId,
                        нТипаДоговора: CHKBOX_FROM_ORGANIZATION.Checked ? contractTypeId : null,
                        нОрг: CHKBOX_FROM_ORGANIZATION.Checked ? selectedOrgId : null,
                        количество_Ед: quantity,
                        сумма: sum,
                        датаЗаявки: тбДатаЗаявки.Value,
                        покупкаСовершена: покупкаСовершена,
                        нС: 0,
                        датаОплаты: датаОплаты,
                        размерШтрафа: 0f,
                        суммаКОплате: discountedPrice
                    );

                    Result<int> result = await _repo.Add(request);
                    if (!result.HasValue)
                    {
                        MessageBox.Show($"Ошибка при создании заявки для услуги {row["Наименование"]}: {result.Error}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                if (RADIOBTN_ONLINEPAY.Checked)
                {
                    bool isCashRegisterAvailable = CheckOnlineCashServiceAvailability();
                    if (isCashRegisterAvailable)
                    {
                        MessageBox.Show($"Заявка #{номерЗаявки} успешно создана!\n\nИтоговые данные:\n{servicesSummary}\n\nОбщая сумма: {discountedSum:F2} руб.{(discountPercentage > 0 ? $"\nСкидка: {discountPercentage}%" : "")}\n\nОплата подтверждена через онлайн-кассу.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"Заявка #{номерЗаявки} успешно создана!\n\nИтоговые данные:\n{servicesSummary}\n\nОбщая сумма: {discountedSum:F2} руб.{(discountPercentage > 0 ? $"\nСкидка: {discountPercentage}%" : "")}\n\nОнлайн-касса недоступна. Реквизиты для оплаты отправлены на ваш email.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show($"Заявка #{номерЗаявки} успешно создана!\n\nИтоговые данные:\n{servicesSummary}\n\nОбщая сумма: {discountedSum:F2} руб.{(discountPercentage > 0 ? $"\nСкидка: {discountPercentage}%" : "")}\n\nПожалуйста, подойдите к портье для оплаты. Помните про срок оплаты, иначе будет начислен штраф в соответствии с Уставом.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
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