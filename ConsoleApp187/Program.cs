using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogical;
using Model;

namespace ConsoleApp187
{
    internal class Program
    {
       
        static void Main(string[] args)
        {
            Logic logic = new Logic();
            Console.WriteLine("Welcome to the construction site.");

            while (true) {
                Console.WriteLine("Меню:");
                Console.WriteLine("1.Добавить специалиста на стройку\n2.Уволить со стройки\n3.Состав стройки\n4.Изменить сотрудника" +
                    "\n5.Просмотреть определенных специалистов\n6.Просмотреть общую плату рабочим\n0. Закрыть стройку");
                int.TryParse(Console.ReadLine(), out int lever);
                switch (lever)
                {
                    case 1:
                        Console.WriteLine("Введите имя работника");
                        string name = Console.ReadLine();
                        Console.WriteLine("Введите возраст");
                        int.TryParse(Console.ReadLine(), out int age);
                        Console.WriteLine("Выберите специальность");
                        int.TryParse(Console.ReadLine(), out int spec);

                        if (!Enum.IsDefined(typeof(WorkerSpecialization), spec))
                        {
                            break;
                        }
                        WorkerSpecialization specialization = (WorkerSpecialization)spec;
                        Console.WriteLine("Введите зарплату");
                        int.TryParse(Console.ReadLine(), out int salary);
                        logic.AddWorker(name, age, salary, specialization);

                        break;
                    case 2:
                        if (logic.Workers.Count != 0)
                        {
                            Console.WriteLine("Некого увольнять со стройки )))))))");
                        }
                        else
                        {
                            Console.WriteLine("Введите id работника");
                            int.TryParse(Console.ReadLine(), out int id);
                            logic.DeleteWorker(id);
                        }
                        break;
                    case 3:
                        Console.WriteLine(logic.ReadWorkers());
                        break;
                    case 4:
                        if (logic.Workers.Count != 0)
                        {
                            Console.WriteLine("Отсутствуют сотрудники");
                        }
                        else
                        {
                            Console.WriteLine("Введите id работника");
                            int.TryParse(Console.ReadLine(), out int id);
                            logic.ChangeWorkers(id);
                        }
                        break;
                    case 5:
                        logic.SortSpeciality();
                        break;
                    case 6:
                        logic.FullSalaryOfAll();
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
