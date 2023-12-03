using Autofac;
using Module.MusicBee.Extension;

namespace Module.Settings;

public class SettingsModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        // todo ???
        builder.RegisterModule<MusicBeeExtensionModule>();
        
        // todo register child DIModule's
    }
}