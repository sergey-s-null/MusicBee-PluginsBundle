using System.Threading.Tasks;
using Grpc.Core;
using Root;

namespace Module.RemoteMusicBeeApi
{
    public class MusicBeeApiServiceImpl : MusicBeeApiService.MusicBeeApiServiceBase
    {
        private readonly MusicBeeApiInterface _mbApi;

        public MusicBeeApiServiceImpl(MusicBeeApiInterface mbApi)
        {
            _mbApi = mbApi;
        }

        public override Task<Library_QueryFilesEx_Response> Library_QueryFilesEx(Library_QueryFilesEx_Request request,
            ServerCallContext context)
        {
            var result = _mbApi.Library_QueryFilesEx(request.Query, out var files);
        
            var response = new Library_QueryFilesEx_Response()
            {
                Result = result
            };
            response.Files.AddRange(files);
        
            return Task.FromResult(response);
        }

        public override Task<Library_GetFileTag_Response> Library_GetFileTag(Library_GetFileTag_Request request,
            ServerCallContext context)
        {
            var tagValue = _mbApi.Library_GetFileTag(request.SourceFileUrl, (MetaDataType)request.Field);
            return Task.FromResult(new Library_GetFileTag_Response()
            {
                TagValue = tagValue
            });
        }
    }
}