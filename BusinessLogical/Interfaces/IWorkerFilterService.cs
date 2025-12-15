using System.Collections.Generic;
using Model;
using BusinessLogical.Models;

namespace BusinessLogical.Interfaces
{
    public interface IWorkerFilterService
    {
        IEnumerable<Worker> FilterWorkers(WorkerFilterCriteria criteria);
    }
}