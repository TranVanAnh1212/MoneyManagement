using MoneyManagemementModel.DAO;
using MoneyManagemementModel.EF;
using MoneyManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MoneyManagement.Model
{
    public class F_ExpenseModel : BaseViewModel
    {
        Money_ManagementEntities db = MoneyManagemementModel.DataProvider.Instance.DB;
        public F_ExpenseModel()
        {
            CategoryList = new ObservableCollection<Category>(new CategoryDAO().GetListCategory());
            Date = DateTime.Today;
            Img = "/Data/Images/Img_empty.png";
            TotalPage = 1;
            Page = 1;
            PageSize = 8;

            NextPageCommand = new RelayCommand<Object>(
                (p) =>
                {
                    if (Page >= TotalPage) return false;

                    return true;
                },
                (p) =>
                {
                    Page += 1;
                }

                );

            PrevPageCommand = new RelayCommand<Object>(
                (p) =>
                {
                    if (Page <= 1) return false;

                    return true;
                },
                (p) =>
                {
                    Page -= 1;
                }
                );

            CancelCommand = new RelayCommand<Object>(
                (p) =>
                {
                    if (Name != "" || Price != 0 || Quantity != 0 || Date != null || Selectedcategory != null || Note != "" || Img != "")
                        return true;

                    return false;
                },
                (p) =>
                {
                    Name = Note  = "";
                    Selectedcategory = null;
                    Date = DateTime.Today;
                    Quantity = Price = 0;
                    Img = "/Data/Images/Img_empty.png";

                }
                );

            //GetListExpense();
        }

        private void GetListExpense()
        {
            var query = from fe in db.F_Expense
                        select new F_ExpenseModel()
                        {
                            Name = fe.Name,
                            Date = fe.Date,
                            Price = (int)fe.Price,
                            Quantity = fe.Quantity.Value,
                            CategID = fe.CategoryID.Value,
                            Note = fe.Note,
                            Img = fe.Img,
                        };

            ListExpense = new ObservableCollection<F_ExpenseModel>(query.OrderByDescending(x => x.Date));
        }

        #region Initiallize
        private string _name;
        private DateTime _date;
        private int _price;
        private int _quantity;
        private long _categID;
        private string _note;
        private string _img;
        private Category _selectedcategory;
        private F_ExpenseModel _selectedItem;
        private ObservableCollection<Category> _categoryList;
        private ObservableCollection<F_ExpenseModel> _listExpense;
        private int _totalPage;
        private int _page;
        private int _totalRecord;
        private int _pageSize;
        public ICommand NextPageCommand { get; set; }
        public ICommand PrevPageCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }
        public DateTime Date { get => _date; set { _date = value; OnPropertyChanged(); } }
        public int Price { get => _price; set { _price = value; OnPropertyChanged(); } }
        public int Quantity { get => _quantity; set { _quantity = value; OnPropertyChanged(); } }
        public long CategID { get => _categID; set { _categID = value; OnPropertyChanged(); } }
        public string Note { get => _note; set { _note = value; OnPropertyChanged(); } }
        public string Img { get => _img; set { _img = value; OnPropertyChanged(); } }
        public Category Selectedcategory { get => _selectedcategory; set { _selectedcategory = value; OnPropertyChanged(); } }
        public F_ExpenseModel SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged();

                if (SelectedItem != null)
                {
                    Name = SelectedItem.Name;
                    Date = SelectedItem.Date;
                    Price = SelectedItem.Price;
                    Quantity = SelectedItem.Quantity;
                    Selectedcategory = CategoryList.FirstOrDefault(x => x.ID == SelectedItem.CategID);
                    Note = SelectedItem.Note;
                    Img = SelectedItem.Img;
                };
            }
        }
        public ObservableCollection<F_ExpenseModel> ListExpense { get => _listExpense; set { _listExpense = value; OnPropertyChanged(); } }
        public ObservableCollection<Category> CategoryList { get => _categoryList; set { _categoryList = value; OnPropertyChanged(); } }

        public int TotalPage { get => _totalPage; set { _totalPage = value; OnPropertyChanged(); } }

        public int Page { get => _page; set { _page = value; OnPropertyChanged(); } }
        public int TotalRecord { get => _totalRecord; set { _totalRecord = value; OnPropertyChanged(); } }
        public int PageSize { get => _pageSize; set { _pageSize = value; OnPropertyChanged(); } }

        #endregion

        //public event PropertyChangedEventHandler PropertyChanged;
        //protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
    }
}
