﻿using System;
using System.IO;

namespace GHC.Parsing
{
    public class FriendlyOutputFileCreator : IDisposable
    {
        private StreamWriter FileWriter;

        public FriendlyOutputFileCreator(string filePath)
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