using Module.DataExporter.Services;
using Ninject.Modules;

namespace Module.DataExporter
{
    public sealed class DataExporterModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDataExportService>()
                .To<DataExportService>();
        }
    }
}