using ClientSide.Infrastructure.Commands;
using ClientSide.Models;
using ClientSide.ViewModels.Base;
using ClientSide.ViewModels.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClientSide.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        private readonly IPalindromeService _palindromeService;

        private string _DirPath = "path/to/files/dir";

        /// <summary>Путь к папке, где хранятся текстовые файлы</summary>
        public string DirPath
        {
            get => _DirPath;
            set => Set(ref _DirPath, value);
        }

        private string _Result = "";

        /// <summary>Результат выполнения</summary>
        public string Result
        {
            get => _Result;
            set => Set(ref _Result, value);
        }

        private int _FilesProcessed = 0;

        /// <summary>Количество обработанных файлов</summary>
        public int FilesProcessed
        {
            get => _FilesProcessed;
            set => Set(ref _FilesProcessed, value);
        }
        #region CheckPalindromeCommand
        public ICommand CheckPalindromeCommand { get; }

        private void OnCheckPalindromeCommandExecuted(object? p)
        {
            var files = _palindromeService.CheckFilesForPalindromes(_DirPath);
            foreach(var file in files)
            {
                _Result += $"\n{file.FileName}: {file.IsPalindrome}";
            }
        }
        private bool CanCheckPalindromeCommandExecute(object? p)
        {
            if (string.IsNullOrEmpty(_DirPath))
            {
                return false;
            }
            return true;
        }
        #endregion

        public MainWindowViewModel()
        {
            CheckPalindromeCommand = new LambdaCommand(OnCheckPalindromeCommandExecuted, CanCheckPalindromeCommandExecute);
        }
    }
}
