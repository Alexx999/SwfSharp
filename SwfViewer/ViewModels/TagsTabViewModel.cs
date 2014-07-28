using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using SwfSharp.Tags;

namespace SwfViewer.ViewModels
{
    class TagsTabViewModel : SecondaryViewModel
    {
        private static IList<SwfTag> _emptyTags = new List<SwfTag>(0);
        private RelayCommand<SwfTag> _selectionChangedCommand;
        private SwfTag _tag;

        public TagsTabViewModel(MainViewModel mainViewModel) : base(mainViewModel)
        {
            _selectionChangedCommand = new RelayCommand<SwfTag>(SelectionChanged);
            Tag = null;
        }

        private void SelectionChanged(SwfTag tag)
        {
            Tag = tag;
        }

        public IList<SwfTag> Tags
        {
            get { return _swf == null ? _emptyTags : _swf.Tags; ; }
        }

        public RelayCommand<SwfTag> SelectionChangedCommand
        {
            get { return _selectionChangedCommand; }
        }

        public SwfTag Tag
        {
            get { return _tag; }
            private set
            {
                _tag = value;
                OnPropertyChanged();
            }
        }
    }
}
