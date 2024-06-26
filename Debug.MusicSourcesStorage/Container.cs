﻿using System;
using System.IO;
using Autofac;
using Debug.Common;
using Mead.MusicBee.Api.Services.Abstract;
using Module.MusicSourcesStorage;
using Module.MusicSourcesStorage.Core;
using Module.Mvvm.Extension;
using Module.Settings.Database.Services.Abstract;
using Module.Vk.Gui.Services.Abstract;
using Moq;
using VkNet.Abstractions;

namespace Debug.MusicSourcesStorage;

public static class Container
{
    public static IContainer Create(bool withVkApi)
    {
        var builder = new ContainerBuilder();

        var configuration = new DebugModuleConfiguration();

        builder
            .RegisterInstance(configuration)
            .As<IModuleConfiguration>();

        builder
            .RegisterInstance(GetSettingsRepositoryMock(configuration))
            .As<ISettingsRepository>();
        builder
            .RegisterInstance(GetMusicBeeApiMock())
            .As<IMusicBeeApi>();

        if (withVkApi)
        {
            var vkApi = VkHelper.GetAuthorizedVkApi();
            builder
                .RegisterInstance(vkApi)
                .As<IVkApi>();

            builder
                .RegisterInstance(GetVkApiProviderMock(vkApi))
                .As<IVkApiProvider>();
        }

        builder.RegisterModule<MvvmExtensionModule>();
        builder.RegisterModule<MusicSourcesStorageModule>();

        return builder.Build();
    }

    private static IVkApiProvider GetVkApiProviderMock(IVkApi vkApi)
    {
        var mock = new Mock<IVkApiProvider>();
        mock.Setup(x => x.TryProvideAuthorizedVkApi(out vkApi))
            .Returns(true);
        return mock.Object;
    }

    private static ISettingsRepository GetSettingsRepositoryMock(IModuleConfiguration configuration)
    {
        var mock = new Mock<ISettingsRepository>();

        mock.Setup(x => x.FindString(
                configuration.SettingsArea,
                "vk-documents-downloading-directory")
            )
            .Returns(Path.Combine(DebugModuleConfiguration.DebugFolder, "VkDocuments"));
        mock.Setup(x => x.FindString(
                configuration.SettingsArea,
                "source-files-downloading-directory")
            )
            .Returns(Path.Combine(DebugModuleConfiguration.DebugFolder, "SourceFiles"));

        return mock.Object;
    }

    private static IMusicBeeApi GetMusicBeeApiMock()
    {
        var mock = new Mock<IMusicBeeApi>();

        var files = Array.Empty<string>();
        mock.Setup(x => x.Library_QueryFilesEx(It.IsAny<string>(), out files))
            .Returns(true);

        return mock.Object;
    }
}