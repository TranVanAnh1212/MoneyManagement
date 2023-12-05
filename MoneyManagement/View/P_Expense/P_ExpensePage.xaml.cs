using Microsoft.Win32;
using MoneyManagement.Controller;
using MoneyManagement.View.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MoneyManagement.View.P_Expense
{
    /// <summary>
    /// Interaction logic for P_ExpensePage.xaml
    /// </summary>
    public partial class P_ExpensePage : Page
    {
        public P_ExpensePage()
        {
            InitializeComponent();

            /// Bắt sự kiện khi nhấn nút thêm sửa xóa thì hiển thị ra thông báo tương ứng
            Messenger.Instance.ShowSuccessMessageRequested += async (sender, e) =>
            {
                Storyboard storyboard = FindResource("Success_Storyboard") as Storyboard;

                if (storyboard != null)
                {
                    storyboard.Begin();
                    await Task.Delay(TimeSpan.FromSeconds(3));
                    storyboard.Stop();
                }
            };
        }

        private void chooseImg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string fileName = "";
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                fileName = ofd.FileName;
                chooseImg.Source = new BitmapImage(new Uri(fileName));
            }

        }

        private void addNewCategory_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new CategoryPage());
        }
    }
}
