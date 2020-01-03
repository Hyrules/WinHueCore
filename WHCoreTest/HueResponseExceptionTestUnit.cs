using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using WinHue_Core.Philips_Hue.Messages;

namespace WHCoreTest
{
    [TestClass]
    public class HueResponseExceptionTestUnit
    {
        [TestMethod]
        public void TestErrorMessageParse()
        {
            HueResponse response = HueResponse.Parse("[{\"error\": { \"type\": 3, \"address\": \"/lights/99\", \"description\": \"resource, /lights/99, not available\" }}]");
            Assert.IsTrue(response.Errors.Count == 1, "Error count value not expected");
        }

        [TestMethod]
        public void TestSuccessDeleteParse()
        {
            HueResponse response = HueResponse.Parse("[{\"success\":\"/groups/1 deleted\"}]");
            Assert.IsTrue(response.Success.Count == 1, "Success count value not expected");
        }

        [TestMethod]
        public void TestSuccessPutParse()
        {
            HueResponse response = HueResponse.Parse("[{\"success\":{\"/lights/1/state/hue\":254}}]");
            Assert.IsTrue(response.Success.Count == 1, "Success count value not expected");

        }
    }
}
