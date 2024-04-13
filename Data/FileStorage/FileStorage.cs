using ClientSide.Data.Interfaces;
using ClientSide.Models;
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

        public async IAsyncEnumerable<string> GetDirFileNamesAsync(string directoryName)
        {
            var files = await Task.Run(() => Directory.EnumerateFiles(directoryName, "*.txt", SearchOption.TopDirectoryOnly)).ConfigureAwait(false);
            foreach (var file in files)
            {
                yield return file;
            }
        }

        public TextFile GetFile(string fileName) => new TextFile(fileName, File.ReadAllText(fileName));
        public async Task<TextFile> GetFileAsync(string fileName) => new TextFile(fileName, await File.ReadAllTextAsync(fileName).ConfigureAwait(false));
    }
}
