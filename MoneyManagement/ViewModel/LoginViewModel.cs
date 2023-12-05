using MoneyManagemementModel.DAO;
using MoneyManagement.Controller;
using MoneyManagement.View.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Navigation;

namespace MoneyManagement.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel()
        {
            IsLogin = false;
            Username = "TranVanAnh";
            Password = "1234";

            LoginCommand = new RelayCommand<Object>(
                (p) => { return true; },
                (p) =>
                {
                    var window = Application.Current.MainWindow;

                    if (window != null)
                    {
                        LogIn(window);
                    }
                }
                );

            CloseWindowCommand = new RelayCommand<Object>(
                (p) => { return true; },
                (p) =>
                {
                    var window = Application.Current.MainWindow;
                    if (window != null)
                    {
                        window.Close();
                    }
                }
                );

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
                    //NavigationService.Navigate(new RegisterPage());
                }
                );
        }

        #region initialize
        private string username;
        private string password;
        private string _message;
        private bool isLogin;

        public ICommand LoginCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand CloseWindowCommand { get; set; }
        public ICommand RegisterCommand { get; set; }

        public string Username { get => username; set { username = value; OnPropertyChanged(); } }
        public string Password { get => password; set { password = value; OnPropertyChanged(); } }
        public bool IsLogin { get => isLogin; set { isLogin = value; OnPropertyChanged(); } }
        public string Message { get => _message; set { _message = value; OnPropertyChanged(); } }

        #endregion

        #region Method

        private async void LogIn(Window p)
        {
            try
            {
                if (p == null)
                {
                    Message = "Không thể đăng nhập, có lỗi xảy ra!";
                    Messenger.Instance.RequestShowSuccessMessage();
                    return;
                }

                if (Username == "" || Password == "")
                {
                    Message = "Tên tài khoản và mật khẩu không được để trống!";
                    Messenger.Instance.RequestShowSuccessMessage();
                    return;
                }

                var result = new AccountDAO().GetAccountByUsername(Username, Password);
                if (result > 0)
                {
                    IsLogin = true;
                    Intro.Instance.ShowIntro();
                    await Task.Delay(TimeSpan.FromSeconds(5));
                    p.Hide();
                    MainWindow main = new MainWindow();
                    main.Show();
                }
                else
                {
                    Message = "Sai tên tài khoản và mật khẩu!";
                    Messenger.Instance.RequestShowSuccessMessage();
                }
            }
            catch (Exception)
            {
                Message = "Không thể đăng nhập, có lỗi xảy ra!";
                Messenger.Instance.RequestShowSuccessMessage();
            }
        }

        public FrameworkElement GetWindowParent(FrameworkElement fe)
        {
            FrameworkElement parent = fe;

            while (parent.Parent != null)
            {
                parent = parent.Parent as FrameworkElement;

            }

            return parent;
        }

        #endregion
    }
}
