using ClientSide.Models;

namespace ClientSide.Services.Interfaces
{
    public interface IPalindromeService
    {
        IEnumerable<TextFile> CheckFilesForPalindromes(string dirName);
        IAsyncEnumerable<TextFile> CheckFilesForPalindromesAsync(string dirName);
    }
}
