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
            "Microsoft.Extensions.DependencyInjection.Abstractions",
            "Microsoft.Extensions.DependencyInjection",
            "Microsoft.Extensions.Logging.Abstractions",
            "System.Runtime.CompilerServices.Unsafe",
            "Microsoft.Bcl.AsyncInterfaces"
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