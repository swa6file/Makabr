using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using BusinessLogical;
using WindowsFormsApp1;


namespace WindowsFormsApp1
{
    public partial class ChangeWorkerForm : Form
    {
        public Worker _worker;
        private void InitializeWorker(Worker worker)
        {
            change_specialization.DataSource = Enum.GetValues(typeof(Specialization)); 

            change_name.Text = worker.Name;
            change_age.Text = worker.Age.ToString();
            change_specialization.SelectedItem = worker.Specialization.ToString();
            change_salary.Text = worker.Salary.ToString();
        }
        public ChangeWorkerForm(Worker worker)
        {
            InitializeComponent();
            _worker = worker;
            InitializeWorker(_worker);
        }
        public event EventHandler Changed;
        private void change_worker2_Click(object sender, EventArgs e)
        {
            try
            {
                _worker.Name = change_name.Text;
                _worker.Age = int.Parse(change_age.Text);
                _worker.Salary = int.Parse(change_salary.Text);
                _worker.Specialization = (Specialization)change_specialization.SelectedItem;

                Changed?.Invoke(this, EventArgs.Empty);

                MessageBox.Show("Изменения прошли успешно");

                this.Close();

            }
            catch (Exception ex) 
            {
                MessageBox.Show("Одно из полей содержит некорректное значение " + ex);
            }
        }
    }
}
