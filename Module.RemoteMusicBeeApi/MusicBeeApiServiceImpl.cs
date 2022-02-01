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

        public override Task<NowPlaying_GetArtistPicture_Response> NowPlaying_GetArtistPicture(NowPlaying_GetArtistPicture_Request request, ServerCallContext context)
        {
            return base.NowPlaying_GetArtistPicture(request, context);
        }
    }
}