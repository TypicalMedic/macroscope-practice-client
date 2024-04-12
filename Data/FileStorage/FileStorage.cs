using ClientSide.Models;
using ClientSide.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSide.Data.FileStorage
{
    class FileStorage : IData
    {
        public IEnumerable<string> GetDirFileNames(string directoryName)
        {
            var files = Directory.EnumerateFiles(directoryName, "*.txt", SearchOption.TopDirectoryOnly);
            foreach (var file in files)
            {
                yield return file;
            }
        }

        public TextFile GetFile(string fileName) => new TextFile(fileName, File.ReadAllText(fileName));
    }
}
