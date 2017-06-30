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
    public class TsvParserTests
    {
        [TestMethod()]
        public void ParseImportTextTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ParseLineTest()
        {
            TsvParser tp = new TsvParser();
            string testStr1 =
                "Vt-Shadowsong\t01/06/17\t20:57:16\t=HYPERLINK(\"https://www.wowhead.com/item=140820&bonus=3517:1492:3528\",\"[Phial of Fel Blood]\")\t140820\t140820::::::::110:264::5:3:3517:1492:3528:::\tGreed/OS\t2\tDEATHKNIGHT\tThe Nighthold-Heroic\tnil\t=HYPERLINK(\"https://www.wowhead.com/item=0\",\"nil\")\t=HYPERLINK(\"https://www.wowhead.com/item=0\",\"nil\")\t5\tfalse";
            LootAward la = tp.ParseLine(testStr1);
            Assert.IsTrue(la.Player.Name == "Vt");
            Assert.IsTrue(la.Player.Realm == "Shadowsong");
            Assert.IsTrue(la.Player.PlayerClass == WowClass.DeathKnight);
            Assert.IsTrue(la.AwardReason == "Greed/OS");
            Assert.IsTrue(la.AwardDate.Equals(new DateTime(2017, 06, 01, 20, 57, 16)));
            Assert.IsTrue(la.Item.Name == "Phial of Fel Blood");
            Assert.IsTrue(la.Item.ItemId == 140820);
            Assert.IsTrue(la.Item.Instance == Zone.TheNighthold);
            Assert.IsTrue(la.Item.BonusIds.Count == 3);
            HashSet<int> bids = new HashSet<int>();
            foreach (BonusId bid in la.Item.BonusIds)
            {
                bids.Add(bid.Id);
            }
            Assert.IsTrue(bids.Contains(3517));
            Assert.IsTrue(bids.Contains(1492));
            Assert.IsTrue(bids.Contains(3528));
            Assert.IsTrue(la.Replacement1 == WowItem.GetNullItem());
            Assert.IsTrue(la.Replacement2 == WowItem.GetNullItem());
        }
    }
}