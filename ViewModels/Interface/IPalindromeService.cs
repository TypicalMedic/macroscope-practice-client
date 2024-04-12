using ClientSide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSide.ViewModels.Interface
{
    interface IPalindromeService
    {
        IEnumerable<TextFile> CheckFilesForPalindromes(string dirName);
    }
}
