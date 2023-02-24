using Autofac;
using Module.DataExporter.Services;

namespace Module.DataExporter;

public sealed class DataExporterModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterType<DataExportService>()
            .As<IDataExportService>();
    }
}