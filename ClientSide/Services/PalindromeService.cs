using ClientSide.Data.Interfaces;
using ClientSide.Models;
using ClientSide.PalindromeValidator.Interfaces;
using ClientSide.Services.Interfaces;

namespace ClientSide.Services
{
    public partial class PalindromeService(IData data, IPalindromeValidator validator) : IPalindromeService
    {
        private readonly IData _data = data;
        private readonly IPalindromeValidator _validator = validator;
        public IEnumerable<TextFile> CheckFilesForPalindromes(string dirName)
        {
            var textFiles = _data.GetDirFileNames(dirName);
            foreach (var txtFile in textFiles)
            {
                TextFile? file = _data.GetFile(txtFile);
                if (file == null)
                {
                    continue;
                }
                file.IsPalindrome = _validator.IsValid(file.Text);
                yield return file;
            }
        }
        public async IAsyncEnumerable<TextFile> CheckFilesForPalindromesAsync(string dirName)
        {
            var textFiles = _data.GetDirFileNames(dirName);
            var tasks = ArrangeTasks(textFiles);

            foreach (Task<TextFile?> task in tasks)
            {
                var result = await task;
                if (result == null)
                {
                    continue;
                }
                yield return result;
            }
        }
        private List<Task<TextFile?>> ArrangeTasks(IEnumerable<string> textFiles)
        {
            List<Task<TextFile?>> result = [];
            foreach (var txtFile in textFiles)
            {
                Task<TextFile?> task = Task.Run(async () =>
                {
                    TextFile? file = await _data.GetFileAsync(txtFile).ConfigureAwait(false);
                    if (file == null)
                    {
                        return null;
                    }
                    file.IsPalindrome = await _validator.IsValidAsync(file.Text).ConfigureAwait(false);
                    return file;
                });
                result.Add(task);
            }
            return result;
        }
    }
}
