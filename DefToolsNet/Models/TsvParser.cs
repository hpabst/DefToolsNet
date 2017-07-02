using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            string[] lines = text.Split('\n');
            HashSet<LootAward> awards = new HashSet<LootAward>();
            //Long-ass regex to make sure that the string we're checking matches the format we're expecting
            string formatRegex =
                "([aA-zZ]+)-([aA-zZ]+)\t([0-9]+)/([0-9]+)/([0-9]+)\t([0-9-:]+)\t=HYPERLINK\\(\"https:\\/\\/www.wowhead.com\\/item=([0-9]+)(&bonus=([0-9-:]+))?\",\"([\' \\/\\\\0-9aA-zZ]+)\"\\)\t([0-9]+)\t([0-9-:]+)\t([ \\/\\\\0-9aA-zZ]+)\t[(0-9)]\t([aA-zZ]+)\t([ \\-aA-zZ]+)\t([aA-zZ]+)\t=HYPERLINK\\(\"https:\\/\\/www.wowhead.com\\/item=([0-9]+)(&bonus=([0-9-:]+))?\",\"([\' \\/\\\\0-9aA-zZ]+)\"\\)\t=HYPERLINK\\(\"https:\\/\\/www.wowhead.com\\/item=([0-9]+)(&bonus=([0-9-:]+))?\",\"([\' \\/\\\\0-9aA-zZ]+)\"\\)\t([0-9]+)\t([aA-zZ]+)";
            foreach (string line in lines)
            {
                Match match = Regex.Match(line, formatRegex);
                if (match.Success)
                {
                    awards.Add(this.ParseLine(line));
                }
            }

            return awards;
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
