using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.OleDb;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
namespace BusinessLogical
{
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
        public ObservableCollection<Worker> ReadWorkers()
        {
            return Workers;
        }

        
        /// <summary>
        /// Выводит информацию о работниках по фильтру
        /// </summary>
        /// <param name="spec">Специализация рабочих</param>
        /// <returns>
        /// 1 - Информация о людях с такой специализацией
        /// 2 - Информация о отсутствии людей с такой специализацией
        /// 3 - Информацию о некорректном выборе
        /// </returns>
        public IQueryable<Worker> SortedWorkers(string name, int? sage, int? eage, int? ssalary, int? essalary, Specialization? specialization)
        {
            var query = Workers.AsQueryable();


            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(w => w.Name.Contains(name));
            }


            if (sage.HasValue)
            {
                query = query.Where(w => w.Age >= sage.Value);
            }
            if (eage.HasValue)
            {
                query = query.Where(w => w.Age <= eage.Value);
            }


            if (ssalary.HasValue)
            {
                query = query.Where(w => w.Salary >= ssalary.Value);
            }
            if (essalary.HasValue)
            {
                query = query.Where(w => w.Salary <= essalary.Value);
            }


            if (specialization.HasValue)
            {
                query = query.Where(w => w.Specialization == specialization.Value);
            }
            return query;
        }

        /// <summary>
        /// Выводит общую информацию о работниках на стройке
        /// </summary>
        /// <returns></returns>       
        public List<int> InformationAboutConstruction()
        {
            List<int> info = new List<int>();
            info.Add(Workers.Sum(w => w.Salary));
            info.Add(Workers.Count(w => w.Specialization == Specialization.Eletrecian));
            info.Add(Workers.Count(w => w.Specialization == Specialization.Painter));
            info.Add(Workers.Count(w => w.Specialization == Specialization.CraneOperator));
            info.Add(Workers.Count(w => w.Specialization == Specialization.GeneralWorker));
            return info;
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
        public bool CheckAge(string age)
        {
            int.TryParse(age, out int agee);
            if (agee < 18 || agee > 65)
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
        public bool CheckSalary(string salary)
        {
            int.TryParse(salary, out int ssalary);
            if (ssalary < 25000 || ssalary > 1000000)
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
