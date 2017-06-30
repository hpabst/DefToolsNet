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
    public class WowItemTests
    {
        [TestMethod()]
        public void WowItemTest()
        {
            WowItem wi1 = new WowItem(1, "Silent Jerkin of the Monkey", Zone.TombOfSargeras);
            Assert.IsTrue(wi1.ItemId == 1);
            Assert.IsTrue(wi1.Instance == Zone.TombOfSargeras);
            Assert.IsTrue(string.Compare(wi1.Name, "Silent Jerkin of the Monkey") == 0);
            Assert.IsTrue(wi1.BonusIds.Count == 0);

            List<BonusId> bis = new List<BonusId>();
            bis.Add(new BonusId(1));
            bis.Add(new BonusId(3));
            bis.Add(new BonusId(int.MaxValue));
            WowItem wi2 = new WowItem(1, "Silent Jerkin of the Monkey", Zone.TheNighthold, bis);
            Assert.IsTrue(wi2.ItemId == 1);
            Assert.IsTrue(wi2.Instance == Zone.TheNighthold);
            Assert.IsTrue(string.Compare(wi2.Name, "Silent Jerkin of the Monkey") == 0);
            Assert.IsTrue(wi2.BonusIds.Count == 3);
        }


        [TestMethod()]
        public void MatchesTest()
        {
            WowItem wi1 = new WowItem(1, "Silent Jerkin of the Monkey", Zone.TombOfSargeras);
            List<BonusId> bis = new List<BonusId>();
            bis.Add(new BonusId(1));
            bis.Add(new BonusId(3));
            bis.Add(new BonusId(int.MaxValue));

            List<BonusId> bis2 = new List<BonusId>();
            bis2.Add(new BonusId(1));
            bis2.Add(new BonusId(2));
            bis2.Add(new BonusId(3));
            bis2.Add(new BonusId(int.MaxValue));

            List<BonusId> bis3 = new List<BonusId>();
            bis3.Add(new BonusId(1));
            bis3.Add(new BonusId(2));
            bis3.Add(new BonusId(int.MaxValue));
            WowItem wi2 = new WowItem(1, "Silent Jerkin of the Monkey", Zone.TheNighthold, bis);
            WowItem wi3 = new WowItem(2, "Noisy Jerkin of the Monkey", Zone.TheNighthold, bis);
            string testStr = "test";

            WowItem wi4 = new WowItem(1, "Quiet Jerkin of the Monkey", Zone.TombOfSargeras);

            WowItem wi5 = new WowItem(1, "Silent Jerkin of the Monkey", Zone.TheNighthold, bis2);

            WowItem wi6 = new WowItem(1, "Silent Jerkin of the Monkey", Zone.TheNighthold, bis3);

            Assert.IsTrue(wi1.Matches(wi1));
            Assert.IsFalse(wi1.Matches(null));
            Assert.IsFalse(wi1.Matches(testStr));

            Assert.IsFalse(wi1.Matches(wi2));
            Assert.IsFalse(wi2.Matches(wi1));

            Assert.IsTrue(wi2.Matches(wi2));
            Assert.IsFalse(wi1.Matches(wi3));
            Assert.IsFalse(wi2.Matches(wi3));
            Assert.IsFalse(wi4.Matches(wi1));
            Assert.IsFalse(wi4.Matches(wi2));

            Assert.IsFalse(wi2.Matches(wi5));
            Assert.IsFalse(wi5.Matches(wi2));

            Assert.IsFalse(wi2.Matches(wi6));
            Assert.IsFalse(wi6.Matches(wi2));
        }

        [TestMethod()]
        public void GetNullItemTest()
        {
            WowItem wi1 = WowItem.GetNullItem();
            WowItem wi2 = WowItem.GetNullItem();
            Assert.IsTrue(wi1 == wi2);
        }
    }
}