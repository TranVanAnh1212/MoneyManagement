using LiveCharts;
using LiveCharts.Wpf;
using MoneyManagemementModel;
using MoneyManagemementModel.DAO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MoneyManagement.ViewModel
{
    public class StatisticalViewModel : BaseViewModel
    {
        public StatisticalViewModel()
        {
            InittialMethod();
        }

        #region Initialized
        private DateTime _toDate;
        private DateTime _fromDate;
        private int _f_TotalAmount;
        private int _p_TotalAmount;
        private int _totalIncome;
        private string _name;
        private int _totalQuantity;
        private int _totalPrice;
        private ChartValues<int> _fSumValue;
        private ChartValues<int> _pSumValue;
        private ChartValues<int> _incomeSumValue;
        private ObservableCollection<StatisticalViewModel> _listBuyMost;
        private bool _is3DayChecked;
        private bool _is7DayChecked;
        private bool _is15DayChecked;
        private bool _is30DayChecked;
        private bool _isHighToLow;
        private bool _isLowToHigh;
        public ICommand SuccessCommand { get; set; }
        public ICommand ThreeDaySortCommand { get; set; }
        public ICommand SevenDaySortCommand { get; set; }
        public ICommand FifteenDaySortCommand { get; set; }
        public ICommand ThirdtyDaySortCommand { get; set; }
        public ICommand HighToLowSortCommand { get; set; }
        public ICommand LowToHighSortCommand { get; set; }
        public ICommand LoadStatisticalCommand { get; set; }

        public DateTime ToDate { get => _toDate; set { _toDate = value; OnPropertyChanged(); } }
        public DateTime FromDate { get => _fromDate; set { _fromDate = value; OnPropertyChanged(); } }
        public int F_TotalAmount { get => _f_TotalAmount; set { _f_TotalAmount = value; OnPropertyChanged(); } }
        public int P_TotalAmount { get => _p_TotalAmount; set { _p_TotalAmount = value; OnPropertyChanged(); } }
        public int TotalIncome { get => _totalIncome; set { _totalIncome = value; OnPropertyChanged(); } }
        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }
        public int TotalQuantity { get => _totalQuantity; set { _totalQuantity = value; OnPropertyChanged(); } }
        public int TotalPrice { get => _totalPrice; set { _totalPrice = value; OnPropertyChanged(); } }
        public ObservableCollection<StatisticalViewModel> ListBuyMost { get => _listBuyMost; set { _listBuyMost = value; OnPropertyChanged(); } }
        public ChartValues<int> FSumValue { get => _fSumValue; set { _fSumValue = value; OnPropertyChanged(); } }
        public ChartValues<int> PSumValue { get => _pSumValue; set { _pSumValue = value; OnPropertyChanged(); } }
        public ChartValues<int> IncomeSumValue { get => _incomeSumValue; set { _incomeSumValue = value; OnPropertyChanged(); } }

        public bool Is3DayChecked
        {
            get => _is3DayChecked;
            set
            {
                _is3DayChecked = value;
                _is7DayChecked = false;
                _is15DayChecked = false;
                _is30DayChecked = false;
                OnPropertyChanged("Is3DayChecked");
                OnPropertyChanged("Is7DayChecked");
                OnPropertyChanged("Is15DayChecked");
                OnPropertyChanged("Is30DayChecked");
            }
        }
        public bool Is7DayChecked { get => _is7DayChecked; set { _is7DayChecked = value; _is3DayChecked = false; _is15DayChecked = false; _is30DayChecked = false; OnPropertyChanged("Is3DayChecked"); OnPropertyChanged("Is7DayChecked"); OnPropertyChanged("Is15DayChecked"); OnPropertyChanged("Is30DayChecked"); } }
        public bool Is15DayChecked { get => _is15DayChecked; set { _is15DayChecked = value; _is7DayChecked = false; _is3DayChecked = false; _is30DayChecked = false; OnPropertyChanged("Is3DayChecked"); OnPropertyChanged("Is7DayChecked"); OnPropertyChanged("Is15DayChecked"); OnPropertyChanged("Is30DayChecked"); } }
        public bool Is30DayChecked { get => _is30DayChecked; set { _is30DayChecked = value; _is7DayChecked = false; _is15DayChecked = false; _is3DayChecked = false; OnPropertyChanged("Is3DayChecked"); OnPropertyChanged("Is7DayChecked"); OnPropertyChanged("Is15DayChecked"); OnPropertyChanged("Is30DayChecked"); } }

        public bool IsHighToLow { get => _isHighToLow; set { _isHighToLow = value; _isLowToHigh = false; OnPropertyChanged("IsHighToLow"); OnPropertyChanged("IsLowToHigh"); } }
        public bool IsLowToHigh { get => _isLowToHigh; set { _isLowToHigh = value; _isHighToLow = false; OnPropertyChanged("IsHighToLow"); OnPropertyChanged("IsLowToHigh"); } }

        #endregion

        #region Method
        private void InittialMethod()
        {
            Is30DayChecked = true;
            IsHighToLow = true;
            ToDate = DateTime.Now;
            FromDate = DateTime.Now.AddDays(-30);
            F_TotalAmount = new F_ExpenseDAO().GetTotalByDay(FromDate, ToDate);
            P_TotalAmount = new P_ExpenseDAO().GetTotalByDate(FromDate, ToDate);

            LoadStatisticalCommand = new RelayCommand<Object>(
                (p) => { return true; },
                (p) =>
                {

                    GetListBuyMost(FromDate, ToDate);
                    GetListStatisticalByMonth();
                }
                );

            SuccessCommand = new RelayCommand<Object>(
                (p) => { return true; },
                (p) =>
                {
                    F_TotalAmount = new F_ExpenseDAO().GetTotalByDay(FromDate, ToDate);
                    P_TotalAmount = new P_ExpenseDAO().GetTotalByDate(FromDate, ToDate);
                    GetListBuyMost(FromDate, ToDate);

                }
                );

            ThreeDaySortCommand = new RelayCommand<Object>(
                (p) => { return true; },
                (p) =>
                {
                    ToDate = DateTime.Now;
                    FromDate = ToDate.AddDays(-3);

                    F_TotalAmount = new F_ExpenseDAO().GetTotalByDay(FromDate, ToDate);
                    P_TotalAmount = new P_ExpenseDAO().GetTotalByDate(FromDate, ToDate);
                    GetListBuyMost(FromDate, ToDate);
                }
                );

            SevenDaySortCommand = new RelayCommand<Object>(
                (p) => { return true; },
                (p) =>
                {
                    ToDate = DateTime.Now;
                    FromDate = ToDate.AddDays(-7);

                    F_TotalAmount = new F_ExpenseDAO().GetTotalByDay(FromDate, ToDate);
                    P_TotalAmount = new P_ExpenseDAO().GetTotalByDate(FromDate, ToDate);
                    GetListBuyMost(FromDate, ToDate);
                }
                );

            FifteenDaySortCommand = new RelayCommand<Object>(
                (p) => { return true; },
                (p) =>
                {
                    ToDate = DateTime.Now;
                    FromDate = ToDate.AddDays(-15);

                    F_TotalAmount = new F_ExpenseDAO().GetTotalByDay(FromDate, ToDate);
                    P_TotalAmount = new P_ExpenseDAO().GetTotalByDate(FromDate, ToDate);
                    GetListBuyMost(FromDate, ToDate);
                }
                );

            ThirdtyDaySortCommand = new RelayCommand<Object>(
                (p) => { return true; },
                (p) =>
                {
                    ToDate = DateTime.Now;
                    FromDate = ToDate.AddDays(-30);

                    F_TotalAmount = new F_ExpenseDAO().GetTotalByDay(FromDate, ToDate);
                    P_TotalAmount = new P_ExpenseDAO().GetTotalByDate(FromDate, ToDate);
                    GetListBuyMost(FromDate, ToDate);
                }
                );

            HighToLowSortCommand = new RelayCommand<Object>(
                (p) => { return true; },
                (p) =>
                {
                    
                    GetListBuyMost(FromDate, ToDate);

                }
                );

            LowToHighSortCommand = new RelayCommand<Object>(
                (p) => { return true; },
                (p) =>
                {
                    
                    GetListBuyMost(FromDate, ToDate);

                }
                );

        }

        private void GetListBuyMost(DateTime fromDate, DateTime toDate)
        {
            Thread list_Thread = new Thread(() =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    var db = DataProvider.Instance.DB;
                    var result = from fe in db.F_Expense
                                 where fe.Date >= fromDate && fe.Date <= toDate
                                 group fe by fe.Name into nameGroup
                                 select new
                                 {
                                     Name = nameGroup.Key,
                                     Count = nameGroup.Count(),
                                     Total = nameGroup.Sum(fe => fe.Price)
                                 };

                    var topNames = result.Take(10)
                                        .Select(x => new StatisticalViewModel
                                        {
                                            Name = x.Name,
                                            TotalQuantity = x.Count,
                                            TotalPrice = (int)x.Total
                                        });

                    if (IsHighToLow)
                    {
                        ListBuyMost = new ObservableCollection<StatisticalViewModel>(topNames.OrderByDescending(x => x.TotalQuantity).ToList());

                    }
                    else
                    {
                        ListBuyMost = new ObservableCollection<StatisticalViewModel>(topNames.OrderBy(x => x.TotalQuantity).ToList());

                    }
                });

            });
            list_Thread.Start();
        }

        private void GetListStatisticalByMonth()
        {
            Thread ListStatistical_Thread = new Thread(() =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    List<int> fListStatis = new List<int>();
                    for (int i = 1; i <= 12; i++)
                    {
                        int sum = new F_ExpenseDAO().GetTotalAmount(i, DateTime.Now.Year);
                        fListStatis.Add(sum);
                    }

                    List<int> pListStatis = new List<int>();
                    for (int i = 1; i <= 12; i++)
                    {
                        int sum = new P_ExpenseDAO().GetTotalAmount(i, DateTime.Now.Year);
                        pListStatis.Add(sum);
                    }

                    List<int> incomeListStatis = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

                    FSumValue = new ChartValues<int>(fListStatis);
                    PSumValue = new ChartValues<int>(pListStatis);
                    IncomeSumValue = new ChartValues<int>(incomeListStatis);
                });
            });

            ListStatistical_Thread.Start();
        }
        #endregion
    }
}
