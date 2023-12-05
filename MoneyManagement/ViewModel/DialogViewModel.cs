using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace MoneyManagement.ViewModel
{
    public class DialogViewModel : BaseViewModel
    {
        public DialogViewModel()
        {
            MoveWindowCommand = new RelayCommand<Window>(
                (p) => { return true; },
                (p) => 
                {
                    MoveWindow(p);
                    //p.DragMove();
                }
            );

            CloseDialogCommand = new RelayCommand<Window>(
                (p) => { return true; },
                (p) => 
                {
                    //CloseDialog(p);
                    p.Close();
                }
            );

            AgreeCommand = new RelayCommand<Object>(
                (p) => { return true; },
                (p) =>
                {
                    AgreeMessage();
                }
            );

            CancelCommand = new RelayCommand<Object>(
                (p) => { return true; },
                (p) =>
                {
                    
                }

                );
        }

        private string _dialogMessage;

        public string DialogMessage { get => _dialogMessage; set { _dialogMessage = value; OnPropertyChanged(); } }

        public ICommand MoveWindowCommand { get; set; }
        public ICommand CloseDialogCommand { get; set; }
        public ICommand AgreeCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        #region Event
        public event EventHandler<EventArgs> AgreeRequested;
        public void AgreeMessage()
        {
            AgreeRequested?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region Method

        private void MoveWindow(FrameworkElement p)
        {
            FrameworkElement window = GetParrentElement(p);
            var ParentWindow = window as Window;

            if (ParentWindow != null)
            {
                ParentWindow.DragMove();
            }

        }

        private void CloseDialog(FrameworkElement fe)
        {
            FrameworkElement window = GetParrentElement(fe);
            var parentWindow = window as Window;

            if (parentWindow != null)
            {
                parentWindow.Close();
            }
        }

        private FrameworkElement GetParrentElement(FrameworkElement fe)
        {
            FrameworkElement parent = fe;
            while (parent.Parent != null)
            {
                parent = parent.Parent as FrameworkElement;
            }

            return parent;
        }
        #endregion
    }
}
