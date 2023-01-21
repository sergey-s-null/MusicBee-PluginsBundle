using Autofac;
using Module.MusicBee.Extension.LibraryQuerying.Entities;
using Module.MusicBee.Extension.LibraryQuerying.Helpers;

namespace Module.MusicBee.Extension.LibraryQuerying;

internal static class ConditionsContainer
{
    public static readonly IContainer Instance = Create();

    private static IContainer Create()
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