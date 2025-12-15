using System.Collections.Generic;
using BusinessLogical.Models;
using BusinessLogical.Services;
using Model;

namespace BusinessLogical.Interfaces
{
    public interface ILogic
    {
        // Методы с nullable параметрами для валидации в Logic
        Result AddWorker(string name, int? age, decimal? salary, string specialization);
        Result DeleteWorker(int id);
        Result UpdateWorker(int? id, string name, int? age, decimal? salary, string specialization);

        Worker GetWorkerById(int workerId);
        IEnumerable<Worker> GetAllWorkers();
        IEnumerable<Worker> FilterWorkers(WorkerFilterCriteria criteria);

        string[] GetAvailableSpecializations();
        ConstructionInfo GetConstructionInfo();

        // События
        event System.EventHandler<Logic.WorkerEventArgs> WorkerAdded;
        event System.EventHandler<Logic.WorkerEventArgs> WorkerDeleted;
        event System.EventHandler<Logic.WorkerEventArgs> WorkerUpdated;
        event System.EventHandler DataChanged;
    }
}