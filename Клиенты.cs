using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Client.Interfaces;
using Client.Models;
using Client.Services;
using Client.Utils;
using Microsoft.VisualBasic;

namespace Client
{
    public partial class Клиенты : Form
    {
        //Источник данных для DGV
        private BindingSource _bsDbClients;
        //редактируемый сотрудник
        private BindingSource _bsCurrentDbClient;
        //работа с БД
        private IDbClientRepository _repo;
        private bool IsAddMethodCalled = false;

        public Клиенты()
        {
            InitializeComponent();
        }

        private async void LoadData()
        {
            //получаем
            var result = await _repo.GetDbClients();
            if (result)
            {
                //извлекаем
                List<DbClient> clients = result.Value;
                //пронумеровываем
                int i = 1;
                clients.ForEach(e => e.OrderNumber = i++);
                //отображаем
                _bsDbClients.DataSource = clients;
                _bsDbClients.MoveFirst();
                SetCurrentDbClient();
            }
            else
            {
                MessageBox.Show(result.Error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void SetCurrentDbClient()
        {
            if (_bsDbClients.Count > 0)
            {
                _bsCurrentDbClient.List[0] = DbClient.GetClone((DbClient)_bsDbClients.Current);

            }
            else
            {
                _bsCurrentDbClient.List[0] = new DbClient(0);
            }

            _bsCurrentDbClient.ResetItem(0);
        }

        private void SetBindings()
        {
            _bsDbClients = new BindingSource();
            _bsDbClients.DataSource = typeof(List<DbClient>);

            //привязки для clientDGV
            clientsDGV.AutoGenerateColumns = false;
            clientsDGV.DataSource = _bsDbClients;

            //привязки у столбцов
            НКл.DataPropertyName = nameof(DbClient.НКл);
            ФИО.DataPropertyName = nameof(DbClient.ФИО);
            Пол.DataPropertyName = nameof(DbClient.Пол);
            ДатаРождения.DataPropertyName = nameof(DbClient.ДатаРождения);
            Логин.DataPropertyName = nameof(DbClient.Логин);
            Пароль.DataPropertyName = nameof(DbClient.Пароль);
            Email.DataPropertyName = nameof(DbClient.Email);

            //текстбоксы
            _bsCurrentDbClient = new BindingSource();
            _bsCurrentDbClient.DataSource = new List<DbClient> { new DbClient(0) };
            тбНКл.DataBindings.Add("Text", _bsCurrentDbClient, nameof(DbClient.НКл));
            тбФИО.DataBindings.Add("Text", _bsCurrentDbClient, nameof(DbClient.ФИО));
            тбПол.DataBindings.Add("Text", _bsCurrentDbClient, nameof(DbClient.Пол));
            тбДатаРождения.DataBindings.Add("Text", _bsCurrentDbClient, nameof(DbClient.ДатаРождения));
        }

        private void Клиенты_Load(object sender, EventArgs e)
        {
            _repo = new MySqlRepository();
            SetBindings();
            LoadData();
            clientsDGV.MouseClick += (s, a) => SetCurrentDbClient();

        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            IsAddMethodCalled = true;
            btnUpd.Text = "Сохранить";
            //курсор на последнего
            _bsDbClients.MoveLast();
            //след.порядковый номер
            int number = (_bsDbClients.Current as DbClient).OrderNumber + 1;
            //добавляем нового
            _bsDbClients.Add(new DbClient(0) { OrderNumber = number });
            //выделяем его
            _bsDbClients.MoveNext();
            SetCurrentDbClient();
            //выделяем НКл для редактирования
            тбНКл.Focus();
        }

        private void SwitchOnWaiting()
        {
            foreach (var button in this.Controls.OfType<Button>())
            {
                button.Enabled = false;
            }
            clientsDGV.Enabled = false;
            Cursor = Cursors.WaitCursor;
        }

        private void SwitchOffWaiting()
        {
            foreach (var button in this.Controls.OfType<Button>())
            {
                button.Enabled = true;
            }
            clientsDGV.Enabled = true;
            Cursor = Cursors.Default;
        }

        private bool IsCorrectCurrent(DbClient client)
        {
            bool isCorrect = true;

            string maskForStr = "<?>";
            if (String.IsNullOrWhiteSpace(client.ФИО)
                || String.IsNullOrEmpty(client.ФИО)
                || client.ФИО.Equals(maskForStr))
            {
                isCorrect = false;
                var message = "Введите правильно ФИО клиента: 'Фамилия Имя Отчество'";
                MessageBox.Show(message, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (String.IsNullOrWhiteSpace(client.Пол)
                || String.IsNullOrEmpty(client.Пол)
                || client.Пол.Equals(maskForStr))
            {
                isCorrect = false;
                var message = "Введите правильно пол клиента: 'муж' или 'жен'";
                MessageBox.Show(message, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return isCorrect;
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            _bsDbClients.MoveNext();
            SetCurrentDbClient();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            _bsDbClients.MovePrevious();
            SetCurrentDbClient();
        }

        private async void btnUpd_Click(object sender, EventArgs e)
        {
            SwitchOnWaiting();
            var current = (DbClient)_bsCurrentDbClient.Current;

            SetCurrentDbClient();
            var СтрокаСоСтарымНКл = (DbClient)_bsCurrentDbClient.Current;
            int НКлОлд = СтрокаСоСтарымНКл.НКл;

            if (!IsCorrectCurrent(current))
            {
                return;
            }

            Result<int> result;
            try
            {
                if (IsAddMethodCalled)
                {
                    //добавляем нового сотрудника
                    IsAddMethodCalled = false;
                    btnUpd.Text = "Обновить";
                    result = await _repo.AddDbClient(current);
                }
                else
                {
                    //иначе обновляем существующего сотрудника
                    result = await _repo.UpdateDbClient(current, НКлОлд);
                }

                if (result)
                {
                    //перечитываем данные
                    LoadData();
                }
            }
            finally
            {
                SwitchOffWaiting();
            }
            if (!result)
            {
                MessageBox.Show(result.Error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnRem_Click(object sender, EventArgs e)
        {
            SwitchOnWaiting();

            //получаем текущего
            var dbclient = (DbClient)_bsDbClients.Current;
            Result<int> result;
            try
            {
                //удаляем из БД
                result = await _repo.RemoveDbClient(dbclient.НКл);
                if (result)
                {
                    //удаляем из отображения
                    _bsDbClients.Remove(dbclient);
                    _bsDbClients.MoveFirst();
                    SetCurrentDbClient();
                }
            }
            finally
            {
                SwitchOffWaiting();
            }

            if (!result)
            {
                MessageBox.Show(result.Error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
