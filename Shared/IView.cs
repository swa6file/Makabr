using System;
using System.Collections.Generic;

namespace Shared
{
    public interface IView
    {
        // События
        event EventHandler ViewLoaded;
        event EventHandler AddWorkerRequested;
        event EventHandler<int> DeleteWorkerRequested;
        event EventHandler<int> ShowWorkerDetailsRequested;
        event EventHandler GetConstructionInfoRequested;
        event EventHandler UpdateWorkerRequested;
        event EventHandler FilterWorkersRequested;

        // Методы для получения данных
        string GetWorkerName();
        int? GetWorkerAge();
        decimal? GetWorkerSalary();  // исправлено на decimal?
        string GetWorkerSpecialization();
        int? GetSelectedWorkerId();

        // Для фильтрации
        object GetFilterCriteria();

        // Методы для отображения
        void DisplayWorkers(IEnumerable<object> workers);
        void DisplayMessage(string message, MessageType type);
        void DisplayConstructionInfo(object info);
        void LoadSpecializations(string[] specializations);
        void ClearInputFields();
        void SetWorkerDataForEdit(object workerData);
    }

    public enum MessageType
    {
        Success,
        Error,
        Warning,
        Info
    }
}