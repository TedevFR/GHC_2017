using System;
using System.IO;
using System.Linq;

namespace GHC.Parsing
{
    public class Parser : IDisposable
    {
        private StreamReader FileReader;

        public Parser(string filePath)
        {
            FileReader = new StreamReader(filePath);
        }

        public int GetIntLine()
        {
            var line = FileReader.ReadLine();
            return int.Parse(line);
        }

        public string GetStringLine()
        {
            return FileReader.ReadLine();
        }

        public int[] GetIntsOfLine(char intSeparator = ' ')
        {
            string line = FileReader.ReadLine();
            return line.Split(intSeparator)
                .Select(_ => int.Parse(_))
                .ToArray();
        }
        
        public string[] GetStringsOfLine(char intSeparator = ' ')
        {
            string line = FileReader.ReadLine();
            return line.Split(intSeparator).ToArray();
        }

        public void Dispose()
        {
            FileReader?.Close();
            FileReader?.Dispose();
        }
    }
}