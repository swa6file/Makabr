using System;
using System.Collections.Generic;
using Shared;

namespace Presenter
{
    /// <summary>
    /// Консольное представление для управления работниками стройки
    /// Реализует интерфейс IView для работы с Presenter
    /// </summary>
    public class DefaultConsoleView : IView
    {
        // Реализация ВСЕХ событий из интерфейса IView
        public event EventHandler ViewLoaded;
        public event EventHandler AddWorkerRequested;
        public event EventHandler<int> DeleteWorkerRequested;
        public event EventHandler<int> ShowWorkerDetailsRequested;
        public event EventHandler GetConstructionInfoRequested;
        public event EventHandler UpdateWorkerRequested;
        public event EventHandler FilterWorkersRequested;

        private int? _selectedWorkerId;
        private object _workerDataForEdit;

        /// <summary>
        /// Конструктор консольного представления
        /// </summary>
        public DefaultConsoleView()
        {
            Console.WriteLine("DefaultConsoleView инициализирован");
        }

        /// <summary>
        /// Отображает список работников в консоли
        /// </summary>
        /// <param name="workers">Коллекция объектов, представляющих работников</param>
        public void DisplayWorkers(IEnumerable<object> workers)
        {
            Console.WriteLine("\n=== Список работников ===");
            int count = 0;
            foreach (var worker in workers)
            {
                count++;
                Console.WriteLine($"  {count}. {worker}");
            }
            Console.WriteLine($"Всего: {count} работников");
        }

        /// <summary>
        /// Отображает сообщение в консоли с указанным типом
        /// </summary>
        /// <param name="message">Текст сообщения</param>
        /// <param name="type">Тип сообщения (Success, Error, Warning, Info)</param>
        public void DisplayMessage(string message, MessageType type)
        {
            ConsoleColor color;

            // ИСПРАВЛЕНО: обычный switch вместо switch expression
            switch (type)
            {
                case MessageType.Success:
                    color = ConsoleColor.Green;
                    break;
                case MessageType.Error:
                    color = ConsoleColor.Red;
                    break;
                case MessageType.Warning:
                    color = ConsoleColor.Yellow;
                    break;
                case MessageType.Info:
                    color = ConsoleColor.Cyan;
                    break;
                default:
                    color = ConsoleColor.White;
                    break;
            }

            Console.ForegroundColor = color;
            Console.WriteLine($"\n{message}");
            Console.ResetColor();
        }

        /// <summary>
        /// Отображает информацию о строительной площадке
        /// </summary>
        /// <param name="info">Объект с информацией о стройке</param>
        public void DisplayConstructionInfo(object info)
        {
            Console.WriteLine("\n=== Информация о строительной площадке ===");
            Console.WriteLine(info);
        }

        /// <summary>
        /// Загружает и отображает доступные специализации
        /// </summary>
        /// <param name="specializations">Массив строк с названиями специализаций</param>
        public void LoadSpecializations(string[] specializations)
        {
            Console.WriteLine("\n=== Доступные специализации ===");
            for (int i = 0; i < specializations.Length; i++)
            {
                Console.WriteLine($"  {i + 1}. {specializations[i]}");
            }
        }

        /// <summary>
        /// Очищает поля ввода (в консольной версии просто выводит сообщение)
        /// </summary>
        public void ClearInputFields()
        {
            Console.WriteLine("\n[Поля ввода очищены]");
        }

        /// <summary>
        /// Запрашивает и возвращает имя рабочего
        /// </summary>
        /// <returns>Строка с именем рабочего или null, если ввод был прерван</returns>
        public string GetWorkerName()
        {
            Console.Write("Введите имя рабочего: ");
            return Console.ReadLine();
        }

        /// <summary>
        /// Запрашивает и возвращает возраст рабочего
        /// </summary>
        /// <returns>Возраст рабочего или null при некорректном вводе</returns>
        public int? GetWorkerAge()
        {
            Console.Write("Введите возраст рабочего: ");
            if (int.TryParse(Console.ReadLine(), out var age) && age > 0 && age < 100)
                return age;

            Console.WriteLine("Некорректный возраст");
            return null;
        }

        /// <summary>
        /// Запрашивает и возвращает зарплату рабочего
        /// </summary>
        /// <returns>Зарплата рабочего или null при некорректном вводе</returns>
        public decimal? GetWorkerSalary()  // ИСПРАВЛЕНО: decimal? вместо int?
        {
            Console.Write("Введите зарплату рабочего: ");
            if (decimal.TryParse(Console.ReadLine(), out var salary) && salary > 0)
                return salary;

            Console.WriteLine("Некорректная зарплата");
            return null;
        }

        /// <summary>
        /// Запрашивает и возвращает специализацию рабочего
        /// </summary>
        /// <returns>Строка со специализацией рабочего</returns>
        public string GetWorkerSpecialization()
        {
            Console.Write("Введите специализацию рабочего: ");
            return Console.ReadLine();
        }

        /// <summary>
        /// Запрашивает параметры фильтрации работников
        /// </summary>
        /// <returns>Объект FilterDto с критериями фильтрации</returns>
        public object GetFilterCriteria()
        {
            Console.WriteLine("\n=== Параметры фильтрации ===");

            var filter = new FilterDto();

            Console.Write("Фильтр по имени (оставьте пустым для пропуска): ");
            filter.Name = Console.ReadLine();

            Console.Write("Минимальный возраст (оставьте пустым для пропуска): ");
            if (int.TryParse(Console.ReadLine(), out var minAge) && minAge > 0)
                filter.MinAge = minAge;

            Console.Write("Максимальный возраст (оставьте пустым для пропуска): ");
            if (int.TryParse(Console.ReadLine(), out var maxAge) && maxAge > 0)
                filter.MaxAge = maxAge;

            Console.Write("Минимальная зарплата (оставьте пустым для пропуска): ");
            if (decimal.TryParse(Console.ReadLine(), out var minSalary) && minSalary > 0)
                filter.MinSalary = minSalary;

            Console.Write("Максимальная зарплата (оставьте пустым для пропуска): ");
            if (decimal.TryParse(Console.ReadLine(), out var maxSalary) && maxSalary > 0)
                filter.MaxSalary = maxSalary;

            Console.Write("Специализация (оставьте пустым для пропуска): ");
            filter.Specialization = Console.ReadLine();

            return filter;
        }

        /// <summary>
        /// Запрашивает ID выбранного рабочего
        /// </summary>
        /// <returns>ID рабочего или null при некорректном вводе</returns>
        public int? GetSelectedWorkerId()
        {
            if (_selectedWorkerId.HasValue)
            {
                Console.WriteLine($"Текущий выбранный ID: {_selectedWorkerId}");
                return _selectedWorkerId;
            }

            Console.Write("Введите ID рабочего: ");
            if (int.TryParse(Console.ReadLine(), out var id) && id > 0)
            {
                _selectedWorkerId = id;
                return id;
            }

            Console.WriteLine("Некорректный ID");
            return null;
        }

        /// <summary>
        /// Устанавливает данные рабочего для редактирования
        /// </summary>
        /// <param name="workerData">Объект с данными рабочего</param>
        public void SetWorkerDataForEdit(object workerData)
        {
            _workerDataForEdit = workerData;
            _selectedWorkerId = GetIdFromWorkerData(workerData);

            Console.WriteLine("\n[Данные рабочего загружены для редактирования]");
            Console.WriteLine(workerData);
        }

        /// <summary>
        /// Запускает основной цикл консольного интерфейса
        /// </summary>
        public void Run()
        {
            Console.WriteLine("\n=== Консольный интерфейс управления стройкой ===");

            // Вызываем событие загрузки представления
            ViewLoaded?.Invoke(this, EventArgs.Empty);

            bool exit = false;

            while (!exit)
            {
                ShowMainMenu();
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": // Показать всех работников
                        ViewLoaded?.Invoke(this, EventArgs.Empty);
                        break;

                    case "2": // Добавить работника
                        AddWorkerRequested?.Invoke(this, EventArgs.Empty);
                        break;

                    case "3": // Удалить работника
                        var deleteId = GetSelectedWorkerId();
                        if (deleteId.HasValue)
                        {
                            DeleteWorkerRequested?.Invoke(this, deleteId.Value);
                        }
                        break;

                    case "4": // Показать детали работника
                        var detailId = GetSelectedWorkerId();
                        if (detailId.HasValue)
                        {
                            ShowWorkerDetailsRequested?.Invoke(this, detailId.Value);
                        }
                        break;

                    case "5": // Обновить работника
                        UpdateWorkerRequested?.Invoke(this, EventArgs.Empty);
                        break;

                    case "6": // Фильтровать работников
                        FilterWorkersRequested?.Invoke(this, EventArgs.Empty);
                        break;

                    case "7": // Информация о стройке
                        GetConstructionInfoRequested?.Invoke(this, EventArgs.Empty);
                        break;

                    case "8": // Выбрать работника для операций
                        GetSelectedWorkerId();
                        break;

                    case "0": // Выход
                        exit = true;
                        Console.WriteLine("\nВыход из программы...");
                        break;

                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }

                if (!exit)
                {
                    Console.WriteLine("\nНажмите Enter для продолжения...");
                    Console.ReadLine();
                }
            }
        }

        /// <summary>
        /// Извлекает ID из данных рабочего с использованием рефлексии
        /// </summary>
        /// <param name="workerData">Объект с данными рабочего</param>
        /// <returns>ID рабочего или null, если не удалось извлечь</returns>
        private int? GetIdFromWorkerData(object workerData)
        {
            try
            {
                // Пытаемся получить свойство Id через рефлексию
                var idProperty = workerData.GetType().GetProperty("Id");
                if (idProperty != null)
                {
                    return (int?)idProperty.GetValue(workerData);
                }
            }
            catch
            {
                // Если не удалось - возвращаем null
            }

            return null;
        }

        /// <summary>
        /// Отображает главное меню консольного интерфейса
        /// </summary>
        private void ShowMainMenu()
        {
            Console.WriteLine("\n=== ГЛАВНОЕ МЕНЮ ===");
            Console.WriteLine("1. Показать всех работников");
            Console.WriteLine("2. Добавить нового работника");
            Console.WriteLine("3. Удалить работника");
            Console.WriteLine("4. Показать детали работника");
            Console.WriteLine("5. Обновить данные работника");
            Console.WriteLine("6. Фильтровать работников");
            Console.WriteLine("7. Показать информацию о стройке");
            Console.WriteLine("8. Выбрать работника (установить ID)");
            Console.WriteLine("0. Выход");
            Console.Write("\nВаш выбор: ");
        }
    }

    /// <summary>
    /// Data Transfer Object для хранения критериев фильтрации работников
    /// </summary>
    public class FilterDto
    {
        /// <summary>
        /// Имя рабочего для фильтрации
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// Минимальный возраст для фильтрации
        /// </summary>
        public int? MinAge { get; set; }

        /// <summary>
        /// Максимальный возраст для фильтрации
        /// </summary>
        public int? MaxAge { get; set; }

        /// <summary>
        /// Минимальная зарплата для фильтрации
        /// </summary>
        public decimal? MinSalary { get; set; }

        /// <summary>
        /// Максимальная зарплата для фильтрации
        /// </summary>
        public decimal? MaxSalary { get; set; }

        /// <summary>
        /// Специализация для фильтрации
        /// </summary>
        public string Specialization { get; set; } = "";

        /// <summary>
        /// Возвращает строковое представление объекта фильтра
        /// </summary>
        /// <returns>Строка с параметрами фильтрации</returns>
        public override string ToString()
        {
            return $"Фильтр: Имя='{Name}', Возраст={MinAge}-{MaxAge}, Зарплата={MinSalary}-{MaxSalary}, Специализация='{Specialization}'";
        }
    }
}