using Module.AudioSourcesComparer.DataClasses;

namespace Module.AudioSourcesComparer.Services.Abstract
{
    public interface IVkToLocalComparerService
    {
        bool TryFindDifferences(out AudiosDifference? difference);
        AudiosDifference FindDifferences();
    }
}