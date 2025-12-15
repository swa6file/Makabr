using System;
using System.Windows.Forms;
using Model;

namespace WindowsFormsApp1
{
    public partial class ChangeWorkerForm : Form
    {
        public Worker UpdatedWorker { get; private set; }

        // ДОБАВЬТЕ ЭТИ СОБЫТИЯ
        public event Action<object, EventArgs> Changed;
        public event Func<string, string, string, bool> ValidateData;

        public ChangeWorkerForm(Worker worker)
        {
            InitializeComponent();
            InitializeForm(worker);
        }

        private void InitializeForm(Worker worker)
        {
            // Заполняем поля данными работника
            change_name.Text = worker.Name;
            change_age.Text = worker.Age.ToString();
            change_salary.Text = worker.Salary.ToString();

            // Заполняем ComboBox специализациями из Model.Specialization
            change_specialization.DataSource = Enum.GetValues(typeof(Model.Specialization));
            change_specialization.SelectedItem = worker.Specialization;
        }

        private void change_worker2_Click(object sender, EventArgs e)
        {
            // Простая валидация ввода
            if (!ValidateInput())
            {
                MessageBox.Show("Пожалуйста, проверьте введенные данные.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // ЯВНОЕ приведение типа к Model.Specialization
                Model.Specialization specialization;

                if (change_specialization.SelectedItem is Model.Specialization)
                {
                    specialization = (Model.Specialization)change_specialization.SelectedItem;
                }
                else if (change_specialization.SelectedItem is int intValue)
                {
                    // Если это int (значение enum)
                    specialization = (Model.Specialization)intValue;
                }
                else if (change_specialization.SelectedItem is string stringValue)
                {
                    // Если это строка
                    if (Enum.TryParse<Model.Specialization>(stringValue, true, out Model.Specialization parsed))
                    {
                        specialization = parsed;
                    }
                    else
                    {
                        throw new InvalidCastException($"Некорректная специализация: {stringValue}");
                    }
                }
                else
                {
                    throw new InvalidCastException("Некорректный тип специализации");
                }

                // Создаем обновленного работника
                UpdatedWorker = new Worker
                {
                    Id = -1, // ID будет установлен позже
                    Name = change_name.Text,
                    Age = int.Parse(change_age.Text),
                    Salary = int.Parse(change_salary.Text),
                    Specialization = specialization
                };

                // ВЫЗЫВАЕМ СОБЫТИЕ
                Changed?.Invoke(this, EventArgs.Empty);

                DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обработке данных: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            // Используем внешнюю валидацию если есть, иначе свою
            if (ValidateData != null)
            {
                return ValidateData(change_name.Text, change_age.Text, change_salary.Text);
            }
            else
            {
                // Простая валидация UI
                if (string.IsNullOrWhiteSpace(change_name.Text))
                {
                    MessageBox.Show("Имя не может быть пустым", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (!int.TryParse(change_age.Text, out int age) || age < 18 || age > 65)
                {
                    MessageBox.Show("Возраст должен быть числом от 18 до 65", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (!int.TryParse(change_salary.Text, out int salary) || salary < 25000 || salary > 1000000)
                {
                    MessageBox.Show("Зарплата должна быть числом от 25000 до 1000000", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (change_specialization.SelectedItem == null)
                {
                    MessageBox.Show("Выберите специализацию", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                return true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}