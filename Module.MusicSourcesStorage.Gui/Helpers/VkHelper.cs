using System.Text.RegularExpressions;

namespace Module.MusicSourcesStorage.Gui.Helpers;

public static class VkHelper
{
    private static readonly Regex PostGlobalIdRegex = new(@"-?(\d+)_(\d+)$");

    public static bool TryParsePostGlobalId(string postGlobalId, out long postOwnerId, out long postId)
    {
        var match = PostGlobalIdRegex.Match(postGlobalId);
        if (!match.Success)
        {
            postOwnerId = 0;
            postId = 0;
            return false;
        }

        if (!long.TryParse(match.Groups[1].Value, out postOwnerId)
            || !long.TryParse(match.Groups[2].Value, out postId))
        {
            postId = 0;
            return false;
        }

        return true;
    }
}