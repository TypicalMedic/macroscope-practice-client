namespace ClientSide.Models
{
    public class TextFile
    {
        public string FileName { get; }
        public string Text { get; }
        public bool IsPalindrome { get; set; } = false;

        public TextFile(string fileName, string text)
        {
            FileName = fileName;
            Text = text;
        }
        public TextFile(string fileName, string text, bool isPalindrome)
        {
            FileName = fileName;
            Text = text;
            IsPalindrome = isPalindrome;
        }
        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }
            TextFile? f = obj as TextFile;
            return obj != null
                && f?.IsPalindrome == IsPalindrome
                && f.Text == Text
                && f.FileName == FileName;
        }
    }

}
