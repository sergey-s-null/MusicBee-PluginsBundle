using System;
using System.Windows.Controls;
using PropertyChanged;
using Root.MVVM;

namespace Test_Desktop.GUI.TestDialog
{
    [AddINotifyPropertyChangedInterface]
    public class TestDialogVM
    {
        private RelayCommand? _someCommand;
        public RelayCommand SomeCommand => _someCommand
            ??= new RelayCommand(_ => SubTest());
        
        public Button Button { get; set; }
        
        private void SubTest()
        {
            Console.WriteLine("asd");
        }

        public void OpenMenu()
        {
            Button.ContextMenu.IsOpen = true;
        }
    }
}