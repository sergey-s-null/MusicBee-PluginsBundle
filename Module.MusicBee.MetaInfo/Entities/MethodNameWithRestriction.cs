using Module.MusicBee.MetaInfo.Enums;

namespace Module.MusicBee.MetaInfo.Entities;

public sealed record MethodNameWithRestriction(
    string MethodName,
    MethodRestriction Restriction = MethodRestriction.None
);