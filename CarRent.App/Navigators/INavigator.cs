using CarRent.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CarRent.App.Navigators
{
    public enum ViewType
    {
        USER_OFFER,
        USER_RESERVATIONS,
        USER_DATA,
    }

    public interface INavigator
    {
        ViewModelBase CurrentViewModel { get; set; }
        event Action StateChanged;
    }
}
