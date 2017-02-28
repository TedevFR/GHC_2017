using System.Collections.Generic;
using System.Diagnostics;

namespace GHC_2017.Entities
{
    [DebuggerDisplay("id : {Id} size: {SizeInMb}")]
    public class Video
    {
        public int Id;
        public int SizeInMb;

        public List<Endpoint> CoveredEndPoints = new List<Endpoint>();
    }
}