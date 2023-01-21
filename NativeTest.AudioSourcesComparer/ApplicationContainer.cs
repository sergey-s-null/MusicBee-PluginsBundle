using Autofac;
using Module.AudioSourcesComparer;
using Module.AudioSourcesComparer.GUI.AbstractViewModels;
using Module.AudioSourcesComparer.GUI.DesignTimeViewModels;

namespace NativeTest.AudioSourcesComparer
{
    public static class ApplicationContainer
    {
        public static IContainer Create()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<AudioSourcesComparerModule>();

            builder
                .RegisterType<VkToLocalComparerWindowDTVM>()
                .As<IVkToLocalComparerWindowVM>();

            return builder.Build();
        }
    }
}