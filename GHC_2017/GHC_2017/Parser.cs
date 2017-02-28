using GHC.Parsing;
using GHC_2017.Entities;
using System.Linq;

namespace GHC_2017
{
    public class Parser
    {
        #region Fields/Properties
        private string _inputFilePath;

        private string _currentLine;
        private ProblemData _problemData;
        #endregion

        #region Constructor
        public Parser(string inpuFilePath)
        {
            _inputFilePath = inpuFilePath;
        }
        #endregion

        public ProblemData Parse()
        {
            _problemData = new ProblemData();

            using (FriendlyParser parser = new FriendlyParser(_inputFilePath))
            {
                ParseNbEntities(parser);
                ParseVideoSizes(parser);
                ParseEndPoints(parser);
                ParseRequests(parser);
            }

            return _problemData;
        }

        private void ParseNbEntities(FriendlyParser parser)
        {
            _currentLine = parser.GetStringLine();
            var lineTab = _currentLine.Split(' ');

            _problemData.NbVideo = int.Parse(lineTab[0]);
            _problemData.NbEndPoints = int.Parse(lineTab[1]);
            _problemData.NbRequest = int.Parse(lineTab[2]);
            _problemData.NbCacheServer = int.Parse(lineTab[3]);
            _problemData.CacheSizeInMo = int.Parse(lineTab[4]);

            InitCacheServers();
            InitEndPoints();
        }
        
        private void InitCacheServers()
        {
            for (int i = 0; i < _problemData.NbCacheServer; i++)
            {
                _problemData.CachesServers.Add(new CacheServer()
                {
                    Id = i,
                    CacheSize = _problemData.CacheSizeInMo,
                    SizeAvailableInMo = _problemData.CacheSizeInMo
                });
            }
        }

        private void InitEndPoints()
        {
            for (int i = 0; i < _problemData.NbEndPoints; i++)
            {
                _problemData.EndPoints.Add(new Endpoint() { Id = i });
            }
        }

        private void ParseVideoSizes(FriendlyParser parser)
        {
            _currentLine = parser.GetStringLine();
            var lineTab = _currentLine.Split(' ');
            int videoId = 0;
            foreach (var strSize in lineTab)
            {
                Video v = new Video();
                v.Id = videoId++;
                v.SizeInMb = int.Parse(strSize);
                _problemData.Videos.Add(v);
            }
        }

        private void ParseEndPoints(FriendlyParser parser)
        {
            _currentLine = parser.GetStringLine();
            var lineTab = _currentLine.Split(' ');
            int endPointId = 0;
            while (lineTab.Count() == 2)
            {
                Endpoint e = _problemData.EndPoints.ElementAt(endPointId++);
                e.LatencyInMs = int.Parse(lineTab[0]);
                int nbCacheServers = int.Parse(lineTab[1]);

                for (int i = 0; i < nbCacheServers; i++)
                {
                    _currentLine = parser.GetStringLine();
                    var lineEndPointTab = _currentLine.Split(' ');

                    int idServerCache = int.Parse(lineEndPointTab[0]);
                    var cacheServer = _problemData.CachesServers.ElementAt(idServerCache);

                    LatencyToCacheServer latency = new LatencyToCacheServer();
                    latency.cacheServer = cacheServer;
                    latency.Endpoint = e;
                    latency.Latency = int.Parse(lineEndPointTab[1]);
                    e.Latencies.Add(latency);
                }

                _currentLine = parser.GetStringLine();
                lineTab = _currentLine.Split(' ');
            }
        }

        private void ParseRequests(FriendlyParser parser)
        {
            int requestId = 0;
            do
            {
                var lineRequestTab = _currentLine.Split(' ');

                int idVideo = int.Parse(lineRequestTab[0]);
                Video v = _problemData.Videos.ElementAt(idVideo);

                int idEndPoint = int.Parse(lineRequestTab[1]);
                Endpoint e = _problemData.EndPoints.ElementAt(idEndPoint);

                int nbRequest = int.Parse(lineRequestTab[2]);

                Request r = new Request();
                r.Id = requestId++;
                r.EndPoint = e;
                r.Video = v;
                r.nbRequests = nbRequest;
                _problemData.Requests.Add(r);

                _currentLine = parser.GetStringLine();
                e.nbRequest += nbRequest;
            }
            while (!string.IsNullOrWhiteSpace(_currentLine));
        }
    }
}