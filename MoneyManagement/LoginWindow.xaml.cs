using MoneyManagemementModel.DAO;
using MoneyManagement.Controller;
using MoneyManagement.View.Login;
using MoneyManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MoneyManagement
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window, INotifyPropertyChanged
    {
        public LoginWindow()
        {
            InitializeComponent();
            //this.DataContext = this;
            //Username = "TranVanAnh";
            //Password = "1234";

            Messenger.Instance.ShowSuccessMessageRequested += async (sender, e) =>
            {
                ////Truyền tham số
                //if (e is CustomEventArgs customArgs)
                //{
                //    string message = customArgs.Message;
                //    // Kích hoạt storyboard ở đây
                //    Storyboard storyboard = FindResource("LoginFailed_Storyboard") as Storyboard;
                //    if (storyboard != null)
                //    {
                //        storyboard.Begin();
                //    }
                //}


                //// Không tuyền tham số
                Storyboard storyboard = FindResource("LoginFailed_Storyboard") as Storyboard;
                if (storyboard != null)
                {
                    storyboard.Begin();
                    await Task.Delay(TimeSpan.FromSeconds(3));
                    storyboard.Stop();
                }
            };

            
        }

        #region Định nghía các thuộc tính sử dụng để binding dữ liệu -- Khong dùng nữa do binding ở tầng ViewModel
        private string _username;
        private string _password;
        private string _message;


        public string Username { get => _username; set { _username = value; OnPropertyChanged("Username"); } }
        public string Password { get => _password; set { _password = value; OnPropertyChanged(); } }
        public string Message { get => _message; set { _message = value; OnPropertyChanged(); } }

        private void btn_Login_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        private async void Login ()
        {
            try
            {
                if (Username == "" || Password == "")
                {
                    Message = "Nhập đầy đủ trường username và password!";
                    Storyboard loginFailedStoryboard = FindResource("LoginFailed_Storyboard") as Storyboard;
                    if (loginFailedStoryboard != null)
                    {
                        loginFailedStoryboard.Begin();
                        await Task.Delay(TimeSpan.FromSeconds(2));
                        loginFailedStoryboard.Stop();
                    }
                }
                else
                {
                    var result = new AccountDAO().GetAccountByUsername(Username, Password);
                    if (result > 0)
                    {
                        Storyboard loginStoryboard = FindResource("Login_Storyboard") as Storyboard;
                        if (loginStoryboard != null)
                        {
                            loginStoryboard.Begin();
                        }

                        await Task.Delay(TimeSpan.FromSeconds(5));

                        MainWindow main = new MainWindow();
                        this.Close();
                        main.ShowDialog();
                    }
                    else
                    {
                        Message = "Sai tên tài khoản hoặc mật khẩu!";
                        Storyboard loginFailedStoryboard = FindResource("LoginFailed_Storyboard") as Storyboard;
                        if (loginFailedStoryboard != null)
                        {
                            loginFailedStoryboard.Begin();
                            await Task.Delay(TimeSpan.FromSeconds(2));
                            loginFailedStoryboard.Stop();
                        }
                    }
                }
            } catch (Exception)
            {
                Message = "Đăng nhập thất bại, có lỗi phát sinh!";
                Storyboard loginFailedStoryboard = FindResource("LoginFailed_Storyboard") as Storyboard;
                if (loginFailedStoryboard != null)
                {
                    loginFailedStoryboard.Begin();
                    await Task.Delay(TimeSpan.FromSeconds(2));
                    loginFailedStoryboard.Stop();
                }
            }
        }        

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void loginWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoginFrame.Navigate(new LoginPage());
        }
        #endregion

    }
}
