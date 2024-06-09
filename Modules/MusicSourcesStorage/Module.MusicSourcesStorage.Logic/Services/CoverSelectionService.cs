using System.Drawing;
using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Entities.Args;
using Module.MusicSourcesStorage.Logic.Entities.EventArgs;
using Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;
using Module.MusicSourcesStorage.Logic.Extensions;
using Module.MusicSourcesStorage.Logic.Factories;
using Module.MusicSourcesStorage.Logic.Helpers;
using Module.MusicSourcesStorage.Logic.Services.Abstract;
using Void = Module.MusicSourcesStorage.Logic.Entities.Void;

namespace Module.MusicSourcesStorage.Logic.Services;

public sealed class CoverSelectionService : ICoverSelectionService
{
    public event EventHandler<CoverChangedEventArgs>? CoverChanged;
    public event EventHandler<CoverRemovedEventArgs>? CoverRemoved;

    private readonly IFilesDownloadingService _filesDownloadingService;
    private readonly IImageService _imageService;
    private readonly IMusicSourcesStorageService _musicSourcesStorageService;

    public CoverSelectionService(
        IFilesDownloadingService filesDownloadingService,
        IImageService imageService,
        IMusicSourcesStorageService musicSourcesStorageService)
    {
        _filesDownloadingService = filesDownloadingService;
        _imageService = imageService;
        _musicSourcesStorageService = musicSourcesStorageService;
    }

    public async Task<Image?> GetCoverAsync(int sourceId, string directoryRelativePath, CancellationToken token)
    {
        var binaryImage = await _musicSourcesStorageService.FindCoverAsync(sourceId, directoryRelativePath, token);
        return binaryImage is not null
            ? ImageHelper.FromBytes(binaryImage)
            : null;
    }

    public async Task<IActivableMultiStepTask<CoverSelectionArgs, Void>> CreateCoverSelectionTaskAsync(
        int imageFileId,
        CancellationToken token)
    {
        var file = await _musicSourcesStorageService.GetSourceFileAsync(imageFileId, token);
        if (file is not ImageFile imageFile)
        {
            throw new InvalidOperationException(
                $"File with id {imageFileId} is not image file. " +
                $"Actual type: {file.GetType()}."
            );
        }

        return await CreateCoverSelectionTaskAsync(imageFile, token);
    }

    public async Task<IActivableMultiStepTask<CoverSelectionArgs, Void>> CreateCoverSelectionTaskAsync(
        ImageFile imageFile,
        CancellationToken token)
    {
        var imageDownloadingTask = await _filesDownloadingService.CreateFileDownloadTaskAsync(imageFile, token);
        var loadingAndResizingTask = ActivableTaskFactory.Create<string, Image>(LoadAndResizeImage);
        var selectAsCoverInStorageTask = ActivableTaskFactory.Create<Image, (byte[], IReadOnlyList<ImageFile>)>(
            (image, internalToken) => SelectImageAsCoverInStorage(imageFile, image, internalToken)
        );
        var dispatchEventsTask = ActivableTaskFactory
            .CreateWithoutResult<(byte[] BinaryImage, IReadOnlyList<ImageFile> UnselectedImages)>(
                args =>
                {
                    DispatchCoverChangedEvent(imageFile, args.BinaryImage);
                    DispatchCoverRemovedEvents(args.UnselectedImages.Select(x => x.Id));
                }
            );
        return imageDownloadingTask
            .ChangeArgs((CoverSelectionArgs args) => new FileDownloadArgs(args.SkipImageDownloadingIfDownloaded))
            .Chain(loadingAndResizingTask)
            .Chain(selectAsCoverInStorageTask)
            .Chain(dispatchEventsTask);
    }

    public async Task RemoveCoverAsync(int fileId, CancellationToken token)
    {
        await _musicSourcesStorageService.RemoveCoverAsync(fileId, token);
        DispatchCoverRemovedEvent(fileId);
    }

    private Image LoadAndResizeImage(string imageFilePath, CancellationToken token)
    {
        return Task
            .Run(
                () =>
                {
                    var image = ImageHelper.FromFileSafely(imageFilePath);
                    return _imageService.ResizeForStorage(image);
                },
                token
            )
            .Result;
    }

    private (byte[], IReadOnlyList<ImageFile>) SelectImageAsCoverInStorage(SourceFile sourceFile, Image image,
        CancellationToken token)
    {
        var binaryImage = ImageHelper.ToBytes(image);
        var unselectedImages = _musicSourcesStorageService.SelectAsCoverAsync(sourceFile.Id, binaryImage, token).Result;
        return (binaryImage, unselectedImages);
    }

    private void DispatchCoverChangedEvent(SourceFile sourceFile, byte[] binaryImage)
    {
        CoverChanged?.Invoke(this, new CoverChangedEventArgs(
            sourceFile.SourceId,
            sourceFile.Id,
            sourceFile.Path,
            binaryImage
        ));
    }

    private void DispatchCoverRemovedEvents(IEnumerable<int> fileIds)
    {
        foreach (var fileId in fileIds)
        {
            DispatchCoverRemovedEvent(fileId);
        }
    }

    private void DispatchCoverRemovedEvent(int fileId)
    {
        CoverRemoved?.Invoke(this, new CoverRemovedEventArgs(fileId));
    }
}