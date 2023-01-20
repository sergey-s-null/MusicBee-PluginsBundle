using System;
using System.Windows.Controls;
using PropertyChanged;
using Root.MVVM;

namespace Test_Desktop.GUI.TestDialog
{
    [AddINotifyPropertyChangedInterface]
    public sealed class TestDialogVM
    {
        private RelayCommand? _someCommand;
        public RelayCommand SomeCommand => _someCommand
            ??= new RelayCommand(_ => SubTest());
        
        private void SubTest()
        {
            Console.WriteLine("asd");
        }

    }
}