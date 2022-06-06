using VkNet.Abstractions;

namespace Root.Helpers
{
    public static class VkApiHelper
    {
        public static bool IsAuthorizedWithCheck(this IVkApi vkApi)
        {
            return vkApi.IsAuthorized
                   && vkApi.UserId is not null;
        }
    }
}