using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presenter.DTOs
{
    public class FilterCriteriaDTO : INotifyPropertyChanged
    {
        private string _name;
        private int? _minAge;
        private int? _maxAge;
        private decimal? _minSalary;
        private decimal? _maxSalary;
        private string _specialization;

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        public int? MinAge
        {
            get => _minAge;
            set { _minAge = value; OnPropertyChanged(); }
        }

        public int? MaxAge
        {
            get => _maxAge;
            set { _maxAge = value; OnPropertyChanged(); }
        }

        public decimal? MinSalary
        {
            get => _minSalary;
            set { _minSalary = value; OnPropertyChanged(); }
        }

        public decimal? MaxSalary
        {
            get => _maxSalary;
            set { _maxSalary = value; OnPropertyChanged(); }
        }

        public string Specialization
        {
            get => _specialization;
            set { _specialization = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Конвертация в бизнес-модель
        public BusinessLogical.Models.WorkerFilterCriteria ToModel()
        {
            // Безопасный парсинг Specialization
            Model.Specialization? spec = null;
            if (!string.IsNullOrEmpty(this.Specialization))
            {
                Model.Specialization parsedSpec;
                if (System.Enum.TryParse(this.Specialization, out parsedSpec))
                {
                    spec = parsedSpec;
                }
            }

            return new BusinessLogical.Models.WorkerFilterCriteria
            {
                Name = this.Name,
                MinAge = this.MinAge,
                MaxAge = this.MaxAge,
                MinSalary = this.MinSalary.HasValue ? (int?)this.MinSalary.Value : null,
                MaxSalary = this.MaxSalary.HasValue ? (int?)this.MaxSalary.Value : null,
                Specialization = spec
            };
        }
    }
}
