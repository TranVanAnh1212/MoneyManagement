using MoneyManagemementModel.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManagemementModel.DAO
{
    public class F_ExpenseDAO
    {
        public List<F_Expense> GetListExpense(string searchStr = null)
        {
            return DataProvider.Instance.DB.F_Expense.Where(x => x.Name.Contains(searchStr)).ToList();
        }

        public int GetTotalAmount(int month, int year)
        {
            return DataProvider.Instance.DB.F_Expense.Where(x => x.Date.Month == month && x.Date.Year == year).Sum(x => (int?)x.Price) ?? 0;
        }

        public int GetTotalByDay(DateTime fromDate, DateTime toDate)
        {
            int total = DataProvider.Instance.DB.F_Expense
                            .Where(x => x.Date >= fromDate && x.Date <= toDate)
                            .Sum(x => (int?)x.Price) ?? 0;

            return total;
        }

        public F_Expense GetExpenseByID(long id)
        {
            return DataProvider.Instance.DB.F_Expense.FirstOrDefault(x => x.ID == id);
        }

        public long AddNewExpense(F_Expense f)
        {
            try
            {
                DataProvider.Instance.DB.F_Expense.Add(f);
                DataProvider.Instance.DB.SaveChanges();

                return f.ID;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public F_Expense GetNewExpense()
        {
            return DataProvider.Instance.DB.F_Expense.OrderByDescending(x => x.Date).Take(1).FirstOrDefault();
        }

        public bool Delete(F_Expense f)
        {
            try
            {
                DataProvider.Instance.DB.F_Expense.Remove(f);
                DataProvider.Instance.DB.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(F_Expense f)
        {
            try
            {
                var fe = new F_Expense()
                {
                    Name = f.Name,
                    Date = f.Date,
                    Price = f.Price,
                    Quantity = f.Quantity,
                    Note = f.Note,
                    Img = f.Img,
                    CategoryID = f.CategoryID
                };

                DataProvider.Instance.DB.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
