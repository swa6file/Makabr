using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
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
    public interface IDomainObject
    {
        int Id { get; set; }


    }
    /// <summary>
    /// Основная информация о работнике
    /// </summary>
    public class Worker : IDomainObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Salary { get; set; }
        public Specialization Specialization { get; set; }

        public string DisplayInfo
        {
            get { return $"{Name} - {Age} лет - {Salary} руб. - {Specialization}"; }
        }


    }
}
