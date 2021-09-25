using System;
using System.Windows;
using System.Windows.Controls;

namespace Test_Desktop.GUI.SpecContextMenu
{
    public class ResourceMenuGetter
    {
        public static ContextMenu Get()
        {
            var resourceDictionary = new ResourceDictionary()
            {
                Source = new Uri(
                    "/Test_Desktop;component/GUI/SpecContextMenu/SpecContextMenu.xaml", 
                    UriKind.Relative)
            };

            var contextMenu = (ContextMenu) resourceDictionary["SpecContextMenu"];

            contextMenu.DataContext = new SpecContextMenuVM();

            return contextMenu;
        }
    }
}