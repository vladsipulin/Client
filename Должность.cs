using Client.Interfaces;
using Client.Services;
using Client.Models;
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

namespace Client
{
    public partial class Должность : Form
    {
        private BindingSource _bs;
        private BindingSource _bsCurrent;
        private IPosition _repo;
        private bool IsAddMethodCalled = false;

        private async void LoadData()
        {
            var result = await _repo.GetPosition();
            if (result)
            {
                List<Position> objOfTable = result.Value;

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
                _bsCurrent.List[0] = Position.GetClone((Position)_bs.Current);

            }
            else
            {
                _bsCurrent.List[0] = new Position(0);
            }

            _bsCurrent.ResetItem(0);
        }

        private void SetBindings()
        {
            _bs = new BindingSource();
            _bs.DataSource = typeof(List<Position>);

            dataTable.AutoGenerateColumns = false;
            dataTable.DataSource = _bs;

            НД.DataPropertyName = nameof(Position.НД);
            Название.DataPropertyName = nameof(Position.Название);
            Оклад.DataPropertyName = nameof(Position.Оклад);
            Занятость.DataPropertyName = nameof(Position.Занятость);

            _bsCurrent = new BindingSource();
            _bsCurrent.DataSource = new List<Position> { new Position(0) };
            тб1.DataBindings.Add("Text", _bsCurrent, nameof(Position.НД));
            тб2.DataBindings.Add("Text", _bsCurrent, nameof(Position.Название));
            тб3.DataBindings.Add("Text", _bsCurrent, nameof(Position.Оклад));
            тб4.DataBindings.Add("Text", _bsCurrent, nameof(Position.Занятость));
        }

        private void SwitchOnWaiting()
        {
            foreach (var button in this.Controls.OfType<Button>())
            {
                button.Enabled = false;
            }
            dataTable.Enabled = false;
            Cursor = Cursors.WaitCursor;
        }

        private void SwitchOffWaiting()
        {
            foreach (var button in this.Controls.OfType<Button>())
            {
                button.Enabled = true;
            }
            dataTable.Enabled = true;
            Cursor = Cursors.Default;
        }

        private bool IsCorrectCurrent(Position obj)
        {
            bool isCorrect = true;

            string maskForStr = "<?>";
            if (String.IsNullOrWhiteSpace(obj.Название)
                || String.IsNullOrEmpty(obj.Название)
                || obj.Название.Equals(maskForStr))
            {
                isCorrect = false;
                var message = "Ошибка ввода названия организации";
                MessageBox.Show(message, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return isCorrect;
        }

        public Должность()
        {
            InitializeComponent();
        }

        private void Должность_Load(object sender, EventArgs e)
        {
            _repo = new RPosition();
            SetBindings();
            LoadData();
            dataTable.MouseClick += (s, a) => SetCurrentRow();
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            IsAddMethodCalled = true;
            btnUpd.Text = "Сохранить";
            _bs.MoveLast();
            int number = (_bs.Current as Position).OrderNumber + 1;
            _bs.Add(new Position(0) { OrderNumber = number });
            _bs.MoveNext();
            SetCurrentRow();
            тб1.Focus();
        }

        private async void btnUpd_Click(object sender, EventArgs e)
        {
            SwitchOnWaiting();
            var current = (Position)_bsCurrent.Current;

            SetCurrentRow();
            var СтрокаСоСтарымID = (Position)_bsCurrent.Current;
            int НДОлд = СтрокаСоСтарымID.НД;

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
                    result = await _repo.AddPosition(current);
                }
                else
                {
                    result = await _repo.UpdatePosition(current, НДОлд);
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

            var objRow = (Position)_bs.Current;
            Result<int> result;
            try
            {
                result = await _repo.RemovePosition(objRow.НД);
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
    }
}
