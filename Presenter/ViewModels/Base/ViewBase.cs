using System;

namespace Presenter.ViewModels.Base
{
    /// <summary>
    /// Базовый абстрактный класс для всех View
    /// </summary>
    public abstract class ViewBase
    {
        /// <summary>
        /// Уникальный идентификатор View
        /// </summary>
        public Guid Id { get; } = Guid.NewGuid();

        /// <summary>
        /// Ссылка на ViewModel этого View
        /// </summary>
        public ViewModelBase ViewModel { get; protected set; }

        /// <summary>
        /// Показать View
        /// </summary>
        public abstract void Show();

        /// <summary>
        /// Закрыть View
        /// </summary>
        public abstract void Close();

        /// <summary>
        /// Установить ViewModel для этого View
        /// </summary>
        /// <param name="viewModel">ViewModel</param>
        public virtual void SetViewModel(ViewModelBase viewModel)
        {
            ViewModel = viewModel;
        }
    }
}