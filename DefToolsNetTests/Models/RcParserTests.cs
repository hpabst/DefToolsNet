using Microsoft.VisualStudio.TestTools.UnitTesting;
using DefToolsNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefToolsNet.Models.Tests
{
    [TestClass()]
    public class RcParserTests
    {

        [TestMethod()]
        public void UnpackItemStringTest()
        {
            string testStr1 = "item:143575::::::::110:264::5:1:570:::";
            string testStr2 = "item:140816::::::::110:264::6:3:3518:1502:3528:::";
            RcParser rp = new TsvParser();
            WowItem item = rp.UnpackItemString(testStr1);
            Assert.IsTrue(item.ItemId == 143575);
            Assert.IsTrue(item.Name == "UNKNOWN");
            Assert.IsTrue(item.Instance == Zone.Unknown);
            Assert.IsTrue(item.BonusIds.Count == 1);
            HashSet<int> ids = new HashSet<int>();
            foreach (BonusId bid in item.BonusIds)
            {
                ids.Add(bid.Id);
            }
            Assert.IsTrue(ids.Contains(570));

            item = rp.UnpackItemString(testStr2);
            Assert.IsTrue(item.ItemId == 140816);
            Assert.IsTrue(item.Name == "UNKNOWN");
            Assert.IsTrue(item.Instance == Zone.Unknown);
            Assert.IsTrue(item.BonusIds.Count == 3);
            ids = new HashSet<int>();
            foreach (BonusId bid in item.BonusIds)
            {
                ids.Add(bid.Id);
            }
            Assert.IsTrue(ids.Contains(3518));
            Assert.IsTrue(ids.Contains(1502));
            Assert.IsTrue(ids.Contains(3528));
        }
    }
}