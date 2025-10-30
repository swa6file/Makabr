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
using Model;
using DataAccessLayer;

namespace BusinessLogical
{
    /// <summary>
    /// Содержит бизнес логику для управления работниками стройки
    /// </summary>
    public class Logic
    {
        private IRepository<Worker> _repository;
        private bool _useEntityFramework;

        private const bool USE_ENTITY_FRAMEWORK = false; 
        /// <summary>
        /// Метод для включения Entity or Dapper
        /// </summary>
        public Logic()
        {
            if (USE_ENTITY_FRAMEWORK)
            {
                string connectionString = GetDefaultConnectionString();
                _repository = new EntityRepository<Worker>(connectionString);
                Console.WriteLine("Используется EntityFramework");
            }
            else
            {
                string connectionString = GetDefaultConnectionString();
                _repository = new DapperRepository<Worker>(connectionString);
                Console.WriteLine("Используется Dapper");
            }
        }

        private string GetDefaultConnectionString()
        {
            return "Data Source=SOVLE\\SQLEXPRESS;Initial Catalog=master;Integrated Security=True";
        }

       

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
        /// Создает и добавляет нового работника в БД
        /// </summary>
        public string AddWorker(string name, int age, int salary, Specialization spec)
        {
            Worker worker = new Worker { Name = name, Age = age, Salary = salary, Specialization = spec };
            _repository.Add(worker);
            return $"Добавлен новый рабочий {name}";
        }

        /// <summary>
        /// Удаляет работника по id из БД
        /// </summary>
        public string DeleteWorker(int id)
        {
            var worker = _repository.ReadById(id);
            if (worker != null)
            {
                _repository.Delete(id);
                return $"Работник {worker.Name} - {worker.Specialization} был уволен";
            }
            else
            {
                return "Работник с таким ID не найден";
            }
        }

        /// <summary>
        /// Получает всех работников из БД
        /// </summary>
        public List<Worker> ReadWorkers()
        {
            return _repository.ReadAll().ToList();
        }

        /// <summary>
        /// Фильтрует работников по критериям
        /// </summary>
        public List<Worker> SortedWorkers(string name, int? sage, int? eage, int? ssalary, int? essalary, Specialization? specialization)
        {
            var allWorkers = _repository.ReadAll().AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                allWorkers = allWorkers.Where(w => w.Name.Contains(name));
            }

            if (sage.HasValue)
            {
                allWorkers = allWorkers.Where(w => w.Age >= sage.Value);
            }
            if (eage.HasValue)
            {
                allWorkers = allWorkers.Where(w => w.Age <= eage.Value);
            }

            if (ssalary.HasValue)
            {
                allWorkers = allWorkers.Where(w => w.Salary >= ssalary.Value);
            }
            if (essalary.HasValue)
            {
                allWorkers = allWorkers.Where(w => w.Salary <= essalary.Value);
            }

            if (specialization.HasValue)
            {
                allWorkers = allWorkers.Where(w => w.Specialization == specialization.Value);
            }

            return allWorkers.ToList();
        }

        /// <summary>
        /// Выводит общую информацию о работниках на стройке
        /// </summary>
        public List<int> InformationAboutConstruction()
        {
            var workers = _repository.ReadAll();

            List<int> info = new List<int>();
            info.Add(workers.Sum(w => w.Salary));
            info.Add(workers.Count(w => w.Specialization == Specialization.Eletrecian));
            info.Add(workers.Count(w => w.Specialization == Specialization.Painter));
            info.Add(workers.Count(w => w.Specialization == Specialization.CraneOperator));
            info.Add(workers.Count(w => w.Specialization == Specialization.GeneralWorker));
            return info;
        }

        public bool CheckName(string name)
        {
            char[] forbiddenChars = { '!', '@', '#', '$', '%', '&', '*', '(', ')',
                             '_', '+', '=', '[', ']', '{', '}', '|', '\\',
                             '/', '<', '>', '?', ';', ':', '"', '\'','№' };
            if (forbiddenChars.Any(w => name.Contains(w)) || name.Any(char.IsDigit))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

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