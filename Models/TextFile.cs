using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSide.Models
{
    internal class TextFile(string fileName, string text)
    {
        public string FileName { get; } = fileName;
        public string Text { get; } = text;
        public bool IsPalindrome { get; set; } = false;
    }
}
