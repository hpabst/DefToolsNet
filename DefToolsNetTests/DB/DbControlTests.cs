using Microsoft.VisualStudio.TestTools.UnitTesting;
using DefToolsNet.Models;
using System;
using Effort;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DefToolsNet.DB;

namespace DefToolsNet.Models.Tests
{
    [TestClass()]
    public class DbControlTests
    {
        private static DbControl ctrl;
        private static DefToolsContext ctx;

        [TestInitialize]
        public void TestSetup()
        {
            ctrl = new DbControl(DbControl.DBNAME_TEST);
            ctx = new DefToolsContext(DbControl.DBNAME_TEST);
            ctx.LootAwards.RemoveRange(ctx.LootAwards);
            ctx.WowItems.RemoveRange(ctx.WowItems);
            ctx.WowPlayers.RemoveRange(ctx.WowPlayers);
            ctx.BonusIds.RemoveRange(ctx.BonusIds);
            ctx.SaveChanges();
        }

        [TestCleanup]
        public void TestTeardown()
        {
            ctx.LootAwards.RemoveRange(ctx.LootAwards);
            ctx.WowItems.RemoveRange(ctx.WowItems);
            ctx.WowPlayers.RemoveRange(ctx.WowPlayers);
            ctx.BonusIds.RemoveRange(ctx.BonusIds);
            ctx.SaveChanges();
            ctx.Dispose();
        }

        private static string RandStr(int length, Random randsrc)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz1234567890!@#$%^&*()-_=+[]{}\\|;:\"'";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[randsrc.Next(s.Length)]).ToArray());
        }

        private static DateTime RandDate(DateTime start, DateTime end, Random randsrc)
        {
            int range = (end - start).Days;
            return start.AddDays(randsrc.Next(range));
        }

        private static ICollection<BonusId> RandBonusIds(int count, Random randsrc)
        {
            List<BonusId> bids = new List<BonusId>();
            for (int i = 0; i < count; i++)
            {
                bids.Add(new BonusId(randsrc.Next(), RandStr(5, randsrc)));
            }

            return bids;
        }

        private static ICollection<WowPlayer> RandWowPlayers(int count, Random randsrc)
        {
            Array classes = Enum.GetValues(typeof(WowClass));
            List<WowPlayer> players = new List<WowPlayer>();
            for (int i = 0; i < count; i++)
            {
                players.Add(new WowPlayer(RandStr(10, randsrc), RandStr(10, randsrc), (WowClass)classes.GetValue(randsrc.Next(classes.Length))));
            }

            return players;
        }

        private static ICollection<WowItem> RandWowItems(int count, int maxBids, ICollection<BonusId> bids, Random randsrc)
        {
            if (maxBids > bids.Count)
            {
                throw new ArgumentException($"Must have at least as many bonus IDs as maxBids. maxBids = {count} and bids count = {bids.Count}");
            }
            List<WowItem> items = new List<WowItem>(count);
            List<BonusId> bidList = new List<BonusId>(bids);
            for (int i = 0; i < count; i++)
            {
                WowItem item = new WowItem(randsrc.Next(), RandStr(5, randsrc));
                int bidCount = randsrc.Next(0, 20);
                List<int> indices = new List<int>();
                while (indices.Count < bidCount)
                {
                    int next = randsrc.Next(bids.Count - 1);
                    if (!indices.Contains(next))
                        indices.Add(next);
                }
                for (int j = 0; j < bidCount; j++)
                {
                    item.BonusIds.Add(bidList[indices[j]]);
                }
                items.Add(item);
            }
            return items;
        }

        private static ICollection<WowItem> RandWowItems(int count, Random randsrc)
        {
            return RandWowItems(count, 50, RandBonusIds(50, randsrc), randsrc);
        }

        private static ICollection<LootAward> RandLootAwards(int count, ICollection<WowPlayer> players,
            ICollection<WowItem> items, Random randsrc)
        {
            List<WowPlayer> playerList = new List<WowPlayer>(players);
            List<WowItem> itemList = new List<WowItem>(items);
            List<LootAward> awards = new List<LootAward>(count);
            for (int i = 0; i < count; i++)
            {
                string reason = RandStr(randsrc.Next(5, 20), randsrc);
                DateTime awardDate = RandDate(new DateTime(2015, 01, 01), new DateTime(2017, 01, 01), randsrc);
                WowItem newItem;
                WowItem r1;
                WowItem r2;
                newItem = itemList[randsrc.Next(itemList.Count - 1)];
                r1 = newItem;
                while (r1 == newItem)
                {
                    r1 = itemList[randsrc.Next(itemList.Count - 1)];
                }
                if (randsrc.Next() % 2 == 0)
                {
                    r2 = WowItem.GetNullItem();
                }
                else
                {
                    r2 = r1;
                    while (!(r2 == r1 || r2 == newItem))
                    {
                        r2 = itemList[randsrc.Next(itemList.Count - 1)];
                    }
                }
                WowPlayer player = playerList[randsrc.Next(playerList.Count - 1)];
                LootAward award = new LootAward(reason, awardDate, newItem, r1, r2, player);
                awards.Add(award);
            }
            return awards;
        }

        private static ICollection<LootAward> RandLootAwards(int count, Random randsrc)
        {
            return RandLootAwards(count, RandWowPlayers(50, randsrc), RandWowItems(100, randsrc), randsrc);
        }

        [TestMethod()]
        public void DbControlTest()
        {
            DbControl dbctrl1 = new DbControl();
            DbControl dbctrl2 = new DbControl(DbControl.DBNAME_DEFAULT);
            DbControl dbctrl3 = new DbControl(DbControl.DBNAME_TEST);
        }

        [TestMethod()]
        public void CheckExistsInDbTest()
        {
            Random rand = new Random(4);
            List<BonusId> bids = RandBonusIds(200, rand).ToList();

            ctx.BonusIds.AddRange(bids);
            ctx.SaveChanges();

            foreach (BonusId b in bids)
            {
                Assert.IsTrue(ctrl.CheckExistsInDb(b));
            }

            List<BonusId> absentbids = RandBonusIds(200, rand).ToList();
            foreach (BonusId b in absentbids)
            {
                Assert.IsFalse(ctrl.CheckExistsInDb(b));
            }
            List<WowItem> itemsBidPresent = RandWowItems(50, 4, bids, rand).ToList();
            ctx.WowItems.AddRange(itemsBidPresent);
            ctx.SaveChanges();
            foreach (var i in itemsBidPresent)
            {
                Assert.IsTrue(ctrl.CheckExistsInDb(i));
            }
            List<WowItem> itemsBidAbsent = RandWowItems(50, 4, absentbids, rand).ToList();
            foreach (var i in itemsBidAbsent)
            {
                Assert.IsFalse(ctrl.CheckExistsInDb(i));
            }

            List<WowPlayer> players = RandWowPlayers(100, rand).ToList();
            ctx.WowPlayers.AddRange(players);
            ctx.SaveChanges();
            foreach (var p in players)
            {
                Assert.IsTrue(ctrl.CheckExistsInDb(p));
            }

            List<LootAward> awards = RandLootAwards(500, players, itemsBidPresent, rand).ToList();
            foreach (var l in awards)
            {
                Assert.IsFalse(ctrl.CheckExistsInDb(l));
                ctrl.AddLootAward(l);
            }
            foreach (var l in awards)
            {
                Assert.IsTrue(ctrl.CheckExistsInDb(l));
            }
            try
            {
                ctrl.CheckExistsInDb("some string");
                Assert.IsTrue(false);
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod()]
        public void FilterTest()
        {
            Random rand = new Random(5);
            List<BonusId> bids = RandBonusIds(200, rand).ToList();
            ctx.BonusIds.AddRange(bids);
            ctx.SaveChanges();

            List<BonusId> absentBids = RandBonusIds(50, rand).ToList();
            ICollection<BonusId> filteredBids = ctrl.Filter(bids.Concat(absentBids).ToList());
            Assert.IsTrue(filteredBids.Count == 50);
            foreach (var i in absentBids)
            {
                bool present = false;
                foreach (var j in filteredBids)
                {
                    if (i.Matches(j))
                    {
                        present = true;
                        break;
                    }
                }

                Assert.IsTrue(present);
            }
            List<WowPlayer> players = RandWowPlayers(100, rand).ToList();
            List<WowPlayer> absentPlayers = RandWowPlayers(200, rand).ToList();
            ctx.WowPlayers.AddRange(players);
            ctx.SaveChanges();

            ICollection<WowPlayer> filteredplayers = ctrl.Filter(players.Concat(absentPlayers).ToList());
            Assert.IsTrue(filteredplayers.Count == 200);
            foreach (var i in absentPlayers)
            {
                bool present = false;
                foreach (var j in filteredplayers)
                {
                    if (i.Matches(j))
                    {
                        present = true;
                        break;
                    }
                }

                Assert.IsTrue(present);
            }


            List<WowItem> items = RandWowItems(50, 4, bids, rand).ToList();
            foreach (var i in items)
            {
                Assert.IsTrue(ctrl.AddItem(i));
            }

            List<WowItem> absentItems = RandWowItems(50, 4, bids, rand).ToList();
            List<WowItem> allItems = items.Concat(absentItems).ToList();
            ICollection<WowItem> filteredItems = ctrl.Filter(allItems);
            Assert.IsTrue(filteredItems.Count == 50);
            foreach (var i in absentItems)
            {
                bool present = false;
                foreach (var j in filteredItems)
                {
                    if (i.Matches(j))
                    {
                        present = true;
                        break;
                    }
                }
                Assert.IsTrue(present);
            }

            List<LootAward> awards = RandLootAwards(500, players, allItems, rand).ToList();
            List<LootAward> absentAwards = RandLootAwards(30, players, allItems, rand).ToList();
            foreach (LootAward award in awards)
            {
                Assert.IsTrue(ctrl.AddLootAward(award));
            }

            ICollection<LootAward> filteredAwards = ctrl.Filter(awards.Concat(absentAwards).ToList());
            Assert.IsTrue(filteredAwards.Count == 30);
            foreach (var i in absentAwards)
            {
                bool present = false;
                foreach (var j in filteredAwards)
                {
                    if (i.Matches(j))
                    {
                        present = true;
                        break;
                    }
                }

                Assert.IsTrue(present);
            }

        }


        [TestMethod()]
        public void FetchAllBonusIdTest()
        {
            Random rand = new Random(5);
            List<BonusId> bids = RandBonusIds(200, rand).ToList();
            ctx.BonusIds.AddRange(bids);
            ctx.SaveChanges();
            ICollection<BonusId> retrievedBids = ctrl.FetchAllBonusId();
            foreach (BonusId bid in bids)
            {
                bool present = false;
                foreach (BonusId fetched in retrievedBids)
                {
                    if (fetched.Matches(bid))
                    {
                        present = true;
                        break;
                    }
                }
                Assert.IsTrue(present);
            }
        }

        [TestMethod()]
        public void FetchAllLootAwardTest()
        {
            Random rand = new Random(5);
            List<BonusId> bids = RandBonusIds(200, rand).ToList();

            ctx.BonusIds.AddRange(bids);
            ctx.SaveChanges();

            List<BonusId> absentbids = RandBonusIds(200, rand).ToList();
            List<WowItem> itemsBidPresent = RandWowItems(50, 4, bids, rand).ToList();

            List<WowItem> itemsBidAbsent = RandWowItems(50, 4, absentbids, rand).ToList();

            List<WowPlayer> players = RandWowPlayers(100, rand).ToList();
            List<WowItem> allItems = itemsBidAbsent.Concat(itemsBidPresent).ToList();
            List<LootAward> awards = RandLootAwards(500, players, allItems, rand).ToList();
            ctx.LootAwards.AddRange(awards);
            ctx.SaveChanges();
            ICollection<LootAward> retrievedAwards = ctrl.FetchAllLootAward();
            foreach (LootAward i in awards)
            {
                bool present = false;
                foreach (LootAward j in retrievedAwards)
                {
                    if (j.Matches(i))
                    {
                        present = true;
                        break;
                    }
                }
                Assert.IsTrue(present);
            }
        }

        [TestMethod()]
        public void FetchAllItemsTest()
        {
            Random rand = new Random(5);
            List<BonusId> bids = RandBonusIds(200, rand).ToList();

            ctx.BonusIds.AddRange(bids);
            ctx.SaveChanges();

            List<WowItem> items = RandWowItems(50, 4, bids, rand).ToList();

            ctx.WowItems.AddRange(items);
            ctx.SaveChanges();
            ICollection<WowItem> retrievedItems = ctrl.FetchAllItems();

            foreach (WowItem i in items)
            {
                bool present = false;
                foreach (WowItem j in retrievedItems)
                {
                    if (j.Matches(i))
                    {
                        present = true;
                        break;
                    }
                }

                Assert.IsTrue(present);
            }

        }

        [TestMethod()]
        public void FetchAllPlayersTest()
        {
            Random rand = new Random(5);
            List<WowPlayer> players = RandWowPlayers(100, rand).ToList();
            ctx.WowPlayers.AddRange(players);
            ctx.SaveChanges();
            ICollection<WowPlayer> retrievedPlayers = ctrl.FetchAllPlayers();
            foreach (WowPlayer i in players)
            {
                bool present = false;
                foreach (WowPlayer j in retrievedPlayers)
                {
                    if (i.Matches(j))
                    {
                        present = true;
                        break;
                    }
                }

                Assert.IsTrue(present);
            }
        }

        [TestMethod()]
        public void AddPlayerTest()
        {
            Random rand = new Random(5);
            List<WowPlayer> players = RandWowPlayers(100, rand).ToList();
            foreach (WowPlayer player in players)
            {
                Assert.IsTrue(ctrl.AddPlayer(player));
            }
            foreach (WowPlayer player in players)
            {
                Assert.IsFalse(ctrl.AddPlayer(player));
            }
        }

        [TestMethod()]
        public void AddBonusIdTest()
        {
            Random rand = new Random(5);
            List<BonusId> bids = RandBonusIds(200, rand).ToList();
            foreach (BonusId bid in bids)
            {
                Assert.IsTrue(ctrl.AddBonusId(bid));
            }
            foreach (BonusId bid in bids)
            {
                Assert.IsFalse(ctrl.AddBonusId(bid));
            }
        }

        [TestMethod()]
        public void AddItemTest()
        {
            Random rand = new Random(5);
            List<BonusId> bids = RandBonusIds(200, rand).ToList();

            ctx.BonusIds.AddRange(bids);
            ctx.SaveChanges();

            List<BonusId> absentbids = RandBonusIds(200, rand).ToList();
            List<WowItem> itemsBidPresent = RandWowItems(50, 4, bids, rand).ToList();
            List<WowItem> itemsBidAbsent = RandWowItems(50, 4, absentbids, rand).ToList();

            foreach (WowItem item in itemsBidPresent)
            {
                Assert.IsTrue(ctrl.AddItem(item));
            }

            foreach (WowItem item in itemsBidAbsent)
            {
                Assert.IsTrue(ctrl.AddItem(item));
                foreach (BonusId bid in item.BonusIds)
                {
                    var query = from b in ctx.BonusIds
                        where b.Id == bid.Id
                        select b;
                    Assert.IsTrue(query.Any());
                }
            }

            foreach (WowItem item in itemsBidAbsent)
            {
                Assert.IsFalse(ctrl.AddItem(item), $"Failed on item Id {item.ItemId} name {item.Name}");
            }

            foreach (WowItem item in itemsBidPresent)
            {
                Assert.IsFalse(ctrl.AddItem(item));
            }

        }

        [TestMethod()]
        public void GetAwardsByPlayerTest()
        {
            WowPlayer p1 = new WowPlayer("Test1","Realm1", WowClass.DEMONHUNTER);
            WowPlayer p2 = new WowPlayer("Test2", "Realm1", WowClass.DRUID);
            WowPlayer p3 = new WowPlayer("Test3", "Realm2", WowClass.DRUID);
            List<LootAward> p1Awards = new List<LootAward>();
            List<LootAward> p2Awards = new List<LootAward>();
            List<LootAward> p3Awards = new List<LootAward>();
            Random rand = new Random(5);
            List<BonusId> bids = RandBonusIds(200, rand).ToList();

            ctx.BonusIds.AddRange(bids);
            ctx.SaveChanges();

            List<BonusId> absentbids = RandBonusIds(200, rand).ToList();

            List<WowItem> itemsBidPresent = RandWowItems(50, 4, bids, rand).ToList();

            List<WowItem> itemsBidAbsent = RandWowItems(50, 4, absentbids, rand).ToList();

            List<WowItem> allItems = itemsBidAbsent.Concat(itemsBidPresent).ToList();
            List<LootAward> awards = new List<LootAward>();
            for (int i = 0; i < 500; i++)
            {
                string reason = RandStr(10, rand);
                DateTime date = RandDate(new DateTime(1995, 01, 01), DateTime.Today, rand);
                int itemIndex = rand.Next(allItems.Count);
                int r1Index = itemIndex;
                int r2Index = itemIndex;
                while (r1Index == itemIndex)
                {
                    r1Index = rand.Next(allItems.Count);
                }
                while (r2Index == itemIndex || r2Index == r1Index)
                {
                    r2Index = rand.Next(allItems.Count);
                }
                WowItem item = allItems[itemIndex];
                WowItem r1 = allItems[r1Index];
                WowItem r2 = allItems[r2Index];
                if (rand.Next() % 2 == 0)
                {
                    r2 = WowItem.GetNullItem();
                }
                int next = rand.Next();
                LootAward award;
                if (next % 3 == 0)
                {
                    award = new LootAward(reason, date, item, r1, r2, p1);
                    p1Awards.Add(award);
                }
                else if (next % 3 == 1)
                {
                    award = new LootAward(reason, date, item, r1, r2, p2);
                    p2Awards.Add(award);
                }
                else
                {
                    award = new LootAward(reason, date, item, r1, r2, p3);
                    p3Awards.Add(award);
                }
                awards.Add(award);
            }
            foreach (LootAward award in awards)
            {
                Assert.IsTrue(ctrl.AddLootAward(award));
            }

            var returnedP1 = ctrl.GetAwardsByPlayer(p1);
            Assert.IsTrue(returnedP1.Count == p1Awards.Count);
            foreach (var i in p1Awards)
            {
                bool present = false;
                foreach (var j in returnedP1)
                {
                    if (i.Matches(j))
                    {
                        present = true;
                    }
                }
                Assert.IsTrue(present);
            }

            var returnedP2 = ctrl.GetAwardsByPlayer(p2);
            Assert.IsTrue(returnedP2.Count == p2Awards.Count);
            foreach (var i in p2Awards)
            {
                bool present = false;
                foreach (var j in returnedP2)
                {
                    if (i.Matches(j))
                    {
                        present = true;
                    }
                }
                Assert.IsTrue(present);
            }

            var returnedP3 = ctrl.GetAwardsByPlayer(p3);
            Assert.IsTrue(returnedP3.Count == p3Awards.Count);
            foreach (var i in p3Awards)
            {
                bool present = false;
                foreach (var j in returnedP3)
                {
                    if (i.Matches(j))
                    {
                        present = true;
                    }
                }
                Assert.IsTrue(present);
            }
        }

        [TestMethod()]
        public void AddLootAwardTest()
        {
            Random rand = new Random(5);
            List<BonusId> bids = RandBonusIds(200, rand).ToList();
            ctx.BonusIds.AddRange(bids);
            ctx.SaveChanges();
            List<BonusId> absentbids = RandBonusIds(200, rand).ToList();
            List<WowItem> itemsBidPresent = RandWowItems(50, 4, bids, rand).ToList();
            List<WowItem> itemsBidAbsent = RandWowItems(50, 4, absentbids, rand).ToList();

            List<WowPlayer> players = RandWowPlayers(100, rand).ToList();
            List<WowItem> allItems = itemsBidAbsent.Concat(itemsBidPresent).ToList();
            List<LootAward> awards = RandLootAwards(500, players, allItems, rand).ToList();

            foreach (LootAward award in awards)
            {
                Assert.IsTrue(ctrl.AddLootAward(award));
            }

            foreach (LootAward award in awards)
            {
                Assert.IsFalse(ctrl.AddLootAward(award));
            }
        }
    }
}