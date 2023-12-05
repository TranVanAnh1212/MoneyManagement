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
    /// Interaction logic for PriceUserControl.xaml
    /// </summary>
    public partial class PriceUserControl : UserControl
    {
        public PriceUserControl()
        {
            InitializeComponent();
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(PriceUserControl));

        public string PriceValue
        {
            get { return (string)GetValue(PriceValueProperty); }
            set { SetValue(PriceValueProperty, value); }
        }

        public static readonly DependencyProperty PriceValueProperty =
            DependencyProperty.Register("PriceValue", typeof(string), typeof(PriceUserControl));

        private void txt_Price_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;

            if (tb.Text.Length > 0)
            {
                double value = 0;
                double.TryParse(txt_Price.Text, out value);
                tb.Text = value.ToString("N0");
                tb.CaretIndex = tb.Text.Length;
            }
        }
    }
}
