using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Ninject;
using Ninject.Syntax;

namespace MusicBeePlugin.GUI.InboxRelocateContextMenu
{
    public static class InboxRelocateContextMenu
    {
        public static ContextMenu LoadInboxRelocateContextMenu(this IResolutionRoot kernel)
        {
            var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            var resourceDictionary = new ResourceDictionary()
            {
                Source = new Uri(
                    $"/{assemblyName};component/GUI/{nameof(InboxRelocateContextMenu)}/{nameof(InboxRelocateContextMenu)}.xaml", 
                    UriKind.Relative)
            };

            var contextMenu = (ContextMenu) resourceDictionary[nameof(InboxRelocateContextMenu)];
            
            contextMenu.DataContext = kernel.Get<InboxRelocateContextMenuVM>();

            return contextMenu;
        }
    }
}