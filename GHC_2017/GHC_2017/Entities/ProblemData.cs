using System.Collections.Generic;

namespace GHC_2017.Entities
{
    public class ProblemData
    {
        public int NbVideo;
        public int NbEndPoints;
        public int NbRequest;
        public int NbCacheServer;
        public int CacheSizeInMo;

        public List<Video> Videos = new List<Video>();
        public List<Endpoint> EndPoints = new List<Endpoint>();
        public List<CacheServer> CachesServers = new List<CacheServer>();
        public List<Request> Requests = new List<Request>();
    }
}