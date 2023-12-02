using Module.MusicSourcesStorage.Logic.Entities;

namespace Module.MusicSourcesStorage.Logic.Extensions;

public static class StreamExtensions
{
    public static async Task CopyToAsync(
        this Stream source,
        Stream destination,
        int bufferSize,
        ProgressCallbackConfiguration callbackConfiguration,
        CancellationToken token = default)
    {
        callbackConfiguration.RelativeProgressCallback?.Invoke(0);
        callbackConfiguration.AbsoluteProgressCallback?.Invoke(0);

        var buffer = new byte[bufferSize];
        var totalWrote = 0L;
        while (true)
        {
            if (token.IsCancellationRequested)
            {
                token.ThrowIfCancellationRequested();
            }

            var hasRead = await source.ReadAsync(buffer, 0, bufferSize, token);
            if (hasRead == 0)
            {
                break;
            }

            await destination.WriteAsync(buffer, 0, hasRead, token);
            totalWrote += hasRead;

            callbackConfiguration.RelativeProgressCallback?.Invoke((double)totalWrote / source.Length);
            callbackConfiguration.AbsoluteProgressCallback?.Invoke(totalWrote);
        }

        callbackConfiguration.RelativeProgressCallback?.Invoke(1);
        callbackConfiguration.AbsoluteProgressCallback?.Invoke(totalWrote);
    }
}