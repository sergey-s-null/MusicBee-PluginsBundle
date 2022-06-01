namespace Root.Services.Abstract
{
    public interface IVkApiAuthorizationsService
    {
        /// <summary>
        /// Производит авторизация IVkApi.
        /// </summary>
        /// <returns>Результат авторизации</returns>
        bool AuthorizeVkApiIfNeeded();
    }
}