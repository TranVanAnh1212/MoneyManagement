using MoneyManagemementModel.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManagemementModel.DAO
{
    public class P_ExpenseDAO
    {
        public int GetTotalAmount(int month, int year)
        {
            return DataProvider.Instance.DB.P_Expense.Where(x => x.Date.Month == month && x.Date.Year == year).Sum(x => (int?)x.Price) ?? 0;
        }

        public int GetTotalByDate(DateTime fromDate, DateTime toDate)
        {
            int total = DataProvider.Instance.DB.P_Expense
                        .Where(x => x.Date >= fromDate && x.Date <= toDate)
                        .Sum(x => (int?)x.Price) ?? 0;

            return total;
        }

        public long AddNewExpense(P_Expense p)
        {
            try
            {
                DataProvider.Instance.DB.P_Expense.Add(p);
                DataProvider.Instance.DB.SaveChanges();

                return p.ID;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public P_Expense GetExpenseByID(long id)
        {
            return DataProvider.Instance.DB.P_Expense.FirstOrDefault(x => x.ID == id);
        }

        public bool Update(P_Expense p)
        {
            try
            {
                var pe = new P_Expense()
                {
                    Name = p.Name,
                    Date = p.Date,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    Note = p.Note,
                    Img = p.Img,
                    CategoryID = p.CategoryID
                };

                DataProvider.Instance.DB.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(P_Expense p)
        {
            try
            {
                DataProvider.Instance.DB.P_Expense.Remove(p);
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
