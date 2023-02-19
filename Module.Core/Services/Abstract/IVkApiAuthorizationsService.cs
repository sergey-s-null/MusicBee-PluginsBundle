namespace Module.Core.Services.Abstract
{
    public interface IVkApiAuthorizationsService
    {
        /// <summary>
        /// Perform IVkApi authorization.
        /// </summary>
        /// <returns>Auth result.</returns>
        bool AuthorizeVkApiIfNeeded();
    }
}