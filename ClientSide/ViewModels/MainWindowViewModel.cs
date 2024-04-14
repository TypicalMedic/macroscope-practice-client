using ClientSide.Infrastructure.Commands;
using ClientSide.Services.Interfaces;
using ClientSide.ViewModels.Base;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace ClientSide.ViewModels
{

    enum Status
    {
        notstarted, inprogress, done
    }
    class MainWindowViewModel : ViewModel
    {
        private static readonly Dictionary<Status, string> StringStatus = new()
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
                MessageBox.Show("Указанный путь не существует!", "Ошибка");
                return;
            }
            CurrentStatus = Status.inprogress;
            DateTime executionStart = DateTime.Now;
            Result += $"Запуск...\n\n";
            try
            {
                var files = _palindromeService.CheckFilesForPalindromesAsync(_DirPath).ConfigureAwait(false);
                await foreach (var file in files)
                {
                    Result += $"{file.FileName}: {(file.IsPalindrome ? "Палиндром" : "Не палиндром")}\n";
                    FilesProcessed++;
                }
                Result += $"\nОбработаны все файлы.";
            }
            catch (Exception e)
            {
                MessageBox.Show($"Ошибка: {e.Message}. При повторении обратитесь в поддержку.");
            }
            CurrentStatus = Status.done;
            Result += $"\nВремя выполнения: {DateTime.Now - executionStart:hh\\:mm\\:ss}";
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
