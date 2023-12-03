namespace Module.Vk.Gui.Services.Abstract;

public interface IVkApiAuthorizationsService
{
    /// <summary>
    /// Perform IVkApi authorization.
    /// </summary>
    /// <returns>true - authorized, false - otherwise.</returns>
    bool AuthorizeVkApiIfNeeded();
}