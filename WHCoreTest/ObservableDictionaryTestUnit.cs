using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using WinHue_Core.Philips_Hue;
using WinHue_Core.Utils;

namespace WHCoreTest
{
    [TestClass]
    public class ObservableDictionaryTestUnit
    {
        [TestMethod]
        public void TestAddToDictionary()
        {
            ObservableDictionary<string,string> od = new ObservableDictionary<string, string>();
            od.Add("testkey", "testvalue");

            Assert.IsTrue(od.ContainsKey("testkey"),"Dictionary does not contain key");
            Assert.IsTrue(od.ContainsValue("testvalue"), "Dictionary does not contain value");
        }

        [TestMethod]
        public void TestAddKVPToDictionary()
        {
            ObservableDictionary<string, string> od = new ObservableDictionary<string, string>();
            KeyValuePair<string, string> kvp = new KeyValuePair<string, string>("testkey", "testvalue");
            od.Add(kvp);
            Assert.IsTrue(od.ContainsKey("testkey"));
            Assert.IsTrue(od.ContainsValue("testvalue"));
        }

        [TestMethod]
        public void TestAddTupleToDictionary()
        {
            ObservableDictionary<string, string> od = new ObservableDictionary<string, string>();
            Tuple<string, string> tuple = new Tuple<string, string>("testkey", "testvalue");
            od.Add(tuple);
            Assert.IsTrue(od.ContainsKey("testkey"));
            Assert.IsTrue(od.ContainsValue("testvalue"));
        }

        [TestMethod]
        public void TestAddDoubleKeyToDictionary()
        {
            ObservableDictionary<string, string> od = new ObservableDictionary<string, string>();
            od.Add("testkey", "testvalue");
            od.Add("testkey", "testvalue");
            Assert.IsTrue(od.Count == 1, "Count not expected...");
        }

        [TestMethod]
        public void TestRemoveFromDictionary()
        {
            ObservableDictionary<string, string> od = new ObservableDictionary<string, string>();
            od.Add("testkey", "testvalue");
            od.Remove("testkey");
            Assert.IsFalse(od.ContainsKey("testkey"), "Key is still present in dictionary");

        }

        [TestMethod]
        public void TestRemoveNotPresent()
        {
            ObservableDictionary<string, string> od = new ObservableDictionary<string, string>();
            bool result = od.Remove("Test");
            Assert.IsFalse(result, "Result is not false while removing non existing key");

        }

        public void TestClearDictionary()
        {
            ObservableDictionary<string, string> od = new ObservableDictionary<string, string>();
            od.Add("testkey", "testvalue");
            od.Add("testkey2", "testvalue2");
            od.Add("testkey3", "testvalue3");
            od.Clear();
            Assert.IsTrue(od.Count == 0, "Dictionary is not empty");

        }
    }
}
