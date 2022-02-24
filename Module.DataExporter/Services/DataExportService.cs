using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Module.DataExporter.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Root.MusicBeeApi;

namespace Module.DataExporter.Services
{
    public class DataExportService : IDataExportService
    {
        private readonly MusicBeeApiMemoryContainer _mbApi;
        
        private readonly IReadOnlyCollection<FilePropertyType> _defaultFilePropertyTypes = new[]
        {
            FilePropertyType.DateAdded,
            FilePropertyType.PlayCount,
            FilePropertyType.SkipCount,
        };
        private readonly IReadOnlyCollection<MetaDataType> _defaultMetaTypes = new[]
        {
            MetaDataType.Artists,
            MetaDataType.TrackTitle,
            MetaDataType.OriginalArtist,
            MetaDataType.OriginalTitle,
            MetaDataType.Comment,
            MetaDataType.Rating,
            MetaDataType.Album,
            MetaDataType.AlbumArtist,
            MetaDataType.AlbumArtistRaw,
            MetaDataType.Year,
            MetaDataType.OriginalYear,
            MetaDataType.TrackNo,
            MetaDataType.TrackCount,
            MetaDataType.DiscNo,
            MetaDataType.DiscCount
        };
        private readonly IReadOnlyCollection<(string, MetaDataType)> _specificMetaTypes = new []
        {
            ("Index1", MetaDataType.Custom1),
            ("Index2", MetaDataType.Custom2),
            ("VkId", MetaDataType.Custom3),
            ("Index", MetaDataType.Custom4),
            ("Pools", MetaDataType.Custom5)
        };
        
        
        public DataExportService(MusicBeeApiMemoryContainer mbApi)
        {
            _mbApi = mbApi;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dirPath"></param>
        /// <exception cref="MusicBeeApiException">Не получилось достать пути к файлам бибилиотеки</exception>
        /// <exception cref="Exception">Много исключений записи в файл</exception>
        public void Export(string dirPath)
        {
            if (!_mbApi.Library_QueryFilesEx("", out var filePaths))
            {
                throw new MusicBeeApiException("MBApi did not return files from library.");
            }

            var items = filePaths
                .Select(f => GetFileProperties(f)
                    .Concat(GetDefaultFileTags(f))
                    .Concat(GetSpecificFileTags(f))
                    .Select(kvp => new JProperty(kvp.Key, kvp.Value))
                )
                .Select(props => new JObject(props));

            var jArray = new JArray(items);

            var fileName = $"{DateTime.Now:yyyy-MM-dd HH-mm-ss}.json";
            
            File.WriteAllText(Path.Combine(dirPath, fileName), jArray.ToString(Formatting.Indented));
        }

        private IEnumerable<KeyValuePair<string, string>> GetFileProperties(string filePath)
        {
            return _defaultFilePropertyTypes
                .Select(t => new KeyValuePair<string, string>(
                    t.ToString(),
                    _mbApi.Library_GetFileProperty(filePath, t))
                );
        }

        private IEnumerable<KeyValuePair<string, string>> GetDefaultFileTags(string filePath)
        {
            _mbApi.Library_GetFileTags(filePath, _defaultMetaTypes.ToArray(), out var values);

            return _defaultMetaTypes
                .Zip(values, (t, v) =>
                    new KeyValuePair<string, string>(t.ToString(), v)
                )
                .Where(x => !string.IsNullOrEmpty(x.Value));
        }

        private IEnumerable<KeyValuePair<string, string>> GetSpecificFileTags(string filePath)
        {
            var additionalMetaTypes = _specificMetaTypes
                .Select(x => x.Item2)
                .ToArray();
            
            _mbApi.Library_GetFileTags(filePath, additionalMetaTypes, out var values);

            return _specificMetaTypes
                .Zip(values, (kmt, v) =>
                    new KeyValuePair<string, string>(kmt.Item1, v)
                );
        }
    }
}