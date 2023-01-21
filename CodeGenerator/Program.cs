using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Autofac;
using CodeGenerator.Builders;
using CodeGenerator.Builders.Abstract;
using CodeGenerator.Builders.ServiceImplBuilder.Abstract;
using CodeGenerator.Enums;
using CodeGenerator.Extensions;
using CodeGenerator.Helpers;
using CodeGenerator.Models;
using Microsoft.Build.Evaluation;
using Module.MusicBee.Services;
using Root.Helpers;

namespace CodeGenerator
{
    internal class Program
    {
        private const string ServiceName = "MusicBeeApiService";
        private const string ReturnParameterName = "result";

        private const ServiceGenerationMode GenerationMode = ServiceGenerationMode.SingleFile;
        private const string ModuleProjectPath = @"..\..\..\Module.RemoteMusicBeeApi";
        private const string ExportPathInsideModuleProject = "Protos";
        private const string ModuleCsProjFilePath = ModuleProjectPath + @"\Module.RemoteMusicBeeApi.csproj";
        private const string ConsoleTestsCsProjFilePath = @"..\..\..\ConsoleTests\ConsoleTests.csproj";
        private const string FromConsoleTestToModulePath = @"..\Module.RemoteMusicBeeApi";

        private static readonly IReadOnlyCollection<MethodNameWithRestriction> MethodNames =
            new MethodNameWithRestriction[]
            {
                new("MB_ReleaseString"),
                new("MB_Trace"),
                new("Setting_GetPersistentStoragePath"),
                new("Setting_GetSkin"),
                new("Setting_GetSkinElementColour"),
                new("Setting_IsWindowBordersSkinned"),
                new("Library_GetFileProperty"),
                new("Library_GetFileTag"),
                new("Library_SetFileTag"),
                new("Library_CommitTagsToFile"),
                new("Library_GetLyrics"),
                // obsolete
                new("Library_GetArtwork", MethodRestriction.Ignore),
                new("Library_QueryFiles"),
                new("Library_QueryGetNextFile"),
                new("Player_GetPosition"),
                new("Player_SetPosition"),
                new("Player_GetPlayState"),
                new("Player_PlayPause"),
                new("Player_Stop"),
                new("Player_StopAfterCurrent"),
                new("Player_PlayPreviousTrack"),
                new("Player_PlayNextTrack"),
                new("Player_StartAutoDj"),
                new("Player_EndAutoDj"),
                new("Player_GetVolume"),
                new("Player_SetVolume"),
                new("Player_GetMute"),
                new("Player_SetMute"),
                new("Player_GetShuffle"),
                new("Player_SetShuffle"),
                new("Player_GetRepeat"),
                new("Player_SetRepeat"),
                new("Player_GetEqualiserEnabled"),
                new("Player_SetEqualiserEnabled"),
                new("Player_GetDspEnabled"),
                new("Player_SetDspEnabled"),
                new("Player_GetScrobbleEnabled"),
                new("Player_SetScrobbleEnabled"),
                new("NowPlaying_GetFileUrl"),
                new("NowPlaying_GetDuration"),
                new("NowPlaying_GetFileProperty"),
                new("NowPlaying_GetFileTag"),
                new("NowPlaying_GetLyrics"),
                new("NowPlaying_GetArtwork"),
                new("NowPlayingList_Clear"),
                new("NowPlayingList_QueryFiles"),
                new("NowPlayingList_QueryGetNextFile"),
                new("NowPlayingList_PlayNow"),
                new("NowPlayingList_QueueNext"),
                new("NowPlayingList_QueueLast"),
                new("NowPlayingList_PlayLibraryShuffled"),
                new("Playlist_QueryPlaylists"),
                new("Playlist_QueryGetNextPlaylist"),
                new("Playlist_GetType"),
                new("Playlist_QueryFiles"),
                new("Playlist_QueryGetNextFile"),
                // IntPtr
                new("MB_GetWindowHandle", MethodRestriction.Extended),
                new("MB_RefreshPanels"),
                new("MB_SendNotification"),
                // EventHandler
                new("MB_AddMenuItem", MethodRestriction.Extended),
                new("Setting_GetFieldName"),
                // obsolete
                new("Library_QueryGetAllFiles", MethodRestriction.Ignore),
                // obsolete
                new("NowPlayingList_QueryGetAllFiles", MethodRestriction.Ignore),
                // obsolete
                new("Playlist_QueryGetAllFiles", MethodRestriction.Ignore),
                // ThreadStart
                new("MB_CreateBackgroundTask", MethodRestriction.Extended),
                new("MB_SetBackgroundTaskMessage"),
                // EventHandler
                new("MB_RegisterCommand", MethodRestriction.Extended),
                // Font
                new("Setting_GetDefaultFont", MethodRestriction.Extended),
                new("Player_GetShowTimeRemaining"),
                new("NowPlayingList_GetCurrentIndex"),
                new("NowPlayingList_GetListFileUrl"),
                new("NowPlayingList_GetFileProperty"),
                new("NowPlayingList_GetFileTag"),
                new("NowPlaying_GetSpectrumData"),
                new("NowPlaying_GetSoundGraph"),
                // Rectangle
                new("MB_GetPanelBounds", MethodRestriction.Extended),
                // Control
                new("MB_AddPanel", MethodRestriction.Extended),
                // Control
                new("MB_RemovePanel", MethodRestriction.Extended),
                new("MB_GetLocalisation"),
                new("NowPlayingList_IsAnyPriorTracks"),
                new("NowPlayingList_IsAnyFollowingTrac"),
                new("Player_ShowEqualiser"),
                new("Player_GetAutoDjEnabled"),
                new("Player_GetStopAfterCurrentEnabled"),
                new("Player_GetCrossfade"),
                new("Player_SetCrossfade"),
                new("Player_GetReplayGainMode"),
                new("Player_SetReplayGainMode"),
                new("Player_QueueRandomTracks"),
                new("Setting_GetDataType"),
                new("NowPlayingList_GetNextIndex"),
                new("NowPlaying_GetArtistPicture"),
                new("NowPlaying_GetDownloadedArtwork"),
                new("MB_ShowNowPlayingAssistant"),
                new("NowPlaying_GetDownloadedLyrics"),
                new("Player_GetShowRatingTrack"),
                new("Player_GetShowRatingLove"),
                // ParameterizedThreadStart
                new("MB_CreateParameterisedBackgroundTask", MethodRestriction.Extended),
                new("Setting_GetLastFmUserId"),
                new("Playlist_GetName"),
                new("Playlist_CreatePlaylist"),
                new("Playlist_SetFiles"),
                new("Library_QuerySimilarArtists"),
                new("Library_QueryLookupTable"),
                new("Library_QueryGetLookupTableValue"),
                new("NowPlayingList_QueueFilesNext"),
                new("NowPlayingList_QueueFilesLast"),
                new("Setting_GetWebProxy"),
                new("NowPlayingList_RemoveAt"),
                new("Playlist_RemoveAt"),
                // Control
                new("MB_SetPanelScrollableArea", MethodRestriction.Extended),
                // object
                new("MB_InvokeCommand", MethodRestriction.Extended),
                new("MB_OpenFilterInTab"),
                new("MB_SetWindowSize"),
                new("Library_GetArtistPicture"),
                new("Pending_GetFileUrl"),
                new("Pending_GetFileProperty"),
                new("Pending_GetFileTag"),
                new("Player_GetButtonEnabled"),
                new("NowPlayingList_MoveFiles"),
                new("Library_GetArtworkUrl"),
                new("Library_GetArtistPictureThumb"),
                new("NowPlaying_GetArtworkUrl"),
                new("NowPlaying_GetDownloadedArtworkUrl"),
                new("NowPlaying_GetArtistPictureThumb"),
                new("Playlist_IsInList"),
                new("Library_GetArtistPictureUrls"),
                new("NowPlaying_GetArtistPictureUrls"),
                new("Playlist_AppendFiles"),
                new("Sync_FileStart"),
                new("Sync_FileEnd"),
                new("Library_QueryFilesEx"),
                new("NowPlayingList_QueryFilesEx"),
                new("Playlist_QueryFilesEx"),
                new("Playlist_MoveFiles"),
                new("Playlist_PlayNow"),
                new("NowPlaying_IsSoundtrack"),
                new("NowPlaying_GetSoundtrackPictureUrls"),
                new("Library_GetDevicePersistentId"),
                new("Library_SetDevicePersistentId"),
                new("Library_FindDevicePersistentId"),
                // object
                new("Setting_GetValue", MethodRestriction.Extended),
                new("Library_AddFileToLibrary"),
                new("Playlist_DeletePlaylist"),
                // DateTime - ради одного метода не хочеться что-то пилить
                new("Library_GetSyncDelta", MethodRestriction.Extended),
                new("Library_GetFileTags"),
                new("NowPlaying_GetFileTags"),
                new("NowPlayingList_GetFileTags"),
                // EventHandler
                new("MB_AddTreeNode", MethodRestriction.Extended),
                new("MB_DownloadFile"),
                new("Setting_GetFileConvertCommandLine"),
                new("Player_OpenStreamHandle"),
                new("Player_UpdatePlayStatistics"),
                new("Library_GetArtworkEx"),
                new("Library_SetArtworkEx"),
                new("MB_GetVisualiserInformation"),
                new("MB_ShowVisualiser"),
                new("MB_GetPluginViewInformation"),
                new("MB_ShowPluginView"),
                new("Player_GetOutputDevices"),
                new("Player_SetOutputDevice"),
                new("MB_UninstallPlugin"),
                new("Player_PlayPreviousAlbum"),
                new("Player_PlayNextAlbum"),
                new("Podcasts_QuerySubscriptions"),
                new("Podcasts_GetSubscription"),
                new("Podcasts_GetSubscriptionArtwork"),
                new("Podcasts_GetSubscriptionEpisodes"),
                new("Podcasts_GetSubscriptionEpisode"),
                new("NowPlaying_GetSoundGraphEx"),
                new("Sync_FileDeleteStart"),
                new("Sync_FileDeleteEnd"),
            };

        private static readonly IReadOnlyCollection<string> MethodNamesWithoutRestrictions = MethodNames
            .Where(x => x.Restriction == MethodRestriction.None)
            .Select(x => x.MethodName)
            .ToReadOnlyCollection();

        private static readonly IReadOnlyCollection<string> ExtendedMethodNames = MethodNames
            .Where(x => x.Restriction == MethodRestriction.Extended)
            .Select(x => x.MethodName)
            .ToReadOnlyCollection();

        private static readonly IReadOnlyCollection<string> MethodNamesExceptIgnored = MethodNames
            .Where(x => x.Restriction != MethodRestriction.Ignore)
            .Select(x => x.MethodName)
            .ToReadOnlyCollection();

        public static void Main(string[] args)
        {
            var container = ApplicationContainer.Create(ServiceImplMode.WrapWithTaskRun);

            var baseMethods = GetMethodsDefinition(MethodNamesWithoutRestrictions);
            var extendedMethods = GetMethodsDefinition(ExtendedMethodNames);
            var methodsExceptIgnored = GetMethodsDefinition(MethodNamesExceptIgnored);

            GenerateProtoFiles(baseMethods);

            AddProtobufToModuleCsProj(baseMethods);
            AddProtobufToConsoleTestsCsProj(baseMethods);

            GenerateServiceImpl(container, baseMethods);

            GenerateBaseInterface(container, baseMethods);
            GenerateExtendedInterface(container, extendedMethods);

            GenerateClientWrapper(container, baseMethods);
            GenerateMemoryContainerWrapper(container, methodsExceptIgnored);
        }

        private static void GenerateProtoFiles(IEnumerable<MBApiMethodDefinition> methods)
        {
            var builder = new ProtoFilesBuilder()
                .SetPostfixes("_Request", "_Response")
                .SetExportPath(ModuleProjectPath, ExportPathInsideModuleProject)
                .SetReturnParameterName(ReturnParameterName)
                .AddMethods(methods)
                .DeleteCurrentProtoFiles();

            if (GenerationMode == ServiceGenerationMode.MessagesInSeparateFiles)
            {
                builder.CreateMessagesProtoFiles();
            }

            builder
                .SetServiceGenerationMode(GenerationMode)
                .CreateServiceProtoFile(ServiceName);
        }

        private static void AddProtobufToModuleCsProj(IEnumerable<MBApiMethodDefinition> methods)
        {
            var projectCollection = new ProjectCollection();
            var project = projectCollection.LoadProject(ModuleCsProjFilePath);

            var serviceFilePath = @$"{ExportPathInsideModuleProject}\{ServiceName}.proto";
            var filePaths = GenerationMode switch
            {
                ServiceGenerationMode.MessagesInSeparateFiles => methods
                    .Select(x => @$"{ExportPathInsideModuleProject}\{x.Name}.proto")
                    .Append(serviceFilePath),
                ServiceGenerationMode.SingleFile => new[] { serviceFilePath },
                _ => throw new ArgumentOutOfRangeException(nameof(GenerationMode), GenerationMode, null)
            };

            new CsProjProtobufBuilder(project)
                .RemoveAllProtobuf()
                .SetProtobufType(ProtobufType.Server)
                .AddProtobufItemGroup(filePaths)
                .SaveProject();
        }

        private static void AddProtobufToConsoleTestsCsProj(IEnumerable<MBApiMethodDefinition> methods)
        {
            var projectCollection = new ProjectCollection();
            var project = projectCollection.LoadProject(ConsoleTestsCsProjFilePath);

            var builder = new CsProjProtobufBuilder(project)
                .RemoveAllProtobuf()
                .SetProtobufType(ProtobufType.Client);

            if (GenerationMode == ServiceGenerationMode.MessagesInSeparateFiles)
            {
                var messagesFilePaths = methods
                    .Select(x =>
                        Path.Combine(FromConsoleTestToModulePath, ExportPathInsideModuleProject, $"{x.Name}.proto"));
                builder
                    .AddProtobufItemGroup(messagesFilePaths)
                    .SetProtoRoot(FromConsoleTestToModulePath);
            }

            var serviceFilePath = Path.Combine(FromConsoleTestToModulePath, ExportPathInsideModuleProject,
                $"{ServiceName}.proto");
            builder
                .AddProtobufItemGroup(serviceFilePath)
                .SaveProject();
        }

        private static void GenerateServiceImpl(
            IComponentContext componentContext,
            IReadOnlyCollection<MBApiMethodDefinition> methods)
        {
            const string filePath = @"..\..\..\Module.RemoteMusicBeeApi\MusicBeeApiServiceImpl.cs";

            var lines = componentContext
                .Resolve<IServiceBuilder>()
                .GenerateServiceLines(methods);

            File.WriteAllLines(filePath, lines);
        }

        private static void GenerateBaseInterface(
            IComponentContext componentContext,
            IReadOnlyCollection<MBApiMethodDefinition> baseMethods)
        {
            const string baseFilePath = @"..\..\..\Root\MusicBeeApi\Abstract\IBaseMusicBeeApi.cs";

            var builder = componentContext.Resolve<IInterfaceBuilder>();
            builder.Namespace = "Root.MusicBeeApi.Abstract";
            builder.Name = "IBaseMusicBeeApi";
            var baseLines = builder
                .GenerateInterfaceLines(baseMethods);
            File.WriteAllLines(baseFilePath, baseLines);
        }

        private static void GenerateExtendedInterface(
            IComponentContext componentContext,
            IReadOnlyCollection<MBApiMethodDefinition> extendedMethods)
        {
            const string extendedFilePath = @"..\..\..\Root\MusicBeeApi\Abstract\IMusicBeeApi.cs";

            var builder = componentContext.Resolve<IInterfaceBuilder>();
            builder.ImportNamespaces = new[]
            {
                "System",
                "System.Drawing",
                "System.Threading",
                "System.Windows.Forms",
            };
            builder.Namespace = "Root.MusicBeeApi.Abstract";
            builder.Name = "IMusicBeeApi";
            builder.BaseInterface = "IBaseMusicBeeApi";

            var extendedLines = builder
                .GenerateInterfaceLines(extendedMethods);
            File.WriteAllLines(extendedFilePath, extendedLines);
        }

        private static void GenerateClientWrapper(
            IComponentContext componentContext,
            IReadOnlyCollection<MBApiMethodDefinition> methods)
        {
            const string wrapperFilePath = @"..\..\..\ConsoleTests\Services\MusicBeeApiClientWrapper.cs";

            var builder = componentContext
                .Resolve<IClientWrapperBuilder>();
            builder.ReturnVariableName = ReturnParameterName;

            var lines = builder
                .GenerateClientWrapperLines(methods);
            File.WriteAllLines(wrapperFilePath, lines);
        }

        private static void GenerateMemoryContainerWrapper(
            IComponentContext componentContext,
            IReadOnlyCollection<MBApiMethodDefinition> methods)
        {
            const string filePath = @"..\..\..\Root\MusicBeeApi\MusicBeeApiMemoryContainerWrapper.cs";

            var builder = componentContext.Resolve<IMemoryContainerWrapperBuilder>();
            builder.Namespace = "Root.MusicBeeApi";

            var lines = builder.GenerateMemoryContainerWrapperLines(methods);
            File.WriteAllLines(filePath, lines);
        }

        private static IReadOnlyCollection<MBApiMethodDefinition> GetMethodsDefinition(
            IReadOnlyCollection<string> fields)
        {
            return typeof(MusicBeeApiMemoryContainer)
                .GetMembers()
                .Where(x => fields.Contains(x.Name))
                .CastOrSkip<MemberInfo, FieldInfo>()
                .Select(Define)
                .ToReadOnlyCollection();
        }

        private static MBApiMethodDefinition Define(FieldInfo delegateFieldInfo)
        {
            var name = delegateFieldInfo.Name;

            var (returnParameter, parameters) = GetDelegateFieldParameters(delegateFieldInfo);

            var inputParameters = parameters
                .Where(x => !x.IsOut)
                .Select(Define)
                .ToReadOnlyCollection();

            var outputParameters = parameters
                .Where(x => x.IsOut)
                .Select(Define)
                .ToReadOnlyCollection();

            return new MBApiMethodDefinition(
                name,
                inputParameters,
                outputParameters,
                new MBApiReturnParameterDefinition(
                    returnParameter.ParameterType,
                    returnParameter.IsNullable()
                )
            );
        }

        private static (ParameterInfo returnParameter, ParameterInfo[] parameters) GetDelegateFieldParameters(
            FieldInfo delegateFieldInfo)
        {
            var invokeMethod = delegateFieldInfo.FieldType.GetMethod("Invoke");
            if (invokeMethod is null)
            {
                throw new Exception($"Invoke method of delegate field {delegateFieldInfo.Name} is null.");
            }

            return (invokeMethod.ReturnParameter, invokeMethod.GetParameters());
        }

        private static MBApiParameterDefinition Define(ParameterInfo parameterInfo)
        {
            return new MBApiParameterDefinition(
                parameterInfo.ParameterType.RemoveRefWrapper(),
                parameterInfo.Name,
                parameterInfo.IsNullable()
            );
        }
    }
}