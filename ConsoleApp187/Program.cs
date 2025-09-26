using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using BusinessLogical;

namespace ConsoleApp187
{
    internal class Program
    {
       
        static void Main(string[] args)
        {
            Logic logic = new Logic();
            Console.WriteLine("Welcome to the construction site.");
            logic.AddWorker("Алексей", 25, 45000, Specialization.Eletrecian);
            logic.AddWorker("Дмитрий", 32, 38000, Specialization.Painter);
            logic.AddWorker("Иван", 28, 52000, Specialization.CraneOperator);
            logic.AddWorker("Сергей", 35, 29000, Specialization.GeneralWorker);
            logic.AddWorker("Андрей", 22, 41000, Specialization.Eletrecian);
            logic.AddWorker("Анна", 29, 48000, Specialization.Painter);
            logic.AddWorker("Елена", 31, 55000, Specialization.CraneOperator);
            logic.AddWorker("Ольга", 26, 32000, Specialization.GeneralWorker);
            logic.AddWorker("Мария", 33, 47000, Specialization.Eletrecian);
            logic.AddWorker("Наталья", 27, 39000, Specialization.Painter);

            while (true) {
                Console.WriteLine("Меню:");
                Console.WriteLine("1.Добавить специалиста на стройку\n2.Уволить со стройки\n3.Состав стройки\n4.Изменить сотрудника" +
                    "\n5.Фильтрация специалистов\n6.Информация о стройке\n0. Закрыть стройку");
                int.TryParse(Console.ReadLine(), out int lever);
                switch (lever)
                {
                    case 1:
                        Console.WriteLine("Введите имя работника");
                        string name = Console.ReadLine();
                        while(!logic.CheckName(name))
                        {
                            Console.WriteLine("Имя работника было введено некорректно, попробуйте снова");
                            name = Console.ReadLine();
                        }
                        Console.WriteLine("Введите возраст");
                        string age = Console.ReadLine();
                        if (!logic.CheckAge(age))
                        {
                            Console.Clear();
                            Console.WriteLine("Возраст не подходит");
                            break;
                        };
                        Console.WriteLine("Введите зарплату");
                        string salary = Console.ReadLine();
                        while (!logic.CheckSalary(salary))
                        {
                            Console.WriteLine("Введите корректную заработную плату");
                            salary = Console.ReadLine();
                        }
                        logic.ShowSpecializations();
                        int.TryParse(Console.ReadLine(), out int val);
                        while (val < 1 || val > 4)
                        {
                            Console.WriteLine("Выберите правильно специализацию");
                            logic.ShowSpecializations();
                            int.TryParse(Console.ReadLine(), out val);
                        }
                        int.TryParse(age, out int ag);
                        int.TryParse(salary, out int salar);
                        Console.WriteLine(logic.AddWorker(name, ag, salar, (Specialization)val));
                        break;
                    case 2:
                        Console.WriteLine("Введите id работника");
                        int.TryParse(Console.ReadLine(), out int id);
                        Console.WriteLine(logic.DeleteWorker(id));
  
                        break;
                    case 3:
                        var workers = logic.ReadWorkers();
                        if (workers.Count != 0)
                        {
                            string lst_wrks = "";
                            foreach (var wrk in workers)
                            {
                                lst_wrks += "==================================\n";
                                lst_wrks += $"ID: {wrk.Id}\n";
                                lst_wrks += $"Имя: {wrk.Name}\n";
                                lst_wrks += $"Возраст: {wrk.Age}\n";
                                lst_wrks += $"Зарплата: {wrk.Salary}\n";
                                lst_wrks += $"Специализация: {wrk.Specialization}\n";
                                lst_wrks += "==================================\n\n";
                            }
                            Console.WriteLine(lst_wrks);
                        }
                        else
                        {
                            Console.WriteLine("Никто не работает на стройке");
                        };
                        break;
                    case 4:
                        Console.WriteLine("Введите id работника");
                        int.TryParse(Console.ReadLine(), out int idd);
                        var worker = logic.Workers.FirstOrDefault(w => w.Id == idd);
                        Console.WriteLine("Введите имя работника (чтобы оставить прежнее нажмите enter)");
                        name = Console.ReadLine();
                        while (!logic.CheckName(name))
                        {
                            if (name.Trim() == "")
                            {
                                break;
                            }
                            Console.WriteLine("Имя работника было введено некорректно, попробуйте снова");
                            name = Console.ReadLine();
                        }
                        Console.WriteLine("Введите возраст (чтобы оставить прежнее нажмите enter)");
                        age = Console.ReadLine();
                        while (!logic.CheckAge(age))
                        {
                            if (age.Trim() == "")
                            {
                                break;
                            }
                            Console.WriteLine("Возраст не подходит");
                            age = Console.ReadLine();
                        };
                        Console.WriteLine("Введите зарплату (чтобы оставить прежнее нажмите enter)");
                        salary = Console.ReadLine();
                        while (!logic.CheckSalary(salary))
                        {
                            if (salary.Trim() == "")
                            {
                                break;
                            }
                            Console.WriteLine("Введите корректную заработную плату");
                            salary = Console.ReadLine();
                        }
                        logic.ShowSpecializations();
                        Console.WriteLine("чтобы оставить прежнее нажмите enter");
                        string value = Console.ReadLine();
                        int.TryParse(value, out val);
                        while (!(value.Trim() == "") || val < 1 && val > 4)
                        {
                            int.TryParse(value, out val);
                            Console.WriteLine("Выберите правильно специализацию");
                            logic.ShowSpecializations();
                            value = Console.ReadLine();
                        }
                        int.TryParse (age, out ag);
                        int.TryParse (salary, out salar);
                        int.TryParse(value, out val);

                        worker.Name = name != "" ? name : worker.Name;
                        worker.Age = ag == 0 ? worker.Age : ag;
                        worker.Salary = salar == 0 ? worker.Salary: salar;
                        worker.Specialization = val == 0 ? worker.Specialization : (Specialization)val;
                        Console.WriteLine("Данные успешно изменены");


                        break;
                    case 5:
                        Console.WriteLine("Введите имя(или нажмите enter для продолжения):");
                        string fname = Console.ReadLine();
                        while (!logic.CheckName(fname))
                        {
                            if (fname.Trim() == "")
                            {
                                break;
                            }
                            Console.WriteLine("Имя работника было введено некорректно, попробуйте снова");
                            fname = Console.ReadLine();
                        }   
                        Console.WriteLine("Введите стартовый возраст(или нажмите enter для продолжения)");
                        string start_age = Console.ReadLine();
                        while (!logic.CheckAge(start_age))
                        {
                            if (start_age.Trim() == "")
                            {
 
                                break;
                            }
                            Console.WriteLine("Введите корректный возраст");
                            start_age = Console.ReadLine();
                        };
                        Console.WriteLine("Введите конечный возраст(или нажмите enter для продолжения)");
                        string end_age = Console.ReadLine();
                        while (!logic.CheckAge(end_age))
                        {
                            if (end_age.Trim() == "")
                            {

                                break;
                            }
                            Console.WriteLine("Введите корректный возраст");
                            end_age = Console.ReadLine();
                        };
                        Console.WriteLine("Введите начальную зарплату(или нажмите enter для продолжения)");
                        string start_salary = Console.ReadLine();
                        while (!logic.CheckSalary(start_salary))
                        {
                            if (start_salary.Trim() == "")
                            {
                                break;
                            }
                            Console.WriteLine("Введите корректную заработную плату");
                            start_salary = Console.ReadLine();
                        }
                        Console.WriteLine("Введите конечную зарплату(или нажмите enter для продолжения)");
                        string last_salary = Console.ReadLine();
                        while (!logic.CheckSalary(last_salary))
                        {
                            if (last_salary.Trim() == "")
                            {

                                break;
                            }
                            Console.WriteLine("Введите корректную заработную плату ");
                            last_salary= Console.ReadLine();
                        }
                        logic.ShowSpecializations();              
                        string spec = Console.ReadLine();
                        int.TryParse(spec, out val);
                        while (!(spec.Trim() == "") || val < 1 && val > 4)
                        {
                            Console.WriteLine("Выберите правильно специализацию");
                            logic.ShowSpecializations();
                            spec = Console.ReadLine();
                        }
                        int? s_age = int.TryParse(start_age.Trim(), out int sage) ? (int?)sage : null;
                        int? e_age = int.TryParse(end_age.Trim(), out int eage) ? (int?)eage : null;
                        int? s_salary = int.TryParse(start_salary.Trim(), out int ssalary) ? (int?)ssalary : null;
                        int? e_salary = int.TryParse(last_salary.Trim(), out int esalary) ? (int?)esalary : null;
                        Specialization? specialization = null;
                        if (int.TryParse(spec.Trim(), out int specValue))
                        {
                            specialization = (Specialization)specValue;
                        }
                        var query = logic.SortedWorkers(fname, s_age, e_age,s_salary,e_salary,specialization);
                        if (query.ToList().Count != 0)
                        {
                            string lst_wrks = "";
                            foreach (var wrk in query)
                            {
                                lst_wrks += "==================================\n";
                                lst_wrks += $"ID: {wrk.Id}\n";
                                lst_wrks += $"Имя: {wrk.Name}\n";
                                lst_wrks += $"Возраст: {wrk.Age}\n";
                                lst_wrks += $"Зарплата: {wrk.Salary}\n";
                                lst_wrks += $"Специализация: {wrk.Specialization}\n";
                                lst_wrks += "==================================\n\n";
                            }
                            Console.WriteLine(lst_wrks);
                        }
                        else
                        {
                            Console.WriteLine("Никто не работает на стройке");
                        };



                        break;
                    case 6:
                        var info = logic.InformationAboutConstruction();
                        Console.WriteLine("Общая заработная плата: " + info[0]);
                        Console.WriteLine("Кол-во Електриков" + info[0]);
                        Console.WriteLine("Кол-во Маляров"+info[0]);
                        Console.WriteLine("Кол-во Крановщиков" + info[0]);
                        Console.WriteLine("Кол-во Разнорабочих" +    info[0]);
                        break;

                    case 0:
                        Console.WriteLine("The enD");
                        return;
                    default:
                        Console.WriteLine("Некорректное значение. Повторите попытку");
                        break;
                }
            }
          
            
        }
    }
}
