using System;
using System.IO;
using Grpc.Core;
using HackModule.AssemblyBindingRedirect.Factories;
using HackModule.AssemblyBindingRedirect.Services.Abstract;
using Module.RemoteMusicBeeApi;
using Ninject;
using Ninject.Syntax;
using Root.MusicBeeApi;
using Root.MusicBeeApi.Abstract;

namespace MusicBeePlugin
{
    public class Plugin
    {
        private const short PluginInfoVersion = 1;
        private const short MinInterfaceVersion = 40; // 41
        private const short MinApiRevision = 53;

        private const string ServerHost = "localhost";
        private const int ServerPort = 4999;

        private IMusicBeeApi? _mbApi;
        private IAssemblyResolver? _assemblyResolver;
        private Server? _server;

        public PluginInfo Initialise(IntPtr apiInterfacePtr)
        {
            var mbApiMemoryContainer = new MusicBeeApiMemoryContainer();
            mbApiMemoryContainer.Initialise(apiInterfacePtr);

            var kernel = Bootstrapper.GetKernel(mbApiMemoryContainer);

            ApplyAssembliesResolution(kernel);

            _mbApi = kernel.Get<IMusicBeeApi>();

            return GetPluginInfo();
        }

        private void ApplyAssembliesResolution(IResolutionRoot kernel)
        {
            var assemblyResolverFactory = kernel.Get<IAssemblyResolverFactory>();
            var assembliesDirectory = Path.Combine(Environment.CurrentDirectory, "Plugins");
            _assemblyResolver = assemblyResolverFactory.Create(assembliesDirectory);
            AppDomain.CurrentDomain.AssemblyResolve += _assemblyResolver.ResolveHandler;
        }

        private static PluginInfo GetPluginInfo()
        {
            return new PluginInfo
            {
                PluginInfoVersion = PluginInfoVersion,
                Name = "Laiser399: Remote Api",
                Description = "Remote api server",
                Author = "Laiser399",
                TargetApplication = "", //  the name of a Plugin Storage device or panel header for a dockable panel
                Type = PluginType.General,
                VersionMajor = 1, // your plugin version
                VersionMinor = 0,
                Revision = 1,
                MinInterfaceVersion = MinInterfaceVersion,
                MinApiRevision = MinApiRevision,
                ReceiveNotifications = ReceiveNotificationFlags.StartupOnly,
                ConfigurationPanelHeight = 0
            };
        }

        public bool Configure(IntPtr _)
        {
            return true;
        }

        public void Close(PluginCloseReason reason)
        {
            StopServer();
        }

        public void Uninstall()
        {
            StopServer();
        }

        public void ReceiveNotification(string sourceFileUrl, NotificationType type)
        {
            if (type == NotificationType.PluginStartup)
            {
                StartServer();
            }
        }

        private void StartServer()
        {
            if (_mbApi is null)
            {
                throw new NullReferenceException($"{nameof(_mbApi)} is null.");
            }

            if (_server is not null)
            {
                return;
            }

            _server = new Server()
            {
                Ports = {{ServerHost, ServerPort, ServerCredentials.Insecure}},
                Services = {MusicBeeApiService.BindService(new MusicBeeApiServiceImpl(_mbApi))},
            };
            _server.Start();
        }

        private void StopServer()
        {
            if (_server is null)
            {
                return;
            }

            _server.ShutdownAsync().Wait();
            _server = null;
        }
    }
}