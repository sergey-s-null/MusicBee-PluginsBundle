using System.Windows;
using Module.MusicSourcesStorage.Logic.Entities;

namespace Module.MusicSourcesStorage.Gui.Helpers;

public static class MessageBoxHelper
{
    public static MessageBoxResult AskForDeletion(SourceFile file)
    {
        return MessageBox.Show(
            "Delete file?\n" +
            $"\tId: {file.Id}\n" +
            $"\tSource path: {file.Path}",
            "?",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question
        );
    }
}