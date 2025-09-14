using System;
using System.Collections.Generic;
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
        public string AddWorker(string name, int age, int salary, WorkerSpecialization specialization)
        {
            Worker worker = new Worker { Name = name, Age = age, Salary = salary, Speciality= specialization };
            Workers.Add(worker);
            return $"Добавлен новый рабочий {name}";    
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
                    lst_wrks += $"Имя: {wrk.Name}\n";
                    lst_wrks += $"Возраст{wrk.Age}\n";
                    lst_wrks += $"Зарплата {wrk.Salary}\n";
                    lst_wrks += $"Специальность {wrk.Speciality} \n";
                    lst_wrks += "==================================\n";
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
                Workers.SingleOrDefault(w => w.Id == id);
                Console.WriteLine("Выберите что нужноизменить");

                return "";
            }
            catch (Exception ex)
            {
                return "Нету";
            }
        }

        public void SortSpeciality(int spec)
        {
            if (spec == 1)
            {
                ;
            }
            else if (spec == 2)
            {
                ;
            }
            else if (spec == 3)
            {
                ;
            }
            else if (spec == 4)
            {
                ;
            }
            else 
            {

            }
        }

        public void FullSalaryOfAll()
        {

        }



    }
}
