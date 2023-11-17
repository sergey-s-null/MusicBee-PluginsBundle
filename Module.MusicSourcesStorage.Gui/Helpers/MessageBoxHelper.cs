using System.Windows;
using Module.MusicSourcesStorage.Logic.Entities;

namespace Module.MusicSourcesStorage.Gui.Helpers;

public static class MessageBoxHelper
{
    public static MessageBoxResult AskForDeletion(SourceFile file)
    {
        return AskForDeletion(file.Id, file.Path);
    }

    public static MessageBoxResult AskForDeletion(int fileId, string filePath)
    {
        return MessageBox.Show(
            "Delete file?\n" +
            $"\tId: {fileId}\n" +
            $"\tSource path: {filePath}",
            "?",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question
        );
    }
}