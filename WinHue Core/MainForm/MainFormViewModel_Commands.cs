using System;
using System.Collections.Generic;
using System.Text;
using WinHue_Core.MVVM;
using AsyncAwaitBestPractices.MVVM;
using System.Windows.Input;
using System.Windows;

namespace WinHue_Core.MainForm
{
    public partial class MainFormViewModel : ViewModelBase
    {

        public ICommand QuitCommand => new RelayCommand(param => Quit());

        private void Quit()
        {
            Application.Current.Shutdown();
        }

    }
}
