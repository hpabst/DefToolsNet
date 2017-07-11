using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DefToolsNet.DB;

namespace DefToolsNet.Models
{
    public class DbControl
    {
        public static string DBNAME_DEFAULT = "";
        public static string DBNAME_TEST = "DefToolsTest";

        private string dbname;

        public DbControl(string dbname)
        {
            this.dbname = dbname;
        }

        public DbControl()
        {
            this.dbname = DBNAME_DEFAULT;
        }

        public bool CheckExistsInDb(object o)
        {
            BonusId b = o as BonusId;
            LootAward a = o as LootAward;
            WowItem i = o as WowItem;
            WowPlayer p = o as WowPlayer;
            if (b == null && a == null && i == null && p == null)
            {
                throw new ArgumentException("CheckExistsInDb only takes objects of type BonusId, LootAward, WowItem, or WowPlayer");
            }
            if (b != null)
            {
                return CheckExistsInDb(b);
            } else if (a != null)
            {
                return CheckExistsInDb(a);
            } else if (i != null)
            {
                return CheckExistsInDb(i);

            } else if (p != null)
            {
                return CheckExistsInDb(p);
            }
            throw new ArgumentException("CheckExistsInDb failed type checks.");
        }

        private bool CheckExistsInDb(BonusId id)
        {
            bool result;

            using (DefToolsContext ctx = new DefToolsContext(this.dbname))
            {
                var query = from b in ctx.BonusIds
                    where b.Id == id.Id
                    select b;
                    result = query.Any();
            }
            return result;
        }

        private bool CheckExistsInDb(LootAward award)
        {
            bool result = false;
            using (DefToolsContext ctx = new DefToolsContext(this.dbname))
            {
                var query = ctx.LootAwards.Where(a => a.AwardReason == award.AwardReason &&
                                                      a.AwardDate == award.AwardDate &&
                                                      a.Item.ItemId == award.Item.ItemId &&
                                                      a.Replacement1.ItemId == award.Replacement1.ItemId &&
                                                      a.Replacement2.ItemId == award.Replacement2.ItemId &&
                                                      a.Player.Name == award.Player.Name &&
                                                      a.Player.Realm == award.Player.Realm).Include(a => a.Player)
                    .Include(a => a.Item).Include(a => a.Replacement1).Include(a => a.Replacement2);
                if (query.Any())
                {
                    foreach (LootAward queried in query)
                    {
                        if (queried.Matches(award))
                            result = true;
                    }
                }
            }
            return result;
        }

        private bool CheckExistsInDb(WowItem item)
        {
            bool result = false;
            using (DefToolsContext ctx = new DefToolsContext(this.dbname))
            {
                var query = ctx.WowItems.Where(i => i.ItemId == item.ItemId).Select(i => new
                {
                    WowItem = i,
                    BonusIds = i.BonusIds
                }).ToList();
                foreach (var queried in query)
                {
                    queried.WowItem.BonusIds = queried.BonusIds;
                }
                if (query.Any())
                {
                    foreach (var q in query)
                    {
                        if (q.WowItem.Matches(item))
                            result = true;
                    }
                }
            }
            return result;
        }

        private bool CheckExistsInDb(WowPlayer player)
        {
            bool result;
            using (DefToolsContext ctx = new DefToolsContext(this.dbname))
            {
                var query = from p in ctx.WowPlayers
                    where p.Name == player.Name
                          && p.Realm == player.Realm
                          && p.PlayerClass == player.PlayerClass
                    select p;
                result = query.Any();
            }
            return result;
        }

        public ICollection<BonusId> Filter(ICollection<BonusId> ids)
        {
            ICollection<BonusId> retSet = new HashSet<BonusId>();
            foreach (BonusId id in ids)
            {
                if (!CheckExistsInDb(id))
                {
                    retSet.Add(id);
                }
            }
            return retSet;
        }

        public ICollection<LootAward> Filter(ICollection<LootAward> awards)
        {
            ICollection<LootAward> retSet = new HashSet<LootAward>();
            foreach (LootAward award in awards)
            {
                if (!CheckExistsInDb(award))
                {
                    retSet.Add(award);
                }
            }
            return retSet;
        }

        public ICollection<WowItem> Filter(ICollection<WowItem> items)
        {
            ICollection<WowItem> retSet = new HashSet<WowItem>();
            foreach (WowItem item in items)
            {
                if (!CheckExistsInDb(item))
                {
                    retSet.Add(item);
                }
            }
            return retSet;
        }

        public ICollection<WowPlayer> Filter(ICollection<WowPlayer> players)
        {
            ICollection<WowPlayer> retSet = new HashSet<WowPlayer>();
            foreach (WowPlayer player in players)
            {
                if (!CheckExistsInDb(player))
                {
                    retSet.Add(player);
                }
            }
            return retSet;
        }

        public ICollection<BonusId> FetchAllBonusId()
        {
            ICollection<BonusId> retCollection;
            using (DefToolsContext ctx = new DefToolsContext(this.dbname))
            {
                return ctx.BonusIds.ToList();
            }
        }

        public ICollection<LootAward> FetchAllLootAward()
        {
            using (DefToolsContext ctx = new DefToolsContext(this.dbname))
            {
                return ctx.LootAwards.Include(a => a.Player)
                    .Include(a => a.Item.BonusIds).Include(a => a.Replacement1.BonusIds).Include(a => a.Replacement2.BonusIds).ToList();
            }
        }

        public ICollection<WowItem> FetchAllItems()
        {
            using (DefToolsContext ctx = new DefToolsContext(this.dbname))
            {
                return ctx.WowItems.Include(w => w.BonusIds).ToList();
            }
        }

        public ICollection<WowPlayer> FetchAllPlayers()
        {
            using (DefToolsContext ctx = new DefToolsContext(this.dbname))
            {
                var query = from b in ctx.WowPlayers
                    select b;
                return query.ToList();
            }
        }

        public bool AddPlayer(WowPlayer player)
        {
            if (!CheckExistsInDb(player))
            {
                using (DefToolsContext ctx = new DefToolsContext(this.dbname))
                {
                    ctx.WowPlayers.Add(player);
                    ctx.SaveChanges();
                }
                return true;
            }
            return false;
        }

        public bool AddBonusId(BonusId bid)
        {
            if (!CheckExistsInDb(bid))
            {
                using (DefToolsContext ctx = new DefToolsContext(this.dbname))
                {
                    ctx.BonusIds.Add(bid);
                    ctx.SaveChanges();
                }
                return true;
            }
            return false;
        }

        public bool AddItem(WowItem item)
        {
            if (!CheckExistsInDb(item))
            {
                using (DefToolsContext ctx = new DefToolsContext(this.dbname))
                {
                    HashSet<BonusId> fetchedBids = new HashSet<BonusId>();
                    foreach (BonusId id in item.BonusIds)
                    {
                        var query = from b in ctx.BonusIds
                            where b.Id == id.Id
                            select b;
                        if (!query.Any())
                        {
                            AddBonusId(id);
                            fetchedBids.Add((from b in ctx.BonusIds
                                where b.Id == id.Id
                                select b).Single());
                        }
                        else
                        {
                            fetchedBids.Add(query.Single());
                        }
                    }
                    item.BonusIds = fetchedBids;
                    ctx.WowItems.Add(item);
                    ctx.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public bool AddLootAward(LootAward award)
        {
            if (!CheckExistsInDb(award))
            {
                using (DefToolsContext ctx = new DefToolsContext(this.dbname))
                {
                    if (!CheckExistsInDb(award.Item))
                    {
                        AddItem(award.Item);
                    }
                    if (!CheckExistsInDb(award.Replacement1))
                    {
                        AddItem(award.Replacement1);
                    }
                    if (!CheckExistsInDb(award.Replacement2))
                    {
                        AddItem(award.Replacement2);
                    }
                    if (!CheckExistsInDb(award.Player))
                    {
                        AddPlayer(award.Player);
                    }
                    WowItem referencedItem = (from i in ctx.WowItems
                        where i.WowItemId == award.Item.WowItemId
                        select i).Single();
                    WowItem referencedr1 = (from i in ctx.WowItems
                        where i.WowItemId == award.Replacement1.WowItemId
                        select i).Single();
                    WowItem referencedr2 = (from i in ctx.WowItems
                        where i.WowItemId == award.Replacement2.WowItemId
                        select i).Single();
                    WowPlayer referencedPlayer = (from p in ctx.WowPlayers
                                                  where p.Name == award.Player.Name &&
                                                  p.Realm == award.Player.Realm &&
                                                  p.PlayerClass == award.Player.PlayerClass
                                                  select p).Single();
                    award.Item = referencedItem;
                    award.Replacement1 = referencedr1;
                    award.Replacement2 = referencedr2;
                    award.Player = referencedPlayer;
                    ctx.LootAwards.Add(award);
                    ctx.SaveChanges();
                    return true;
                }
            }
            return false;
        }
    }
}
