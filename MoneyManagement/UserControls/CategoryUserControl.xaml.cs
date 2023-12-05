using MoneyManagemementModel.EF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MoneyManagement.UserControls
{
    /// <summary>
    /// Interaction logic for CategoryUserControl.xaml
    /// </summary>
    public partial class CategoryUserControl : UserControl
    {
        public CategoryUserControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(CategoryUserControl));

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly DependencyProperty CategoryListUCProperty =
            DependencyProperty.Register("CategoryList_uc", typeof(ObservableCollection<Category>), typeof(CategoryUserControl));

        public ObservableCollection<Category> CategoryList_uc
        {
            get => (ObservableCollection<Category>)GetValue(CategoryListUCProperty);
            set => SetValue(CategoryListUCProperty, value);
        }

        public static readonly DependencyProperty CategorySelectedItemProperty =
            DependencyProperty.Register("CategorySelectedItem", typeof(Category), typeof(CategoryUserControl));

        public Category CategorySelectedItem
        {
            get => (Category)GetValue(CategorySelectedItemProperty);
            set => SetValue(CategorySelectedItemProperty, value);
        }

        // Nơi dùng userCtrl sẽ đăng ký event này
        public event RoutedEventHandler ButtonClick;

        // Khi Button.Click sảy ra, hàm dưới đây sẽ được gọi để thực thi các đăng ký event
        private void ActionExcuting(object sender, RoutedEventArgs e)
        {
            if (ButtonClick != null)
            {
                ButtonClick.Invoke(this, new RoutedEventArgs());
            }
        }

    }
}
