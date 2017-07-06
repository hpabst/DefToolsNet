using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefToolsNet.Models
{
    public class WowItem
    {
        [Key]
        public int WowItemId { get; set; }
        public int ItemId { get; set; }
        public string Name { get; set; }
        public ICollection<BonusId> BonusIds { get; set; }

        private static WowItem _nullItem;
        private static int _nullId = int.MinValue;
        private static string _nullName = "NULL";

        public WowItem(int itemId, string name, ICollection<BonusId> bonusIds)
        {
            this.ItemId = itemId;
            this.Name = name;
            this.BonusIds = bonusIds;
        }

        public WowItem(int itemId, string name)
        {
            this.ItemId = itemId;
            this.Name = name;
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
                WowItem._nullItem = new WowItem(_nullId, _nullName);
            }

            return WowItem._nullItem;
        }


    }
}
