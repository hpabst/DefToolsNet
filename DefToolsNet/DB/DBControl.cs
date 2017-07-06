using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DefToolsNet.DB;

namespace DefToolsNet.Models
{
    public static class DbControl
    {

        public static bool CheckExistsInDb(object o)
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

        private static bool CheckExistsInDb(BonusId id)
        {
            bool result;
            using (DefToolsContext ctx = new DefToolsContext())
            {
                var query = from b in ctx.BonusIds
                    where b.Id == id.Id
                    select b;
                    result = query.Any();
            }
            return result;
        }

        private static bool CheckExistsInDb(LootAward award)
        {
            bool result = false;
            using (DefToolsContext ctx = new DefToolsContext())
            {
                var query = from a in ctx.LootAwards
                    where a.AwardReason == award.AwardReason
                          && a.AwardDate.Date == award.AwardDate.Date
                          && a.Item.ItemId == award.Item.ItemId
                          && a.Replacement1.ItemId == award.Replacement1.ItemId
                          && a.Replacement2.ItemId == award.Replacement2.ItemId
                          && a.Player.Name == award.Player.Name
                          && a.Player.Realm == award.Player.Realm
                    select a;
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

        private static bool CheckExistsInDb(WowItem item)
        {
            bool result = false;
            using (DefToolsContext ctx = new DefToolsContext())
            {
                var query = from i in ctx.WowItems
                    where i.ItemId == item.ItemId
                    select i;
                bool exists = query.Any();
                if (exists)
                {
                    WowItem existing = query.First();
                    result = existing.Matches(item);
                }
            }
            return result;
        }

        private static bool CheckExistsInDb(WowPlayer player)
        {
            bool result;
            using (DefToolsContext ctx = new DefToolsContext())
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

        public static ICollection<BonusId> Filter(ICollection<BonusId> ids)
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

        public static ICollection<LootAward> Filter(ICollection<LootAward> awards)
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

        public static ICollection<WowItem> Filter(ICollection<WowItem> items)
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

        public static ICollection<WowPlayer> Filter(ICollection<WowPlayer> players)
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

        public static ICollection<BonusId> FetchAllBonusId()
        {
            using (DefToolsContext ctx = new DefToolsContext())
            {
                var query = from b in ctx.BonusIds
                    select b;
                return query.ToList();
            }
        }

        public static ICollection<LootAward> FetchAllLootAward()
        {
            using (DefToolsContext ctx = new DefToolsContext())
            {
                var query = from b in ctx.LootAwards
                    select b;
                return query.ToList();
            }
        }

        public static ICollection<WowItem> FetchAllItems()
        {
            using (DefToolsContext ctx = new DefToolsContext())
            {
                var query = from b in ctx.WowItems
                    select b;
                return query.ToList();
            }
        }

        public static ICollection<WowPlayer> FetchAllPlayers()
        {
            using (DefToolsContext ctx = new DefToolsContext())
            {
                var query = from b in ctx.WowPlayers
                    select b;
                return query.ToList();
            }
        }
    }
}
