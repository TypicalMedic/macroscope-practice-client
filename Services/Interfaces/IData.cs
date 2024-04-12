using ClientSide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSide.Services.Interfaces
{
    interface IData
    {
        TextFile GetFile(string fileName);
        IEnumerable<string> GetDirFileNames(string directoryName);
    }
}
