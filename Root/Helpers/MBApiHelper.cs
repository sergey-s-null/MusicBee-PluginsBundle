using System.Collections.Generic;

namespace Root.Helpers
{
    public static class MBApiHelper
    {
        private const int AudiosPerBlock = 20;
        
        private const MetaDataType VkIdField = MetaDataType.Custom3;
        private const MetaDataType IndexField = MetaDataType.Custom4;
        private const MetaDataType Index1Field = MetaDataType.Custom1;
        private const MetaDataType Index2Field = MetaDataType.Custom2;
        
        public static bool Playlist_QueryPlaylistsEx(
            this MusicBeeApiInterface mbApi, 
            out IReadOnlyCollection<string> playlists)
        {
            if (!mbApi.Playlist_QueryPlaylists())
            {
                playlists = null;
                return false;
            }

            var result = new List<string>();
            while (true)
            {
                var next = mbApi.Playlist_QueryGetNextPlaylist();
                if (string.IsNullOrEmpty(next))
                {
                    break;
                }
                result.Add(next);
            }

            playlists = result;
            return true;
        }
        
        public static bool TryGetVkId(this MusicBeeApiInterface api, 
            string filePath, out long id)
        {
            var idStr = api.Library_GetFileTag(filePath, VkIdField);
            return long.TryParse(idStr, out id);
        }

        public static bool SetVkId(this MusicBeeApiInterface api, 
            string filePath, long id, bool commit = true)
        {
            var res = api.Library_SetFileTag(filePath, VkIdField, id.ToString());
            if (!res)
                return false;

            if (commit)
                return api.Library_CommitTagsToFile(filePath);
            
            return true;
        }

        public static bool TryGetIndex(this MusicBeeApiInterface api, 
            string filePath, out int index)
        {
            var indexStr = api.Library_GetFileTag(filePath, IndexField);
            if (int.TryParse(indexStr, out index))
                return true;
            
            index = -1;
            return false;
        }

        public static bool SetIndex(this MusicBeeApiInterface api, 
            string filePath, int index, bool commit = true)
        {
            var res = api.Library_SetFileTag(filePath, IndexField, index.ToString());
            if (!res)
                return false;

            if (commit)
                return api.Library_CommitTagsToFile(filePath);
            
            return true;
        }

        public static bool ClearIndex(this MusicBeeApiInterface mbApi, string filePath, bool commit = true)
        {
            if (!mbApi.Library_SetFileTag(filePath, IndexField, ""))
            {
                return false;
            }

            if (commit)
            {
                return mbApi.Library_CommitTagsToFile(filePath);
            }

            return true;
        }
        
        public static bool TryGetIndex1(this MusicBeeApiInterface api, 
            string filePath, out int index1)
        {
            var index1Str = api.Library_GetFileTag(filePath, Index1Field);
            return int.TryParse(index1Str, out index1);
        }

        public static bool SetIndex1(this MusicBeeApiInterface api, 
            string filePath, int index1, bool commit = true)
        {
            var i1Str = index1.ToString().PadLeft(2, '0');
            var res = api.Library_SetFileTag(filePath, Index1Field, i1Str);
            if (!res)
                return false;

            if (commit)
                return api.Library_CommitTagsToFile(filePath);
            
            return true;
        }

        public static bool ClearIndex1(this MusicBeeApiInterface mbApi, string filePath, bool commit = true)
        {
            if (!mbApi.Library_SetFileTag(filePath, Index1Field, ""))
            {
                return false;
            }

            if (commit)
            {
                return mbApi.Library_CommitTagsToFile(filePath);
            }

            return true;
        }
        
        public static bool TryGetIndex2(this MusicBeeApiInterface api, 
            string filePath, out int index2)
        {
            var index2Str = api.Library_GetFileTag(filePath, Index2Field);
            return int.TryParse(index2Str, out index2);
        }

        public static bool SetIndex2(this MusicBeeApiInterface api, 
            string filePath, int index2, bool commit = true)
        {
            var i2Str = index2.ToString().PadLeft(2, '0');
            var res = api.Library_SetFileTag(filePath, Index2Field, i2Str);
            if (!res)
                return false;

            if (commit)
                return api.Library_CommitTagsToFile(filePath);
            
            return true;
        }
        
        public static bool ClearIndex2(this MusicBeeApiInterface mbApi, string filePath, bool commit = true)
        {
            if (!mbApi.Library_SetFileTag(filePath, Index2Field, ""))
            {
                return false;
            }

            if (commit)
            {
                return mbApi.Library_CommitTagsToFile(filePath);
            }

            return true;
        }
        
        public static void CalcIndices(int index, out int index1, out int index2)
        {
            index1 = index / AudiosPerBlock + 1;
            index2 = index % AudiosPerBlock + 1;
        }
    }
}