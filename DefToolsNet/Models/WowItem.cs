using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefToolsNet.Models
{
    public class WowItem
    {
        public int ItemId;
        public string Name;
        public Zone Instance;
        public ICollection<BonusId> BonusIds;

        private static WowItem _nullItem;
        private static int _nullId = int.MinValue;
        private static string _nullName = "NULL";
        private static Zone _nullZone = Zone.Unknown;

        public WowItem(int itemId, string name, Zone instance, ICollection<BonusId> bonusIds)
        {
            this.ItemId = itemId;
            this.Name = name;
            this.Instance = instance;
            this.BonusIds = bonusIds;
        }

        public WowItem(int itemId, string name, Zone instance)
        {
            this.ItemId = itemId;
            this.Name = name;
            this.Instance = instance;
            this.BonusIds = new List<BonusId>();
        }

        public bool Matches(object obj)
        {
            if(obj == null)
            {
                return false;
            }
            WowItem i = obj as WowItem;
            if((object)i == null)
            {
                return false;
            }
            if (i.ItemId != this.ItemId) return false;
            if (String.Compare(this.Name, i.Name) != 0) return false;
            if (i.Instance != this.Instance) return false;
            HashSet<int> itemIds = new HashSet<int>();
            foreach (BonusId id in i.BonusIds)
            {
                itemIds.Add(id.Id);
            }
            if (itemIds.Count != this.BonusIds.Count) return false;
            foreach (BonusId id in this.BonusIds)
            {
                if (!itemIds.Contains(id.Id)) return false;
            }

            return true;
        }

        public static WowItem GetNullItem()
        {
            if(WowItem._nullItem == null)
            {
                WowItem._nullItem = new WowItem(_nullId, _nullName, _nullZone);
            }

            return WowItem._nullItem;
        }


    }
}
