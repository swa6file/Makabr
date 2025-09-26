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
        private void InitializeListBox()
        {
            workers_list.DataSource = logic.ReadWorkers();
            logic.AddWorker("Максим", 19 ,200000, Specialization.CraneOperator);
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
            workers_list.DisplayMember = "Name";
            workers_list.ValueMember = "Id";

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
            InitializeListBox();
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
        private void Form1_oad(object sender, EventArgs e)
        {
            if (workers_list.SelectedItem != null)
            {

                Worker selectedWorker = (Worker)workers_list.SelectedItem;

                WorkerInfo infoForm = new WorkerInfo(selectedWorker);
                infoForm.ShowDialog();
            }
        }
        private void RefreshListBox()
        {
            workers_list.DataSource = null;
            workers_list.DataSource = logic.ReadWorkers();
            workers_list.DisplayMember = "Name";
            workers_list.ValueMember = "Id";
        }
        private void Add_Click(object sender, EventArgs e)
        {
            try
            {
                string name = textName.Text;
                string age = Age.Text;
                string salary = Salary.Text;
                Specialization specialization = (Specialization)comboSpecializatiion.SelectedItem;

                if (!logic.CheckName (name))
                {
                    MessageBox.Show("Введено некорректное имя");
                    textName.Focus();
                }
                else if (!logic.CheckAge(age))
                {
                    MessageBox.Show("Возраст не подходит\nНужен от 18 до 65");
                    textName.Focus();
                }
                else if (!logic.CheckSalary(salary))
                {
                    MessageBox.Show("Запрлата некорректна\nМинимум 25 000 Максимум 1 000 000");
                    textName.Focus();
                }
                else
                {
                    int ag = int.Parse(age);
                    int salar = int.Parse(salary);
                    logic.AddWorker(name, ag, salar, specialization);

                    RefreshListBox();

                    textName.Text = "";
                    Age.Text = "";
                    Salary.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении: {ex.Message}");
            }

            ;
            


        }
        private void DeleteSelectedWorker_Click(object sender, EventArgs e)
        {
            if (workers_list.SelectedItem == null) 
            {
                MessageBox.Show("Сначала нужно выбрать элемент");
                return;
            }
            else
            {
                Worker selectedwrk = (Worker)workers_list.SelectedItem;
                MessageBox.Show(logic.DeleteWorker(selectedwrk.Id));
                RefreshListBox();
            };
        }

        private void ChangeWorker_Click(object sender, EventArgs e)
        {
            if (workers_list.SelectedItem != null)
            {
                ChangeWorkerForm changeWorkerForm = new ChangeWorkerForm((Worker)workers_list.SelectedItem);
                changeWorkerForm.Show();
                changeWorkerForm.ValidateData += ValidateWorker;
                changeWorkerForm.Changed += (s, args) =>
                {
                    RefreshListBox();
                };
            }
            else
            {
                MessageBox.Show("Для изменения выберите элемент");
            }

        }

        private void SortedWorkers_Click(object sender, EventArgs e)
        {
            SortWorkers sortWorkers = new SortWorkers();

            sortWorkers.Sort += (s, args) =>
            {
                var query = logic.SortedWorkers(sortWorkers.fname, sortWorkers.sage, sortWorkers.eage,
                              sortWorkers.ssalary, sortWorkers.esalary, sortWorkers.spec);
                workers_list.DataSource = null;
                workers_list.DataSource = query.ToList();
                workers_list.DisplayMember = "Name";
                workers_list.ValueMember = "Id";
            };
          
            sortWorkers.Show();
        }

        private void ResetSort_Click(object sender, EventArgs e)
        {
            RefreshListBox();

        }

        private void InformationAboutConstruction_Click(object sender, EventArgs e)
        {
            var list = logic.InformationAboutConstruction();
        InfoConstruction infoConstruction = new InfoConstruction(list[0], list[1], list[2], list[3], list[4]);
            infoConstruction.Show();
        }

        private void workers_list_DoubleClick(object sender, EventArgs e)
        {
            if (workers_list.SelectedItem != null)
            {

                Worker selectedWorker = (Worker)workers_list.SelectedItem;

                WorkerInfo infoForm = new WorkerInfo(selectedWorker);
                infoForm.ShowDialog();
            }
        }
    }

}
