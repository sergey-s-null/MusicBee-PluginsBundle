using MusicBeePlugin;

namespace VkMusicDownloader.Helpers
{
    public static class MBApiInterfaceEx
    {
        private const Plugin.MetaDataType VkIdField = Plugin.MetaDataType.Custom3;
        private const Plugin.MetaDataType IndexField = Plugin.MetaDataType.Custom4;
        private const Plugin.MetaDataType Index1Field = Plugin.MetaDataType.Custom1;
        private const Plugin.MetaDataType Index2Field = Plugin.MetaDataType.Custom2;

        public static bool TryGetVkId(this Plugin.MusicBeeApiInterface api, 
            string filePath, out long id)
        {
            var idStr = api.Library_GetFileTag(filePath, VkIdField);
            return long.TryParse(idStr, out id);
        }

        public static bool SetVkId(this Plugin.MusicBeeApiInterface api, 
            string filePath, long id, bool commit = true)
        {
            bool res = api.Library_SetFileTag(filePath, VkIdField, id.ToString());
            if (!res)
                return false;

            if (commit)
                return api.Library_CommitTagsToFile(filePath);
            
            return true;
        }

        public static bool TryGetIndex(this Plugin.MusicBeeApiInterface api, 
            string filePath, out int index)
        {
            string indexStr = api.Library_GetFileTag(filePath, IndexField);
            if (int.TryParse(indexStr, out index))
                return true;
            
            index = -1;
            return false;
        }

        public static bool SetIndex(this Plugin.MusicBeeApiInterface api, 
            string filePath, int index, bool commit = true)
        {
            bool res = api.Library_SetFileTag(filePath, IndexField, index.ToString());
            if (!res)
                return false;

            if (commit)
                return api.Library_CommitTagsToFile(filePath);
            
            return true;
        }

        public static bool TryGetIndex1(this Plugin.MusicBeeApiInterface api, 
            string filePath, out int index1)
        {
            string index1Str = api.Library_GetFileTag(filePath, Index1Field);
            return int.TryParse(index1Str, out index1);
        }

        public static bool SetIndex1(this Plugin.MusicBeeApiInterface api, 
            string filePath, int index1, bool commit = true)
        {
            string i1Str = index1.ToString().PadLeft(2, '0');
            bool res = api.Library_SetFileTag(filePath, Index1Field, i1Str);
            if (!res)
                return false;

            if (commit)
                return api.Library_CommitTagsToFile(filePath);
            
            return true;
        }

        public static bool TryGetIndex2(this Plugin.MusicBeeApiInterface api, 
            string filePath, out int index2)
        {
            string index2Str = api.Library_GetFileTag(filePath, Index2Field);
            return int.TryParse(index2Str, out index2);
        }

        public static bool SetIndex2(this Plugin.MusicBeeApiInterface api, 
            string filePath, int index2, bool commit = true)
        {
            string i2Str = index2.ToString().PadLeft(2, '0');
            bool res = api.Library_SetFileTag(filePath, Index2Field, i2Str);
            if (!res)
                return false;

            if (commit)
                return api.Library_CommitTagsToFile(filePath);
            
            return true;
        }
    }
}