using System;
using System.Reflection;
using Shared;

namespace Presenter
{
    /// <summary>
    /// Фабрика для создания экземпляров представлений (IView) 
    /// с динамической загрузкой сборок.
    /// </summary>
    public static class ViewFactory
    {
        /// <summary>
        /// Создает консольное представление, пытаясь загрузить 
        /// пользовательскую реализацию из сборки ConsoleApp187.dll.
        /// </summary>
        /// <returns>
        /// Экземпляр пользовательского ConsoleView при успешной загрузке,
        /// либо экземпляр DefaultConsoleView в случае ошибки.
        /// </returns>
        public static IView CreateConsoleView()
        {
            try
            {
                var assembly = Assembly.LoadFrom("ConsoleApp187.dll");
                var viewType = assembly.GetType("ConsoleApp187.ConsoleView");

                if (viewType == null)
                {
                    return new DefaultConsoleView();
                }

                var viewInstance = Activator.CreateInstance(viewType) as IView;

                if (viewInstance == null)
                {
                    return new DefaultConsoleView();
                }

                return viewInstance;
            }
            catch
            {
                return new DefaultConsoleView();
            }
        }

        /// <summary>
        /// Создает Windows Forms представление, пытаясь загрузить 
        /// форму Form1 из сборки WindowsFormsApp1.dll.
        /// </summary>
        /// <returns>
        /// Экземпляр Form1 при успешной загрузке,
        /// либо консольное представление (CreateConsoleView()) в случае ошибки.
        /// </returns>
        public static IView CreateWindowsFormsView()
        {
            try
            {
                var assembly = Assembly.LoadFrom("WindowsFormsApp1.dll");
                var viewType = assembly.GetType("WindowsFormsApp1.Form1");

                if (viewType == null)
                {
                    return CreateConsoleView();
                }

                var viewInstance = Activator.CreateInstance(viewType) as IView;

                if (viewInstance == null)
                {
                    return CreateConsoleView();
                }

                return viewInstance;
            }
            catch
            {
                return CreateConsoleView();
            }
        }
    }
}