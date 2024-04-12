using ClientSide.Models;
using ClientSide.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSide.Services
{
    class PalindromeService(IData data, IPalindromeValidator validator)
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
    }
}
