﻿using System;
using SwfSharp;

namespace SwfViewer.ViewModels
{
    internal abstract class SecondaryViewModel : BaseViewModel
    {
        protected SwfFile _swf;
        protected MainViewModel MainViewModel;

        protected SecondaryViewModel(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;
            mainViewModel.PropertyChanged +=
                (sender, args) => { if (args.PropertyName == "Swf") Swf = mainViewModel.Swf; };
        }

        private SwfFile Swf
        {
            get { return _swf; }
            set
            {
                _swf = value;
                Refresh();
                OnPropertyChanged("");
            }
        }

        protected void DoIfNotNull(object obj, Action action)
        {
            if (obj != null)
            {
                action.Invoke();
            }
        }

        protected virtual void Refresh()
        {
            
        }
    }
}
