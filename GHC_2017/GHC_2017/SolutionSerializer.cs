using GHC.Parsing;
using GHC_2017.Entities;
using System.Collections.Generic;
using System.Linq;

namespace GHC_2017
{
    public class SolutionSerializer
    {
        #region Fields/Properties
        private string _outputFilePath;
        private ProblemData _problemData;
        #endregion

        #region Constructor
        public SolutionSerializer(string outputFilePath, ProblemData problemData)
        {
            _outputFilePath = outputFilePath;
            _problemData = problemData;
        }
        #endregion

        public void Serialize()
        {
            using (FriendlyOutputFileCreator fc = new FriendlyOutputFileCreator(_outputFilePath))
            {
                List<CacheServer> cacheServers = GetUsedCacheServers();

                fc.WriteLine(cacheServers.Count().ToString());

                foreach (var cache in cacheServers)
                {
                    string line = $"{cache.Id} ";
                    line += string.Join(" ", cache.Videos.Select(_ => _.Id).ToArray());

                    fc.WriteLine(line);
                }
            }
        }

        private List<CacheServer> GetUsedCacheServers()
        {
            return _problemData.CachesServers
                .Where(_ => _.Videos.Count() > 0)
                .ToList();
        }
    }
}