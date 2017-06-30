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
    public class BonusIdTests
    {
        [TestMethod()]
        public void BonusIdTest()
        {
            BonusId bi = new BonusId(1, "effect");
            Assert.IsTrue(bi.Id == 1);
            Assert.IsTrue(string.Compare(bi.Effect,"effect") == 0);

            bi = new BonusId(10);
            Assert.IsTrue(bi.Id == 10);
            Assert.IsTrue(string.Compare(bi.Effect, "UNKNOWN") == 0);
        }


        [TestMethod()]
        public void MatchesTest()
        {
            BonusId bi1 = new BonusId(1, "effect");
            BonusId bi2 = new BonusId(1);
            BonusId bi3 = new BonusId(2, "effect");

            String testStr = "test";

            Assert.IsFalse(bi1.Matches(null));
            Assert.IsFalse(bi1.Matches(bi3));
            Assert.IsFalse(bi3.Matches(bi1));
            Assert.IsTrue(bi1.Matches(bi2));
            Assert.IsTrue(bi2.Matches(bi1));

            Assert.IsFalse(bi1.Matches(testStr));
        }
    }
}