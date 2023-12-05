using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManagement.Controller
{
    public class Messenger
    {
        private static readonly Messenger _instance = new Messenger();

        public static Messenger Instance => _instance;

        // Không truyền tham số
        public event EventHandler<EventArgs> ShowSuccessMessageRequested;
        public void RequestShowSuccessMessage()
        {
            ShowSuccessMessageRequested?.Invoke(this, EventArgs.Empty);
        }

        // === Truyền tham số
        //public event EventHandler<CustomEventArgs> ShowSuccessMessageRequested;
        //public void RequestShowSuccessMessage(string message)
        //{
        //    ShowSuccessMessageRequested?.Invoke(this, new CustomEventArgs(message));
        //}
    }

    public class CustomEventArgs : EventArgs
    {
        public string Message { get; }

        public CustomEventArgs(string message)
        {
            Message = message;
        }
    }
}
