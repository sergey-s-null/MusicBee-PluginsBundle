using Mead.MusicBee.Api.Services.Abstract;
using Mead.MusicBee.Enums;
using Module.MusicSourcesStorage.Logic.Entities.Args;
using Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;
using Module.MusicSourcesStorage.Logic.Factories;
using Module.MusicSourcesStorage.Logic.Services.Abstract;
using Void = Module.MusicSourcesStorage.Logic.Entities.Void;

namespace Module.MusicSourcesStorage.Logic.Services;

public sealed class NewFileInitializationService : INewFileInitializationService
{
    private readonly IMusicBeeApi _musicBeeApi;
    private readonly IMusicSourcesStorageSettingsAccessor _settingsAccessor;

    public NewFileInitializationService(
        IMusicBeeApi musicBeeApi,
        IMusicSourcesStorageSettingsAccessor settingsAccessor)
    {
        _musicBeeApi = musicBeeApi;
        _settingsAccessor = settingsAccessor;
    }

    public IActivableTask<NewFileInitializationArgs, Void> CreateTask()
    {
        return ActivableTaskFactory.CreateWithoutResult<NewFileInitializationArgs>(InitializeNewFile);
    }

    public void InitializeNewFile(int fileId, string filePath)
    {
        _musicBeeApi.Library_AddFileToLibrary(filePath, LibraryCategory.Inbox);

        _musicBeeApi.Library_SetFileTag(filePath, _settingsAccessor.FileIdField, fileId.ToString());
        _musicBeeApi.Library_CommitTagsToFile(filePath);
    }

    private void InitializeNewFile(NewFileInitializationArgs args)
    {
        InitializeNewFile(args.FileId, args.FilePath);
    }
}