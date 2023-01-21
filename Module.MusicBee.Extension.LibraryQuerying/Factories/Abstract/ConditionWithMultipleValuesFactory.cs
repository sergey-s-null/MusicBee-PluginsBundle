using System.Collections.Generic;
using Module.MusicBee.Extension.LibraryQuerying.Entities;
using Module.MusicBee.Extension.LibraryQuerying.Entities.Abstract;

namespace Module.MusicBee.Extension.LibraryQuerying.Factories.Abstract;

internal delegate ConditionWithMultipleValues<TValue> ConditionWithMultipleValuesFactory<TValue>(
    IField field,
    Comparison comparison,
    IReadOnlyCollection<TValue> values
);