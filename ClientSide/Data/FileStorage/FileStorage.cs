using ClientSide.Data.Interfaces;
using ClientSide.Models;
using System.IO;
using System.Windows;

namespace ClientSide.Data.FileStorage
{
    public partial class FileStorage : IData
    {
        public IEnumerable<string> GetDirFileNames(string directoryName)
        {
            if (Directory.Exists(directoryName))
            {
                var files = Directory.EnumerateFiles(directoryName, "*.txt", SearchOption.TopDirectoryOnly);
                foreach (var file in files)
                {
                    yield return file;
                }
            }
        }

        public async IAsyncEnumerable<string> GetDirFileNamesAsync(string directoryName)
        {
            if (Directory.Exists(directoryName))
            {
                var files = await Task.Run(() => Directory.EnumerateFiles(directoryName, "*.txt", SearchOption.TopDirectoryOnly)).ConfigureAwait(false);
                foreach (var file in files)
                {
                    yield return file;
                }
            }
        }

        public TextFile? GetFile(string fileName)
        {
            try
            {
                return new TextFile(fileName, File.ReadAllText(fileName));
            }
            catch (FileNotFoundException)
            {
                return null;
            }
            catch (Exception e)
            {
                MessageBox.Show($"Непредвиденная ошибка: {e.Message}. Обратитесь в поддержку.");
                Environment.Exit(1);
            }
            return null;
        }
        public async Task<TextFile?> GetFileAsync(string fileName)
        {
            try
            {
                return new TextFile(fileName, await File.ReadAllTextAsync(fileName).ConfigureAwait(false));
            }
            catch (FileNotFoundException)
            {
                return null;
            }
            catch (Exception e)
            {
                MessageBox.Show($"Непредвиденная ошибка: {e.Message}. Обратитесь в поддержку.");
                Environment.Exit(1);
            }
            return null;
        }
    }
}
