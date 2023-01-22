using System;

namespace Module.MusicBee.MetaInfo.Entities;

public sealed record ParameterDefinition(
    Type Type,
    string Name,
    bool IsNullable
);