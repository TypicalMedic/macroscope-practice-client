using ClientSide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSide.Data.Interfaces
{
    interface IData
    {
        TextFile GetFile(string fileName);
        Task<TextFile> GetFileAsync(string fileName);
        IEnumerable<string> GetDirFileNames(string directoryName);
        IAsyncEnumerable<string> GetDirFileNamesAsync(string directoryName);
    }
}
