﻿using Module.MusicSourcesStorage.Logic.Enums;

namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface IFileClassifier
{
    FileType Classify(string filePath);
}