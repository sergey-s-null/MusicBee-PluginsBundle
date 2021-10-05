using System.IO;
using System.Reflection;
using System.Resources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Ninject;
using Ninject.Syntax;

namespace MusicBeePlugin.GUI.InboxRelocateContextMenu
{
    public static class InboxRelocateContextMenu
    {
        public static ContextMenu LoadInboxRelocateContextMenu(this IResolutionRoot kernel)
        {
            var resourceDictionary = LoadDictionary();

            var contextMenu = (ContextMenu) resourceDictionary[nameof(InboxRelocateContextMenu)];
            
            contextMenu.DataContext = kernel.Get<InboxRelocateContextMenuVM>();

            return contextMenu;
        }

        private static ResourceDictionary LoadDictionary()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = $"{nameof(MusicBeePlugin)}.{nameof(GUI)}.{nameof(GUI.InboxRelocateContextMenu)}.{nameof(InboxRelocateContextMenu)}.xaml";
            
            using var stream = assembly.GetManifestResourceStream(resourceName);
            
            if (stream is null)
            {
                throw new MissingManifestResourceException($"Resource with name \"{resourceName}\" not found.");
            }

            using var streamReader = new StreamReader(stream);
            
            return (ResourceDictionary) XamlReader.Parse(streamReader.ReadToEnd());
        }
    }
}