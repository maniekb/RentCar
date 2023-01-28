using CarRent.App.Navigators;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRent.App.ViewModels.Factories
{
    public interface IRentAppViewModelFactory
    {
        ViewModelBase CreateViewModel(ViewType viewType);
    }
}
