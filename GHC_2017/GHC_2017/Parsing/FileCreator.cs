using System;
using System.IO;

namespace GHC.Parsing
{
    public class FileCreator : IDisposable
    {
        private StreamWriter FileWriter;

        public FileCreator(string filePath)
        {
            FileInfo f = new FileInfo(filePath);
            Directory.CreateDirectory(f.DirectoryName);
            FileWriter = new StreamWriter(filePath, false);
        }

        public void WriteLine(string line)
        {
            FileWriter.WriteLine(line);
        }

        public void Dispose()
        {
            FileWriter?.Flush();
            FileWriter?.Close();
            FileWriter?.Dispose();
        }
    }
}