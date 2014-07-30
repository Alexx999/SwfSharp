using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using SwfSharp.Tags;

namespace SwfViewer.ViewModels
{
    class TagsTabViewModel : SecondaryViewModel
    {
        private static IList<SwfTag> _emptyTags = new List<SwfTag>(0);
        private static TagTypeInternal[] _enumValues;
        private RelayCommand<SwfTag> _selectionChangedCommand;
        private RelayCommand<string> _filterStringChangedCommand;
        private string _filter = "";
        private IList<SwfTag> _tags;
        private SwfTag _tag;

        public TagsTabViewModel(MainViewModel mainViewModel) : base(mainViewModel)
        {
            _selectionChangedCommand = new RelayCommand<SwfTag>(SelectionChanged);
            _filterStringChangedCommand = new RelayCommand<string>(FilterChanged);
            Tag = null;
            Tags = _emptyTags;
        }

        private void FilterChanged(string filter)
        {
            _filter = filter.ToUpperInvariant();
            FilterTags();
        }

        private void SelectionChanged(SwfTag tag)
        {
            Tag = tag;
        }

        public IList<SwfTag> Tags
        {
            get { return _tags; }
            set
            {
                _tags = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand<SwfTag> SelectionChangedCommand
        {
            get { return _selectionChangedCommand; }
        }

        public RelayCommand<string> FilterStringChangedCommand
        {
            get { return _filterStringChangedCommand; }
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

        protected override void Refresh()
        {
            base.Refresh();
            FilterTags();
        }

        private void FilterTags()
        {
            if(_swf == null) return;
            if (string.IsNullOrWhiteSpace(_filter))
            {
                Tags = _swf.Tags;
                return;
            }

            var itemsToShow = GetEnumValues().Where(e => e.Name.Contains(_filter)).ToList();
            var enumValues = itemsToShow.Select(e => e.Value).ToList();
            Tags = _swf.Tags.Where(tag => enumValues.Contains(tag.TagType)).ToList();
        }

        private TagTypeInternal[] GetEnumValues()
        {
            if (_enumValues == null)
            {
                var names = Enum.GetNames(typeof(TagType));
                var values = Enum.GetValues(typeof (TagType));
                _enumValues = new TagTypeInternal[names.Length];
                for (int i = 0; i < names.Length; i++)
                {
                    _enumValues[i] = new TagTypeInternal((TagType)values.GetValue(i), names[i].ToUpperInvariant());
                }
            }
            return _enumValues;
        }

        private class TagTypeInternal
        {
            public TagType Value;
            public string Name;

            public TagTypeInternal(TagType value, string name)
            {
                Value = value;
                Name = name;
            }
        }
    }
}
