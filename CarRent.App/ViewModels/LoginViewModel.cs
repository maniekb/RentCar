using System;
using System.Net;
using System.Security;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using CarRent.Data.Services.Abstract;

namespace CarRent.App.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _email;
        private SecureString _password;
        private string _errorMessage;
        private bool _isViewVisible = true;

        private readonly IUserService _userService;

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

        public bool IsViewVisible
        {
            get
            {
                return _isViewVisible;
            }

            set
            {
                _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }

        //-> Commands
        public ICommand LoginCommand { get; }
        public ICommand RecoverPasswordCommand { get; }
        public ICommand ShowPasswordCommand { get; }
        public ICommand RememberPasswordCommand { get; }

        //Constructor
        public LoginViewModel(IUserService userService)
        {
            _userService = userService;
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            RecoverPasswordCommand = new ViewModelCommand(p => ExecuteRecoverPassCommand(""));
        }

        private bool CanExecuteLoginCommand(object obj)
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(Email) || Password == null || Password.Length < 3)
                validData = false;
            else
                validData = true;
            return validData;
        }

        private async void ExecuteLoginCommand(object obj)
        {
            var authenticationResult = await _userService.AuthenticateUser(new NetworkCredential(Email, Password));
            if (authenticationResult.IsAuthenticated)
            {
                if (authenticationResult.IsAdmin)
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(
                        new GenericIdentity(Email), new string[] {"admin"});
                    IsViewVisible = false;
                }
                else
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(
                        new GenericIdentity(Email), null);
                    IsViewVisible = false;
                }
            }
            else
            {
                ErrorMessage = "Invalid email or password";
            }
        }

        private void ExecuteRecoverPassCommand(string email)
        {
            throw new NotImplementedException();
        }
    }
}
