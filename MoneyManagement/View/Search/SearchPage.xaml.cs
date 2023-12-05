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
using MoneyManagemementModel;
using MoneyManagemementModel.DAO;
using MoneyManagemementModel.EF;

namespace MoneyManagement.View.Search
{
    public partial class SearchPage : Page
    {

        public SearchPage()
        {
            InitializeComponent();
            this.DataContext = this;

            List<MoneyManagemementModel.EF.Category> list = 
                new List<MoneyManagemementModel.EF.Category>(new CategoryDAO().GetListCategory());


        }

        
    }
}
