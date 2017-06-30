using System;
using System.Collections.Generic;
using System.Globalization;
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
            DateTime awardDate = DateTime.ParseExact(components[1], "dd/MM/yy", CultureInfo.InvariantCulture);
            WowClass playerClass = (WowClass)Enum.Parse(typeof(WowClass), components[8]);
            WowItem item = this.UnpackWowheadLink(components[3]);
            WowItem replace1 = this.UnpackWowheadLink(components[11]);
            WowItem replace2 = this.UnpackWowheadLink(components[12]);
            return new LootAward(awardReason, awardDate, item, replace1, replace2, new WowPlayer(playerName, playerRealm, playerClass));
        }
    }
}
