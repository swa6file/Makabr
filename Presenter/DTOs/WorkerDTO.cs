using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Presenter.DTOs
{
    public class WorkerDTO : INotifyPropertyChanged
    {
        private int _id;
        private string _name;
        private int _age;
        private decimal _salary;
        private string _specialization;

        public int Id
        {
            get => _id;
            set => SetField(ref _id, value);
        }

        public string Name
        {
            get => _name;
            set => SetField(ref _name, value);
        }

        public int Age
        {
            get => _age;
            set
            {
                if (value < 0 || value > 150)
                    return;
                SetField(ref _age, value);
            }
        }

        public decimal Salary
        {
            get => _salary;
            set
            {
                if (value < 0)
                    return;
                SetField(ref _salary, value);
            }
        }

        public string Specialization
        {
            get => _specialization;
            set => SetField(ref _specialization, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        // Методы конвертации остаются
        public Model.Worker ToModel()
        {
            return new Model.Worker
            {
                Id = this.Id,
                Name = this.Name,
                Age = this.Age,
                Salary = (int)this.Salary,
                Specialization = (Model.Specialization)Enum.Parse(typeof(Model.Specialization), this.Specialization)
            };
        }

        public static WorkerDTO FromModel(Model.Worker worker)
        {
            return new WorkerDTO
            {
                Id = worker.Id,
                Name = worker.Name,
                Age = worker.Age,
                Salary = worker.Salary,
                Specialization = worker.Specialization.ToString()
            };
        }
    }
}