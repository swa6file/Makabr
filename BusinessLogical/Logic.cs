using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.OleDb;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BusinessLogical
{
/// <summary>
/// Спеацилизицая работников стройки
/// </summary>
    public enum Specialization
    {
        /// <summary>
        /// Электрик
        /// </summary>
        Eletrecian = 1,
        /// <summary>
        /// Маляр
        /// </summary>
        Painter = 2,
        /// <summary>
        /// Крановщик
        /// </summary>
        CraneOperator = 3,
        /// <summary>
        /// Разнорабочий
        /// </summary>
        GeneralWorker = 4
    }
    /// <summary>
    /// Основная информация о работнике
    /// </summary>
    public class Worker
    {
        private static int _nextId = 1;
        /// <summary>
        /// Конструктор генирирующий авто идентификатор
        /// </summary>
        public Worker()
        {
            Id = _nextId++;
        }
        /// <summary>
        /// Уникальный идентификатор работника
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// Имя работника
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Возраст рабочего
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// Зарплата рабочего
        /// </summary>
        public int Salary { get; set; }
        /// <summary>
        /// Специализация работника
        /// </summary>
        public Specialization Specialization { get; set; }

    }
    /// <summary>
    /// Содержит бизнес логику для управления работниками стройки
    /// </summary>
    public class Logic
    {
        public ObservableCollection<Worker> Workers = new ObservableCollection<Worker>();

        /// <summary>
        /// Показывает доступные специализации
        /// </summary>
        public void ShowSpecializations()
        {
            Console.WriteLine("Выберите специальность:");
            Console.WriteLine("1. Электрик");
            Console.WriteLine("2. Маляр");
            Console.WriteLine("3. Крановщик");
            Console.WriteLine("4. Разнорабочий");
        }

        /// <summary>
        /// Создает и добавляет нового работника в коллекцию работников.
        /// </summary>
        /// <param name="name">Имя работника</param>
        /// <param name="age">Возраст работника</param>
        /// <param name="salary">Зарплата работника</param>
        /// <param name="spec">Специализация работника</param>
        /// <returns>Строку объявляющую о добавлении пользователя</returns>
        public string AddWorker(string name, int age, int salary, Specialization spec)
        {
            Specialization specialization = (Specialization)spec;
            Worker worker = new Worker { Name = name, Age = age, Salary = salary, Specialization = specialization };
            Workers.Add(worker);
            return $"Добавлен новый рабочий {name}";
        }
        /// <summary>
        /// Удаляет работника по id, из коллекции работников
        /// </summary>
        /// <param name="id">Идентификатор работника</param>
        /// <returns>Результат  выполнения операции</returns>
        public string DeleteWorker(int id)
        {
            Worker wrk_del = Workers.FirstOrDefault(w => w.Id == id);
            if (wrk_del != null)
            {
                Workers.Remove(wrk_del);
                return $"Работник {wrk_del.Name} - {wrk_del.Specialization} был уволен";
            }
            else
            {
                return "Нечего удалять";
            }
        }

        /// <summary>
        /// Выдает информацию о работниках на стройке
        /// </summary>
        /// <returns>
        /// 1 - Строку с информацией о всех работниках
        /// 2 - Сообщение об отсутствие работников
        /// </returns>
        public string ReadWorkers()
        {
            if (Workers.Count != 0)
            {
                string lst_wrks = "";
                foreach (var wrk in Workers)
                {
                    lst_wrks += "==================================\n";
                    lst_wrks += $"ID: {wrk.Id}\n";
                    lst_wrks += $"Имя: {wrk.Name}\n";
                    lst_wrks += $"Возраст: {wrk.Age}\n";
                    lst_wrks += $"Зарплата: {wrk.Salary}\n";
                    lst_wrks += $"Специализация: {wrk.Specialization}\n";
                    lst_wrks += "==================================\n\n";
                }
                return lst_wrks;
            }
            else
            {
                return "Никто не работает на стройке";
            };
        }

        /// <summary>
        /// Изменяет работника по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <returns>
        /// 1 - Информацию о успешном изменении данных работника
        /// 2 - Информацию о некорректном выборе
        ///</returns>
        public string ChangeWorkers(int id)
        {
            try
            {
                Worker work_choose = Workers.SingleOrDefault(w => w.Id == id);
                Console.WriteLine("Выберите что нужно изменить:");
                Console.WriteLine("1. Зарплата");
                Console.WriteLine("2. Специализацию");

                int.TryParse(Console.ReadLine(), out int chose);
                if (chose == 1)
                {
                    int.TryParse(Console.ReadLine(), out int salar);
                    while (!CheckSalary(salar))
                    {
                        Console.WriteLine("Введите корректную заработную плату");
                        int.TryParse(Console.ReadLine(), out salar);
                    }
                    work_choose.Salary = salar;

                }
                else if (chose == 2)
                {
                    ShowSpecializations();
                    int.TryParse(Console.ReadLine(), out int special);
                    while (!CheckSpecialization(special))
                    {
                        Console.WriteLine("Выберите правильно специализацию");
                        ShowSpecializations();
                        int.TryParse(Console.ReadLine(), out special);
                    }
                    Specialization specialization = (Specialization)special;
                    work_choose.Specialization = specialization;
                }

                return "Данные были изменены";
            }
            catch
            {
                return "Выбор был некорректен";
            }
        }
        /// <summary>
        /// Выводит информацию о работниках конкретной специализации
        /// </summary>
        /// <param name="spec">Специализация рабочих</param>
        /// <returns>
        /// 1 - Информация о людях с такой специализацией
        /// 2 - Информация о отсутствии людей с такой специализацией
        /// 3 - Информацию о некорректном выборе
        /// </returns>
        public string SortSpeciality(int spec)
        {
            try
            {
                Specialization speci = (Specialization)spec;
                List<Worker> workerss = Workers.Where(w => w.Specialization == speci).ToList();
                string s = "";
                if (workerss.Count != 0)
                {
                    foreach (Worker worker in workerss)
                    {
                        s += "==================================\n";
                        s += $"ID: {worker.Id}\n";
                        s += $"Имя: {worker.Name}\n";
                        s += $"Возраст{worker.Age}\n";
                        s += $"Зарплата {worker.Salary}\n";
                        s += $"Специализация {worker.Specialization}\n";
                        s += "==================================\n\n";
                    }
                }
                else
                {
                    s += "Людей с такой специализации нету";
                }
                return s;
            }
            catch
            {
                return "Выбор был некорректен";
            }
        }

        /// <summary>
        /// Выводит общую информацию о работниках на стройке
        /// </summary>
        /// <returns></returns>
        public string InformationAboutConstruction()
        {
            int allsalery = Workers.Sum(w => w.Salary);
            int electricians = Workers.Count(w => w.Specialization == Specialization.Eletrecian);
            int painters = Workers.Count(w => w.Specialization == Specialization.Painter);
            int craneOperators = Workers.Count(w => w.Specialization == Specialization.CraneOperator);
            int generalWorkers = Workers.Count(w => w.Specialization == Specialization.GeneralWorker);
            return $"Общая сумма зарплаты всех работников {allsalery} руб\n" +
                $"Кол-во Электриков {electricians}" +
                $"Кол-во Маляров {painters}" +
                $"Кол-во Крановщиков {craneOperators}" +
                $"Кол-во Разнорабочих {generalWorkers}";
        }

        /// <summary>
        /// Проверка валидности имени работника
        /// </summary>
        /// <param name="name">Имя работника</param>
        /// <returns>
        /// true - если имя прошло валидацию
        /// false - если не прошло валидацию
        /// </returns>
        /// <remarks>
        /// Запрещенные символы: !@#$%&*()_+=[]{}|\/&lt;&gt;?;:"'№
        /// </remarks>
        public bool CheckName(string name)
        {
            char[] forbiddenChars = { '!', '@', '#', '$', '%', '&', '*', '(', ')',
                             '_', '+', '=', '[', ']', '{', '}', '|', '\\',
                             '/', '<', '>', '?', ';', ':', '"', '\'','№' }
            ;
            if (forbiddenChars.Any(w => name.Contains(w)) || name.Any(char.IsDigit))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// Проверка возраста работника
        /// </summary>
        /// <param name="age">Возраст работника</param>
        /// <returns>
        /// true - если возраст работника подходит
        /// false - если не подходит
        /// </returns>
        /// <remarks>
        /// Возраст должен быть от 18 до 65
        /// </remarks>
        public bool CheckAge(int age)
        {
            if (age < 18 || age > 65)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// Проверка на валидность зарплаты
        /// </summary>
        /// <param name="salary">Зарплата работника</param>
        /// <returns>
        /// true - если зарплата прошла валидацию
        /// false - если нет
        /// </returns>
        ///<remarks>
        /// Зарплата работника должна быть от 25_000 до 1_000_000
        /// </remarks>
        public bool CheckSalary(int salary)
        {
            if (salary < 25000 || salary > 1000000)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// Проверка cпециализации на существование
        /// </summary>
        /// <param name="spec">Специализация работника</param>
        /// <returns>
        /// true - специализация существует
        /// false - такой специализации нет
        /// </returns>
        public bool CheckSpecialization(int spec)
        {
            if (spec < 1 || spec > 4)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
