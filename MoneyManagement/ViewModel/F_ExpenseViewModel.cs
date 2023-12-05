using MoneyManagemementModel;
using MoneyManagemementModel.DAO;
using MoneyManagemementModel.EF;
using MoneyManagement.Controller;
using MoneyManagement.Model;
using MoneyManagement.View.Customs;
using MoneyManagement.View.F_Expense;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace MoneyManagement.ViewModel
{
    public class F_ExpenseViewModel : BaseViewModel
    {
        public F_ExpenseViewModel()
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
        private F_ExpenseViewModel _selectedItem;
        private ObservableCollection<Category> _categoryList;
        private ObservableCollection<F_ExpenseViewModel> _listExpense;
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
        public F_ExpenseViewModel SelectedItem
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

                    var temp1 = 0;
                    if (int.TryParse(SelectedItem.Price, out temp1))
                    {
                        var tempPrice = temp1.ToString();
                        Price = tempPrice;
                    }
                    else
                    {
                        string[] tempArr = SelectedItem.Price.Split('.');
                        Price = tempArr[0];

                    }
                    
                    Quantity = SelectedItem.Quantity;
                    Selectedcategory = CategoryList.FirstOrDefault(x => x.ID == SelectedItem.CategID);
                    Note = SelectedItem.Note;
                    CategID = SelectedItem.CategID;
                    Img = (SelectedItem.Img == null || SelectedItem.Img == "") ? "/Data/Images/Img_empty.png" : SelectedItem.Img;
                };
            }
        }
        public ObservableCollection<F_ExpenseViewModel> ListExpense { get => _listExpense; set { _listExpense = value; OnPropertyChanged(); } }
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

            #region Commen
            //// Get List Expense Binding
            //LoadExpenseWindowCommand = new RelayCommand<Window>(
            //    (p) => { return true; },
            //    (p) =>
            //    {
            //        var db = DataProvider.Instance.DB;
            //        var resultList = from fe in db.F_Expense
            //                         select new F_ExpenseViewModel
            //                         {
            //                             Name = fe.Name,
            //                             Date = fe.Date,
            //                             Price = (int)fe.Price,
            //                             Quantity = fe.Quantity.Value,
            //                             CategID = fe.CategoryID.Value,
            //                             Note = fe.Note,
            //                             Img = fe.Img
            //                         };

            //        ListExpense = new ObservableCollection<F_ExpenseViewModel>(resultList);
            //    }
            //    );

            #endregion

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
                    if (Name == "" || string.IsNullOrEmpty(Price) || Date == null || Selectedcategory == null || SelectedItem != null)
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
                    if (string.IsNullOrEmpty(SearchStr))
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

        private void GetTotalAmount()
        {
            TotalAmount = new F_ExpenseDAO().GetTotalAmount(DateTime.Now.Month, DateTime.Now.Year);
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
                    var query = from fe in db.F_Expense
                                where fe.Date.Month == month && fe.Date.Year == DateTime.Now.Year
                                select new F_ExpenseViewModel()
                                {
                                    Id = fe.ID,
                                    Name = fe.Name,
                                    Date = fe.Date,
                                    Price = fe.Price.ToString(),
                                    Quantity = fe.Quantity.Value,
                                    CategID = fe.CategoryID.Value,
                                    CategName = fe.Category.Name,
                                    Note = fe.Note,
                                    Img = fe.Img == null ? "" : fe.Img,
                                };

                    if (month > 12)
                    {
                        query = from fe in db.F_Expense
                                where fe.Date.Year == DateTime.Now.Year
                                select new F_ExpenseViewModel()
                                {
                                    Id = fe.ID,
                                    Name = fe.Name,
                                    Date = fe.Date,
                                    Price = fe.Price.ToString(),
                                    Quantity = fe.Quantity.Value,
                                    CategID = fe.CategoryID.Value,
                                    CategName = fe.Category.Name,
                                    Note = fe.Note,
                                    Img = fe.Img == null ? "" : fe.Img,
                                };
                    }

                    TotalRecord = query.Count();
                    TotalPage = (int)Math.Ceiling((Double)TotalRecord / (Double)PageSize);
                    ListExpense = new ObservableCollection<F_ExpenseViewModel>(query.OrderByDescending(x => x.Date).Skip((Page - 1) * PageSize).Take(PageSize));

                    //// Grouping listview wpf
                    CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ListExpense);
                    PropertyGroupDescription groupDescription = new PropertyGroupDescription("Date");
                    view.GroupDescriptions.Add(groupDescription);
                });
            });
            listExpense_Thread.Start();
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

        public void GetListExpenseSearch(string searchStr = null)
        {
            var db = DataProvider.Instance.DB;
            var query = from fe in db.F_Expense
                        where fe.Date.Month == DateTime.Now.Month && fe.Date.Year == DateTime.Now.Year && fe.Name.Contains(searchStr)
                        select new F_ExpenseViewModel()
                        {
                            Id = fe.ID,
                            Name = fe.Name,
                            Date = fe.Date,
                            Price = fe.Price.ToString(),
                            Quantity = fe.Quantity.Value,
                            CategID = fe.CategoryID.Value,
                            CategName = fe.Category.Name,
                            Note = fe.Note,
                            Img = fe.Img,
                        };

            TotalRecord = query.Count();
            TotalPage = (int)Math.Ceiling((Double)TotalRecord / (Double)PageSize);
            ListExpense = new ObservableCollection<F_ExpenseViewModel>(query.OrderByDescending(x => x.Date).Skip((Page - 1) * PageSize).Take(PageSize));

            //// Grouping listview wpf
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ListExpense);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("Date");
            view.GroupDescriptions.Add(groupDescription);
        }


        public void GetListSorted(int id)
        {
            var db = DataProvider.Instance.DB;
            var query = from fe in db.F_Expense
                        where fe.Date.Month == DateTime.Now.Month && fe.Date.Year == DateTime.Now.Year
                        select new F_ExpenseViewModel()
                        {
                            Id = fe.ID,
                            Name = fe.Name,
                            Date = fe.Date,
                            Price = fe.Price.ToString(),
                            Quantity = fe.Quantity.Value,
                            CategID = fe.CategoryID.Value,
                            CategName = fe.Category.Name,
                            Note = fe.Note,
                            Img = fe.Img,
                        };

            TotalRecord = query.Count();
            TotalPage = (int)Math.Ceiling((Double)TotalRecord / (Double)PageSize);

            if (id == 2)
                ListExpense = new ObservableCollection<F_ExpenseViewModel>(query.OrderBy(x => x.Name).Skip((Page - 1) * PageSize).Take(PageSize));
            else if (id == 3)
                ListExpense = new ObservableCollection<F_ExpenseViewModel>(query.OrderBy(x => x.Price).Skip((Page - 1) * PageSize).Take(PageSize));
            else if (id == 1)
                ListExpense = new ObservableCollection<F_ExpenseViewModel>(query.OrderBy(x => x.Date).Skip((Page - 1) * PageSize).Take(PageSize));


            //// Grouping listview wpf
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ListExpense);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("Date");
            view.GroupDescriptions.Add(groupDescription);
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
            Price = null;
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
                string[] a = Price.Split('.');
                string b = string.Concat(a);
                priceParse = Convert.ToInt32(b);

                var newfe = new F_Expense()
                {
                    Name = Name.Trim(),
                    Date = Date,
                    Price = priceParse,
                    Quantity = Quantity,
                    Img = Img == "/Data/Images/Img_empty.png" ? null : Img,
                    Note = Note,
                    CategoryID = Selectedcategory.ID,

                };

                var result = new F_ExpenseDAO().AddNewExpense(newfe);

                if (result > 0)
                {
                    // Thành công thì fill màu xanh
                    ShowNotitication("Thêm mới chi tiêu thành công!", "#FF40ED49", "/Data/Images/SuccessIcon.png");

                    // add new expense to list expense
                    var fe_VM = new F_ExpenseViewModel()
                    {
                        Id = result,
                        Name = newfe.Name,
                        Date = newfe.Date,
                        Price = newfe.Price.ToString(),
                        Quantity = newfe.Quantity.Value,
                        Img = newfe.Img,
                        Note = newfe.Note,
                        CategID = newfe.Category.ID,
                        CategName = newfe.Category.Name,
                    };
                    ListExpense.Insert(0, fe_VM);

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
        /// Hàm xóa chi tiêu
        /// </summary>
        private void DeleteExpense()
        {
            Dialog d = new Dialog();
            d.DialogMessage = "Bạn thực sự muốn xóa?";
            d.Owner = Window.GetWindow(new F_ExpensePage());
            if (true == d.ShowDialog())
            {
                try
                {
                    var fe = new F_ExpenseDAO().GetExpenseByID(SelectedItem.Id);
                    var result = new F_ExpenseDAO().Delete(fe);

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
            d.Owner = Window.GetWindow(new F_ExpensePage());
            if (true == d.ShowDialog())
            {
                try
                {
                    int priceParse = 0;
                    string[] a = Price.Split('.');
                    string b = string.Concat(a);
                    priceParse = Convert.ToInt32(b);

                    var fe = new F_ExpenseDAO().GetExpenseByID(SelectedItem.Id);
                    fe.Name = Name.Trim();
                    fe.Date = Date;
                    fe.Price = priceParse;
                    fe.Quantity = Quantity;
                    fe.Note = Note;
                    fe.Img = Img == "/Data/Images/Img_empty.png" ? null : Img;
                    fe.CategoryID = Selectedcategory.ID;

                    var result = new F_ExpenseDAO().Update(fe);

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

        public void ShowNotitication(string mess, string fill, string imgSource)
        {
            Message = mess;
            MessageFill = fill; // Thành công thì fill màu xanh
            MessageimgSource = imgSource;
            Messenger.Instance.RequestShowSuccessMessage();
        }
        #endregion
    }
}
