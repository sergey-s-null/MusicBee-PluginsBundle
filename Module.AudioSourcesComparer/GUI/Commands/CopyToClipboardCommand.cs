using System;
using System.Windows;
using System.Windows.Input;

namespace Module.AudioSourcesComparer.GUI.Commands
{
    public sealed class CopyToClipboardCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;

            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter is not null)
            {
                Clipboard.SetText(parameter.ToString());
            }
        }
    }
}