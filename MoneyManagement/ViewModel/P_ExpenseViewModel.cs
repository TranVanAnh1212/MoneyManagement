using MoneyManagemementModel;
using MoneyManagemementModel.DAO;
using MoneyManagemementModel.EF;
using MoneyManagement.Controller;
using MoneyManagement.Model;
using MoneyManagement.View.Customs;
using MoneyManagement.View.P_Expense;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace MoneyManagement.ViewModel
{
    public class P_ExpenseViewModel : BaseViewModel
    {
        public P_ExpenseViewModel()
        {
            InitializedMethod();
        }

        #region Initialize
        private string _name;
        private DateTime _date;
        private string _price;
        private int _quantity;
        private long _categID;
        private string _categName;
        private string _note;
        private string _img;
        private Category _selectedcategory;
        private P_ExpenseViewModel _selectedItem;
        private ObservableCollection<Category> _categoryList;
        private ObservableCollection<P_ExpenseViewModel> _listExpense;
        private int _totalPage;
        private int _page;
        private int _totalRecord;
        private int _pageSize;
        private int _totalAmount;
        private string _message;
        private string _messageFill;
        private string _messageimgSource;
        private long _id;
        private List<Month> _listMonth;
        private Month _selectedMonth;
        private List<Sorted> _sortedBy;
        private Sorted _selectedSorted;
        private int _sortBy;
        private string _searchStr;
        public ICommand NextPageCommand { get; set; }
        public ICommand PrevPageCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand AddNewRecordCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand LoadWindowCommand { get; set; }
        public ICommand RefeshCommand { get; set; }
        public ICommand ReduceQuantityCommand { get; set; }
        public ICommand IncreaseQuantityCommand { get; set; }

        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }
        public DateTime Date { get => _date; set { _date = value; OnPropertyChanged(); } }
        public string Price { get => _price; set { _price = value; OnPropertyChanged(); } }
        public int Quantity { get => _quantity; set { _quantity = value; OnPropertyChanged(); } }
        public long CategID { get => _categID; set { _categID = value; OnPropertyChanged(); } }
        public string Note { get => _note; set { _note = value; OnPropertyChanged(); } }
        public string Img { get => _img; set { _img = value; OnPropertyChanged(); } }
        public Category Selectedcategory { get => _selectedcategory; set { _selectedcategory = value; OnPropertyChanged(); } }
        public P_ExpenseViewModel SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged();

                if (SelectedItem != null)
                {
                    Id = SelectedItem.Id;
                    Name = SelectedItem.Name;
                    Date = SelectedItem.Date;
                    Price = SelectedItem.Price;
                    Quantity = SelectedItem.Quantity;
                    Selectedcategory = CategoryList.FirstOrDefault(x => x.ID == SelectedItem.CategID);
                    Note = SelectedItem.Note;
                    CategID = SelectedItem.CategID;
                    Img = (SelectedItem.Img == null || SelectedItem.Img == "") ? "/Data/Images/Img_empty.png" : SelectedItem.Img;
                };
            }
        }
        public ObservableCollection<P_ExpenseViewModel> ListExpense { get => _listExpense; set { _listExpense = value; OnPropertyChanged(); } }
        public ObservableCollection<Category> CategoryList { get => _categoryList; set { _categoryList = value; OnPropertyChanged(); } }
        public int TotalPage { get => _totalPage; set { _totalPage = value; OnPropertyChanged(); } }
        public int Page { get => _page; set { _page = value; OnPropertyChanged(); } }
        public int TotalRecord { get => _totalRecord; set { _totalRecord = value; OnPropertyChanged(); } }
        public int PageSize { get => _pageSize; set { _pageSize = value; OnPropertyChanged(); } }
        public int TotalAmount { get => _totalAmount; set { _totalAmount = value; OnPropertyChanged(); } }
        public string Message { get => _message; set { _message = value; OnPropertyChanged(); } }
        public string CategName { get => _categName; set { _categName = value; OnPropertyChanged(); } }
        public long Id { get => _id; set => _id = value; }
        public string MessageFill { get => _messageFill; set { _messageFill = value; OnPropertyChanged(); } }
        public List<Month> ListMonth { get => _listMonth; set { _listMonth = value; OnPropertyChanged(); } }
        public Month SelectedMonth { get => _selectedMonth; set { _selectedMonth = value; OnPropertyChanged(); } }
        public string SearchStr { get => _searchStr; set { _searchStr = value; OnPropertyChanged(); } }

        public List<Sorted> SortedBy { get => _sortedBy; set { _sortedBy = value; OnPropertyChanged(); } }
        public Sorted SelectedSorted
        {
            get => _selectedSorted; set
            {
                _selectedSorted = value;
                OnPropertyChanged();
                SortBy = SelectedSorted.ID;

                GetListSorted(SortBy);
            }
        }

        public int SortBy { get => _sortBy; set { _sortBy = value; OnPropertyChanged(); } }

        public string MessageimgSource { get => _messageimgSource; set { _messageimgSource = value; OnPropertyChanged(); } }
        #endregion

        #region Method

        private void InitializedMethod()
        {
            GetListCategory();

            Date = DateTime.Today;
            Quantity = 1;
            Img = "/Data/Images/Img_empty.png";
            TotalPage = 1;
            Page = 1;
            PageSize = 8;
            GetTotalAmount();

            ListMonth = new List<Month>()
            {
                new Month() {ID = 1, Name="Tháng 1"},
                new Month() {ID = 2, Name="Tháng 2"},
                new Month() {ID = 3, Name="Tháng 3"},
                new Month() {ID = 4, Name="Tháng 4"},
                new Month() {ID = 5, Name="Tháng 5"},
                new Month() {ID = 6, Name="Tháng 6"},
                new Month() {ID = 7, Name="Tháng 7"},
                new Month() {ID = 8, Name="Tháng 8"},
                new Month() {ID = 9, Name="Tháng 9"},
                new Month() {ID = 10, Name="Tháng 10"},
                new Month() {ID = 11, Name="Tháng 11"},
                new Month() {ID = 12, Name="Tháng 12"},
                new Month() {ID = 13, Name="Tất cả"},
            };
            SelectedMonth = ListMonth.FirstOrDefault(x => x.ID == DateTime.Now.Month);

            LoadWindowCommand = new RelayCommand<Page>(
                (p) => { return true; },
                (p) =>
                {
                    GetListExpense(SelectedMonth.ID);
                }
                );

            CancelCommand = new RelayCommand<Object>(
                (p) =>
                {
                    if (Name == null || string.IsNullOrEmpty(Price) || Date == null)
                        return false;

                    return true;
                },
                (p) =>
                {
                    SetEmptyField();
                }
            );

            AddNewRecordCommand = new RelayCommand<Object>(
                (p) =>
                {
                    if ( string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Price) || Date == null || Selectedcategory == null || SelectedItem != null)
                        return false;

                    return true;
                },
                (p) =>
                {
                    AddNewExpense();
                }
            );

            DeleteCommand = new RelayCommand<Object>(
                (p) =>
                {
                    if (Name == "" || string.IsNullOrEmpty(Price) || Date == null || Selectedcategory == null || SelectedItem == null)
                        return false;

                    return true;
                },
                (p) =>
                {
                    DeleteExpense();
                }
           );

            UpdateCommand = new RelayCommand<Object>(
                (p) =>
                {
                    if (Name == "" || string.IsNullOrEmpty(Price) || Date == null || Selectedcategory == null || SelectedItem == null)
                        return false;

                    return true;
                },
                (p) =>
                {
                    UpdateExpense();
                }
            );


            SortedBy = new List<Sorted>() {
                new Sorted() { ID = 1, Name = "Date" },
                new Sorted() { ID = 2, Name = "Name" },
                new Sorted() { ID = 3, Name = "Price" },
            };
            //SelectedSorted = SortedBy.FirstOrDefault(x => x.ID == 0);

            /// Tìm kiếm
            SearchCommand = new RelayCommand<Object>(
                (p) =>
                {
                    return true;
                },
                (p) =>
                {
                    if (String.IsNullOrEmpty(SearchStr))
                    {
                        // lỗi
                        // Không Thành công thì fill màu đỏ
                        ShowNotitication("Cập nhật không thành công!", "#FFFF1C1C", "/MoneyManagement;component/Data/Images/ErrorIcon.png");

                    }
                    else
                    {
                        GetListExpenseSearch(SearchStr);
                    }
                }
            );

            RefeshCommand = new RelayCommand<Object>(
                (p) => { return true; },
                (p) =>
                {
                    Page = 1;
                    GetListExpense(SelectedMonth.ID);
                }
            );

            IncreaseQuantityCommand = new RelayCommand<Object>(
                (p) => { return true; },
                (p) =>
                {
                    Quantity += 1;
                }
            );

            ReduceQuantityCommand = new RelayCommand<Object>(
                (p) => { return true; },
                (p) =>
                {
                    if (Quantity <= 1) return;
                    else Quantity -= 1;
                }
            );

            NextPageCommand = new RelayCommand<Object>(
                (p) =>
                {
                    if (Page >= TotalPage) return false;

                    return true;
                },
                (p) =>
                {
                    if (Page >= TotalPage)
                    {
                        return;
                    }
                    else
                    {
                        Page++;
                        GetListExpense(SelectedMonth.ID);
                    }
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
                    if (Page <= 1)
                    {
                        return;
                    }
                    else
                    {
                        Page--;
                        GetListExpense(SelectedMonth.ID);
                    }
                }
            );
        }

        /// <summary>
        /// Hàm lấy danh sách danh mục chi tiêu
        /// </summary>
        private void GetListCategory()
        {
            Thread categList_Thread = new Thread(delegate ()
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    CategoryList = new ObservableCollection<Category>(new CategoryDAO().GetListCategory());
                });

            });
            categList_Thread.Start();
        }

        private void GetTotalAmount()
        {
            TotalAmount = new P_ExpenseDAO().GetTotalAmount(DateTime.Now.Month, DateTime.Now.Year);
        }


        public void GetListSorted(int id)
        {
            var db = DataProvider.Instance.DB;
            var query = from pe in db.P_Expense
                        where pe.Date.Month == DateTime.Now.Month && pe.Date.Year == DateTime.Now.Year
                        select new P_ExpenseViewModel()
                        {
                            Id = pe.ID,
                            Name = pe.Name,
                            Date = pe.Date,
                            Price = pe.Price.ToString(),
                            Quantity = pe.Quantity.Value,
                            CategID = pe.CategoryID.Value,
                            CategName = pe.Category.Name,
                            Note = pe.Note,
                            Img = pe.Img,
                        };

            TotalRecord = query.Count();
            TotalPage = (int)Math.Ceiling((Double)TotalRecord / (Double)PageSize);

            if (id == 2)
                ListExpense = new ObservableCollection<P_ExpenseViewModel>(query.OrderBy(x => x.Name).Skip((Page - 1) * PageSize).Take(PageSize));
            else if (id == 3)
                ListExpense = new ObservableCollection<P_ExpenseViewModel>(query.OrderBy(x => x.Price).Skip((Page - 1) * PageSize).Take(PageSize));
            else if (id == 1)
                ListExpense = new ObservableCollection<P_ExpenseViewModel>(query.OrderBy(x => x.Date).Skip((Page - 1) * PageSize).Take(PageSize));


            //// Grouping listview wpf
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ListExpense);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("Date");
            view.GroupDescriptions.Add(groupDescription);
        }


        /// <summary>
        /// Hàm lấy ra danh sách các khoản chi tiêu trong tháng
        /// </summary>
        public void GetListExpense(int month)
        {
            //// Chạy theo luồng
            Thread listExpense_Thread = new Thread(() =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    var db = DataProvider.Instance.DB;
                    var query = from pe in db.P_Expense
                                where pe.Date.Month == month && pe.Date.Year == DateTime.Now.Year
                                select new P_ExpenseViewModel()
                                {
                                    Id = pe.ID,
                                    Name = pe.Name,
                                    Date = pe.Date,
                                    Price = pe.Price.ToString(),
                                    Quantity = pe.Quantity.Value,
                                    CategID = pe.CategoryID.Value,
                                    CategName = pe.Category.Name,
                                    Note = pe.Note,
                                    Img = pe.Img == null ? "" : pe.Img,
                                };

                    if (month > 12)
                    {
                        query = from pe in db.P_Expense
                                where pe.Date.Year == DateTime.Now.Year
                                select new P_ExpenseViewModel()
                                {
                                    Id = pe.ID,
                                    Name = pe.Name,
                                    Date = pe.Date,
                                    Price = pe.Price.ToString(),
                                    Quantity = pe.Quantity.Value,
                                    CategID = pe.CategoryID.Value,
                                    CategName = pe.Category.Name,
                                    Note = pe.Note,
                                    Img = pe.Img == null ? "" : pe.Img,
                                };
                    }

                    TotalRecord = query.Count();
                    TotalPage = (int)Math.Ceiling((Double)TotalRecord / (Double)PageSize);
                    ListExpense = new ObservableCollection<P_ExpenseViewModel>(query.OrderByDescending(x => x.Date).Skip((Page - 1) * PageSize).Take(PageSize));

                    //// Grouping listview wpf
                    CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ListExpense);
                    PropertyGroupDescription groupDescription = new PropertyGroupDescription("Date");
                    view.GroupDescriptions.Add(groupDescription);
                });
            });
            listExpense_Thread.Start();
        }

        /// <summary>
        /// Hàm đưa các trường về rỗng / trạng thái khởi tạo
        /// </summary>
        private void SetEmptyField()
        {
            Name = Note = null;
            SelectedItem = null;
            Selectedcategory = null;
            Date = DateTime.Today;
            Quantity = 1;
            Price = string.Empty;
            Img = "/Data/Images/Img_empty.png";
        }

        /// <summary>
        /// Hàm thêm chi tiêu mới
        /// </summary>
        private void AddNewExpense()
        {
            try
            {
                int priceParse = 0;
                var priceStr = Price.Split('.');
                var price = string.Concat(priceStr);
                priceParse = Convert.ToInt32(price);

                var newpe = new P_Expense()
                {
                    Name = Name.Trim(),
                    Date = Date,
                    Price = priceParse,
                    Quantity = Quantity,
                    Img = (Img == "/Data/Images/Img_empty.png") ? null : Img,
                    Note = Note,
                    CategoryID = Selectedcategory.ID,

                };

                var result = new P_ExpenseDAO().AddNewExpense(newpe);

                if (result > 0)
                {
                    // add new expense to list expense
                    var pe_VM = new P_ExpenseViewModel()
                    {
                        Id = result,
                        Name = newpe.Name,
                        Date = newpe.Date,
                        Price = newpe.Price.ToString(),
                        Quantity = newpe.Quantity.Value,
                        Img = newpe.Img,
                        Note = newpe.Note,
                        CategID = newpe.Category.ID,
                        CategName = newpe.Category.Name,
                    };

                    if (ListExpense.Count() > 0 || ListExpense != null)
                        ListExpense.Insert(0, pe_VM);
                    else
                        ListExpense.Add(pe_VM);

                    // Thành công thì fill màu xanh
                    ShowNotitication("Thêm mới chi tiêu thành công!", "#FF40ED49", "/Data/Images/SuccessIcon.png");

                    GetTotalAmount();
                    // Reset fields null
                    SetEmptyField();
                }
                else
                {
                    // Không Thành công thì fill màu đỏ
                    ShowNotitication("Thêm mới chi tiêu không thành công!", "#FFFF1C1C", "/MoneyManagement;component/Data/Images/ErrorIcon.png");

                }
            }
            catch (Exception)
            {
                // Không Thành công thì fill màu đỏ
                ShowNotitication("Thêm mới chi tiêu không thành công!", "#FFFF1C1C", "/MoneyManagement;component/Data/Images/ErrorIcon.png");

            }
        }

        /// <summary>
        /// Hàm gửi sự kiện để hiển thị thông báo
        /// </summary>
        /// <param name="mess"></param>
        /// <param name="fill"></param>
        /// <param name="imgSource"></param>
        private void ShowNotitication(string mess, string fill, string imgSource)
        {
            Message = mess;
            MessageFill = fill; // Thành công thì fill màu xanh
            MessageimgSource = imgSource;
            Messenger.Instance.RequestShowSuccessMessage();
        }


        /// <summary>
        /// Hàm xóa chi tiêu
        /// </summary>
        private void DeleteExpense()
        {
            Dialog d = new Dialog();
            d.DialogMessage = "Bạn thực sự muốn xóa?";
            d.Owner = Window.GetWindow(new P_ExpensePage());
            if (true == d.ShowDialog())
            {
                try
                {
                    var fe = new P_ExpenseDAO().GetExpenseByID(SelectedItem.Id);
                    var result = new P_ExpenseDAO().Delete(fe);

                    if (result)
                    {
                        ListExpense.Remove(SelectedItem);

                        // Thành công thì fill màu xanh
                        ShowNotitication("Xóa thành công!", "#FF40ED49", "/Data/Images/SuccessIcon.png");
                        GetTotalAmount();
                        // Reset fields null
                        SetEmptyField();
                    }
                    else
                    {
                        // Không Thành công thì fill màu đỏ
                        ShowNotitication("Xóa không thành công!", "#FFFF1C1C", "/MoneyManagement;component/Data/Images/ErrorIcon.png");

                    }
                }
                catch (Exception)
                {
                    // Không Thành công thì fill màu đỏ
                    ShowNotitication("Xóa không thành công!", "#FFFF1C1C", "/MoneyManagement;component/Data/Images/ErrorIcon.png");
                }
            }
        }

        /// <summary>
        /// Hàm cập nhật lại chi tiêu
        /// </summary>
        private void UpdateExpense()
        {
            Dialog d = new Dialog();
            d.DialogMessage = "Bạn muốn cập nhật thông tin chi tiêu?";
            d.Owner = Window.GetWindow(new P_ExpensePage());
            if (true == d.ShowDialog())
            {
                try
                {
                    int priceParse = 0;
                    var priceStr = Price.Split('.');

                    var fe = new P_ExpenseDAO().GetExpenseByID(SelectedItem.Id);
                    fe.Name = Name.Trim();
                    fe.Date = Date;
                    fe.Price = priceParse;
                    fe.Quantity = Quantity;
                    fe.Note = Note;
                    fe.Img = Img;
                    fe.CategoryID = Selectedcategory.ID;

                    var result = new P_ExpenseDAO().Update(fe);

                    if (result)
                    {
                        //Update lại các thông số trong listExpense
                        SelectedItem.Name = Name.Trim();
                        SelectedItem.Date = Date;
                        SelectedItem.Price = Price;
                        SelectedItem.Quantity = Quantity;
                        SelectedItem.Note = Note;
                        SelectedItem.Img = Img;
                        SelectedItem.CategName = Selectedcategory.Name;

                        // Thành công thì fill màu xanh
                        ShowNotitication("Cập nhật thành công!", "#FF40ED49", "/Data/Images/SuccessIcon.png");
                        GetTotalAmount();
                        SetEmptyField();
                    }
                    else
                    {
                        // Không Thành công thì fill màu đỏ
                        ShowNotitication("Cập nhật không thành công!", "#FFFF1C1C", "/MoneyManagement;component/Data/Images/ErrorIcon.png");

                    }
                }
                catch (Exception)
                {
                    // Không Thành công thì fill màu đỏ
                    ShowNotitication("Cập nhật không thành công!", "#FFFF1C1C", "/MoneyManagement;component/Data/Images/ErrorIcon.png");
                }
            }
        }

        public void GetListExpenseSearch(string searchStr = null)
        {
            var db = DataProvider.Instance.DB;
            var query = from pe in db.P_Expense
                        where pe.Date.Month == DateTime.Now.Month && pe.Date.Year == DateTime.Now.Year && pe.Name.Contains(searchStr)
                        select new P_ExpenseViewModel()
                        {
                            Id = pe.ID,
                            Name = pe.Name,
                            Date = pe.Date,
                            Price = pe.Price.ToString(),
                            Quantity = pe.Quantity.Value,
                            CategID = pe.CategoryID.Value,
                            CategName = pe.Category.Name,
                            Note = pe.Note,
                            Img = pe.Img,
                        };

            TotalRecord = query.Count();
            TotalPage = (int)Math.Ceiling((Double)TotalRecord / (Double)PageSize);
            ListExpense = new ObservableCollection<P_ExpenseViewModel>(query.OrderByDescending(x => x.Date).Skip((Page - 1) * PageSize).Take(PageSize));

            //// Grouping listview wpf
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ListExpense);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("Date");
            view.GroupDescriptions.Add(groupDescription);
        }
        #endregion
    }
}
