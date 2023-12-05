using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManagement.Controller
{
    public class Intro
    {
        private static readonly Intro _instance = new Intro();

        public static Intro Instance => _instance;

        public event EventHandler<EventArgs> ShowIntroRequest;
        
        public void ShowIntro ()
        {
            ShowIntroRequest?.Invoke(this, EventArgs.Empty);
        }
    }
}
