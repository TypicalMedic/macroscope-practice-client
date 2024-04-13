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

        private string _DirPath = "D:\\input";

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
            _Result = "";
            _FilesProcessed = 0;
            var files = _palindromeService.CheckFilesForPalindromes(_DirPath);
            foreach(var file in files)
            {
                Result += $"{file.FileName}: {file.IsPalindrome}\n";
                FilesProcessed ++;
            }
        }
        private bool CanCheckPalindromeCommandExecute(object? p) => !string.IsNullOrEmpty(_DirPath);
        #endregion

        public MainWindowViewModel(IPalindromeService palindromeService)
        {
            _palindromeService= palindromeService;
            CheckPalindromeCommand = new LambdaCommand(OnCheckPalindromeCommandExecuted, CanCheckPalindromeCommandExecute);
        }
    }
}
