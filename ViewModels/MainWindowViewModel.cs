using ClientSide.Infrastructure.Commands;
using ClientSide.Models;
using ClientSide.Services.Interfaces;
using ClientSide.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClientSide.ViewModels
{

    enum Status
    {
        notstarted, inprogress, done
    }
    class MainWindowViewModel : ViewModel
    {
        private static Dictionary<Status, string> StringStatus = new Dictionary<Status, string>()
        {
            { Status.notstarted, "готово к работе" },
            { Status.inprogress, "выполнение..." },
            { Status.done, "завершено" },
        };

        private Status _CurrentStatus;

        private Status CurrentStatus
        {
            get => _CurrentStatus;
            set
            {
                _CurrentStatus = value;
                StatusText = StringStatus[value];
            }
        }
        private readonly IPalindromeService _palindromeService;

        private string _DirPath = Environment.CurrentDirectory;

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

        private string _StatusText = "готов к работе";

        /// <summary>Статус выполнения</summary>
        public string StatusText
        {
            get => _StatusText;
            set => Set(ref _StatusText, value);
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

        private async void OnCheckPalindromeCommandExecuted(object? p)
        {

            Result = "";
            FilesProcessed = 0;
            if (!Directory.Exists(DirPath))
            {
                Result += $"Указанный путь не существует!";
                return;
            }
            CurrentStatus = Status.inprogress;
            Result += $"Запуск...\n\n";
            var files = _palindromeService.CheckFilesForPalindromesAsync(_DirPath).ConfigureAwait(false);
            await foreach (var file in files)
            {
                Result += $"{file.FileName}: {(file.IsPalindrome ? "Палиндром" : "Не палиндром")}\n";
                FilesProcessed++;
            }
            Result += $"\nОбработаны все файлы.";
            CurrentStatus = Status.done;
        }
        private bool CanCheckPalindromeCommandExecute(object? p) => !string.IsNullOrEmpty(_DirPath) && CurrentStatus != Status.inprogress;
        #endregion

        public MainWindowViewModel(IPalindromeService palindromeService)
        {
            CurrentStatus = Status.notstarted;
            _palindromeService = palindromeService;
            CheckPalindromeCommand = new LambdaCommand(OnCheckPalindromeCommandExecuted, CanCheckPalindromeCommandExecute);
        }
    }
}
