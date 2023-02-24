namespace Module.InboxAdder.Services;

public interface IInboxAddService
{
    void AddToLibrary(string filePath);
    void RetrieveToInbox(string filePath);
}