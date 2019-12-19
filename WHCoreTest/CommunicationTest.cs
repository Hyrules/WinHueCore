using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WinHue_Core.Philips_Hue;

namespace WHCoreTest
{
    [TestClass]
    public class CommunicationTest
    {
        /// <summary>
        /// Test Functionnal get bridge
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task TestSendAsyncTask()
        {
            Bridge br = await Communication.SendRequestAsyncTask<Bridge>("http://192.168.5.30/api/config", RestSharp.Method.GET);

        }

        public async Task TestSendAsyncTaskTimeout()
        {
            Bridge br = await Communication.SendRequestAsyncTask<Bridge>("http://192.168.5.31/api/config", RestSharp.Method.GET);
        }

    }
}
