using GHC_2017.Entities;
using MoreLinq;
using System.Collections.Generic;
using System.Linq;

namespace GHC_2017
{
    public class VideoDistributor
    {
        #region Fields/Properties
        private ProblemData _problemData;
        #endregion

        #region Contructor
        public VideoDistributor(ProblemData problemData)
        {
            _problemData = problemData;
        }
        #endregion

        public void Distribute()
        {
            // Fill cache servers at 80% min
            foreach (Endpoint e in _problemData.EndPoints.OrderByDescending(_ => _.nbRequest))
            {
                var requests = _problemData.Requests.Where(r => r.EndPoint.Id == e.Id).ToList();
                var videos = requests.Select(_ => _.Video).Distinct().ToList();

                List<VideoRequest> videoRequests = new List<VideoRequest>();
                foreach (Video v in videos)
                {
                    VideoRequest p = new VideoRequest();
                    p.Video = v;

                    var requestsForVideo = requests.Where(_ => _.Video.Id == v.Id);
                    p.TotalRequests = requestsForVideo.Sum(_ => _.nbRequests);
                    videoRequests.Add(p);
                }

                videoRequests = videoRequests.OrderByDescending(_ => _.TotalRequests).ToList();

                int nbVideoRequestsTreated = 0;
                foreach (var videoRequest in videoRequests)
                {
                    int percentage = nbVideoRequestsTreated * 100 / videoRequests.Count();
                    if (percentage > 80)
                        break;

                    if (e.Latencies.Select(_ => _.cacheServer).Any(_ => _.Videos.Contains(videoRequest.Video)))
                        continue;

                    var caches = e.Latencies.Where(_ => _.cacheServer.SizeAvailableInMo >= videoRequest.Video.SizeInMb).ToList();
                    if (caches.Count() == 0)
                        continue;

                    var minCache = caches?.MinBy(_ => _.Latency);
                    var cache = minCache?.cacheServer;
                    if (cache == null)
                        continue;

                    if (e.LatencyInMs <= minCache.Latency)
                        continue;

                    cache.Videos.Add(videoRequest.Video);
                    cache.SizeAvailableInMo -= videoRequest.Video.SizeInMb;
                    videoRequest.Video.CoveredEndPoints.Add(e);

                    nbVideoRequestsTreated++;
                }
            }


            // Fill cache servers to maximum capacity (algo is about the same)
            foreach (Endpoint e in _problemData.EndPoints.OrderByDescending(_ => _.nbRequest))
            {
                var requests = _problemData.Requests.Where(r => r.EndPoint.Id == e.Id).ToList();

                var videos = requests
                    .Select(_ => _.Video)
                    .Distinct()
                    .ToList();

                videos = videos.Where(_ => !_.CoveredEndPoints.Contains(e)).ToList();

                List<VideoRequest> videoRequests = new List<VideoRequest>();
                foreach (Video v in videos)
                {
                    VideoRequest p = new VideoRequest();
                    p.Video = v;

                    var requestsForVideo = requests.Where(_ => _.Video.Id == v.Id);
                    p.TotalRequests = requestsForVideo.Sum(_ => _.nbRequests);
                    videoRequests.Add(p);
                }

                videoRequests = videoRequests.OrderByDescending(_ => _.TotalRequests).ToList();

                foreach (var videoRequest in videoRequests)
                {
                    if (e.Latencies.Select(_ => _.cacheServer).Any(_ => _.Videos.Contains(videoRequest.Video)))
                        continue;

                    var caches = e.Latencies.Where(_ => _.cacheServer.SizeAvailableInMo >= videoRequest.Video.SizeInMb).ToList();
                    if (caches.Count() == 0)
                        continue;
                    var minCache = caches?.MinBy(_ => _.Latency);
                    var cache = minCache?.cacheServer;
                    if (cache == null)
                        continue;

                    if (e.LatencyInMs <= minCache.Latency)
                        continue;

                    cache.Videos.Add(videoRequest.Video);
                    cache.SizeAvailableInMo -= videoRequest.Video.SizeInMb;
                }
            }
        }
    }
}