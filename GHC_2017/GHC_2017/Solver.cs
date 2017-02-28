using GHC_2017.Entities;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GHC_2017
{
    public class Solver
    {
        #region Fields/Properties
        private string _inputFilePath;
        private string _outputFilePath;

        private ProblemData _problemData;
        #endregion

        #region Constructor
        public Solver(string inputFilePath, string outputFilePath)
        {
            _inputFilePath = inputFilePath;
            _outputFilePath = outputFilePath;
        }
        #endregion

        #region Public methods
        public void Solve()
        {
            ParseInput();
            Cache();
            CreateOutput();
            DisplaySolution();
        }

        private void Cache()
        {
            VideoDistributor distributor = new VideoDistributor(_problemData);
            distributor.Distribute();
        }
        #endregion

        #region Private methods
        private void ParseInput()
        {
            Parser parser = new Parser(_inputFilePath);
            _problemData = parser.Parse();
        }

        private void CreateOutput()
        {
            SolutionSerializer serializer = new SolutionSerializer(_outputFilePath, _problemData);
            serializer.Serialize();
        }

        private void DisplaySolution()
        {
            foreach(var c in _problemData.CachesServers)
            {
                Console.WriteLine($"server {c.Id} has {c.SizeAvailableInMo} Mo free space");
            }

            Console.WriteLine(string.Format("{0}{0}{0}{0}", Environment.NewLine));
        }
        #endregion
    }
}