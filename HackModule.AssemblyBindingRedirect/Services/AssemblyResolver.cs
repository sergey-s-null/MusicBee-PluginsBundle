﻿using System.Reflection;
using HackModule.AssemblyBindingRedirect.Services.Abstract;

namespace HackModule.AssemblyBindingRedirect.Services;

public sealed class AssemblyResolver : IAssemblyResolver
{
    private static readonly IReadOnlyCollection<string> AssembliesToRedirect = new[]
    {
        "Castle.Core",
        "Microsoft.Bcl.AsyncInterfaces",
        "Microsoft.Extensions.DependencyInjection",
        "Microsoft.Extensions.DependencyInjection.Abstractions",
        "Microsoft.Extensions.Logging.Abstractions",
        "Newtonsoft.Json",
        "System.Buffers",
        "System.Collections.Immutable",
        "System.Diagnostics.DiagnosticSource",
        "System.Memory",
        "System.Runtime.CompilerServices.Unsafe",
        "System.Text.Encoding.CodePages",
        "System.Threading.Tasks.Extensions",
    };

    private readonly string _assembliesDirectory;

    public AssemblyResolver(string assembliesDirectory)
    {
        _assembliesDirectory = assembliesDirectory;
    }

    public Assembly? ResolveHandler(object sender, ResolveEventArgs eventArgs)
    {
        var assemblyName = new AssemblyName(eventArgs.Name);

        if (!IsRedirect(assemblyName))
        {
            return null;
        }

        var dllPath = Path.Combine(_assembliesDirectory, $"{assemblyName.Name}.dll");

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
               && (AssembliesToRedirect.Contains(shortName) || shortName.StartsWith("MahApps.Metro.IconPacks"));
    }
}