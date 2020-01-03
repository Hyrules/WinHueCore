using System;
using System.Collections.Generic;
using System.Text;
using WinHue_Core.MVVM;
using AsyncAwaitBestPractices.MVVM;
using System.Windows.Input;
using System.Windows;
using WinHue_Core.Logging;
using WinHue_Core.Functions.BridgeFindPair;

namespace WinHue_Core.MainForm
{
    public partial class MainFormViewModel : ViewModelBase
    {
        


        public ICommand QuitCommand => new RelayCommand(param => Quit());
        public ICommand DetectAndPairBridgeCommand => new RelayCommand(param => DetectAndPairBridge());
        
            
            
        private void Quit()
        {
            Logger.Log.Info("Closing WinHue Core.");
            Application.Current.Shutdown();
        }

        private void DetectAndPairBridge()
        {
            Form_BridgePairing formBridgePair = new Form_BridgePairing() { Owner = Application.Current.MainWindow };
            formBridgePair.ShowDialog();
        }

    }
}
