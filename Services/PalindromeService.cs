using ClientSide.Data.Interfaces;
using ClientSide.Models;
using ClientSide.PalindromeValidator.Interfaces;
using ClientSide.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSide.Services
{
    class PalindromeService(IData data, IPalindromeValidator validator) : IPalindromeService
    {
        private readonly IData _data = data;
        private readonly IPalindromeValidator _validator = validator;
        public IEnumerable<TextFile> CheckFilesForPalindromes(string dirName)
        {
            var textFiles = _data.GetDirFileNames(dirName);
            foreach (var txtFile in textFiles)
            {
                TextFile file = _data.GetFile(txtFile);
                bool isPalindrome = _validator.IsValid(file.Text);
                file.IsPalindrome = isPalindrome;
                yield return file;
            }
        }
        public async IAsyncEnumerable<TextFile> CheckFilesForPalindromesAsync(string dirName)
        {
            var textFiles = _data.GetDirFileNames(dirName);
            var tasks = ArrangeTasks(textFiles);

            foreach (Task<TextFile> task in tasks)
            {
                yield return await task;
            }
        }
        private List<Task<TextFile>> ArrangeTasks(IEnumerable<string> textFiles)
        {
            List<Task<TextFile>> result = new();
            foreach (var txtFile in textFiles)
            {
                result.Add(Task.Run(async () =>
                {
                    TextFile file = await _data.GetFileAsync(txtFile).ConfigureAwait(false);
                    file.IsPalindrome = await _validator.IsValidAsync(file.Text).ConfigureAwait(false);
                    return file;
                }));
            }
            return result;
        }
    }
}
