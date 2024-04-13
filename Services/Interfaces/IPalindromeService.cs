using ClientSide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSide.Services.Interfaces
{
    interface IPalindromeService
    {
        IEnumerable<TextFile> CheckFilesForPalindromes(string dirName);
        IAsyncEnumerable<TextFile> CheckFilesForPalindromesAsync(string dirName);
    }
}
