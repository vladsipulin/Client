using Client.Interfaces;
using Client.Models;
using Client.Services;
using Client.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Client
{
    public partial class Организация : Form
    {
        private BindingSource _bs;
        private BindingSource _bsCurrent;
        private IOrganization _repo;
        private bool IsAddMethodCalled = false;

        public Организация()
        {
            InitializeComponent();
        }

        private async void LoadData()
        {
            var result = await _repo.GetOrganization();
            if (result)
            {
                List<Models.Organization> objOfTable = result.Value;

                int i = 1;
                objOfTable.ForEach(e => e.OrderNumber = i++);

                _bs.DataSource = objOfTable;
                _bs.MoveFirst();
                SetCurrentRow();
            }
            else
            {
                MessageBox.Show(result.Error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void SetCurrentRow()
        {
            if (_bs.Count > 0)
            {
                _bsCurrent.List[0] = Models.Organization.GetClone((Models.Organization)_bs.Current);

            }
            else
            {
                _bsCurrent.List[0] = new Models.Organization(0);
            }

            _bsCurrent.ResetItem(0);
        }

        private void SetBindings()
        {
            _bs = new BindingSource();
            _bs.DataSource = typeof(List<Models.Organization>);

            orgsDGV.AutoGenerateColumns = false;
            orgsDGV.DataSource = _bs;

            НОрг.DataPropertyName = nameof(Models.Organization.НОрг);
            Наименование.DataPropertyName = nameof(Models.Organization.Наименование);
            СфераДеятельности.DataPropertyName = nameof(Models.Organization.СфераДеятельности);
            ДатаРегистрации.DataPropertyName = nameof(Models.Organization.ДатаРегистрации);

            _bsCurrent = new BindingSource();
            _bsCurrent.DataSource = new List<Models.Organization> { new Models.Organization(0) };
            тбНОрг.DataBindings.Add("Text", _bsCurrent, nameof(Models.Organization.НОрг));
            тбНаименование.DataBindings.Add("Text", _bsCurrent, nameof(Models.Organization.Наименование));
            тбСД.DataBindings.Add("Text", _bsCurrent, nameof(Models.Organization.СфераДеятельности));
            тбДР.DataBindings.Add("Text", _bsCurrent, nameof(Models.Organization.ДатаРегистрации));
        }

        private void SwitchOnWaiting()
        {
            foreach (var button in this.Controls.OfType<Button>())
            {
                button.Enabled = false;
            }
            orgsDGV.Enabled = false;
            Cursor = Cursors.WaitCursor;
        }

        private void SwitchOffWaiting()
        {
            foreach (var button in this.Controls.OfType<Button>())
            {
                button.Enabled = true;
            }
            orgsDGV.Enabled = true;
            Cursor = Cursors.Default;
        }

        private bool IsCorrectCurrent(Models.Organization obj)
        {
            bool isCorrect = true;

            string maskForStr = "<?>";
            if (String.IsNullOrWhiteSpace(obj.Наименование)
                || String.IsNullOrEmpty(obj.Наименование)
                || obj.Наименование.Equals(maskForStr))
            {
                isCorrect = false;
                var message = "Ошибка ввода наименования организации";
                MessageBox.Show(message, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (String.IsNullOrWhiteSpace(obj.СфераДеятельности)
                || String.IsNullOrEmpty(obj.СфераДеятельности)
                || obj.СфераДеятельности.Equals(maskForStr))
            {
                isCorrect = false;
                var message = "Ошибка ввода cферы деятельности организации";
                MessageBox.Show(message, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return isCorrect;
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            IsAddMethodCalled = true;
            btnUpd.Text = "Сохранить";
            _bs.MoveLast();
            int number = (_bs.Current as Models.Organization).OrderNumber + 1;
            _bs.Add(new Models.Organization(0) { OrderNumber = number });
            _bs.MoveNext();
            SetCurrentRow();
            тбНОрг.Focus();
        }

        private async void btnUpd_Click(object sender, EventArgs e)
        {
            SwitchOnWaiting();
            var current = (Models.Organization)_bsCurrent.Current;

            SetCurrentRow();
            var СтрокаСоСтарымID = (Models.Organization)_bsCurrent.Current;
            int НОргОлд = СтрокаСоСтарымID.НОрг;

            if (!IsCorrectCurrent(current))
            {
                return;
            }

            Result<int> result;
            try
            {
                if (IsAddMethodCalled)
                {
                    IsAddMethodCalled = false;
                    btnUpd.Text = "Обновить";
                    result = await _repo.AddOrganization(current);
                }
                else
                {
                    result = await _repo.UpdateOrganization(current, НОргОлд);
                }

                if (result)
                {
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

            var objRow = (Models.Organization)_bs.Current;
            Result<int> result;
            try
            {
                result = await _repo.RemoveOrganization(objRow.НОрг);
                if (result)
                {
                    _bs.Remove(objRow);
                    _bs.MoveFirst();
                    SetCurrentRow();
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

        private void btnForward_Click(object sender, EventArgs e)
        {
            _bs.MoveNext();
            SetCurrentRow();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            _bs.MovePrevious();
            SetCurrentRow();
        }

        private void Организация_Load(object sender, EventArgs e)
        {
            _repo = new Services.Organization();
            SetBindings();
            LoadData();
            orgsDGV.MouseClick += (s, a) => SetCurrentRow();
        }
    }
}
