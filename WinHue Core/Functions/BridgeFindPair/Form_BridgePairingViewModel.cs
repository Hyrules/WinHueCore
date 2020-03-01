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
using WinHue_Core.Logging;

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
        private KeyValuePair<IPAddress,Bridge>? _selectedBridge;
        private string _ScanProgressText;
        private IProgress<IPAddress> _ipScanProgressReport;
        private IProgress<Tuple<IPAddress, Bridge>> _ipScanBridgeFoundProgress;
        private string _message;

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

        public bool CanFindBridge()
        {
            if (IsScanning) return false;
            return true;
        }

        public bool CanPairBridge()
        {
            if (SelectedBridge == null) return false;
            return true;
        }



        #endregion

        #region COMMANDS
        public ICommand FindBridgeCommand => new AsyncRelayCommand(param => FindBridge(), param => CanFindBridge());

        public ICommand ScanIPCommand => new AsyncRelayCommand(param => ScanIP(), param => CanFindBridge());
        public ICommand AbortScanCommand => new RelayCommand(param => AbortScan(), param => !_cancellationToken.IsCancellationRequested && IsScanning);
        public ICommand ScanUPNPCommand => new AsyncRelayCommand(param => ScanUPNP(), param => CanFindBridge());

        public ICommand PairBridgeCommand => new AsyncRelayCommand(param => PairBridge(), param => CanPairBridge());

        public ICommand ScanPortalCommand => new AsyncRelayCommand(param => ScanPortal(), param => CanFindBridge());


        #endregion

        #region METHODS

        private async Task FindBridge()
        {

            IsScanning = true;
            await ScanPortal();
            if (ListBridges.Count == 0) await ScanUPNP();
            if (ListBridges.Count == 0) await ScanIP();
            IsScanning = false;
            Message = Properties.Dictionary.DetectPairBridgeScanComplete;
        }

        private async Task ScanPortal()
        {
            Logger.Log.Info("Scanning portal for bridges...");
            IsScanning = true;
            ListBridges.AddRange(await Hue.ScanPortalForBridgeAsyncTask(_cancellationToken).ConfigureAwait(false));
            IsScanning = false;
            Message = Properties.Dictionary.DetectPairBridgeScanPortalComplete;
            Logger.Log.Info("Finished Scanning portal for bridges.");
        }

        public async Task PairBridge()
        {
            Logger.Log.Info($"Beginning pairing sequence for bridge {SelectedBridge.Value.Value.Name} at IP : {SelectedBridge.Value.Key} ...");

        }


        public async Task ScanIP()
        {
            Logger.Log.Info("Scanning IP for bridges...");
            ProgressUnknown = false;
            IsScanning = true;
            ListBridges.AddRange(await Hue.ScanIPForBridgeAsyncTask(_cancellationToken, _ipScanProgressReport, _ipScanBridgeFoundProgress).ConfigureAwait(false));
            IsScanning = false;
            ScanProgressText = string.Empty;
            Message = Properties.Dictionary.DetectPairBridgeScanIPComplete;
            Logger.Log.Info("Finished scanning IP for bridges...");
        }

        public void AbortScan()
        {
            Logger.Log.Info("Aborting scan for bridges...");
            _cancellationTokenSource.Cancel();
            ScanProgressText = string.Empty;
            IsScanning = false;
            Message = Properties.Dictionary.DetectPairBridgeScanAborted;
            Logger.Log.Info("Scan aborted.");
        }

        public async Task ScanUPNP()
        {
            Logger.Log.Info("Scanning UPNP for bridges...");
            ScanProgressText = "Scanning UPNP for bridges";
            ProgressUnknown = true;
            IsScanning = true;
            ListBridges.AddRange(await Hue.ScanUPNPForBridgeAsyncTask().ConfigureAwait(false));
            IsScanning = false;
            ProgressUnknown = false;
            ScanProgressText = string.Empty;
            Message = Properties.Dictionary.DetectPairBridgeScanUPNPComplete;
            Logger.Log.Info("Finished scanning UPNP for bridges...");
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
             }
        }
        public KeyValuePair<IPAddress,Bridge>? SelectedBridge { 
            get => _selectedBridge;
            set
            {
                SetProperty(ref _selectedBridge, value);

            }
        }
        public string ScanProgressText { get => _ScanProgressText; set => SetProperty(ref _ScanProgressText,value); }
        public string Message { get => _message; set => SetProperty(ref _message,value); }

        #endregion


    }
}
