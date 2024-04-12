using ClientSide.Models;
using ClientSide.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSide.Data.FileStorage
{
    class FileStorage : IData
    {
        public IEnumerable<string> GetDirFileNames(string directoryName)
        {
            throw new NotImplementedException();
        }

        public TextFile GetFile(string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
