using System.Diagnostics;

namespace GHC_2017.Entities
{
    [DebuggerDisplay("id : {Id} video: {Video.Id} Endpoint: {EndPoint.Id}")]
    public class Request
    {
        public int Id;
        public Video Video;
        public Endpoint EndPoint;
        public int nbRequests;
    }
}