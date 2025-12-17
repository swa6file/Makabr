using System;
using System.Windows.Input;
using BusinessLogical.Interfaces;
using Ninject;
using Presenter.DTOs;
using Presenter.ViewModels.Base;

namespace Presenter.ViewModels
{
    public class ConstructionInfoViewModel : ViewModelBase
    {
        private readonly ILogic _logic;
        private ConstructionInfoDTO _info;
        private int _totalWorkers;

        public ConstructionInfoDTO Info
        {
            get => _info;
            set => SetField(ref _info, value);
        }

        public int TotalWorkers
        {
            get => _totalWorkers;
            set => SetField(ref _totalWorkers, value);
        }

        public ICommand CloseCommand { get; }

        public ConstructionInfoViewModel()
        {
            var kernel = new StandardKernel(new BusinessLogical.SimpleConfigModule());
            _logic = kernel.Get<ILogic>();

            CloseCommand = new RelayCommand(Close);
        }

        public void LoadData()
        {
            try
            {
                var modelInfo = _logic.GetConstructionInfo();
                Info = ConstructionInfoDTO.FromModel(modelInfo);

                // Вычисляем общее количество
                TotalWorkers = modelInfo.ElectriciansCount +
                              modelInfo.PaintersCount +
                              modelInfo.CraneOperatorsCount +
                              modelInfo.GeneralWorkersCount;

                System.Diagnostics.Debug.WriteLine($"ConstructionInfo загружена: {TotalWorkers} работников");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка загрузки ConstructionInfo: {ex.Message}");
            }
        }

        private void Close(object parameter)
        {
            // Команда закрытия будет обработана View
        }
    }
}