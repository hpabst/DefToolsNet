using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefToolsNet.Models
{
    public class LootAward
    {
        public string AwardReason;
        public DateTime AwardDate;
        public WowItem Item;
        public WowItem Replacement1;
        public WowItem Replacement2;
        public WowPlayer Player;

        public LootAward(string awardReason, DateTime awardDate, WowItem item, WowItem r1, WowItem r2, WowPlayer player)
        {
            this.AwardReason = awardReason;
            this.AwardDate = awardDate;
            this.Item = item;
            this.Replacement1 = r1;
            this.Replacement2 = r2;
            this.Player = player;
            return;
        }

        public bool Matches(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            LootAward i = obj as LootAward;
            if ((object)i == null)
            {
                return false;
            }

            return String.Compare(this.AwardReason.ToLower(), i.AwardReason.ToLower()) == 0 &&
                    this.AwardDate.CompareTo(i.AwardDate) == 0 &&
                    this.Item.Matches(i.Item) &&
                    (this.Replacement1.Matches(i.Replacement1) || this.Replacement1.Matches(i.Replacement2)) &&
                    (this.Replacement2.Matches(i.Replacement2) || this.Replacement2.Matches(i.Replacement1)) &&
                    this.Player.Matches(i.Player);
        }
    }
}
