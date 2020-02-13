using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WinHue_Core.Philips_Hue;
using WinHue_Core.Philips_Hue.Comms;
using WinHue_Core.Philips_Hue.Messages;
using WinHue_Core.Philips_Hue.BaseObjects;
using WinHue_Core.Utils;

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
            IRestResponse<Bridge> br = await Communication.SendRequestAsyncTask<Bridge>("http://192.168.5.30/api/config", Method.GET);
            Assert.IsTrue(br.StatusCode == HttpStatusCode.OK, "Status code not OK");
            Assert.IsNotNull(br.Data, "Bridge is null");
        }

        [TestMethod]
        public async Task TestSendAsyncTaskTimeout()
        { 
            IRestResponse<Bridge> br = await Communication.SendRequestAsyncTask<Bridge>("http://192.168.5.31/api/config", Method.GET);
            Assert.IsTrue(br.StatusCode == 0, "Status code not 0");
            Assert.IsNull(br.Data, "Status code not null");
        }

        [TestMethod]
        public async Task TestSendAsyncTaskNotBridge()
        {
            IRestResponse<Bridge> br = await Communication.SendRequestAsyncTask<Bridge>("http://192.168.5.51", Method.GET);
            Assert.IsTrue(br.StatusCode == HttpStatusCode.OK, "Status code not OK");
            Assert.IsTrue(br.Data == null, "Object not null");
        }

        [TestMethod]
        public async Task TestGetInvalidObject()
        {
            try
            {
                HueObject light = await Communication.GetObject("http://192.168.5.30/api/30jodHoH6BvouvzmGR-Y8nJfa0XTN1j8sz2tstYJ/lights/99");
                Assert.Fail("Exception not triggered");
            }
            catch(HueGetErrorException e)
            {
                Assert.IsTrue(e.Response.Count == 1, "Error count not expected");
            }
            
        }


        [TestMethod]
        public async Task TestGetObject()
        {
            try
            {
                dynamic light = await Communication.GetObject("http://192.168.5.30/api/30jodHoH6BvouvzmGR-Y8nJfa0XTN1j8sz2tstYJ/lights/2");
                
                Assert.IsTrue(light.name == "Bloom", "Light name not expected");
            }
            catch (HueGetErrorException e)
            {
                Assert.Fail("Exception triggered");
                
            }

        }

        [TestMethod]
        public async Task TestGetListObjects()
        {
            try
            {
                List<dynamic> listlights = await Communication.GetListObjects("http://192.168.5.30/api/30jodHoH6BvouvzmGR-Y8nJfa0XTN1j8sz2tstYJ/lights");
                Assert.IsTrue(listlights.Count != 0, "List lights is empty");
            }
            catch(HueGetErrorException e)
            {
                Assert.Fail("Exception triggered");
            }
        }

        

    }
}
