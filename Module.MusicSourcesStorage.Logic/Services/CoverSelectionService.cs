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

    public async Task<IActivableMultiStepTask<CoverSelectionArgs, Void>> CreateImageFileAsCoverSelectionTaskAsync(
        ImageFile imageFile,
        CancellationToken token)
    {
        var imageDownloadingTask = await _filesDownloadingService.CreateFileDownloadTaskAsync(imageFile, token);
        var loadingAndResizingTask = ActivableTaskFactory.Create<string, Image>(LoadAndResizeImage);
        var selectAsCoverInStorageTask = ActivableTaskFactory.Create<Image, byte[]>(
            (image, internalToken) => SelectImageAsCoverInStorage(imageFile, image, internalToken)
        );
        var dispatchEventTask = ActivableTaskFactory.Create<byte[]>(
            binaryImage => DispatchCoverChangedEvent(imageFile, binaryImage)
        );

        return imageDownloadingTask
            .ChangeArgs((CoverSelectionArgs args) => new FileDownloadArgs(args.SkipImageDownloadingIfDownloaded))
            .Chain(loadingAndResizingTask)
            .Chain(selectAsCoverInStorageTask)
            .Chain(dispatchEventTask);
    }

    private Image LoadAndResizeImage(string imageFilePath, CancellationToken token)
    {
        return Task
            .Run(
                () =>
                {
                    var image = Image.FromFile(imageFilePath);
                    return _imageService.ResizeForStorage(image);
                },
                token
            )
            .Result;
    }

    private byte[] SelectImageAsCoverInStorage(SourceFile sourceFile, Image image, CancellationToken token)
    {
        var binaryImage = ImageHelper.ToBytes(image);
        _musicSourcesStorageService.SelectAsCoverAsync(sourceFile.Id, binaryImage, token).Wait(token);
        return binaryImage;
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
}