using Module.AudioSourcesComparer.DataClasses;
using Module.AudioSourcesComparer.Exceptions;

namespace Module.AudioSourcesComparer.Services.Abstract
{
    public interface IVkToLocalComparerService
    {
        /// <exception cref="VkApiUnauthorizedException">Vk api is unauthorized.</exception>
        /// <exception cref="MBApiException">Error related with music bee api.</exception>
        /// <exception cref="MBLibraryInvalidStateException">Error related with music bee library state.</exception>
        AudiosDifference FindDifferences();
    }
}