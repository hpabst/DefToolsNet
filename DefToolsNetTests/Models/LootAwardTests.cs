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
    public class LootAwardTests
    {
        [TestMethod()]
        public void LootAwardTest()
        {
            WowItem wi1 = new WowItem(1, "Item of the Monkey");
            WowItem wi2 = new WowItem(2, "Item of the Tiger");
            WowItem wi3 = WowItem.GetNullItem();
            LootAward la1 = new LootAward("Reason", DateTime.MinValue, wi1, wi2, wi3, WowPlayer.GetDefaultPlayer());

            Assert.IsTrue(la1.AwardReason == "Reason");
            Assert.IsTrue(la1.Item == wi1);
            Assert.IsTrue(la1.Replacement1 == wi2);
            Assert.IsTrue(la1.Replacement2 == WowItem.GetNullItem());
            Assert.IsTrue(la1.Player == WowPlayer.GetDefaultPlayer());
        }

        [TestMethod()]
        public void MatchesTest()
        {

            WowItem wi1 = new WowItem(1, "Item of the Monkey");
            WowItem wi2 = new WowItem(2, "Item of the Tiger");
            WowItem wi3 = WowItem.GetNullItem();
            WowPlayer wp1 = WowPlayer.GetDefaultPlayer();
            WowPlayer wp2 = new WowPlayer("Player", "Realm", WowClass.Druid);
            LootAward la1 = new LootAward("Reason", DateTime.MinValue, wi1, wi2, wi3, wp1);
            LootAward la2 = new LootAward("Reason2", DateTime.MinValue, wi1, wi2, wi3, wp1);
            LootAward la3 = new LootAward("Reason", DateTime.MaxValue, wi1, wi2, wi3, wp1);
            LootAward la4 = new LootAward("Reason", DateTime.MinValue, wi3, wi2, wi1, wp1);
            LootAward la5 = new LootAward("Reason", DateTime.MinValue, wi1, wi3, wi2, wp1);
            LootAward la6 = new LootAward("Reason", DateTime.MinValue, wi1, wi2, wi3, wp2);

            Assert.IsTrue(la1.Matches(la1));
            Assert.IsFalse(la1.Matches(null));
            Assert.IsFalse(la1.Matches("string"));
            Assert.IsFalse(la1.Matches(la2));
            Assert.IsFalse(la2.Matches(la1));
            Assert.IsFalse(la1.Matches(la3));
            Assert.IsFalse(la3.Matches(la1));
            Assert.IsFalse(la1.Matches(la4));
            Assert.IsFalse(la4.Matches(la1));
            Assert.IsTrue(la1.Matches(la5));
            Assert.IsTrue(la5.Matches(la1));
            Assert.IsFalse(la1.Matches(la6));
            Assert.IsFalse(la6.Matches(la1));
        }
    }
}