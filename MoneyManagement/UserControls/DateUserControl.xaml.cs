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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MoneyManagement.UserControls
{
    /// <summary>
    /// Interaction logic for DateUserControl.xaml
    /// </summary>
    public partial class DateUserControl : UserControl
    {
        public DateUserControl()
        {
            InitializeComponent();
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(DateUserControl));

        public DateTime DateValue
        {
            get { return (DateTime)GetValue(DateValueProperty); }
            set { SetValue(DateValueProperty, value); }
        }

        public static readonly DependencyProperty DateValueProperty =
            DependencyProperty.Register("DateValue", typeof(DateTime), typeof(DateUserControl));
    }
}
