using System;
using System.Windows.Forms;
using BusinessLogical;
using Ninject;
using Shared;
using ConsoleApp187;
using WindowsFormsApp1;
using BusinessLogical.Interfaces;

namespace Presenter
{
    internal class Program
    {
        private static IKernel _kernel;

        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("=== Система управления строительной площадкой ===");

                InitializeDependencyInjection();

                var viewType = SelectViewType();
                RunApplication(viewType);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n=== КРИТИЧЕСКАЯ ОШИБКА ===");
                Console.WriteLine($"Сообщение: {ex.Message}");
                Console.WriteLine($"Тип ошибки: {ex.GetType().Name}");
                Console.WriteLine($"\nStackTrace:\n{ex.StackTrace}");

                if (ex.InnerException != null)
                {
                    Console.WriteLine($"\n=== ВНУТРЕННЯЯ ОШИБКА ===");
                    Console.WriteLine($"Сообщение: {ex.InnerException.Message}");
                    Console.WriteLine($"Тип: {ex.InnerException.GetType().Name}");
                }

                Console.WriteLine("\nНажмите любую клавишу для выхода...");
                Console.ReadKey();
            }
        }

        private static void InitializeDependencyInjection()
        {
            Console.WriteLine("Инициализация DI контейнера...");
            try
            {
                _kernel = new StandardKernel(new SimpleConfigModule());
                Console.WriteLine("DI контейнер успешно создан");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка создания DI контейнера: {ex.Message}");
                throw;
            }
        }

        private static ViewType SelectViewType()
        {
            Console.WriteLine("\nВыберите тип интерфейса:");
            Console.WriteLine("1. Консольный интерфейс");
            Console.WriteLine("2. Графический интерфейс (Windows Forms)");
            Console.WriteLine("0. Выход");

            while (true)
            {
                Console.Write("\nВаш выбор: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.WriteLine("Выбран консольный интерфейс");
                        return ViewType.Console;
                    case "2":
                        Console.WriteLine("Выбран Windows Forms интерфейс");
                        return ViewType.WindowsForms;
                    case "0":
                        Console.WriteLine("Выход из программы");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }

        private static void RunApplication(ViewType viewType)
        {
            Console.WriteLine($"\nЗапуск {viewType} приложения...");

            switch (viewType)
            {
                case ViewType.Console:
                    RunConsoleApplication();
                    break;
                case ViewType.WindowsForms:
                    RunWindowsFormsApplication();
                    break;
            }
        }

        private static void RunConsoleApplication()
        {
            try
            {
                Console.WriteLine("\n=== ЗАПУСК КОНСОЛЬНОГО ПРИЛОЖЕНИЯ ===");

                Console.WriteLine("Создание консольного представления...");
                var consoleView = ViewFactory.CreateConsoleView();

                if (consoleView == null)
                {
                    Console.WriteLine("ОШИБКА: ViewFactory.CreateConsoleView() вернул null");
                    return;
                }

                Console.WriteLine($"Тип представления: {consoleView.GetType().Name}");
                Console.WriteLine("Создание презентера...");

                var logic = _kernel.Get<ILogic>(); // Получаем ILogic из контейнера
                var presenter = new MainPresenter(consoleView, logic);
                Console.WriteLine("Презентер создан");

                if (consoleView is ConsoleApp187.ConsoleView cv)
                {
                    Console.WriteLine("Запуск ConsoleView.Run()...");
                    cv.Run();
                }
                else if (consoleView is DefaultConsoleView dcv)
                {
                    Console.WriteLine("Запуск DefaultConsoleView.Run()...");
                    dcv.Run();
                }
                else
                {
                    Console.WriteLine($"Неизвестный тип представления: {consoleView.GetType().Name}");
                }

                Console.WriteLine("Консольное приложение завершено");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n=== ОШИБКА В КОНСОЛЬНОМ ПРИЛОЖЕНИИ ===");
                Console.WriteLine($"Сообщение: {ex.Message}");
                Console.WriteLine($"Тип: {ex.GetType().Name}");
                Console.WriteLine($"\nStackTrace:\n{ex.StackTrace}");
                Console.WriteLine("\nНажмите любую клавишу для выхода...");
                Console.ReadKey();
            }
        }

        private static void RunWindowsFormsApplication()
        {
            try
            {
                Console.WriteLine("\n=== ЗАПУСК WINDOWS FORMS ПРИЛОЖЕНИЯ ===");

                Console.WriteLine("Включение визуальных стилей...");
                Application.EnableVisualStyles();

                Console.WriteLine("Настройка рендеринга текста...");
                Application.SetCompatibleTextRenderingDefault(false);

                Console.WriteLine("Создание формы...");
                var formsView = ViewFactory.CreateWindowsFormsView();

                if (formsView == null)
                {
                    Console.WriteLine("ОШИБКА: ViewFactory.CreateWindowsFormsView() вернул null");
                    return;
                }

                Console.WriteLine($"Тип формы: {formsView.GetType().Name}");

                if (!(formsView is IView))
                {
                    Console.WriteLine($"ОШИБКА: Форма не реализует IView. Тип: {formsView.GetType().Name}");
                    return;
                }

                Console.WriteLine("Создание презентера...");
                var logic = _kernel.Get<ILogic>(); // Получаем ILogic из контейнера
                var presenter = new MainPresenter((IView)formsView, logic);
                Console.WriteLine("Презентер создан");

                if (!(formsView is Form))
                {
                    Console.WriteLine($"ОШИБКА: Представление не является Form. Тип: {formsView.GetType().Name}");
                    return;
                }

                Console.WriteLine("Запуск Application.Run()...");
                Form form = (Form)formsView;

                // Подписываемся на события формы для отладки
                form.Load += (s, e) => Console.WriteLine("Форма загружена (Load событие)");
                form.Shown += (s, e) => Console.WriteLine("Форма показана (Shown событие)");
                form.FormClosed += (s, e) => Console.WriteLine("Форма закрыта");

                Console.WriteLine("Передача управления Windows Forms...");
                Application.Run(form);

                Console.WriteLine("Windows Forms приложение завершено");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n=== ОШИБКА В WINDOWS FORMS ПРИЛОЖЕНИИ ===");
                Console.WriteLine($"Сообщение: {ex.Message}");
                Console.WriteLine($"Тип: {ex.GetType().Name}");
                Console.WriteLine($"\nStackTrace:\n{ex.StackTrace}");

                if (ex.InnerException != null)
                {
                    Console.WriteLine($"\n=== ВНУТРЕННЯЯ ОШИБКА ===");
                    Console.WriteLine($"Сообщение: {ex.InnerException.Message}");
                    Console.WriteLine($"Тип: {ex.InnerException.GetType().Name}");
                }

                Console.WriteLine("\nНажмите любую клавишу для выхода...");
                Console.ReadKey();
            }
        }
    }

    public enum ViewType
    {
        Console,
        WindowsForms
    }
}
