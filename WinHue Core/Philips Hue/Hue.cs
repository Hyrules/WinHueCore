using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using WinHue_Core.Logging;
using RestSharp;
using Rssdp;
using System.Threading;
using WinHue_Core.Utils;
using System.Net.NetworkInformation;
using System.Linq;
using System.Windows;
using System.Collections.ObjectModel;
using WinHue_Core.Philips_Hue.Comms;

namespace WinHue_Core.Philips_Hue
{
    public static class Hue
    {

        private const string BRIDGE_MODELID = "BSB002";
        private const string BRIDGE_APIVERSION = "1.35.0";
      

        static Hue()
        {
        
        }

        #region IPSCANNER

        public static async Task<ObservableDictionary<IPAddress,Bridge>> ScanIPForBridgeAsyncTask(CancellationToken token, IProgress<IPAddress> progress = null, IProgress<Tuple<IPAddress,Bridge>> bridgefound = null)
        {
            ObservableDictionary<IPAddress,Bridge> newlist = new ObservableDictionary<IPAddress,Bridge>();

            try
            {

                Logger.Log.Info("Starting IP scan for bridge...");
                IPAddress ip = IPAddress.Parse(GetLocalIPAddress());
                byte[] ipArray = ip.GetAddressBytes();
                byte currentip = ipArray[3];


                for (byte i = 2; i <= 254; i++)
                {


                    if (token.IsCancellationRequested) break;
                    ipArray[3] = i;
                    IPAddress pollip = new IPAddress(ipArray);

                    
                    progress?.Report(pollip);
                    Logger.Log.Info($"Scanning {pollip} ...");
                    Tuple<IPAddress, Bridge> br = await TryGetBridge(pollip.ToString());
                    if (br != null)
                    {
                        if (newlist.ContainsKey(br.Item1)) continue;
                        Logger.Log.Info($"Found bridge {br.Item2.Name} at {br.Item1} adding it to the list.");
                        newlist.Add(br);
                        bridgefound?.Report(br);

                    }
                }


            }
            catch (Exception ex)
            {
                Logger.Log.Error($"Error while scanning for ip : {ex.Message}");
            }

            return newlist;

        }


        #endregion

        #region UPNPScanner

        public static async Task<ObservableDictionary<IPAddress,Bridge>> ScanUPNPForBridgeAsyncTask()
        {
            ObservableDictionary<IPAddress,Bridge> newlist = new ObservableDictionary<IPAddress,Bridge>();
            IEnumerable<DiscoveredSsdpDevice> dev;

            using (SsdpDeviceLocator loc = new SsdpDeviceLocator())
            {
                dev = await loc.SearchAsync("upnp:rootdevice", TimeSpan.FromSeconds(7));
            }

            foreach (DiscoveredSsdpDevice d in dev)
            {
                if (!d.ResponseHeaders.Contains("hue-bridgeid")) continue;
                Logger.Log.Info($"Testing {d.DescriptionLocation.Host}...");
                Tuple<IPAddress, Bridge> br = await TryGetBridge(d.DescriptionLocation.Host).ConfigureAwait(false);
                if (br != null)
                {
                    if (newlist.ContainsKey(br.Item1)) continue;
                    Logger.Log.Info($"Found bridge {br.Item2.Name} at {br.Item1} adding it to the list.");
                    newlist.Add(br);
                }
            }

            return newlist;
        }


        #endregion

        #region HUEPORTAL

        public static async Task<ObservableDictionary<IPAddress,Bridge>> ScanPortalForBridgeAsyncTask(CancellationToken token = new CancellationToken())
        {
            ObservableDictionary<IPAddress,Bridge> newlist = new ObservableDictionary<IPAddress,Bridge>();
            IRestResponse<List<PortalDevice>> response = await Communication.SendRequestAsyncTask<List<PortalDevice>>("https://discovery.meethue.com", Method.GET);
            if (response.StatusCode == HttpStatusCode.OK && response.Data is List<PortalDevice>)
            {
                foreach (PortalDevice p in response.Data)
                {
                    Tuple<IPAddress, Bridge> br = await TryGetBridge(p.InternalIpAddress).ConfigureAwait(false);
                    if (br != null)
                    {
                        if (newlist.ContainsKey(br.Item1)) continue;
                        newlist.Add(br);
                    }
                }
            }

            return newlist;
        }

         #endregion

        private static async Task<Tuple<IPAddress,Bridge>> TryGetBridge(string ip)
        {
            IRestResponse<Bridge> result = await Communication.SendRequestAsyncTask<Bridge>($"http://{ip}/api/config", Method.GET, 500);
            if (result.StatusCode == HttpStatusCode.OK && result.Data is Bridge)
            {
                if (result.Data.ModelId != BRIDGE_MODELID)
                {
                    Logger.Log.Info($"Bridge {result.Data.Name} - {ip} is a V1. Not compatible because of old model");
                    return null;
                }

                if (Version.Parse(result.Data.ApiVersion) < Version.Parse(BRIDGE_APIVERSION))
                {
                    Logger.Log.Info($"Bridge {result.Data.Name} - {ip} is a V1. Not compatible because of old api version");
                    return null;
                }

                Logger.Log.Info($"Adding bridge {result.Data.Name} - {ip} to bridge list.");
                return new Tuple<IPAddress, Bridge>(IPAddress.Parse(ip),result.Data);
            }
            return null;
        }

        /// <summary>
        /// Get the current IP of the computer to scan current range for bridge.
        /// </summary>
        /// <returns></returns>
        private static string GetLocalIPAddress()
        {
            string output = "";
            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (item.NetworkInterfaceType == NetworkInterfaceType.Ethernet && item.OperationalStatus == OperationalStatus.Up)
                {
                    IPInterfaceProperties adapterProperties = item.GetIPProperties();

                    if (adapterProperties.GatewayAddresses.FirstOrDefault() != null)
                    {
                        foreach (UnicastIPAddressInformation ip in adapterProperties.UnicastAddresses)
                        {
                            if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                            {
                                output = ip.Address.ToString();
                            }
                        }
                    }
                }
            }

            return output;
        }

    }
}
