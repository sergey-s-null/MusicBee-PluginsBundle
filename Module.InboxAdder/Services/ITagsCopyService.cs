namespace Module.InboxAdder.Services
{
    public interface ITagsCopyService
    {
        bool CopyTags(string sourceFilePath, string targetFilePath);
    }
}