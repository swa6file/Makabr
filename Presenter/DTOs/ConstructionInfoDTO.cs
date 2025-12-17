using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presenter.DTOs
{
    public class ConstructionInfoDTO : INotifyPropertyChanged
    {
        private int _totalSalaryExpenses;
        private int _electriciansCount;
        private int _paintersCount;
        private int _craneOperatorsCount;
        private int _generalWorkersCount;

        public int TotalSalaryExpenses
        {
            get => _totalSalaryExpenses;
            set { _totalSalaryExpenses = value; OnPropertyChanged(); }
        }

        public int ElectriciansCount
        {
            get => _electriciansCount;
            set { _electriciansCount = value; OnPropertyChanged(); }
        }

        public int PaintersCount
        {
            get => _paintersCount;
            set { _paintersCount = value; OnPropertyChanged(); }
        }

        public int CraneOperatorsCount
        {
            get => _craneOperatorsCount;
            set { _craneOperatorsCount = value; OnPropertyChanged(); }
        }

        public int GeneralWorkersCount
        {
            get => _generalWorkersCount;
            set { _generalWorkersCount = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static ConstructionInfoDTO FromModel(BusinessLogical.Models.ConstructionInfo info)
        {
            return new ConstructionInfoDTO
            {
                TotalSalaryExpenses = info.TotalSalaryExpenses,
                ElectriciansCount = info.ElectriciansCount,
                PaintersCount = info.PaintersCount,
                CraneOperatorsCount = info.CraneOperatorsCount,
                GeneralWorkersCount = info.GeneralWorkersCount
            };
        }
    }
}