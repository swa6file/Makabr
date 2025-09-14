using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public enum WorkerSpecialization
    {
        Builder = 1,
        Foreman = 2,
        Painter = 3,
        Crane_operator = 4
    }
    public class Worker
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age {  get; set; }

        public int Salary { get; set; }

        public WorkerSpecialization Speciality {get; set; }
    }
}
