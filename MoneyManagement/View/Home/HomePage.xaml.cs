using MoneyManagement.View.F_Expense;
using MoneyManagement.View.P_Expense;
using MoneyManagement.View.Search;
using MoneyManagement.View.Statistical;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Navigation;

namespace MoneyManagement.View.Home
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();

        }

        Storyboard loginStb;
        Storyboard infoStb;
        bool isShowStb = false;


        #region Xử lý hiệu ứng 
        public void MouseMoveEvent(object sender, MouseEventArgs e)
        {
            DropShadowEffect effect = new DropShadowEffect()
            {
                Color = Colors.DarkGray,
                Direction = 270,
                BlurRadius = 20,
                ShadowDepth = 10
            };

            ScaleTransform scale = new ScaleTransform();
            scale.ScaleX = 1.1;
            scale.ScaleY = 1.1;

            Image img = sender as Image;
            if (img.Tag.Equals("ImgF_Expense"))
            {
                F_ExpenseBorder.Effect = effect;
                ImgF_Expense.RenderTransform = scale;
            }
            else if (img.Tag.Equals("ImgP_Expense"))
            {
                P_ExpenseBorder.Effect = effect;
                ImgP_Expense.RenderTransform = scale;
            }
            else if (img.Tag.Equals("ImgStatistical"))
            {
                StatisticalBorder.Effect = effect;
                ImgStatistical.RenderTransform = scale;
            }
            else if (img.Tag.Equals("ImgFillOrSearch"))
            {
                FillterOrSearchBorder.Effect = effect;
                ImgFillOrSearch.RenderTransform = scale;
            }

        }

        public void MouseLeaveEvent(object sender, MouseEventArgs e)
        {
            DropShadowEffect effect = new DropShadowEffect()
            {
                Color = Colors.DarkGray,
                Direction = 270,
                BlurRadius = 20,
                ShadowDepth = 5
            };

            ScaleTransform scale = new ScaleTransform();
            scale.ScaleX = 1;
            scale.ScaleY = 1;

            Image img = sender as Image;
            if (img.Tag.Equals("ImgF_Expense"))
            {
                F_ExpenseBorder.Effect = effect;
                ImgF_Expense.RenderTransform = scale;
            }
            else if (img.Tag.Equals("ImgP_Expense"))
            {
                P_ExpenseBorder.Effect = effect;
                ImgP_Expense.RenderTransform = scale;
            }
            else if (img.Tag.Equals("ImgStatistical"))
            {
                StatisticalBorder.Effect = effect;
                ImgStatistical.RenderTransform = scale;
            }
            else if (img.Tag.Equals("ImgFillOrSearch"))
            {
                FillterOrSearchBorder.Effect = effect;
                ImgFillOrSearch.RenderTransform = scale;
            }
        }

        #endregion

        private void ImgF_Expense_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new F_ExpensePage());
            //var dem = new DEmo();
            //dem.ShowDialog();
        }

        private void ImgP_Expense_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new P_ExpensePage());

        }

        private void logoutIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Window window = Window.GetWindow(this);

            if (this != null)
            {
                LoginWindow login = new LoginWindow();
                login.Show();
                window.Close();
            }
        }

        private void ImgStatistical_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new StatisticalPage());
        }

        private void ellipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            loginStb = (Storyboard)FindResource("ShowLogOut_Storyboard");
            infoStb = (Storyboard)FindResource("ShowInfor_Storyboard");

            if (!isShowStb)
            {
                loginStb.Begin();
                infoStb.Begin();
                isShowStb = true;
            }
            else
            {
                loginStb.Stop();
                infoStb.Stop();
                isShowStb = false;
            }
        }

        private void ImgFillOrSearch_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new SearchPage());
        }
    }
}
