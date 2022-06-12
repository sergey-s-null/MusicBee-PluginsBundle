using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace HackModule.AssemblyBindingRedirect
{
    public static class AssemblyRedirectService
    {
        private static readonly IReadOnlyCollection<string> AssembliesToRedirect = new[]
        {
            "Microsoft.Bcl.AsyncInterfaces",
            "Microsoft.Extensions.DependencyInjection",
            "Microsoft.Extensions.DependencyInjection.Abstractions",
            "Microsoft.Extensions.Logging.Abstractions",
            "Newtonsoft.Json",
            "Ninject",
            "System.Collections.Immutable",
            "System.Runtime.CompilerServices.Unsafe",
            "System.Threading.Tasks.Extensions",
        };

        public static void ApplyRedirects(AppDomain appDomain)
        {
            appDomain.AssemblyResolve += ResolveHandler;
        }

        private static Assembly? ResolveHandler(object sender, ResolveEventArgs eventArgs)
        {
            var assemblyName = new AssemblyName(eventArgs.Name);

            if (!IsRedirect(assemblyName))
            {
                return null;
            }

            var dllPath = Path.Combine(Environment.CurrentDirectory, "Plugins", $"{assemblyName.Name}.dll");

            return Assembly.LoadFile(dllPath);
        }

        private static bool IsRedirect(AssemblyName assemblyName)
        {
            var shortName = assemblyName.Name;

            if (shortName is null)
            {
                return false;
            }

            return !shortName.EndsWith("resources")
                   && (AssembliesToRedirect.Contains(shortName) || shortName.StartsWith("MahApps.Metro.IconPacks."));
        }
    }
}