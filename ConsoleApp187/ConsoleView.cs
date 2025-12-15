using System;
using System.Collections.Generic;
using System.Linq;
using Shared;

namespace ConsoleApp187
{
    public class ConsoleView : IView
    {
        // События IView (ВСЕ необходимые)
        public event EventHandler ViewLoaded;
        public event EventHandler AddWorkerRequested;
        public event EventHandler<int> DeleteWorkerRequested;
        public event EventHandler<int> ShowWorkerDetailsRequested;
        public event EventHandler GetConstructionInfoRequested;

        // ДОБАВЛЕНО: недостающие события
        public event EventHandler UpdateWorkerRequested;    // ИСПРАВЛЕНО: должен быть без параметра int
        public event EventHandler FilterWorkersRequested;

        // Текущие данные для передачи в Presenter
        private string _currentName;
        private int? _currentAge;
        private decimal? _currentSalary;  // ИСПРАВЛЕНО: decimal? вместо int?
        private string _currentSpecialization;
        private object _currentFilterCriteria;
        private int? _selectedWorkerId;

        // ДОБАВЛЕНО: для SetWorkerDataForEdit
        private object _workerDataForEdit;

        public void Run()
        {
            Console.Clear();
            Console.WriteLine("=== КОНСОЛЬНОЕ УПРАВЛЕНИЕ СТРОИТЕЛЬНОЙ ПЛОЩАДКОЙ ===");
            Console.WriteLine();

            // Уведомляем Presenter о загрузке
            ViewLoaded?.Invoke(this, EventArgs.Empty);

            ShowMainMenu();
        }

        private void ShowMainMenu()
        {
            while (true)
            {
                Console.WriteLine("\n=== ГЛАВНОЕ МЕНЮ ===");
                Console.WriteLine("1. Показать всех работников");
                Console.WriteLine("2. Добавить работника");
                Console.WriteLine("3. Удалить работника");
                Console.WriteLine("4. Изменить работника");
                Console.WriteLine("5. Фильтровать работников");
                Console.WriteLine("6. Сбросить фильтр");
                Console.WriteLine("7. Информация о стройке");
                Console.WriteLine("8. Детали работника");
                Console.WriteLine("0. Выход");

                Console.Write("\nВыберите действие: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        ViewLoaded?.Invoke(this, EventArgs.Empty);
                        WaitForContinue();
                        break;
                    case "2":
                        Console.Clear();
                        ShowAddWorkerMenu();
                        WaitForContinue();
                        break;
                    case "3":
                        Console.Clear();
                        ShowDeleteWorkerMenu();
                        WaitForContinue();
                        break;
                    case "4":
                        Console.Clear();
                        ShowUpdateWorkerMenu();
                        WaitForContinue();
                        break;
                    case "5":
                        Console.Clear();
                        ShowFilterMenu();
                        WaitForContinue();
                        break;
                    case "6":
                        Console.Clear();
                        // Сброс фильтра
                        _currentFilterCriteria = null;
                        FilterWorkersRequested?.Invoke(this, EventArgs.Empty);
                        WaitForContinue();
                        break;
                    case "7":
                        Console.Clear();
                        GetConstructionInfoRequested?.Invoke(this, EventArgs.Empty);
                        WaitForContinue();
                        break;
                    case "8":
                        Console.Clear();
                        ShowWorkerDetailsMenu();
                        WaitForContinue();
                        break;
                    case "0":
                        Console.Clear();
                        Console.WriteLine("Выход из программы.");
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        WaitForContinue();
                        break;
                }

                Console.Clear(); // Очищаем консоль после каждого действия
            }
        }

        private void WaitForContinue()
        {
            Console.WriteLine("\n\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        private void ShowAddWorkerMenu()
        {
            Console.WriteLine("\n=== ДОБАВЛЕНИЕ НОВОГО РАБОТНИКА ===");

            // Собираем данные
            _currentName = GetInput("Введите имя работника: ", true);
            _currentAge = GetIntInput("Введите возраст (18-65): ", 18, 65);
            _currentSalary = GetDecimalInput("Введите зарплату (25000-1000000): ", 25000, 1000000);
            _currentSpecialization = GetSpecializationInput();

            // Уведомляем Presenter
            AddWorkerRequested?.Invoke(this, EventArgs.Empty);

            // Очищаем поля после отправки
            ClearCurrentData();
        }

        private void ShowDeleteWorkerMenu()
        {
            Console.WriteLine("\n=== УДАЛЕНИЕ РАБОТНИКА ===");

            // Сначала показываем список работников
            ViewLoaded?.Invoke(this, EventArgs.Empty);

            _selectedWorkerId = GetIntInput("\nВведите ID работника для удаления: ", 1, int.MaxValue);

            if (_selectedWorkerId.HasValue)
            {
                DeleteWorkerRequested?.Invoke(this, _selectedWorkerId.Value);
            }
        }

        private void ShowUpdateWorkerMenu()
        {
            Console.WriteLine("\n=== ИЗМЕНЕНИЕ ДАННЫХ РАБОТНИКА ===");

            // Показываем список работников
            ViewLoaded?.Invoke(this, EventArgs.Empty);

            _selectedWorkerId = GetIntInput("\nВведите ID работника для изменения: ", 1, int.MaxValue);

            if (_selectedWorkerId.HasValue)
            {
                Console.WriteLine("\nВведите новые данные (оставьте поле пустым для сохранения текущего значения):");

                _currentName = GetInput("Имя: ", false);
                _currentAge = GetOptionalIntInput("Возраст (18-65): ", 18, 65);
                _currentSalary = GetOptionalDecimalInput("Зарплата (25000-1000000): ", 25000, 1000000);
                _currentSpecialization = GetOptionalSpecializationInput();

                // ИСПРАВЛЕНО: UpdateWorkerRequested должен быть без параметра
                UpdateWorkerRequested?.Invoke(this, EventArgs.Empty);
                ClearCurrentData();
            }
        }

        private void ShowFilterMenu()
        {
            Console.WriteLine("\n=== ФИЛЬТРАЦИЯ РАБОТНИКОВ ===");

            Console.WriteLine("Введите критерии фильтрации (оставьте поле пустым для пропуска):");

            var criteria = new
            {
                Name = GetInput("Имя: ", false),
                MinAge = GetOptionalIntInput("Минимальный возраст: ", 1, 65),
                MaxAge = GetOptionalIntInput("Максимальный возраст: ", 1, 65),
                MinSalary = GetOptionalDecimalInput("Минимальная зарплата: ", 1, 1000000),
                MaxSalary = GetOptionalDecimalInput("Максимальная зарплата: ", 1, 1000000),
                Specialization = GetOptionalSpecializationInput()
            };

            _currentFilterCriteria = criteria;
            FilterWorkersRequested?.Invoke(this, EventArgs.Empty);
        }

        private void ShowWorkerDetailsMenu()
        {
            Console.WriteLine("\n=== ПРОСМОТР ДЕТАЛЕЙ РАБОТНИКА ===");

            ViewLoaded?.Invoke(this, EventArgs.Empty);

            _selectedWorkerId = GetIntInput("\nВведите ID работника для просмотра деталей: ", 1, int.MaxValue);

            if (_selectedWorkerId.HasValue)
            {
                ShowWorkerDetailsRequested?.Invoke(this, _selectedWorkerId.Value);
            }
        }

        // Реализация методов IView

        public string GetWorkerName() => _currentName;

        public int? GetWorkerAge() => _currentAge;

        public decimal? GetWorkerSalary() => _currentSalary;  // ИСПРАВЛЕНО: decimal? вместо int?

        public string GetWorkerSpecialization() => _currentSpecialization;

        public object GetFilterCriteria() => _currentFilterCriteria;

        // ДОБАВЛЕНО: новые методы интерфейса
        public int? GetSelectedWorkerId()
        {
            return _selectedWorkerId;
        }

        public void SetWorkerDataForEdit(object workerData)
        {
            _workerDataForEdit = workerData;

            // Извлекаем данные из объекта
            try
            {
                var type = workerData.GetType();
                var nameProp = type.GetProperty("Name");
                var ageProp = type.GetProperty("Age");
                var salaryProp = type.GetProperty("Salary");
                var specProp = type.GetProperty("Specialization");
                var idProp = type.GetProperty("Id");

                if (nameProp != null) _currentName = nameProp.GetValue(workerData) as string;
                if (ageProp != null) _currentAge = ageProp.GetValue(workerData) as int?;
                if (salaryProp != null) _currentSalary = salaryProp.GetValue(workerData) as decimal?;
                if (specProp != null) _currentSpecialization = specProp.GetValue(workerData) as string;
                if (idProp != null) _selectedWorkerId = idProp.GetValue(workerData) as int?;

                Console.WriteLine("\nДанные рабочего загружены для редактирования:");
                Console.WriteLine(workerData);
            }
            catch
            {
                Console.WriteLine("\nНе удалось загрузить данные рабочего для редактирования.");
            }
        }

        public void DisplayWorkers(IEnumerable<object> workers)
        {
            Console.Clear();
            Console.WriteLine("=== СПИСОК РАБОТНИКОВ ===");

            if (workers == null || !workers.Any())
            {
                Console.WriteLine("Работники не найдены.");
                return;
            }

            // Создаем таблицу
            Console.WriteLine(new string('═', 90));
            Console.WriteLine("│ {0,3} │ {1,-20} │ {2,6} │ {3,10} │ {4,-15} │",
                "ID", "Имя", "Возраст", "Зарплата", "Специализация");
            Console.WriteLine(new string('═', 90));

            foreach (var obj in workers)
            {
                // Извлекаем данные через рефлексию
                var type = obj.GetType();
                var id = (int)type.GetProperty("Id")?.GetValue(obj);
                var name = (string)type.GetProperty("Name")?.GetValue(obj);
                var age = (int)type.GetProperty("Age")?.GetValue(obj);
                var salaryProp = type.GetProperty("Salary");
                object salaryValue = salaryProp?.GetValue(obj);
                var salary = salaryValue is decimal ? (decimal)salaryValue :
                            salaryValue is int ? (decimal)(int)salaryValue : 0;
                var spec = (string)type.GetProperty("Specialization")?.GetValue(obj);

                Console.WriteLine("│ {0,3} │ {1,-20} │ {2,6} │ {3,10:N0} │ {4,-15} │",
                    id, name, age, salary, spec);
            }

            Console.WriteLine(new string('═', 90));
            Console.WriteLine($"Всего работников: {workers.Count()}");
        }

        public void DisplayMessage(string message, MessageType type)
        {
            // Сохраняем текущий цвет
            ConsoleColor originalColor = Console.ForegroundColor;

            // ИСПРАВЛЕНО: обычный switch вместо switch expression
            ConsoleColor messageColor;
            string prefix;

            switch (type)
            {
                case MessageType.Success:
                    messageColor = ConsoleColor.Green;
                    prefix = "[УСПЕХ] ";
                    break;
                case MessageType.Error:
                    messageColor = ConsoleColor.Red;
                    prefix = "[ОШИБКА] ";
                    break;
                case MessageType.Warning:
                    messageColor = ConsoleColor.Yellow;
                    prefix = "[ВНИМАНИЕ] ";
                    break;
                case MessageType.Info:
                    messageColor = ConsoleColor.Cyan;
                    prefix = "[ИНФО] ";
                    break;
                default:
                    messageColor = ConsoleColor.White;
                    prefix = "[СООБЩЕНИЕ] ";
                    break;
            }

            // Устанавливаем цвет и выводим сообщение
            Console.ForegroundColor = messageColor;
            Console.WriteLine($"\n{prefix}{message}");
            Console.ForegroundColor = originalColor;
        }

        public void DisplayConstructionInfo(object info)
        {
            Console.Clear();
            Console.WriteLine("\n=== ИНФОРМАЦИЯ О СТРОИТЕЛЬНОЙ ПЛОЩАДКЕ ===");
            Console.WriteLine(new string('═', 50));

            if (info != null)
            {
                var type = info.GetType();

                try
                {
                    var total = (int)type.GetProperty("TotalSalaryExpenses")?.GetValue(info);
                    var electric = (int)type.GetProperty("ElectriciansCount")?.GetValue(info);
                    var painters = (int)type.GetProperty("PaintersCount")?.GetValue(info);
                    var crane = (int)type.GetProperty("CraneOperatorsCount")?.GetValue(info);
                    var general = (int)type.GetProperty("GeneralWorkersCount")?.GetValue(info);

                    Console.WriteLine($"Общие расходы на зарплаты: {total:N0} руб.");
                    Console.WriteLine($"Количество электриков: {electric}");
                    Console.WriteLine($"Количество маляров: {painters}");
                    Console.WriteLine($"Количество крановщиков: {crane}");
                    Console.WriteLine($"Количество разнорабочих: {general}");
                    Console.WriteLine($"Всего работников: {electric + painters + crane + general}");
                }
                catch
                {
                    Console.WriteLine("Не удалось получить информацию о стройке.");
                }
            }
            else
            {
                Console.WriteLine("Информация о стройке недоступна.");
            }

            Console.WriteLine(new string('═', 50));
        }

        public void LoadSpecializations(string[] specializations)
        {
            Console.WriteLine("\nДоступные специализации:");

            if (specializations != null && specializations.Length > 0)
            {
                for (int i = 0; i < specializations.Length; i++)
                {
                    Console.WriteLine($"  {i + 1}. {specializations[i]}");
                }
            }
            else
            {
                Console.WriteLine("  Специализации не загружены.");
            }
        }

        public void ClearInputFields()
        {
            // Очищаем текущие данные
            ClearCurrentData();
            Console.WriteLine("\nПоля ввода очищены.");
        }

        // Вспомогательные методы

        private void ClearCurrentData()
        {
            _currentName = null;
            _currentAge = null;
            _currentSalary = null;
            _currentSpecialization = null;
            _currentFilterCriteria = null;
            _selectedWorkerId = null;
        }

        private string GetInput(string prompt, bool required)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine()?.Trim();

                if (!required || !string.IsNullOrWhiteSpace(input))
                    return input;

                Console.WriteLine("Это поле обязательно для заполнения!");
            }
        }

        private int GetIntInput(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (int.TryParse(input, out int result))
                {
                    if (result >= min && result <= max)
                        return result;

                    Console.WriteLine($"Пожалуйста, введите число от {min} до {max}.");
                }
                else
                {
                    Console.WriteLine("Пожалуйста, введите корректное число.");
                }
            }
        }

        // Новый метод для получения decimal ввода
        private decimal GetDecimalInput(string prompt, decimal min, decimal max)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (decimal.TryParse(input, out decimal result))
                {
                    if (result >= min && result <= max)
                        return result;

                    Console.WriteLine($"Пожалуйста, введите число от {min} до {max}.");
                }
                else
                {
                    Console.WriteLine("Пожалуйста, введите корректное число.");
                }
            }
        }

        private int? GetOptionalIntInput(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine()?.Trim();

                // Если пустая строка - возвращаем null
                if (string.IsNullOrEmpty(input))
                    return null;

                if (int.TryParse(input, out int result))
                {
                    if (result >= min && result <= max)
                        return result;

                    Console.WriteLine($"Пожалуйста, введите число от {min} до {max} или оставьте поле пустым.");
                }
                else
                {
                    Console.WriteLine("Пожалуйста, введите корректное число или оставьте поле пустым.");
                }
            }
        }

        // Новый метод для получения optional decimal ввода
        private decimal? GetOptionalDecimalInput(string prompt, decimal min, decimal max)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine()?.Trim();

                // Если пустая строка - возвращаем null
                if (string.IsNullOrEmpty(input))
                    return null;

                if (decimal.TryParse(input, out decimal result))
                {
                    if (result >= min && result <= max)
                        return result;

                    Console.WriteLine($"Пожалуйста, введите число от {min} до {max} или оставьте поле пустым.");
                }
                else
                {
                    Console.WriteLine("Пожалуйста, введите корректное число или оставьте поле пустым.");
                }
            }
        }

        private string GetSpecializationInput()
        {
            Console.WriteLine("\nВыберите специализацию:");
            Console.WriteLine("  1. Eletrecian (Электрик)");
            Console.WriteLine("  2. Painter (Маляр)");
            Console.WriteLine("  3. CraneOperator (Крановщик)");
            Console.WriteLine("  4. GeneralWorker (Разнорабочий)");

            while (true)
            {
                Console.Write("Введите номер (1-4): ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        return "Eletrecian";
                    case "2":
                        return "Painter";
                    case "3":
                        return "CraneOperator";
                    case "4":
                        return "GeneralWorker";
                    default:
                        Console.WriteLine("Пожалуйста, введите число от 1 до 4.");
                        break;
                }
            }
        }

        private string GetOptionalSpecializationInput()
        {
            Console.WriteLine("\nВыберите специализацию (оставьте пустым для всех):");
            Console.WriteLine("  1. Eletrecian (Электрик)");
            Console.WriteLine("  2. Painter (Маляр)");
            Console.WriteLine("  3. CraneOperator (Крановщик)");
            Console.WriteLine("  4. GeneralWorker (Разнорабочий)");
            Console.WriteLine("  Enter - Все специализации");

            while (true)
            {
                Console.Write("Введите номер (1-4) или Enter: ");
                string input = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(input))
                    return null;

                switch (input)
                {
                    case "1":
                        return "Eletrecian";
                    case "2":
                        return "Painter";
                    case "3":
                        return "CraneOperator";
                    case "4":
                        return "GeneralWorker";
                    default:
                        Console.WriteLine("Пожалуйста, введите число от 1 до 4 или нажмите Enter.");
                        break;
                }
            }
        }
    }
}