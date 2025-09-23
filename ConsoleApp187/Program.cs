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
                    "\n5.Просмотреть определенных специалистов\n6.Информация о стройке\n0. Закрыть стройку");
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
                        int.TryParse(Console.ReadLine(), out int age);
                        if (!logic.CheckAge(age))
                        {
                            Console.Clear();
                            Console.WriteLine("Возраст не подходит");
                            break;
                        };
                        Console.WriteLine("Введите зарплату");
                        int.TryParse(Console.ReadLine(), out int salary);
                        while (!logic.CheckSalary(salary))
                        {
                            Console.WriteLine("Введите корректную заработную плату");
                            int.TryParse(Console.ReadLine(), out salary);
                        }
                        logic.ShowSpecializations();
                        int.TryParse(Console.ReadLine(), out var value);
                        while (!logic.CheckSpecialization(value))
                        {
                            Console.WriteLine("Выберите правильно специализацию");
                            logic.ShowSpecializations();
                            int.TryParse(Console.ReadLine(), out value);
                        }
                        Console.WriteLine(logic.AddWorker(name, age, salary, (Specialization)value));
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
                        Console.WriteLine("Выберите специальность:");
                        logic.ShowSpecializations();
                        int.TryParse(Console.ReadLine(), out int spe);
                        Console.WriteLine(logic.SortSpeciality(spe));
                        break;
                    case 6:
                        Console.WriteLine(logic.InformationAboutConstruction());
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
