using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefToolsNet.Models
{
    public class BonusId
    {
        public int Id;
        public string Effect;

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
