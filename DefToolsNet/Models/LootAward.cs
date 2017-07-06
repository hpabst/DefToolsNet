using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefToolsNet.Models
{
    public class LootAward
    {
        [Key]
        public int LootAwardId { get; set; }
        public string AwardReason { get; set; }
        public DateTime AwardDate { get; set; }
        public WowItem Item { get; set; }
        public WowItem Replacement1 { get; set; }
        public WowItem Replacement2 { get; set; }
        public WowPlayer Player { get; set; }

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
