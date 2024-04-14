using ClientSide.Models;

namespace ClientSide.Data.Interfaces
{
    interface IData
    {
        TextFile? GetFile(string fileName);
        Task<TextFile?> GetFileAsync(string fileName);
        IEnumerable<string> GetDirFileNames(string directoryName);
        IAsyncEnumerable<string> GetDirFileNamesAsync(string directoryName);
    }
}
