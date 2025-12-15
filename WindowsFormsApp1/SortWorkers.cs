using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Model;

namespace WindowsFormsApp1
{
    public partial class SortWorkers : Form
    {
        // Свойства для хранения критериев фильтрации
        public string WorkerName { get; private set; }
        public int? MinAge { get; private set; }
        public int? MaxAge { get; private set; }
        public int? MinSalary { get; private set; }
        public int? MaxSalary { get; private set; }
        public Specialization? Specialization { get; private set; }

        // ДОБАВЬТЕ ЭТИ СВОЙСТВА ДЛЯ ДОСТУПА ИЗ ВНЕ
        public string fname => WorkerName;
        public int? sage => MinAge;
        public int? eage => MaxAge;
        public int? ssalary => MinSalary;
        public int? esalary => MaxSalary;
        public Specialization? spec => Specialization;

        // ДОБАВЬТЕ ЭТО СОБЫТИЕ
        public event EventHandler Sort;

        public SortWorkers()
        {
            InitializeComponent();
            InitializeSpecializationComboBox();
        }

        private void InitializeSpecializationComboBox()
        {
            // Добавляем "Не выбрано" и все значения перечисления
            var items = new List<object> { "Не выбрано" };

            // ИСПРАВЛЕНО: преобразуем Array в IEnumerable<object>
            var enumValues = Enum.GetValues(typeof(Specialization));
            foreach (var value in enumValues)
            {
                items.Add(value);
            }

            fspecialization.DataSource = items;
        }

        private void sort_Click(object sender, EventArgs e)
        {
            // Собираем данные из формы
            WorkerName = string.IsNullOrWhiteSpace(find_name.Text) ? null : find_name.Text;

            MinAge = int.TryParse(start_age.Text, out int minAge) ? minAge : (int?)null;
            MaxAge = int.TryParse(end_age.Text, out int maxAge) ? maxAge : (int?)null;
            MinSalary = int.TryParse(start_salary.Text, out int minSalary) ? minSalary : (int?)null;
            MaxSalary = int.TryParse(last_salary.Text, out int maxSalary) ? maxSalary : (int?)null;

            // ИСПРАВЛЕНО: правильная конвертация выбранного значения
            if (fspecialization.SelectedItem != null &&
                fspecialization.SelectedItem.ToString() != "Не выбрано")
            {
                if (fspecialization.SelectedItem is Specialization spec)
                {
                    Specialization = spec;
                }
                else if (fspecialization.SelectedItem is string specStr)
                {
                    if (Enum.TryParse<Specialization>(specStr, true, out Specialization parsedSpec))
                    {
                        Specialization = parsedSpec;
                    }
                    else
                    {
                        Specialization = null;
                    }
                }
                else
                {
                    Specialization = null;
                }
            }
            else
            {
                Specialization = null;
            }

            // ВЫЗЫВАЕМ СОБЫТИЕ
            Sort?.Invoke(this, EventArgs.Empty);

            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            // Очищаем все поля
            find_name.Text = "";
            start_age.Text = "";
            end_age.Text = "";
            start_salary.Text = "";
            last_salary.Text = "";
            fspecialization.SelectedIndex = 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}