using System.Collections.Generic;
using System.Diagnostics;

namespace GHC_2017.Entities
{
    [DebuggerDisplay("id : {Id} latency: {LatencyInMs}")]
    public class Endpoint
    {
        public int Id;
        public int LatencyInMs;

        public List<LatencyToCacheServer> Latencies = new List<LatencyToCacheServer>();

        public int nbRequest = 0;
    }
}