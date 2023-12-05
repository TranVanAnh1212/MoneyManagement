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
    /// Interaction logic for QuantityUserControl.xaml
    /// </summary>
    public partial class QuantityUserControl : UserControl
    {
        public QuantityUserControl()
        {
            InitializeComponent();
        }        

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(QuantityUserControl));

        public int QuantityValue
        {
            get { return (int)GetValue(QuantityValueProperty); }
            set { SetValue(QuantityValueProperty, value); }
        }

        public static readonly DependencyProperty QuantityValueProperty =
            DependencyProperty.Register("QuantityValue", typeof(int), typeof(QuantityUserControl));

        public ICommand ReduceQuantityCommand_uc
        {
            get => (ICommand)GetValue(ReduceQuantityCommandProperty);
            set => SetValue(ReduceQuantityCommandProperty, value);
        }

        public static readonly DependencyProperty ReduceQuantityCommandProperty =
            DependencyProperty.Register("ReduceQuantityCommand_uc", typeof(ICommand), typeof(QuantityUserControl));

        public ICommand IncreaseQuantityCommand_uc
        {
            get => (ICommand)GetValue(IncreaseQuantityCommandProperty);
            set => SetValue(IncreaseQuantityCommandProperty, value);
        }

        public static readonly DependencyProperty IncreaseQuantityCommandProperty =
            DependencyProperty.Register("IncreaseQuantityCommand_uc", typeof(ICommand), typeof(QuantityUserControl));
     }
}
