using Module.MusicBee.Extension.LibraryQuerying.Entities;
using Module.MusicBee.Extension.LibraryQuerying.Entities.Abstract;

namespace Module.MusicBee.Extension.LibraryQuerying.Factories.Abstract;

internal delegate ConditionWithSingleValue<TValue> ConditionWithSingleValueFactory<TValue>(
    IField field,
    Comparison comparison,
    TValue value
);