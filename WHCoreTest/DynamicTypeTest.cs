using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using RestSharp;
using WinHue_Core.Philips_Hue.Comms;
using Newtonsoft.Json.Linq;

namespace WHCoreTest
{
    [TestClass]
    public class DynamicTypeTest
    {
        [TestMethod]
        public void TestDynamicObject()
        {
            dynamic d =JObject.Parse(Communication.SendRequestAsyncTask("http://192.168.5.30/api/30jodHoH6BvouvzmGR-Y8nJfa0XTN1j8sz2tstYJ/lights/2",Method.GET).Result.Content);
            Assert.IsNotNull(d.state);
            
        }

        [TestMethod]
        public void TestDynamicObjectList()
        {
            dynamic d = JObject.Parse(Communication.SendRequestAsyncTask("http://192.168.5.30/api/30jodHoH6BvouvzmGR-Y8nJfa0XTN1j8sz2tstYJ/lights", Method.GET).Result.Content);
            Assert.IsNotNull(d);
            
        }


    }
}
