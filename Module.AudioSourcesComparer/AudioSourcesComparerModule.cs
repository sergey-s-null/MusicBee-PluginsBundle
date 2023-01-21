using Autofac;
using Module.AudioSourcesComparer.GUI.AbstractViewModels;
using Module.AudioSourcesComparer.GUI.ViewModels;
using Module.AudioSourcesComparer.GUI.Views;
using Module.AudioSourcesComparer.Services;
using Module.AudioSourcesComparer.Services.Abstract;

namespace Module.AudioSourcesComparer
{
    public sealed class AudioSourcesComparerModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<VkToLocalComparerService>()
                .As<IVkToLocalComparerService>()
                .SingleInstance();

            builder
                .RegisterType<VkToLocalComparerWindowVM>()
                .As<IVkToLocalComparerWindowVM>();
            builder
                .RegisterType<VkAudioVM>()
                .As<IVkAudioVM>();

            builder
                .RegisterType<VkToLocalComparerWindow>()
                .AsSelf();
        }
    }
}