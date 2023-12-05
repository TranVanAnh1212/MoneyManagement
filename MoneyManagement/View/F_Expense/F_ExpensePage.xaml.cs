using Microsoft.Win32;
using MoneyManagemementModel;
using MoneyManagemementModel.EF;
using MoneyManagement.Controller;
using MoneyManagement.View.Category;
using MoneyManagement.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace MoneyManagement.View.F_Expense
{
    /// <summary>
    /// Interaction logic for F_ExpensePage.xaml
    /// </summary>
    public partial class F_ExpensePage : Page
    {
        F_ExpenseViewModel fViewModel = null;
        Money_ManagementEntities db = DataProvider.Instance.DB;

        public F_ExpensePage()
        {

            InitializeComponent();
            fViewModel = new F_ExpenseViewModel();
            this.DataContext = fViewModel;

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

        #region Method
        // --------------
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void chooseImg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string fileName = "";
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files (*.jpg; *.png; *.bmp)|*.jpg; *.png; *.bmp|All Files (*.*)|*.*";
            if (ofd.ShowDialog() == true)
            {
                fileName = ofd.FileName;
                //chooseImg.Source = new BitmapImage(new Uri(fileName));
                fViewModel.Img = fileName;
            }
        }

        private void closeMessageSuccess_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Storyboard storyboard = FindResource("Success_Storyboard") as Storyboard;

            if (storyboard != null)
            {
                storyboard.Stop();
            }
        }

        //public void GetListExpense()
        //{
        //    var query = from fe in db.F_Expense
        //                where fe.Date.Month == DateTime.Now.Month && fe.Date.Year == DateTime.Now.Year
        //                select new F_ExpenseViewModel()
        //                {
        //                    Id = fe.ID,
        //                    Name = fe.Name,
        //                    Date = fe.Date,
        //                    Price = (int) fe.Price,
        //                    Quantity = fe.Quantity.Value,
        //                    CategID = fe.CategoryID.Value,
        //                    CategName = fe.Category.Name,
        //                    Note = fe.Note,
        //                    Img = fe.Img,
        //                };

        //    fViewModel.TotalRecord = query.Count();
        //    fViewModel.TotalPage = (int)Math.Ceiling((Double)fViewModel.TotalRecord / (Double)fViewModel.PageSize);
        //    fViewModel.ListExpense = new ObservableCollection<F_ExpenseViewModel>(query.OrderByDescending(x => x.Date).Skip((fViewModel.Page - 1) * fViewModel.PageSize).Take(fViewModel.PageSize));
        //    lv_Expense.ItemsSource = fViewModel.ListExpense;

        //    //// Grouping listview wpf
        //    CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lv_Expense.ItemsSource);
        //    PropertyGroupDescription groupDescription = new PropertyGroupDescription("Date");
        //    view.GroupDescriptions.Add(groupDescription);
        //}
        #endregion

        private void addNewCategory_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new CategoryPage());
        }
    }
}
