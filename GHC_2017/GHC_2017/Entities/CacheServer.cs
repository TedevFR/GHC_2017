using System.Collections.Generic;
using System.Diagnostics;

namespace GHC_2017.Entities
{
    [DebuggerDisplay("id : {Id} cacheSize: {CacheSize}")]
    public class CacheServer
    {
        public int Id;
        public int CacheSize;

        public int SizeAvailableInMo;
        public List<Video> Videos = new List<Video>();
    }
}