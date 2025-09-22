using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Versioning;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLogical;

namespace WindowsFormsApp1
{
    public partial class SortWorkers : Form
    {
        public string fname;
        public int? sage;
        public int? eage;
        public int? ssalary;
        public int? esalary;
        public Specialization? spec;
        public SortWorkers()
        {
            InitializeComponent();
            fspecialization.DataSource = Enum.GetValues(typeof(Specialization));
        }

        public event EventHandler Sort;
        private void sort_Click(object sender, EventArgs e)
        {
            try
            {
                fname = string.IsNullOrEmpty(find_name.Text) ? null : find_name.Text;


                sage = int.TryParse(start_age.Text, out int minAge) ? minAge : (int?)null;
                eage = int.TryParse(end_age.Text, out int maxAge) ? maxAge : (int?)null;
                ssalary = int.TryParse(start_salary.Text, out int minSalary) ? minSalary : (int?)null;
                esalary = int.TryParse(last_salary.Text, out int maxSalary) ? maxSalary : (int?)null;

                spec = fspecialization.SelectedItem != null ?
                    (Specialization?)fspecialization.SelectedItem : null;

                Sort?.Invoke(this, EventArgs.Empty);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка ввода данных: {ex.Message}");
            }

        }
    }
}
