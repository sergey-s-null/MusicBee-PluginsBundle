using System;
using Autofac;
using Module.MusicBee.Extension.LibraryQuerying.Entities;
using Module.MusicBee.Extension.LibraryQuerying.Enums;
using Module.MusicBee.Extension.LibraryQuerying.Factories.Abstract;
using Module.MusicBee.Extension.LibraryQuerying.Helpers;

namespace Module.MusicBee.Extension.LibraryQuerying;

internal static class ConditionsContainer
{
    public static readonly ConditionWithSingleValueFactory<string> ConditionWithSingleStringFactory;
    public static readonly ConditionWithMultipleValuesFactory<string> ConditionWithMultipleStringsFactory;

    public static readonly ConditionWithSingleValueFactory<int> ConditionWithSingleNumberFactory;
    public static readonly ConditionWithTwoValuesFactory<int> ConditionWithTwoNumbersFactory;

    public static readonly ConditionWithSingleValueFactory<DateTime> ConditionWithSingleDateTimeFactory;

    public static readonly ConditionWithSingleValueFactory<TimeOffset> ConditionWithSingleTimeOffsetFactory;

    public static readonly ConditionWithSingleValueFactory<Rating> ConditionWithSingleRatingFactory;
    public static readonly ConditionWithTwoValuesFactory<Rating> ConditionWithTwoRatingsFactory;

    static ConditionsContainer()
    {
        var containerInstance = CreateContainer();

        ConditionWithSingleStringFactory = containerInstance
            .Resolve<ConditionWithSingleValueFactory<string>>();
        ConditionWithMultipleStringsFactory = containerInstance
            .Resolve<ConditionWithMultipleValuesFactory<string>>();

        ConditionWithSingleNumberFactory = containerInstance
            .Resolve<ConditionWithSingleValueFactory<int>>();
        ConditionWithTwoNumbersFactory = containerInstance
            .Resolve<ConditionWithTwoValuesFactory<int>>();

        ConditionWithSingleDateTimeFactory = containerInstance
            .Resolve<ConditionWithSingleValueFactory<DateTime>>();

        ConditionWithSingleTimeOffsetFactory = containerInstance
            .Resolve<ConditionWithSingleValueFactory<TimeOffset>>();

        ConditionWithSingleRatingFactory = containerInstance
            .Resolve<ConditionWithSingleValueFactory<Rating>>();
        ConditionWithTwoRatingsFactory = containerInstance
            .Resolve<ConditionWithTwoValuesFactory<Rating>>();
    }

    private static IContainer CreateContainer()
    {
        var builder = new ContainerBuilder();

        builder
            .RegisterGeneric(typeof(ConditionWithSingleValue<>))
            .AsSelf();
        builder
            .RegisterGeneric(typeof(ConditionWithTwoValues<>))
            .AsSelf();
        builder
            .RegisterGeneric(typeof(ConditionWithMultipleValues<>))
            .AsSelf();

        builder
            .RegisterInstance(RepresentationHelper.RepresentString)
            .AsSelf()
            .SingleInstance();
        builder
            .RegisterInstance(RepresentationHelper.RepresentInt)
            .AsSelf()
            .SingleInstance();
        builder
            .RegisterInstance(RepresentationHelper.RepresentDateTime)
            .AsSelf()
            .SingleInstance();
        builder
            .RegisterInstance(RepresentationHelper.RepresentTimeOffset)
            .AsSelf()
            .SingleInstance();
        builder
            .RegisterInstance(RepresentationHelper.RepresentRating)
            .AsSelf()
            .SingleInstance();

        return builder.Build();
    }
}