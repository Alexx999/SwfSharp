using System.Windows;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using SwfSharp;
using SwfViewer.Windows;

namespace SwfViewer.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        private const int GeneralTabIndex = 0;
        private const int TagsTabIndex = 1;
        private readonly RelayCommand _openFileCommand;
        private readonly RelayCommand _saveFileCommand;
        private readonly RelayCommand _quitCommand;
        private readonly RelayCommand _helpCommand;
        private readonly RelayCommand _aboutCommand;
        private GeneralTabViewModel _generalTabTabViewModel;
        private TagsTabViewModel _tagsTabViewModel;
        private SwfFile _swf;
        private int _currentTab;

        public SwfFile Swf
        {
            get { return _swf; }
            set
            {
                _swf = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand SaveFileCommand
        {
            get { return _saveFileCommand; }
        }

        public RelayCommand OpenFileCommand
        {
            get { return _openFileCommand; }
        }

        public RelayCommand QuitCommand
        {
            get { return _quitCommand; }
        }

        public RelayCommand HelpCommand
        {
            get { return _helpCommand; }
        }

        public RelayCommand AboutCommand
        {
            get { return _aboutCommand; }
        }

        public GeneralTabViewModel GeneralTabTabViewModel
        {
            get { return _generalTabTabViewModel; }
        }

        public TagsTabViewModel TagsTabViewModel
        {
            get { return _tagsTabViewModel; }
        }

        public int CurrentTab
        {
            get { return _currentTab; }
            set
            {
                _currentTab = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            _aboutCommand = new RelayCommand(About);
            _helpCommand = new RelayCommand(Help);
            _quitCommand = new RelayCommand(Quit);
            _openFileCommand = new RelayCommand(OpenFile);
            _saveFileCommand = new RelayCommand(SaveFile, () => _swf != null);
            _generalTabTabViewModel = new GeneralTabViewModel(this);
            _tagsTabViewModel = new TagsTabViewModel(this);
        }

        public void ShowTagsAndFilter(string filter)
        {
            _tagsTabViewModel.Filter = filter;
            CurrentTab = TagsTabIndex;
        }

        private void SaveFile()
        {
            var dialog = new SaveFileDialog() { Filter = "Adobe Flash File|*.swf" };
            var result = dialog.ShowDialog();

            if (result == true)
            {
                var filename = dialog.FileName;
                SaveFile(filename);
            }
        }

        private void SaveFile(string path)
        {
            _swf.ToFile(path);
        }

        private void OpenFile()
        {
            var dialog = new OpenFileDialog { Filter = "Adobe Flash File|*.swf" };
            var result = dialog.ShowDialog();

            if (result == true)
            {
                var filename = dialog.FileName;
                OpenFile(filename);
            }
        }

        private void OpenFile(string path)
        {
            Swf = SwfFile.FromFile(path, true);
        }

        private void Help()
        {
            var helpWindow = new HelpWindow { Owner = Application.Current.MainWindow };
            helpWindow.ShowDialog();
        }

        private void About()
        {
            var aboutWindow = new AboutWindow { Owner = Application.Current.MainWindow };
            aboutWindow.ShowDialog();
        }

        private void Quit()
        {
            Application.Current.Shutdown();
        }
    }
}
