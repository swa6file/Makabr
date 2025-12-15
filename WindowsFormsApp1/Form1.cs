using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Model;
using Shared;
using BusinessLogical.Models;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form, IView
    {
        // Реализация событий IView
        public event EventHandler ViewLoaded;
        public event EventHandler AddWorkerRequested;
        public event EventHandler<int> DeleteWorkerRequested;
        public event EventHandler<int> ShowWorkerDetailsRequested;
        public event EventHandler GetConstructionInfoRequested;
        public event EventHandler UpdateWorkerRequested;
        public event EventHandler FilterWorkersRequested;

        private int? _selectedWorkerId;

        public Form1()
        {
            InitializeComponent();
            InitializeForm();

            // Генерируем событие загрузки формы
            this.Load += (s, e) => ViewLoaded?.Invoke(this, EventArgs.Empty);
        }

        private void InitializeForm()
        {
            InitializeDataGridView();
            InitializeSpecializationComboBox();

            // Подписываем кнопки на вызов событий (но оставляем старые обработчики для обратной совместимости)
            Add.Click += Add_Click;
            DeleteSelectedWorker.Click += DeleteSelectedWorker_Click;
            ChangeWorker.Click += ChangeWorker_Click;
            SortedWorkers.Click += SortedWorkers_Click;
            ResetSort.Click += ResetSort_Click;
            InformationAboutConstruction.Click += InformationAboutConstruction_Click;
        }

        private void InitializeDataGridView()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.Columns.Clear();

            // Колонки
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "colId",
                DataPropertyName = "Id",
                HeaderText = "ID",
                Width = 50,
                Visible = false
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "colName",
                DataPropertyName = "Name",
                HeaderText = "Имя",
                Width = 150
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "colAge",
                DataPropertyName = "Age",
                HeaderText = "Возраст",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle()
                {
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "colSalary",
                DataPropertyName = "Salary",
                HeaderText = "Зарплата",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle()
                {
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Format = "N0"
                }
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "colSpecialization",
                DataPropertyName = "Specialization",
                HeaderText = "Специализация",
                Width = 150
            });

            // Выбор строки
            dataGridView1.SelectionChanged += (s, e) =>
            {
                if (dataGridView1.CurrentRow?.DataBoundItem != null)
                {
                    var worker = dataGridView1.CurrentRow.DataBoundItem as Worker;
                    if (worker != null)
                    {
                        _selectedWorkerId = worker.Id;
                    }
                }
            };

            // Двойной клик
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
        }

        private void InitializeSpecializationComboBox()
        {
            comboSpecializatiion.DataSource = Enum.GetValues(typeof(Specialization));
            comboSpecializatiion.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        // ============ Старые обработчики (для обратной совместимости) ============

        private void Add_Click(object sender, EventArgs e)
        {
            // Вызываем событие для Presenter
            AddWorkerRequested?.Invoke(this, EventArgs.Empty);
        }

        private void DeleteSelectedWorker_Click(object sender, EventArgs e)
        {
            if (_selectedWorkerId.HasValue)
            {
                // Вызываем событие для Presenter
                DeleteWorkerRequested?.Invoke(this, _selectedWorkerId.Value);
            }
            else
            {
                DisplayMessage("Выберите работника для удаления", MessageType.Warning);
            }
        }

        private void ChangeWorker_Click(object sender, EventArgs e)
        {
            // Вызываем событие для Presenter
            UpdateWorkerRequested?.Invoke(this, EventArgs.Empty);
        }

        private void SortedWorkers_Click(object sender, EventArgs e)
        {
            // Вызываем событие для Presenter
            FilterWorkersRequested?.Invoke(this, EventArgs.Empty);
        }

        private void ResetSort_Click(object sender, EventArgs e)
        {
            // Вызываем событие загрузки для Presenter
            ViewLoaded?.Invoke(this, EventArgs.Empty);
        }

        private void InformationAboutConstruction_Click(object sender, EventArgs e)
        {
            // Вызываем событие для Presenter
            GetConstructionInfoRequested?.Invoke(this, EventArgs.Empty);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var worker = dataGridView1.Rows[e.RowIndex].DataBoundItem as Worker;
                if (worker != null)
                {
                    // Вызываем событие для Presenter
                    ShowWorkerDetailsRequested?.Invoke(this, worker.Id);
                }
            }
        }

        // ============ Реализация IView ============

        public string GetWorkerName() => textName.Text;

        public int? GetWorkerAge()
        {
            return int.TryParse(Age.Text, out int age) ? age : (int?)null;
        }

        public decimal? GetWorkerSalary()
        {
            return decimal.TryParse(Salary.Text, out decimal salary) ? salary : (decimal?)null;
        }

        public string GetWorkerSpecialization() => comboSpecializatiion.SelectedItem?.ToString();

        public int? GetSelectedWorkerId() => _selectedWorkerId;

        public object GetFilterCriteria()
        {
            // Для фильтрации можно открыть форму сортировки или вернуть null
            return null;
        }

        public void DisplayWorkers(IEnumerable<object> workers)
        {
            var workerList = workers.OfType<Worker>().ToList();
            dataGridView1.DataSource = workerList;
        }

        public void DisplayMessage(string message, MessageType type)
        {
            MessageBoxIcon icon;

            switch (type)
            {
                case MessageType.Success:
                    icon = MessageBoxIcon.Information;
                    break;
                case MessageType.Error:
                    icon = MessageBoxIcon.Error;
                    break;
                case MessageType.Warning:
                    icon = MessageBoxIcon.Warning;
                    break;
                case MessageType.Info:
                    icon = MessageBoxIcon.Information;
                    break;
                default:
                    icon = MessageBoxIcon.None;
                    break;
            }

            MessageBox.Show(message, type.ToString(), MessageBoxButtons.OK, icon);
        }

        public void DisplayConstructionInfo(object info)
        {
            if (info is ConstructionInfo constructionInfo)
            {
                var infoForm = new InfoConstruction(
                    constructionInfo.TotalSalaryExpenses,
                    constructionInfo.ElectriciansCount,
                    constructionInfo.PaintersCount,
                    constructionInfo.CraneOperatorsCount,
                    constructionInfo.GeneralWorkersCount);
                infoForm.Show();
            }
        }

        public void LoadSpecializations(string[] specializations)
        {
            if (specializations != null && specializations.Length > 0)
                comboSpecializatiion.DataSource = specializations;
        }

        public void ClearInputFields()
        {
            textName.Text = "";
            Age.Text = "";
            Salary.Text = "";
            if (comboSpecializatiion.Items.Count > 0)
                comboSpecializatiion.SelectedIndex = 0;
            _selectedWorkerId = null;
        }

        public void SetWorkerDataForEdit(object workerData)
        {
            try
            {
                var type = workerData.GetType();

                var nameProp = type.GetProperty("Name");
                if (nameProp != null)
                    textName.Text = nameProp.GetValue(workerData)?.ToString();

                var ageProp = type.GetProperty("Age");
                if (ageProp != null)
                    Age.Text = ageProp.GetValue(workerData)?.ToString();

                var salaryProp = type.GetProperty("Salary");
                if (salaryProp != null)
                    Salary.Text = salaryProp.GetValue(workerData)?.ToString();

                var specProp = type.GetProperty("Specialization");
                if (specProp != null)
                {
                    string specValue = specProp.GetValue(workerData)?.ToString();
                    if (!string.IsNullOrEmpty(specValue))
                    {
                        for (int i = 0; i < comboSpecializatiion.Items.Count; i++)
                        {
                            if (comboSpecializatiion.Items[i].ToString() == specValue)
                            {
                                comboSpecializatiion.SelectedIndex = i;
                                break;
                            }
                        }
                    }
                }

                var idProp = type.GetProperty("Id");
                if (idProp != null)
                    _selectedWorkerId = Convert.ToInt32(idProp.GetValue(workerData));
            }
            catch (Exception ex)
            {
                DisplayMessage($"Ошибка: {ex.Message}", MessageType.Error);
            }
        }

        // Класс DTO для фильтрации (нужен для GetFilterCriteria)
        public class FilterDto
        {
            public string Name { get; set; } = "";
            public int? MinAge { get; set; }
            public int? MaxAge { get; set; }
            public decimal? MinSalary { get; set; }
            public decimal? MaxSalary { get; set; }
            public string Specialization { get; set; } = "";
        }
    }
}