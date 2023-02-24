using Module.AudioSourcesComparer.DataClasses;
using Module.AudioSourcesComparer.Exceptions;

namespace Module.AudioSourcesComparer.Services.Abstract;

public interface IVkToLocalComparerService
{
    /// <exception cref="VkApiUnauthorizedException">Vk api is unauthorized.</exception>
    /// <exception cref="VkApiInvalidValueException">Error on get invalid value from vk api.</exception>
    /// <exception cref="MBApiException">Error related with music bee api.</exception>
    /// <exception cref="MBLibraryInvalidStateException">Error related with music bee library state.</exception>
    Task<AudiosDifference> FindDifferencesAsync();
}