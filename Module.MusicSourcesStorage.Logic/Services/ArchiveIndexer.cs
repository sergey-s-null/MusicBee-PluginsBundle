﻿using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Exceptions;
using Module.MusicSourcesStorage.Logic.Services.Abstract;
using SharpCompress.Archives.Zip;

namespace Module.MusicSourcesStorage.Logic.Services;

public sealed class ArchiveIndexer : IArchiveIndexer
{
    public IReadOnlyList<MusicSourceFile> Index(string filePath)
    {
        using var archive = OpenArchive(filePath);

        return archive.Entries
            .Where(x => !x.IsDirectory)
            .Select(x => new MusicSourceFile(x.Key, x.Size))
            .ToList();
    }

    private static ZipArchive OpenArchive(string filePath)
    {
        try
        {
            return ZipArchive.Open(filePath);
        }
        catch (Exception e)
        {
            throw new ArchiveIndexingException($"Error on open archive \"{filePath}\".", e);
        }
    }
}