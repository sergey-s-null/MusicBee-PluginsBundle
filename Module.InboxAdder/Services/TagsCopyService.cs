using System.Collections.Generic;
using System.Linq;
using Root;
using Root.Helpers;

namespace Module.InboxAdder.Services
{
    public class TagsCopyService : ITagsCopyService
    {
        // TODO когда-нибудь сделать копирование FilePropertyType {PlayCount, SkipCount, DateAdded}. (пока что апи не позволяет)
        private static readonly IReadOnlyCollection<MetaDataType> TagsToCopy = new[]
        {
            MBApiHelper.IndexField,
            MBApiHelper.Index1Field,
            MBApiHelper.Index2Field,
            MBApiHelper.PoolsField
        };

        private readonly MusicBeeApiInterface _mbApi;
        
        public TagsCopyService(MusicBeeApiInterface mbApi)
        {
            _mbApi = mbApi;
        }
        
        public bool CopyTags(string sourceFilePath, string targetFilePath)
        {
            var copyResults = TagsToCopy
                .Select(x => CopyTag(sourceFilePath, targetFilePath, x))
                .ToReadOnlyCollection();
            return copyResults.All(x => x) 
                   && _mbApi.Library_CommitTagsToFile(targetFilePath);
        }

        private bool CopyTag(string sourceFilePath, string targetFilePath, MetaDataType type)
        {
            var sourceValue = _mbApi.Library_GetFileTag(sourceFilePath, type);
            return _mbApi.Library_SetFileTag(targetFilePath, type, sourceValue);
        }
    }
}