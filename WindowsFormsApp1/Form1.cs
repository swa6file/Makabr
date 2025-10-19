using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Configuration;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLogical;
using static System.Net.Mime.MediaTypeNames;

namespace WindowsFormsApp1
{
    /// <summary>
    /// Главная форма приложения для управления работниками стройки
    /// </summary>
    public partial class Form1 : Form
    {
        Logic logic = new Logic();

        public event Func<string, string, string, bool> ValidateWorkerData;

        private void InitializeDataGridView()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Name",
                DataPropertyName = "Name",
                HeaderText = "Имя",
                Width = 150
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Age",
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
                Name = "Salary",
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
                Name = "Specialization",
                DataPropertyName = "Specialization",
                HeaderText = "Специализация",
                Width = 150
            });

            // Добавляем тестовые данные
            logic.AddWorker("Максим", 19, 200000, Specialization.CraneOperator);
            logic.AddWorker("Алексей", 25, 45000, Specialization.Eletrecian);
            logic.AddWorker("Дмитрий", 32, 38000, Specialization.Painter);
            logic.AddWorker("Иван", 28, 52000, Specialization.CraneOperator);
            logic.AddWorker("Сергей", 35, 29000, Specialization.GeneralWorker);
            logic.AddWorker("Андрей", 22, 41000, Specialization.Eletrecian);
            logic.AddWorker("Анна", 29, 48000, Specialization.Painter);
            logic.AddWorker("Елена", 31, 55000, Specialization.CraneOperator);
            logic.AddWorker("Ольга", 26, 32000, Specialization.GeneralWorker);
            logic.AddWorker("Мария", 33, 47000, Specialization.Eletrecian);
            logic.AddWorker("Наталья", 27, 39000, Specialization.Painter);

           
            RefreshDataGridView();
        }

        private void InitializeSpecializationComboBox()
        {
            comboSpecializatiion.DataSource = Enum.GetValues(typeof(Specialization));
        }

        /// <summary>
        /// Иницилизация элементов формы
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            InitializeDataGridView();
            InitializeSpecializationComboBox();
            ValidateWorkerData += ValidateWorker;
        }

        private bool ValidateWorker(string name, string age, string salary)
        {
            if (!logic.CheckName(name))
            {
                MessageBox.Show("Введено некорректное имя");
                return false;
            }
            else if (!logic.CheckAge(age))
            {
                MessageBox.Show("Возраст не подходит\nНужен от 18 до 65");
                return false;
            }
            else if (!logic.CheckSalary(salary))
            {
                MessageBox.Show("Запрлата некорректна\nМинимум 25 000 Максимум 1 000 000");
                return false;
            }
            return true;
        }

        private void RefreshDataGridView()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = logic.ReadWorkers();
        }

        private void Add_Click(object sender, EventArgs e)
        {
            try
            {
                string name = textName.Text;
                string age = Age.Text;
                string salary = Salary.Text;
                Specialization specialization = (Specialization)comboSpecializatiion.SelectedItem;

                if (!logic.CheckName(name))
                {
                    MessageBox.Show("Введено некорректное имя");
                    textName.Focus();
                }
                else if (!logic.CheckAge(age))
                {
                    MessageBox.Show("Возраст не подходит\nНужен от 18 до 65");
                    Age.Focus();
                }
                else if (!logic.CheckSalary(salary))
                {
                    MessageBox.Show("Запрлата некорректна\nМинимум 25 000 Максимум 1 000 000");
                    Salary.Focus();
                }
                else
                {
                    int ag = int.Parse(age);
                    int salar = int.Parse(salary);
                    logic.AddWorker(name, ag, salar, specialization);

                    RefreshDataGridView();

                    textName.Text = "";
                    Age.Text = "";
                    Salary.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении: {ex.Message}");
            }
        }

        private void DeleteSelectedWorker_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Сначала нужно выбрать строку");
                return;
            }
            else
            {
                Worker selectedWorker = (Worker)dataGridView1.SelectedRows[0].DataBoundItem;
                MessageBox.Show(logic.DeleteWorker(selectedWorker.Id));
                RefreshDataGridView();
            }
        }

        private void ChangeWorker_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                Worker selectedWorker = (Worker)dataGridView1.SelectedRows[0].DataBoundItem;
                ChangeWorkerForm changeWorkerForm = new ChangeWorkerForm(selectedWorker);
                changeWorkerForm.Show();
                changeWorkerForm.ValidateData += ValidateWorker;
                changeWorkerForm.Changed += (s, args) =>
                {
                    RefreshDataGridView();
                };
            }
            else
            {
                MessageBox.Show("Для изменения выберите строку");
            }
        }

        private void SortedWorkers_Click(object sender, EventArgs e)
        {
            SortWorkers sortWorkers = new SortWorkers();

            sortWorkers.Sort += (s, args) =>
            {
                var query = logic.SortedWorkers(sortWorkers.fname, sortWorkers.sage, sortWorkers.eage,
                              sortWorkers.ssalary, sortWorkers.esalary, sortWorkers.spec);
                dataGridView1.DataSource = query.ToList();
            };

            sortWorkers.Show();
        }

        private void ResetSort_Click(object sender, EventArgs e)
        {
            RefreshDataGridView();
        }

        private void InformationAboutConstruction_Click(object sender, EventArgs e)
        {
            var list = logic.InformationAboutConstruction();
            InfoConstruction infoConstruction = new InfoConstruction(list[0], list[1], list[2], list[3], list[4]);
            infoConstruction.Show();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Worker selectedWorker = (Worker)dataGridView1.Rows[e.RowIndex].DataBoundItem;
                WorkerInfo infoForm = new WorkerInfo(selectedWorker);
                infoForm.ShowDialog();
            }
        }
    }
}