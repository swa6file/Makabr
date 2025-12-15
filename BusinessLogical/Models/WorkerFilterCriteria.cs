using Model;

namespace BusinessLogical.Models
{
    public class WorkerFilterCriteria
    {
        public string Name { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        public int? MinSalary { get; set; }
        public int? MaxSalary { get; set; }
        public Specialization? Specialization { get; set; }
    }
}