using System;
using System.Windows.Input;
using PropertyChanged;
using Root.Exceptions;
using Root.GUI.AbstractViewModels;
using Root.MVVM;
using Root.Settings;

namespace Root.GUI.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public abstract class BaseSettingsVM : IBaseSettingsVM
    {
        public ICommand ReloadCmd => _reloadCmd ??= new RelayCommand(_ => Load());
        private ICommand? _reloadCmd;

        public bool Loaded { get; private set; }
        public string LoadingErrorMessage { get; private set; } = string.Empty;

        private readonly ISettings _settings;

        protected BaseSettingsVM(ISettings settings)
        {
            _settings = settings;
        }

        public void Load()
        {
            try
            {
                _settings.Load();
                LoadingErrorMessage = string.Empty;
                Loaded = true;
            }
            catch (SettingsLoadException e)
            {
                LoadingErrorMessage = e.ToString();
                Loaded = false;
            }

            SetSettingsFromInnerServiceToViewModel();
        }

        public bool Save()
        {
            try
            {
                SetSettingsFromViewModelToInnerService();
            }
            catch (ArgumentException)
            {
                return false;
            }

            _settings.Save();
            return true;
        }

        protected abstract void SetSettingsFromInnerServiceToViewModel();

        /// <exception cref="ArgumentException">Error on set values to inner settings service.</exception>
        protected abstract void SetSettingsFromViewModelToInnerService();
    }
}