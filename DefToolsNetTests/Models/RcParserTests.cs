using Microsoft.VisualStudio.TestTools.UnitTesting;
using DefToolsNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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
            string testStr3 = "item:143575:110:264:";
            RcParser rp = new TsvParser();
            WowItem item = rp.UnpackItemString(testStr1);
            Assert.IsTrue(item.ItemId == 143575);
            Assert.IsTrue(item.Name == "UNKNOWN");
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
            Assert.IsTrue(item.BonusIds.Count == 3);
            ids = new HashSet<int>();
            foreach (BonusId bid in item.BonusIds)
            {
                ids.Add(bid.Id);
            }
            Assert.IsTrue(ids.Contains(3518));
            Assert.IsTrue(ids.Contains(1502));
            Assert.IsTrue(ids.Contains(3528));
            try
            {
                rp.UnpackItemString(testStr3);
                Assert.IsTrue(false);
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod()]
        public void UnpackWowheadLinkTest()
        {
            string teststr1 =
                "=HYPERLINK(\"https://www.wowhead.com/item=143575&bonus=570\",\"[Helm of the Foreseen Protector]\")";
            string teststr2 =
                "=HYPERLINK(\"https://www.wowhead.com/item=140917&bonus=3517:41:1492:3528\",\"[Netherbranded Shoulderpads]\")";
            string teststr3 = "=HYPERLINK(\"https://www.wowhead.com/item=0\",\"nil\")";
            string teststr4 = "doesn't match wowhead link format";
            string teststr5 = "=HYPERLINK(\"https://www.wowhead.com/item=140917\",\"[Netherbranded Shoulderpads]\")";
            RcParser rp = new TsvParser();
            WowItem result = rp.UnpackWowheadLink(teststr1);
            Assert.IsTrue(result.Name == "Helm of the Foreseen Protector", result.Name);
            Assert.IsTrue(result.ItemId == 143575);
            Assert.IsTrue(result.BonusIds.Count == 1);
            Assert.IsTrue(result.BonusIds.First().Matches(new BonusId(570)));

            result = rp.UnpackWowheadLink(teststr2);
            Assert.IsTrue(result.Name == "Netherbranded Shoulderpads");
            Assert.IsTrue(result.ItemId == 140917);
            Assert.IsTrue(result.BonusIds.Count == 4);
            HashSet<BonusId> bids = new HashSet<BonusId>();
            bids.Add(new BonusId(3517));
            bids.Add(new BonusId(41));
            bids.Add(new BonusId(1492));
            bids.Add(new BonusId(3528));
            foreach (BonusId checkId in bids)
            {
                bool present = false;
                foreach (BonusId id in result.BonusIds)
                {
                    if (id.Matches(checkId))
                    {
                        present = true;
                    }
                }
                Assert.IsTrue(present);
            }

            result = rp.UnpackWowheadLink(teststr3);
            Assert.IsTrue(result == WowItem.GetNullItem());

            try
            {
                result = rp.UnpackWowheadLink(teststr4);
                Assert.IsTrue(false);
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(true);
            }

            result = rp.UnpackWowheadLink(teststr5);
            Assert.IsTrue(result.Name == "Netherbranded Shoulderpads");
            Assert.IsTrue(result.ItemId == 140917);
            Assert.IsTrue(result.BonusIds.Count == 0);
        }
    }
}