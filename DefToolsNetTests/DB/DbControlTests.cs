﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using DefToolsNet.Models;
using System;
using Effort;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void FilterTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void FilterTest1()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void FilterTest2()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void FilterTest3()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void FetchAllBonusIdTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void FetchAllLootAwardTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void FetchAllItemsTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void FetchAllPlayersTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void AddPlayerTest()
        {
            Random rand = new Random(5);
            Array classes = Enum.GetValues(typeof(WowClass));
            List<WowPlayer> players = new List<WowPlayer>();
            for (int i = 0; i < 100; i++)
            {
                players.Add(new WowPlayer(RandStr(10, rand), RandStr(10, rand),(WowClass)classes.GetValue(rand.Next(classes.Length))));
            }
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
            List<BonusId> bids = new List<BonusId>();
            for (int i = 0; i < 200; i++)
            {
                bids.Add(new BonusId(i + 100, RandStr(5, rand)));
            }

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
            List<BonusId> bids = new List<BonusId>();
            for (int i = 0; i < 200; i++)
            {
                bids.Add(new BonusId(i + 100, RandStr(5, rand)));
            }

            ctx.BonusIds.AddRange(bids);
            ctx.SaveChanges();

            List<BonusId> absentbids = new List<BonusId>();
            for (int i = 0; i < 200; i++)
            {
                absentbids.Add(new BonusId(i + 500, RandStr(5, rand)));
            }

            List<WowItem> itemsBidPresent = new List<WowItem>();
            for (int i = 0; i < 50; i++)
            {
                WowItem item = new WowItem(i + 100, RandStr(10, rand));
                for (int j = i * 4; j < i * 4 + 4; j++)
                {
                    item.BonusIds.Add(bids[j]);
                }
                itemsBidPresent.Add(item);
            }

            List<WowItem> itemsBidAbsent = new List<WowItem>();
            for (int i = 0; i < 50; i++)
            {
                WowItem item = new WowItem(i + 100, RandStr(10, rand));
                for (int j = i * 4; j < i * 4 + 4; j++)
                {
                    item.BonusIds.Add(absentbids[j]);
                }
                itemsBidAbsent.Add(item);
            }

            foreach (WowItem item in itemsBidPresent)
            {
                Assert.IsTrue(ctrl.AddItem(item));
            }

            foreach (WowItem item in itemsBidAbsent)
            {
                Assert.IsTrue(ctrl.AddItem(item));
            }

            foreach (BonusId bid in absentbids)
            {
                var query = from b in ctx.BonusIds
                    where b.Id == bid.Id &&
                          b.Effect == bid.Effect
                    select b;
                Assert.IsTrue(query.Any());
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
        public void AddLootAwardTest()
        {
            Random rand = new Random(5);
            List<BonusId> bids = new List<BonusId>();
            for (int i = 0; i < 200; i++)
            {
                bids.Add(new BonusId(i + 100, RandStr(5, rand)));
            }

            ctx.BonusIds.AddRange(bids);
            ctx.SaveChanges();

            List<BonusId> absentbids = new List<BonusId>();
            for (int i = 0; i < 200; i++)
            {
                absentbids.Add(new BonusId(i + 500, RandStr(5, rand)));
            }

            List<WowItem> itemsBidPresent = new List<WowItem>();
            for (int i = 0; i < 50; i++)
            {
                WowItem item = new WowItem(i + 100, RandStr(10, rand));
                for (int j = i * 4; j < i * 4 + 4; j++)
                {
                    item.BonusIds.Add(bids[j]);
                }
                itemsBidPresent.Add(item);
            }

            List<WowItem> itemsBidAbsent = new List<WowItem>();
            for (int i = 0; i < 50; i++)
            {
                WowItem item = new WowItem(i + 100, RandStr(10, rand));
                for (int j = i * 4; j < i * 4 + 4; j++)
                {
                    item.BonusIds.Add(absentbids[j]);
                }
                itemsBidAbsent.Add(item);
            }

            Array classes = Enum.GetValues(typeof(WowClass));
            List<WowPlayer> players = new List<WowPlayer>();
            for (int i = 0; i < 100; i++)
            {
                players.Add(new WowPlayer(RandStr(10, rand), RandStr(10, rand), (WowClass)classes.GetValue(rand.Next(classes.Length))));
            }
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
                WowPlayer player = players[rand.Next(players.Count)];
                awards.Add(new LootAward(reason, date, item, r1, r2, player));
            }

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