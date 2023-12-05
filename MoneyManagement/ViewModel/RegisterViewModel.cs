using MoneyManagement.Common;
using MoneyManagement.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace MoneyManagement.ViewModel
{
    public class RegisterViewModel : BaseViewModel
    {
        LoginViewModel lvm = null;

        public RegisterViewModel()
        {
            Initialize();
            lvm = new LoginViewModel();
        }

        private string _username;
        private string _password;
        private string _confirmedPassword;

        public ICommand RegisterCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand ConfirmedPasswordCommand { get; set; }

        public string Username { get => _username; set { _username = value; OnPropertyChanged(); } }
        public string Password { get => _password; set { _password = value; OnPropertyChanged(); } }
        public string ConfirmedPassword { get => _confirmedPassword; set { _confirmedPassword = value; OnPropertyChanged(); } }

        #region Method
        public void Initialize()
        {
            PasswordChangedCommand = new RelayCommand<PasswordBox>(
                (p) => { return true; },
                (p) =>
                {
                    Password = p.Password;
                }
                );

            RegisterCommand = new RelayCommand<Object>(
                (p) => { return true; },
                (p) =>
                {
                    Register(Username, Password, ConfirmedPassword);
                }
                );

            ConfirmedPasswordCommand = new RelayCommand<PasswordBox>(
                (p) => true,
                (p) =>
                {
                    ConfirmedPassword = p.Password;
                }
                );
        }

        private void Register(string username, string password, string ConfirmPassword)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(ConfirmPassword))
            {

                return;
            }

            if (!password.Equals(ConfirmPassword))
            {
                
                return;
            }

            string hashedPass = BcryptAlgorithm.Instance.Encoded(password);
        }

        public void ShowNotification ()
        {

        }


        #endregion
    }
}
