using System.Diagnostics;

namespace GHC_2017.Entities
{
    [DebuggerDisplay("cache : {cacheServer.id} latency: {Latency} endpoint: {Endpoint.Id}")]
    public class LatencyToCacheServer
    {
        public CacheServer cacheServer;
        public int Latency;
        public Endpoint Endpoint;
    }
}