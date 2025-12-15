using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogical.Interfaces;
using Model;
using Shared;

namespace Presenter
{
    public class MainPresenter
    {
        private readonly IView _view;
        private readonly ILogic _logic;

        public MainPresenter(IView view, ILogic logic)
        {
            _view = view;
            _logic = logic;
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            _view.ViewLoaded += OnViewLoaded;
            _view.AddWorkerRequested += OnAddWorkerRequested;
            _view.DeleteWorkerRequested += OnDeleteWorkerRequested;
            _view.ShowWorkerDetailsRequested += OnShowWorkerDetailsRequested;
            _view.GetConstructionInfoRequested += OnGetConstructionInfoRequested;
            _view.UpdateWorkerRequested += OnUpdateWorkerRequested;
            _view.FilterWorkersRequested += OnFilterWorkersRequested;
        }


        private void OnViewLoaded(object sender, EventArgs e)
        {
            var workers = _logic.GetAllWorkers();
            var specializations = _logic.GetAvailableSpecializations();
            _view.DisplayWorkers(workers.Cast<object>());
            _view.LoadSpecializations(specializations);
        }

        private void OnAddWorkerRequested(object sender, EventArgs e)
        {
            
            var name = _view.GetWorkerName();
            var age = _view.GetWorkerAge();
            var salary = _view.GetWorkerSalary();
            var specStr = _view.GetWorkerSpecialization();

   
            var result = _logic.AddWorker(name, age, salary, specStr);

            _view.DisplayMessage(result.Message, result.IsSuccess ? MessageType.Success : MessageType.Error);

            if (result.IsSuccess)
            {
                _view.ClearInputFields();
                RefreshWorkersList();
            }
        }

        private void OnDeleteWorkerRequested(object sender, int workerId)
        {
            var result = _logic.DeleteWorker(workerId);
            _view.DisplayMessage(result.Message, result.IsSuccess ? MessageType.Success : MessageType.Error);

            if (result.IsSuccess)
            {
                RefreshWorkersList();
            }
        }

        private void OnShowWorkerDetailsRequested(object sender, int workerId)
        {
            var worker = _logic.GetWorkerById(workerId);

            if (worker != null)
            {
                var workerData = new
                {
                    worker.Id,
                    worker.Name,
                    worker.Age,
                    worker.Salary,
                    Specialization = worker.Specialization.ToString()
                };
                _view.SetWorkerDataForEdit(workerData);
            }
            else
            {
                _view.DisplayMessage("Рабочий не найден", MessageType.Error);
            }
        }

        private void OnGetConstructionInfoRequested(object sender, EventArgs e)
        {
            var info = _logic.GetConstructionInfo();
            _view.DisplayConstructionInfo(info);
        }

        private void OnUpdateWorkerRequested(object sender, EventArgs e)
        {
            var workerId = _view.GetSelectedWorkerId();
            var name = _view.GetWorkerName();
            var age = _view.GetWorkerAge();
            var salary = _view.GetWorkerSalary();
            var specStr = _view.GetWorkerSpecialization();

            var result = _logic.UpdateWorker(workerId, name, age, salary, specStr);

            _view.DisplayMessage(result.Message, result.IsSuccess ? MessageType.Success : MessageType.Error);

            if (result.IsSuccess)
            {
                _view.ClearInputFields();
                RefreshWorkersList();
            }
        }

        private void OnFilterWorkersRequested(object sender, EventArgs e)
        {
            var filterData = _view.GetFilterCriteria();
            var workers = _logic.GetAllWorkers();
            _view.DisplayWorkers(workers.Cast<object>());
        }

        private void RefreshWorkersList()
        {
            var workers = _logic.GetAllWorkers();
            _view.DisplayWorkers(workers.Cast<object>());
        }
    }
}