using System.Collections.Generic;
using System.IO;
using PropertyChanged;
using Root;
using Root.Helpers;

namespace Module.InboxAdder.GUI.FileByIndexSelectDialog
{
    [AddINotifyPropertyChangedInterface]
    public class FileByIndexSelectDialogVM
    {
        private readonly MusicBeeApiInterface _mbApi;
        private readonly IReadOnlyDictionary<int, string> _allAudionByIndex;

        [OnChangedMethod(nameof(OnIndexChanged))]
        public string Index { get; set; } = "";
        public string FileName { get; private set; } = "";
        public string Directory { get; private set; } = "";

        [DoNotNotify]
        public string? FilePath { get; private set; }

        public FileByIndexSelectDialogVM(MusicBeeApiInterface mbApi)
        {
            _mbApi = mbApi;
            _allAudionByIndex = GetAllFilesByIndices();
        }

        private IReadOnlyDictionary<int, string> GetAllFilesByIndices()
        {
            _mbApi.Library_QueryFilesEx("", out var files);
            
            var filesByIndices = new Dictionary<int, string>();
            
            foreach (var file in files)
            {
                if (_mbApi.TryGetIndex(file, out var index) 
                    && !filesByIndices.ContainsKey(index))
                {
                    filesByIndices.Add(index, file);
                }
            }

            return filesByIndices;
        }

        private void OnIndexChanged()
        {
            if (int.TryParse(Index, out var index) && _allAudionByIndex.ContainsKey(index))
            {
                var filePath = _allAudionByIndex[index];
                FileName = Path.GetFileName(filePath);
                Directory = Path.GetDirectoryName(filePath) ?? string.Empty;
                FilePath = filePath;
            }
            else
            {
                FileName = "";
                Directory = "";
                FilePath = null;
            }
        }
    }
}