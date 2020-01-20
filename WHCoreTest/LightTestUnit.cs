using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using WinHue_Core.Philips_Hue;
using WinHue_Core.Philips_Hue.BaseObjects;

namespace WHCoreTest
{
    [TestClass]
    public class LightTestUnit
    {

        [TestMethod]
        public void TestSetStateValidProperty()
        {
            Bridge bridge = new Bridge();
            Light l = new Light(ref bridge);
            l.SetState("Hue", 60000);

            Assert.IsTrue(l.State.Hue == 60000, "Hue value not expected");

        }

        [TestMethod]
        public void TestSetStateInvalidProperty()
        {
            Bridge bridge = new Bridge();
            Light l = new Light(ref bridge);
            l.SetState("zoom", 34);

            Assert.IsTrue(l.State.Hue == 60000, "Hue value not expected");

        }
    }
}
