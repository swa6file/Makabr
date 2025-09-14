using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace BusinessLogical
{
    public class Logic
    {
        public List<Worker> Workers = new List<Worker>();
        public void AddWorker(string name, int age, int salary, WorkerSpecialization specialization)
        {
            Worker worker = new Worker { Name = name, Age = age, Salary = salary, Speciality= specialization };
            Workers.Add(worker);
        }

        public void DeleteWorker(int id)
        {
            if (Workers.Any(w => w.Id == id))
            {
                Workers.Remove(Workers[id]);
            }
            else
            {

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

        public void ChangeWorkers(int id)
        {
            
        }

        public void SortSpeciality()
        {

        }

        public void FullSalaryOfAll()
        {

        }



    }
}
