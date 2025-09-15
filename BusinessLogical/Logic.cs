using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace BusinessLogical
{
    public class Logic
    {
        public List<Worker> Workers = new List<Worker>();

        public void ShowSpecializations()
        {
            Console.WriteLine("Выберите специальность:");
            Console.WriteLine("1. Электрик");
            Console.WriteLine("2. Маляр");
            Console.WriteLine("3. Крановщик");
            Console.WriteLine("4. Разнорабочий");
        }
        public string AddWorker(string name, int age, int salary, int value)
        {
            if (value < 1 || value > 4)
            {
                return "Некорректный ввод специальности";
            }
            else
            {
                Specialization specialization = (Specialization)value;
                Worker worker = new Worker { Name = name, Age = age, Salary = salary, Specialization = specialization};
                Workers.Add(worker);
                return $"Добавлен новый рабочий {name}";
            }
        }

        public string DeleteWorker(int id)
        {
            if (Workers.Any(w => w.Id == id))
            {
                Workers.Remove(Workers[id]);
                return "Работник был уволен";
            }
            else
            {
                return "Нечего удалять";
            }
        }

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
                    work_choose.Salary = salar;
                }
                else if (chose == 2)
                {
                    ShowSpecializations();
                    int.TryParse(Console.ReadLine(), out int special);
                    Specialization specialization = (Specialization)special;
                    work_choose.Specialization = specialization;
                }
                
                return "Данные были изменены";
            }
            catch (Exception ex)
            {
                return "Нету";
            }
        }

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
                return "Такой специализации не существует";
            }
        }

        public string FullSalaryOfAll()
        {
            return $"Общая сумма зарплаты всех работников {Workers.Sum(w => w.Salary)} руб";
        }



    }
}
