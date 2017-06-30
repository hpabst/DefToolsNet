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
    public class WowPlayerTests
    {
        [TestMethod()]
        public void WowPlayerTest()
        {
            WowPlayer wp1 = new WowPlayer("Player", "Shadowsong", WowClass.DeathKnight);
            Assert.IsTrue(wp1.PlayerClass == WowClass.DeathKnight);
            Assert.IsTrue(wp1.Realm == "Shadowsong");
            Assert.IsTrue(wp1.Name == "Player");
        }

        [TestMethod()]
        public void MatchesTest()
        {
            WowPlayer wp1 = new WowPlayer("Player", "Shadowsong", WowClass.DeathKnight);
            WowPlayer wp2 = new WowPlayer("Player", "Shadowsong", WowClass.DemonHunter);
            WowPlayer wp3 = new WowPlayer("Player2", "Shadowsong", WowClass.DeathKnight);
            WowPlayer wp4 = new WowPlayer("Player", "BoreanTundra", WowClass.DeathKnight);
            string testStr = "test";

            Assert.IsFalse(wp1.Matches(null));
            Assert.IsFalse(wp1.Matches(testStr));
            Assert.IsTrue(wp1.Matches(wp1));
            Assert.IsFalse(wp1.Matches(wp2));
            Assert.IsFalse(wp2.Matches(wp1));
            Assert.IsFalse(wp1.Matches(wp3));
            Assert.IsFalse(wp3.Matches(wp1));
            Assert.IsFalse(wp1.Matches(wp4));
            Assert.IsFalse(wp4.Matches(wp1));
        }

        [TestMethod()]
        public void GetDefaultPlayerTest()
        {
            WowPlayer wp1 = WowPlayer.GetDefaultPlayer();
            WowPlayer wp2 = WowPlayer.GetDefaultPlayer();
            Assert.IsTrue(wp1 == wp2);
        }
    }
}