namespace ClientSide.Models
{
    internal class TextFile(string fileName, string text)
    {
        public string FileName { get; } = fileName;
        public string Text { get; } = text;
        public bool IsPalindrome { get; set; } = false;
    }
}
