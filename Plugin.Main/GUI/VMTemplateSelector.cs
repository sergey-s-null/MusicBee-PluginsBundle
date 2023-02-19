using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Plugin.Main.GUI
{
    public sealed class VMTemplateSelector : DataTemplateSelector
    {
        private static readonly Regex InterfaceNameRegex = new (@"^I.+VM$");
        
        public ResourceDictionary? ResourceDictionary { get; set; }
        
        public override DataTemplate? SelectTemplate(object? item, DependencyObject container)
        {
            if (item is null || ResourceDictionary is null)
            {
                return base.SelectTemplate(item, container);
            }
            
            var interfaceType = item
                .GetType()
                .GetInterfaces()
                .FirstOrDefault(i => InterfaceNameRegex.IsMatch(i.Name));

            if (interfaceType is null)
            {
                return base.SelectTemplate(item, container);
            }

            var resource = ResourceDictionary[new DataTemplateKey(interfaceType)];
            
            return resource is DataTemplate dataTemplate
                ? dataTemplate
                : base.SelectTemplate(item, container);
        }
    }
}