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
using Model;

namespace WindowsFormsApp1
{
    /// <summary>
    /// Форма для сортировки работника по определенным критериям
    /// </summary>
    public partial class SortWorkers : Form
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string fname;
        /// <summary>
        /// Начальный возраст
        /// </summary>
        public int? sage;
        /// <summary>
        /// Конечный возраст
        /// </summary>
        public int? eage;
        /// <summary>
        /// Начальная зарплата
        /// </summary>
        public int? ssalary;
        /// <summary>
        /// Конечная зарплата
        /// </summary>
        public int? esalary;
        /// <summary>
        /// Специализация
        /// </summary>
        public Specialization? spec;

        
        /// <summary>
        /// Иницилизация компонентов формы и заполнение возможных специализаций
        /// </summary>
        public SortWorkers()
        {
            InitializeComponent();
            var specializations = new List<object> { "Не выбрано" };
            specializations.AddRange(Enum.GetValues(typeof(Specialization)).Cast<object>());
            fspecialization.DataSource = specializations;
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

                spec = fspecialization.SelectedItem?.ToString() != "Не выбрано"
                   ? (Specialization?)fspecialization.SelectedItem
                   : null;


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
