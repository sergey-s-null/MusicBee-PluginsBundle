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
            var shortName = new AssemblyName(eventArgs.Name).Name;

            if (!AssembliesToRedirect.Contains(shortName))
            {
                return null;
            }

            var dllPath = Path.Combine(Environment.CurrentDirectory, "Plugins", $"{shortName}.dll");

            return Assembly.LoadFile(dllPath);
        }
    }
}