using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefToolsNet.Models
{
    public class BonusId
    {
        [Key]
        public int BonusIdId { get; set; }
        public int Id { get; set; }
        public string Effect { get; set; }

        public ICollection<WowItem> WowItems { get; set; }//Required for EF many-to-many relationship.

        public BonusId(int id, string effect)
        {
            this.Id = id;
            this.Effect = effect;
        }

        public BonusId(int id)
        {
            this.Id = id;
            this.Effect = "UNKNOWN";
        }

        public bool Matches(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            BonusId i = obj as BonusId;
            if ((object)i == null)
            {
                return false;
            }

            return this.Id == i.Id;
        }
    }
}
