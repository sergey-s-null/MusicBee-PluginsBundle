using System.Windows;
using PropertyChanged;
using Root.MVVM;

namespace Test_Desktop.GUI.SpecContextMenu
{
    [AddINotifyPropertyChangedInterface]
    public sealed class SpecContextMenuVM
    {
        private RelayCommand? _someCommand;
        public RelayCommand SomeCommand => _someCommand ??= new RelayCommand(_ => Some());

        private void Some()
        {
            MessageBox.Show("halo");
        }
    }
}