using Module.MusicSourcesStorage.Logic.Entities.Args;
using Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;
using Void = Module.MusicSourcesStorage.Logic.Entities.Void;

namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface ISourceFilesRetargetingService
{
    IActivableTask<FilesRetargetingArgs, Void> CreateRetargetingTask();
}