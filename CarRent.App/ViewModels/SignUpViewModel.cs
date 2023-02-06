using System;
using System.Collections.Generic;
using System.Net;
using System.Security;
using System.Text;
using System.Windows;
using System.Windows.Input;
using CarRent.Data.Services.Abstract;

namespace CarRent.App.ViewModels
{
    public class SignUpViewModel : ViewModelBase
    {
        private string _email;
        private string _name;
        private string _lastName;
        private SecureString _password;
        private SecureString _password2;
        private string _errorMessage;

        private readonly IUserService _userService;
        public ICommand SignUpCommand { get; }
        public string Email
        {
            get
            {
                return _email;
            }

            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string LastName
        {
            get
            {
                return _lastName;
            }

            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public SecureString Password
        {
            get
            {
                return _password;
            }

            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public SecureString Password2
        {
            get
            {
                return _password2;
            }

            set
            {
                _password2 = value;
                OnPropertyChanged(nameof(Password2));
            }
        }

        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }

            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }



        public SignUpViewModel(IUserService userService)
        {
            _userService = userService;
            SignUpCommand = new ViewModelCommand(ExecuteSignUpCommand, CanExecuteSignUpCommand);

        }

        private bool CanExecuteSignUpCommand(object obj)
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(Email) || Password == null || Password.Length < 3 || string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(LastName))
                validData = false;
            else
                validData = true;
            return validData;
        }


        private async void ExecuteSignUpCommand(object obj)
        {
            _userService.AddUser(new NetworkCredential(Email, Password), Name, LastName);
            MessageBox.Show($"Zarejestrowano uzytkownika {Name}");
        }
    }
}
