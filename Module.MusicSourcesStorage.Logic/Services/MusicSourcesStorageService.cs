using AutoMapper;
using Module.MusicSourcesStorage.Database.Models;
using Module.MusicSourcesStorage.Database.Services.Abstract;
using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services;

public sealed class MusicSourcesStorageService : IMusicSourcesStorageService
{
    private readonly IMapper _mapper;
    private readonly IMusicSourcesStorage _musicSourcesStorage;

    public MusicSourcesStorageService(
        IMapper mapper,
        IMusicSourcesStorage musicSourcesStorage)
    {
        _mapper = mapper;
        _musicSourcesStorage = musicSourcesStorage;
    }

    public Task AddMusicSourceAsync(MusicSource musicSource, CancellationToken token = default)
    {
        var model = _mapper.Map<MusicSourceModel>(musicSource);

        return _musicSourcesStorage.AddAsync(model, token);
    }
}