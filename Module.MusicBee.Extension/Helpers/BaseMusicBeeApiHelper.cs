﻿using Mead.MusicBee.Api.Services.Abstract;
using Mead.MusicBee.Enums;

namespace Module.MusicBee.Extension.Helpers;

public static class BaseMusicBeeApiHelper
{
    private const int AudiosPerBlock = 20;

    private const MetaDataType VkIdField = MetaDataType.Custom3;
    private const MetaDataType IndexField = MetaDataType.Custom4;
    private const MetaDataType Index1Field = MetaDataType.Custom1;
    private const MetaDataType Index2Field = MetaDataType.Custom2;

    public static bool Playlist_QueryPlaylistsEx(
        this IMusicBeeApi mbApi,
        out IReadOnlyCollection<string>? playlists)
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

    public static IEnumerable<long> EnumerateVkIdsInLibrary(this IMusicBeeApi api)
    {
        // todo переписать на новое апи, когда (если) будет сделано
        const string query =
            "<Source Type=\"1\">" + // библиотека (без новых файлов)
            "    <Conditions CombineMethod=\"All\">" +
            "        <Condition Field=\"Custom3\" Comparison=\"IsNotNull\"/>" + // vk id не пусто
            "    </Conditions>" +
            "</Source>";

        if (!api.Library_QueryFilesEx(query, out var filePaths))
        {
            yield break;
        }

        foreach (var filePath in filePaths!)
        {
            if (api.TryGetVkId(filePath, out var vkId))
            {
                yield return vkId;
            }
        }
    }

    public static IEnumerable<long> EnumerateVkIdsInIncoming(this IMusicBeeApi musicBeeApi)
    {
        // todo переписать на новое апи, когда (если) будет сделано
        const string query =
            "<Source Type=\"4\">" + // новые файлы (входящие)
            "    <Conditions CombineMethod=\"All\">" +
            "        <Condition Field=\"Custom3\" Comparison=\"IsNotNull\"/>" + // vk id не пусто
            "    </Conditions>" +
            "</Source>";

        if (!musicBeeApi.Library_QueryFilesEx(query, out var filePaths))
        {
            yield break;
        }

        foreach (var filePath in filePaths!)
        {
            if (musicBeeApi.TryGetVkId(filePath, out var vkId))
            {
                yield return vkId;
            }
        }
    }

    public static long GetVkIdOrDefault(this IMusicBeeApi api,
        string filePath, long defaultId)
    {
        return api.TryGetVkId(filePath, out var id)
            ? id
            : defaultId;
    }

    public static long? GetVkIdOrNull(this IMusicBeeApi api, string filePath)
    {
        return api.TryGetVkId(filePath, out var id)
            ? id
            : null;
    }

    public static bool TryGetVkId(this IMusicBeeApi api,
        string filePath, out long id)
    {
        var idStr = api.Library_GetFileTag(filePath, VkIdField);
        return long.TryParse(idStr, out id);
    }

    public static bool SetVkId(this IMusicBeeApi api,
        string filePath, long id, bool commit = true)
    {
        var res = api.Library_SetFileTag(filePath, VkIdField, id.ToString());
        if (!res)
            return false;

        if (commit)
            return api.Library_CommitTagsToFile(filePath);

        return true;
    }

    public static int GetIndexOrDefault(this IMusicBeeApi api,
        string filePath, int defaultIndex)
    {
        return api.TryGetIndex(filePath, out var index)
            ? index
            : defaultIndex;
    }

    public static bool TryGetIndex(this IMusicBeeApi api,
        string filePath, out int index)
    {
        var indexStr = api.Library_GetFileTag(filePath, IndexField);
        if (int.TryParse(indexStr, out index))
            return true;

        index = -1;
        return false;
    }

    public static bool SetIndex(this IMusicBeeApi api,
        string filePath, int index, bool commit = true)
    {
        var res = api.Library_SetFileTag(filePath, IndexField, index.ToString());
        if (!res)
            return false;

        if (commit)
            return api.Library_CommitTagsToFile(filePath);

        return true;
    }

    public static bool ClearIndex(this IMusicBeeApi mbApi, string filePath, bool commit = true)
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

    public static bool TryGetIndex1(this IMusicBeeApi api,
        string filePath, out int index1)
    {
        var index1Str = api.Library_GetFileTag(filePath, Index1Field);
        return int.TryParse(index1Str, out index1);
    }

    public static bool SetIndex1(this IMusicBeeApi api,
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

    public static bool ClearIndex1(this IMusicBeeApi mbApi, string filePath, bool commit = true)
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

    public static bool TryGetIndex2(this IMusicBeeApi api,
        string filePath, out int index2)
    {
        var index2Str = api.Library_GetFileTag(filePath, Index2Field);
        return int.TryParse(index2Str, out index2);
    }

    public static bool SetIndex2(this IMusicBeeApi api,
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

    public static bool ClearIndex2(this IMusicBeeApi mbApi, string filePath, bool commit = true)
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