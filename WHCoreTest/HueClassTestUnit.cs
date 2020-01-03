using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WinHue_Core;
using WinHue_Core.Philips_Hue;
using WinHue_Core.Utils;

namespace WHCoreTest
{
    [TestClass]
    public class HueClassTestUnit
    {
        CancellationTokenSource source;
        CancellationToken token;

        [TestInitialize]
        public void Init()
        {
            source = new CancellationTokenSource();
            token = source.Token;
        }

        [TestMethod]
        public async Task Test_ScanForIP()
        {
            CancellationToken token = new CancellationToken();
            ObservableDictionary<IPAddress,Bridge> bridges = await Hue.ScanIPForBridgeAsyncTask(token);
            Assert.IsTrue(bridges.Count == 1,"IPScanComplete event was not triggered");
        }

        [TestMethod]
        public async Task Test_ScanUPNP()
        {

            ObservableDictionary<IPAddress,Bridge> bridges = await Hue.ScanUPNPForBridgeAsyncTask();       
            Assert.IsTrue(bridges.Count == 1, "IPUPNPScanComplete event was not triggered");
        }

        [TestMethod]
        public async Task Test_ScanPortalForBridge()
        {
            ObservableDictionary<IPAddress,Bridge> bridges = await Hue.ScanPortalForBridgeAsyncTask();
            Assert.IsTrue(bridges.Count == 1, "Portal scan result not expected");
        }


    }
}
