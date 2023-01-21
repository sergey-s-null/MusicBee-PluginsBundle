using Module.MusicBee.Extension.LibraryQuerying.Entities.Abstract;

namespace Module.MusicBee.Extension.LibraryQuerying.Entities;

public sealed class Field :
    IExtendedStringField,
    INumberField,
    IBoolField,
    IDateField,
    IFlagField,
    IRatingField,
    IUnknownTypeField
{
    /// <summary>
    /// Любое поле
    /// </summary>
    public static readonly IStringField Any = new Field("None");

    /// <summary>
    /// BPM
    /// </summary>
    public static readonly INumberField BeatsPerMinute = new Field("BeatsPerMin");

    /// <summary>
    /// № диска
    /// </summary>
    public static readonly INumberField DiscNo = new Field("DiscNo");

    /// <summary>
    /// № дорожки
    /// </summary>
    public static readonly INumberField TrackNo = new Field("TrackNo");

    /// <summary>
    /// Автор слов
    /// </summary>
    public static readonly IStringField Lyricist = new Field("Lyricist");

    /// <summary>
    /// Слова?
    /// </summary>
    public static readonly IBoolField HasLyrics = new Field("HasLyrics");

    /// <summary>
    /// Альбом
    /// </summary>
    public static readonly IStringField Album = new Field("Album");

    /// <summary>
    /// Альбом [Год]
    /// </summary>
    public static readonly IStringField AlbumAndYear = new Field("AlbumAndYear");

    /// <summary>
    /// Альбом для сортировки
    /// </summary>
    public static readonly IStringField SortAlbum = new Field("SortAlbum");

    /// <summary>
    /// Альбом прослушан?
    /// </summary>
    public static readonly IBoolField AlbumPlayed = new Field("AlbumPlayed");

    /// <summary>
    /// Вид
    /// </summary>
    public static readonly IEnumField FileKind = new Field("FileKind");

    /// <summary>
    /// Время
    /// </summary>
    public static readonly INumberField FileDuration = new Field("FileDuration");

    /// <summary>
    /// Год
    /// </summary>
    public static readonly INumberField Year = new Field("Year");

    /// <summary>
    /// Год (гггг)
    /// </summary>
    public static readonly INumberField YearOnly = new Field("YearOnly");

    /// <summary>
    /// Год перв. исполнения
    /// </summary>
    public static readonly IDateField OriginalYear = new Field("OriginalYear");

    /// <summary>
    /// Группировка
    /// </summary>
    public static readonly IEnumField Grouping = new Field("Grouping");

    /// <summary>
    /// Дата добавления альбома
    /// </summary>
    public static readonly IDateField AlbumDateAdded = new Field("AlbumDateAdded");

    /// <summary>
    /// Дата последней игры альбома
    /// </summary>
    public static readonly IDateField AlbumLastPlayed = new Field("AlbumLastPlayed");

    /// <summary>
    /// Десятилетия
    /// </summary>
    public static readonly IEnumField Decade = new Field("Decade");

    /// <summary>
    /// Дирижер
    /// </summary>
    public static readonly IStringField Conductor = new Field("Conductor");

    /// <summary>
    /// Добавлено
    /// </summary>
    public static readonly IDateField FileDateAdded = new Field("FileDateAdded");

    /// <summary>
    /// Жанр
    /// </summary>
    public static readonly IEnumField GenreSplits = new Field("GenreSplits");

    /// <summary>
    /// Издатель
    /// </summary>
    public static readonly IStringField Publisher = new Field("Publisher");

    /// <summary>
    /// Изменено
    /// </summary>
    public static readonly IDateField FileDateModified = new Field("FileDateModified");

    /// <summary>
    /// Имя файла
    /// </summary>
    public static readonly IStringField FileName = new Field("FileName");

    /// <summary>
    /// Инструменты: Соисполнитель
    /// </summary>
    public static readonly IStringField InstrumentsPerformers = new Field("InstrumentsPerformers");

    /// <summary>
    /// Исполнители: Гость
    /// </summary>
    public static readonly IStringField ArtistsWithGuestRole = new Field("ArtistsWithGuestRole");

    /// <summary>
    /// Исполнители: Исполнитель
    /// </summary>
    public static readonly IStringField ArtistsWithArtistRole = new Field("ArtistsWithArtistRole");

    /// <summary>
    /// Исполнители: Ремиксер
    /// </summary>
    public static readonly IStringField ArtistsWithRemixerRole = new Field("ArtistsWithRemixerRole");

    /// <summary>
    /// Исполнители: Соисполнитель
    /// </summary>
    public static readonly IStringField ArtistsWithPerformerRole = new Field("ArtistsWithPerformerRole");

    /// <summary>
    /// Исполнитель
    /// </summary>
    public static readonly IExtendedStringField ArtistPeople = new Field("ArtistPeople");

    /// <summary>
    /// Исполнитель альбома
    /// </summary>
    public static readonly IStringField AlbumArtist = new Field("AlbumArtist");

    /// <summary>
    /// Исполнитель альбома для сортировки
    /// </summary>
    public static readonly IStringField SortAlbumArtist = new Field("SortAlbumArtist");

    /// <summary>
    /// Исполнитель для сортировки
    /// </summary>
    public static readonly IStringField SortArtist = new Field("SortArtist");

    /// <summary>
    /// Каналы
    /// </summary>
    public static readonly IEnumField FileChannels = new Field("FileChannels");

    /// <summary>
    /// Категория жанра
    /// </summary>
    public static readonly IEnumField GenreCategory = new Field("GenreCategory");

    /// <summary>
    /// Качество
    /// </summary>
    public static readonly IEnumField Quality = new Field("Quality");

    /// <summary>
    /// Качество кодирования
    /// </summary>
    public static readonly INumberField FileEncodingQuality = new Field("FileEncodingQuality");

    /// <summary>
    /// Ключевые слова
    /// </summary>
    public static readonly IEnumField Keywords = new Field("Keywords");

    /// <summary>
    /// Кодировщик
    /// </summary>
    public static readonly IStringField Encoder = new Field("Encoder");

    /// <summary>
    /// Количество частей
    /// </summary>
    public static readonly INumberField MovementCount = new Field("MovementCount");

    /// <summary>
    /// Комментарий
    /// </summary>
    public static readonly IStringField Comment = new Field("Comment");

    /// <summary>
    /// Композитор
    /// </summary>
    public static readonly IStringField ComposerPeople = new Field("ComposerPeople");

    /// <summary>
    /// Композитор для сортировки
    /// </summary>
    public static readonly IStringField SortComposer = new Field("SortComposer");

    /// <summary>
    /// Название
    /// </summary>
    public static readonly IStringField Title = new Field("Title");

    /// <summary>
    /// Название для сортировки
    /// </summary>
    public static readonly IStringField SortTitle = new Field("SortTitle");

    /// <summary>
    /// Название папки
    /// </summary>
    public static readonly IStringField FolderName = new Field("FolderName");

    /// <summary>
    /// Название части
    /// </summary>
    public static readonly IStringField MovementName = new Field("MovementName");

    /// <summary>
    /// Настроение
    /// </summary>
    public static readonly IEnumField Mood = new Field("Mood");

    /// <summary>
    /// Номер части #
    /// </summary>
    public static readonly INumberField MovementNo = new Field("MovementNo");

    /// <summary>
    /// Нормализация альбома
    /// </summary>
    public static readonly INumberField ReplayGainAlbum = new Field("ReplayGainAlbum");

    /// <summary>
    /// Нормализация дорожки
    /// </summary>
    public static readonly INumberField ReplayGainTrack = new Field("ReplayGainTrack");

    /// <summary>
    /// Нравится
    /// </summary>
    public static readonly IEnumField RatingLoveBan = new Field("RatingLoveBan");

    /// <summary>
    /// Описание эпизода
    /// </summary>
    public static readonly IStringField EpisodeDescription = new Field("EpisodeDescription");

    /// <summary>
    /// Отметка
    /// </summary>
    public static readonly IFlagField FileTickedFlag = new Field("FileTickedFlag");

    /// <summary>
    /// Перв. исполнитель
    /// </summary>
    public static readonly IStringField OriginalArtist = new Field("OriginalArtist");

    /// <summary>
    /// Перв. название
    /// </summary>
    public static readonly IStringField OriginalAlbum = new Field("OriginalAlbum");

    /// <summary>
    /// Плейлист
    /// </summary>
    public static readonly IEnumField Playlists = new Field("Playlists");

    /// <summary>
    /// Полный альбом?
    /// </summary>
    public static readonly IBoolField AlbumComplete = new Field("AlbumComplete");

    /// <summary>
    /// Продолжительность альбома
    /// </summary>
    public static readonly INumberField AlbumDuration = new Field("AlbumDuration");

    /// <summary>
    /// Проиграно
    /// </summary>
    public static readonly IDateField FileLastPlayed = new Field("FileLastPlayed");

    /// <summary>
    /// Произведение
    /// </summary>
    public static readonly IStringField Work = new Field("Work");

    /// <summary>
    /// Пропущено
    /// </summary>
    public static readonly INumberField FileSkipCount = new Field("FileSkipCount");

    /// <summary>
    /// Путь
    /// </summary>
    public static readonly IStringField FilePath = new Field("FilePath");

    /// <summary>
    /// Разрядность
    /// </summary>
    public static readonly INumberField BitDepth = new Field("BitDepth");

    /// <summary>
    /// Расширение файла
    /// </summary>
    public static readonly IStringField FileExtension = new Field("FileExtension");

    /// <summary>
    /// Рейтинг
    /// </summary>
    public static readonly IRatingField Rating = new Field("Rating");

    /// <summary>
    /// Рейтинг альбома
    /// </summary>
    public static readonly IRatingField RatingAlbum = new Field("RatingAlbum");

    /// <summary>
    /// Сезон
    /// </summary>
    public static readonly IStringField VideoSeason = new Field("VideoSeason");

    /// <summary>
    /// Серия
    /// </summary>
    public static readonly IStringField VideoEpisode = new Field("VideoEpisode");

    /// <summary>
    /// Ситуация
    /// </summary>
    public static readonly IEnumField Occasion = new Field("Occasion");

    /// <summary>
    /// Скорость потока
    /// </summary>
    public static readonly INumberField FileBitrate = new Field("FileBitrate");

    /// <summary>
    /// Слова
    /// </summary>
    public static readonly IStringField Lyrics = new Field("Lyrics");

    /// <summary>
    /// Сыграно
    /// </summary>
    public static readonly INumberField FilePlayCount = new Field("FilePlayCount");

    /// <summary>
    /// Темп
    /// </summary>
    public static readonly IEnumField Tempo = new Field("Tempo");

    /// <summary>
    /// Тип видео
    /// </summary>
    public static readonly IEnumField VideoKind = new Field("VideoKind");

    /// <summary>
    /// Частота дискретизации
    /// </summary>
    public static readonly INumberField FileSampleRate = new Field("FileSampleRate");

    /// <summary>
    /// Число дисков
    /// </summary>
    public static readonly INumberField DiscCount = new Field("DiscCount");

    /// <summary>
    /// Число дорожек
    /// </summary>
    public static readonly INumberField TrackCount = new Field("TrackCount");

    /// <summary>
    /// Число дорожек альбома
    /// </summary>
    public static readonly INumberField AlbumTrackCount = new Field("AlbumTrackCount");

    /// <summary>
    /// Число дорожек исполнителя
    /// </summary>
    public static readonly INumberField ArtistTrackCount = new Field("ArtistTrackCount");

    /// <summary>
    /// Язык
    /// </summary>
    public static readonly IStringField Language = new Field("Language");

    public static readonly IUnknownTypeField Custom1 = new Field("Custom1");
    public static readonly IUnknownTypeField Custom2 = new Field("Custom2");
    public static readonly IUnknownTypeField Custom3 = new Field("Custom3");
    public static readonly IUnknownTypeField Custom4 = new Field("Custom4");
    public static readonly IUnknownTypeField Custom5 = new Field("Custom5");
    public static readonly IUnknownTypeField Custom6 = new Field("Custom6");
    public static readonly IUnknownTypeField Custom7 = new Field("Custom7");
    public static readonly IUnknownTypeField Custom8 = new Field("Custom8");
    public static readonly IUnknownTypeField Custom9 = new Field("Custom9");
    public static readonly IUnknownTypeField Custom10 = new Field("Custom10");
    public static readonly IUnknownTypeField Custom11 = new Field("Custom11");
    public static readonly IUnknownTypeField Custom12 = new Field("Custom12");
    public static readonly IUnknownTypeField Custom13 = new Field("Custom13");
    public static readonly IUnknownTypeField Custom14 = new Field("Custom14");
    public static readonly IUnknownTypeField Custom15 = new Field("Custom15");
    public static readonly IUnknownTypeField Custom16 = new Field("Custom16");

    public static readonly IUnknownTypeField Virtual1 = new Field("Virtual1");
    public static readonly IUnknownTypeField Virtual2 = new Field("Virtual2");
    public static readonly IUnknownTypeField Virtual3 = new Field("Virtual3");
    public static readonly IUnknownTypeField Virtual4 = new Field("Virtual4");
    public static readonly IUnknownTypeField Virtual5 = new Field("Virtual5");
    public static readonly IUnknownTypeField Virtual6 = new Field("Virtual6");
    public static readonly IUnknownTypeField Virtual7 = new Field("Virtual7");
    public static readonly IUnknownTypeField Virtual8 = new Field("Virtual8");
    public static readonly IUnknownTypeField Virtual9 = new Field("Virtual9");
    public static readonly IUnknownTypeField Virtual10 = new Field("Virtual10");
    public static readonly IUnknownTypeField Virtual11 = new Field("Virtual11");
    public static readonly IUnknownTypeField Virtual12 = new Field("Virtual12");
    public static readonly IUnknownTypeField Virtual13 = new Field("Virtual13");
    public static readonly IUnknownTypeField Virtual14 = new Field("Virtual14");
    public static readonly IUnknownTypeField Virtual15 = new Field("Virtual15");
    public static readonly IUnknownTypeField Virtual16 = new Field("Virtual16");
    public static readonly IUnknownTypeField Virtual17 = new Field("Virtual17");
    public static readonly IUnknownTypeField Virtual18 = new Field("Virtual18");
    public static readonly IUnknownTypeField Virtual19 = new Field("Virtual19");
    public static readonly IUnknownTypeField Virtual20 = new Field("Virtual20");
    public static readonly IUnknownTypeField Virtual21 = new Field("Virtual21");
    public static readonly IUnknownTypeField Virtual22 = new Field("Virtual22");
    public static readonly IUnknownTypeField Virtual23 = new Field("Virtual23");
    public static readonly IUnknownTypeField Virtual24 = new Field("Virtual24");
    public static readonly IUnknownTypeField Virtual25 = new Field("Virtual25");
    public static readonly IUnknownTypeField Virtual26 = new Field("Virtual26");
    public static readonly IUnknownTypeField Virtual27 = new Field("Virtual27");
    public static readonly IUnknownTypeField Virtual28 = new Field("Virtual28");
    public static readonly IUnknownTypeField Virtual29 = new Field("Virtual29");
    public static readonly IUnknownTypeField Virtual30 = new Field("Virtual30");
    public static readonly IUnknownTypeField Virtual31 = new Field("Virtual31");
    public static readonly IUnknownTypeField Virtual32 = new Field("Virtual32");

    public string XName { get; }

    private Field(string name)
    {
        XName = name;
    }
}