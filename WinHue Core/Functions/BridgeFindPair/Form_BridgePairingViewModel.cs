using AsyncAwaitBestPractices.MVVM;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WinHue_Core.MVVM;
using WinHue_Core.Philips_Hue;

using System.Collections.ObjectModel;
using WinHue_Core.Utils;

namespace WinHue_Core.Functions.BridgeFindPair
{
    public class Form_BridgePairingViewModel : ViewModelBase
    {
        private bool _isScanning;
        private CancellationToken _cancellationToken;
        private CancellationTokenSource _cancellationTokenSource;
        private ObservableDictionary<IPAddress,Bridge> _listBridges;
        private int _ipScanProgress;
        private bool _progressUnknown;
        private Bridge _selectedBridge;
        private string _ScanProgressText;
        private IProgress<IPAddress> _ipScanProgressReport;
        private IProgress<Tuple<IPAddress, Bridge>> _ipScanBridgeFoundProgress;

        public Form_BridgePairingViewModel()
        {
            IsScanning = false;
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            ListBridges = new ObservableDictionary<IPAddress,Bridge>();
            _ipScanProgressReport = new Progress<IPAddress>(x => IpScanProgressReport(x));
            _ipScanBridgeFoundProgress = new Progress<Tuple<IPAddress, Bridge>>(x => IPScanBridgeFound(x));
            IpScanProgress = 0;
            ScanProgressText = string.Empty;
            
        }

        private void IPScanBridgeFound(Tuple<IPAddress,Bridge> bridge)
        {
            ListBridges.Add(bridge);        
        }


        private void IpScanProgressReport(IPAddress e)
        {
            IpScanProgress = e.GetAddressBytes()[3];
            ScanProgressText = e.ToString();
        }

        #region CANEXECUTE
        public Visibility CanAbort
        {
            get => IsScanning ? Visibility.Visible : Visibility.Collapsed;
        }

        public bool CanFindBridge()
        {
            if (IsScanning) return false;
            return true;
        }

        public bool CanScanUPNP()
        {
            if (IsScanning) return false;
            return true;
        }
        #endregion

        #region COMMANDS
        public IAsyncCommand ScanIPCommand => new AsyncCommand(ScanIP, param => CanFindBridge());
        public ICommand AbortScanCommand => new RelayCommand(param => AbortScan(), param => !_cancellationToken.IsCancellationRequested);
        public IAsyncCommand ScanUPNPCommand => new AsyncCommand(ScanUPNP, param => CanScanUPNP());
        
        #endregion

        #region METHODS
        public async Task ScanIP()
        {
            ProgressUnknown = false;
            IsScanning = true;
            ListBridges.AddRange(await Hue.ScanIPForBridgeAsyncTask(_cancellationToken, _ipScanProgressReport, _ipScanBridgeFoundProgress).ConfigureAwait(false));
            IsScanning = false;
        }

        public void AbortScan()
        {
            _cancellationTokenSource.Cancel();
            IsScanning = false;
        }

        public async Task ScanUPNP()
        {
            ScanProgressText = "Scanning UPNP for bridges";
            ProgressUnknown = true;
            IsScanning = true;
            ListBridges.AddRange(await Hue.ScanUPNPForBridgeAsyncTask().ConfigureAwait(false));
            IsScanning = false;
            ScanProgressText = string.Empty;
        }
        #endregion

        #region PROPERTIES
        public ObservableDictionary<IPAddress,Bridge> ListBridges { get => _listBridges; set => SetProperty(ref _listBridges,value); }
        public int IpScanProgress { get => _ipScanProgress; set => SetProperty(ref _ipScanProgress,value); }
        public bool ProgressUnknown { get => _progressUnknown; set => SetProperty(ref _progressUnknown,value); }
        public bool IsScanning {
            get => _isScanning;
            set {
                SetProperty(ref _isScanning, value);
                RaisePropertyChanged(nameof(CanAbort));
            }
        }
        public Bridge SelectedBridge { get => _selectedBridge; set => SetProperty(ref _selectedBridge, value); }
        public string ScanProgressText { get => _ScanProgressText; set => SetProperty(ref _ScanProgressText,value); }

        #endregion


    }
}
