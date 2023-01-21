using Module.MusicBee.Extension.LibraryQuerying.Entities;
using Module.MusicBee.Extension.LibraryQuerying.Entities.Abstract;

namespace Module.MusicBee.Extension.LibraryQuerying.Factories.Abstract;

internal delegate ConditionWithTwoValues<TValue> ConditionWithTwoValuesFactory<TValue>(
    IField field,
    Comparison comparison,
    TValue firstValue,
    TValue secondValue
);