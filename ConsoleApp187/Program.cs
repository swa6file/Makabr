using System;
using System.Collections.Generic;
using System.Linq;
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
                        Console.WriteLine("Введите зарплату");
                        int.TryParse(Console.ReadLine(), out int salary);
                        logic.ShowSpecializations();
                        int.TryParse(Console.ReadLine(), out var value);
                        Console.WriteLine(logic.AddWorker(name, age, salary, value));
                        break;
                    case 2:
                        Console.WriteLine("Введите id работника");
                        int.TryParse(Console.ReadLine(), out int id);
                        Console.WriteLine(logic.DeleteWorker(id));
  
                        break;
                    case 3:
                        Console.WriteLine(logic.ReadWorkers());
                        break;
                    case 4:
                        Console.WriteLine("Введите id работника");
                        int.TryParse(Console.ReadLine(), out int idd);
                        logic.ChangeWorkers(idd);
                        break;
                    case 5:
                        int.TryParse(Console.ReadLine(), out int spe);
                        Console.WriteLine(logic.SortSpeciality(spe));
                        break;
                    case 6:
                        Console.WriteLine(logic.FullSalaryOfAll());
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
