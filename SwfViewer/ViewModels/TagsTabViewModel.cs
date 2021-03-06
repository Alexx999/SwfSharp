﻿using System;
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
        private static TagTypeInternal[] _enumValues;
        private RelayCommand<SwfTag> _selectionChangedCommand;
        private string _filter = "";
        private IList<SwfTag> _tags;
        private SwfTag _tag;

        public TagsTabViewModel(MainViewModel mainViewModel) : base(mainViewModel)
        {
            _selectionChangedCommand = new RelayCommand<SwfTag>(SelectionChanged);
        }

        public string Filter
        {
            get { return _filter; }
            set
            {
                _filter = value;
                FilterTags();
                RaisePropertyChanged();
            }
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
                RaisePropertyChanged();
            }
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
                RaisePropertyChanged();
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
            var filterVal = _filter.ToUpperInvariant();
            var itemsToShow = GetEnumValues().Where(e => e.Name.Contains(filterVal)).ToList();
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
