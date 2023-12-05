using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManagement.Common
{
    public class BcryptAlgorithm
    {
        private static readonly BcryptAlgorithm _instance = new BcryptAlgorithm();

        public static BcryptAlgorithm Instance => _instance;

        public string Encoded(string pass)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt(12);

            string hashedPass = BCrypt.Net.BCrypt.HashPassword(pass, salt);

            return hashedPass;
        }

    }
}
