using Module.ArtworksSearcher.Exceptions;

namespace Module.ArtworksSearcher.Services.Abstract;

public interface IGoogleImageSearchService
{
    /// <exception cref="GoogleSearchImageException">Error during receive data from google.</exception>
    Task<IReadOnlyCollection<string>> SearchAsync(
        string query,
        int offset,
        CancellationToken cancellationToken);
}