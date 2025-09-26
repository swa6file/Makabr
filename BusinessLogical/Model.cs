using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.OleDb;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BusinessLogical
{
        /// <summary>
        /// Спеацилизицая работников стройки
        /// </summary>
        public enum Specialization
        {
            /// <summary>
            /// Электрик
            /// </summary>
            Eletrecian = 1,
            /// <summary>
            /// Маляр
            /// </summary>
            Painter = 2,
            /// <summary>
            /// Крановщик
            /// </summary>
            CraneOperator = 3,
            /// <summary>
            /// Разнорабочий
            /// </summary>
            GeneralWorker = 4
        }
        /// <summary>
        /// Основная информация о работнике
        /// </summary>
        public class Worker
        {
            private static int _nextId = 1;
            /// <summary>
            /// Конструктор генирирующий авто идентификатор
            /// </summary>
            public Worker()
            {
                Id = _nextId++;
            }
            /// <summary>
            /// Уникальный идентификатор работника
            /// </summary>
            public int Id { get; private set; }
            /// <summary>
            /// Имя работника
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// Возраст рабочего
            /// </summary>
            public int Age { get; set; }
            /// <summary>
            /// Зарплата рабочего
            /// </summary>
            public int Salary { get; set; }
            /// <summary>
            /// Специализация работника
            /// </summary>
            public Specialization Specialization { get; set; }

        }
}
