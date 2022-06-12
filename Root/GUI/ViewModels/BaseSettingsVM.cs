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
                Loaded = true;
            }
            catch (SettingsLoadException)
            {
                // todo display error
                Loaded = false;
            }

            SetSettingsFromInnerServiceToViewModel();
        }

        public void Save()
        {
            SetSettingsFromViewModelToInnerService();

            _settings.Save();
        }

        protected abstract void SetSettingsFromInnerServiceToViewModel();

        protected abstract void SetSettingsFromViewModelToInnerService();
    }
}