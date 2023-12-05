using MoneyManagemementModel.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManagemementModel.DAO
{
    public class AccountDAO
    {
        public int GetAccountByUsername(string username, string password)
        {
            string query = string.Format("Exec USP_Login {0}, {1}", username, password);
            return DataProvider.Instance.DB.Account.SqlQuery(query).Count();
        }
    }
}
