using System;

namespace Module.MusicBee.MetaInfo.Entities;

public record ParameterDefinition(
    Type Type,
    string Name,
    bool IsNullable
);