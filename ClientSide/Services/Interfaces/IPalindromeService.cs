using ClientSide.Models;

namespace ClientSide.Services.Interfaces
{
    interface IPalindromeService
    {
        IEnumerable<TextFile> CheckFilesForPalindromes(string dirName);
        IAsyncEnumerable<TextFile> CheckFilesForPalindromesAsync(string dirName);
    }
}
