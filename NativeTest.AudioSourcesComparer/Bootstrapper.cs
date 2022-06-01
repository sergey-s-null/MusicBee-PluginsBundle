using Module.AudioSourcesComparer;
using Module.AudioSourcesComparer.GUI.AbstractViewModels;
using Module.AudioSourcesComparer.GUI.DesignTimeViewModels;
using Ninject;

namespace NativeTest.AudioSourcesComparer
{
    public static class Bootstrapper
    {
        public static IKernel GetKernel()
        {
            var kernel = new StandardKernel();

            kernel.Load<AudioSourcesComparerModule>();

            kernel
                .Rebind<IVkToLocalComparerWindowVM>()
                .To<VkToLocalComparerWindowDTVM>();

            return kernel;
        }
    }
}