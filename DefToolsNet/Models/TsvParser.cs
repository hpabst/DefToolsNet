using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefToolsNet.Models
{
    public class TsvParser : RcParser
    {
        public TsvParser()
        {
            return;
        }

        public override ICollection<LootAward> ParseImportText(string text)
        {
            throw new NotImplementedException();
        }

        public override LootAward ParseLine(string text)
        {
            string[] components = text.Split('\t');
            string playerName = components[0].Split('-')[0];
            string playerRealm = components[0].Split('-')[1];
            string awardReason = components[6];
            DateTime awardDate = Convert.ToDateTime(components[1]);
            WowClass playerClass = (WowClass)Enum.Parse(typeof(WowClass), components[8]);
            WowItem item = this.UnpackItemString("item:" + components[5]);
            throw new NotImplementedException();
        }
    }
}
