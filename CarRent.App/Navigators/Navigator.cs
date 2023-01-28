﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using CarRent.App.ViewModels;

namespace CarRent.App.Navigators
{
    public class Navigator : INavigator
    {
        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                //_currentViewModel?.Dispose();

                _currentViewModel = value;
                StateChanged?.Invoke();
            }
        }

        public event Action StateChanged;

    }
}
