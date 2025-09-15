using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public enum Specialization
    {
        Eletrecian = 1,
        Painter = 2,
        CraneOperator = 3,
        GeneralWorker = 4 
    }
    public class Worker
    {
        private static int _nextId = 1;
        public Worker() 
        {
            Id = _nextId++;
        }
        public int Id { get; private set; }

        public string Name { get; set; }

        public int Age {  get; set; }

        public int Salary { get; set; }

        public Specialization Specialization { get; set; }

    }
}
