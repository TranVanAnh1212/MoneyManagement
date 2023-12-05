using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MoneyManagement.ViewModel
{
    public class NavbarUCViewModel : BaseViewModel
    {
        public ICommand MoveWindowCommand { get; set; }
        public ICommand MaximizeCommand { get; set; }
        public ICommand MinimizeCommand { get; set; }
        public ICommand CloseWindowCommand { get; set; }

        public NavbarUCViewModel()
        {
            MoveWindowCommand = new RelayCommand<UserControl>(
                (p) => { return p != null ? true : false; },
                (p) =>
                {
                    MoveWindow(p);
                }
            );

            MaximizeCommand = new RelayCommand<UserControl>(
                (p) => { return p != null ? true : false; },
                (p) =>
                {
                    FrameworkElement window = GetParentElement(p);
                    var isWindow = window as Window;

                    if (isWindow != null)
                    {
                        if (isWindow.WindowState == WindowState.Normal)
                        {
                            isWindow.WindowState = WindowState.Maximized;
                        }
                    }

                }
                );

            CloseWindowCommand = new RelayCommand<UserControl>(
                (p) => { return p != null ? true : false; },
                (p) =>
                {
                    FrameworkElement window = GetParentElement(p);
                    var isWindow = window as Window;

                    if (isWindow != null)
                    {
                        Application.Current.Shutdown();
                    }
                }
                );

            MinimizeCommand = new RelayCommand<UserControl>(
                (p) => { return p != null ? true : false; },
                (p) =>
                {
                    FrameworkElement window = GetParentElement(p);
                    var isWindow = window as Window;

                    if (isWindow != null)
                    {
                        if (isWindow.WindowState == WindowState.Normal || isWindow.WindowState == WindowState.Maximized)
                        {
                            isWindow.WindowState = WindowState.Minimized;
                        }
                    }
                }

                );
        }

        #region Method
        private void MoveWindow(FrameworkElement fe)
        {
            FrameworkElement window = GetParentElement(fe);
            var isWindow = window as Window;

            if (isWindow != null)
            {
                isWindow.DragMove();
            }   
        }

        public FrameworkElement GetParentElement(FrameworkElement fe)
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
