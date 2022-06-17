using Ninject;
using Root.MusicBeeApi;
using Root.MusicBeeApi.Abstract;

namespace MusicBeePlugin
{
    public static class Bootstrapper
    {
        public static IKernel GetKernel(MusicBeeApiMemoryContainer mbApiMemoryContainer)
        {
            var kernel = new StandardKernel();

            kernel
                .Bind<MusicBeeApiMemoryContainer>()
                .ToConstant(mbApiMemoryContainer);
            kernel
                .Bind<IMusicBeeApi>()
                .To<MusicBeeApiMemoryContainerWrapper>()
                .InSingletonScope();

            return kernel;
        }
    }
}