﻿using Autofac;
using Module.MusicSourcesStorage.Services;
using Module.MusicSourcesStorage.Services.Abstract;
using GuiDIModule = Module.MusicSourcesStorage.Gui.DIModule;
using LogicDIModule = Module.MusicSourcesStorage.Logic.DIModule;
using DatabaseDIModule = Module.MusicSourcesStorage.Database.DIModule;

namespace Module.MusicSourcesStorage;

public sealed class MusicSourcesStorageModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterModule<GuiDIModule>();
        builder.RegisterModule<LogicDIModule>();
        builder.RegisterModule<DatabaseDIModule>();

        builder
            .RegisterType<MusicSourcesStorageModuleActions>()
            .As<IMusicSourcesStorageModuleActions>()
            .SingleInstance();
    }
}