using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DefToolsNet.Models
{
    public abstract class RcParser
    {

        public abstract ICollection<LootAward> ParseImportText(String text);

        public abstract LootAward ParseLine(String text);

        /// <summary>
        /// Unpack a Wow formatted item string into a WowItem object.
        /// </summary>
        /// <param name="text">Wow formatted item string.</param>
        /// <returns>WowItem object representing the passed in string.</returns>
        public WowItem UnpackItemString(String text)
        {
            //As of 7.0.3, the format string follows:
            //item:itemID:enchantID:gemID1:gemID2:gemID3:gemID4:suffixID:uniqueID:linkLevel:specializationID:upgradeTypeID:instanceDifficultyID:numBonusIDs[:bonusID1:bonusID2:...][:upgradeValue1:upgradeValue2:...]:relic1NumBonusIDs[:relic1BonusID1:relic1BonusID2:...]:relic2NumBonusIDs[:relic2BonusID1:relic2BonusID2:...]:relic3NumBonusIDs[:relic3BonusID1:relic3BonusID2:...]
            String[] components = text.Split(':');
            if (components.Length < 14)
            {
                throw new ArgumentException();
            }
            int id = int.Parse(components[1]);
            List<BonusId> bonuses = new List<BonusId>();
            int numBonus = int.Parse(components[13]);
            for (int i = 14; i < (14 + numBonus); i++)
            {
                int bId = int.Parse(components[i]);
                bonuses.Add(new BonusId(bId));
            }
            return new WowItem(id, "UNKNOWN", Zone.Unknown, bonuses);
        }

        /// <summary>
        /// Unpack a Wowhead item hyperlink into a WowItem object.
        /// </summary>
        /// <param name="text">Wowhead hyperlink.</param>
        /// <returns>WowItem object representing the passed in string.</returns>
        public WowItem UnpackWowheadLink(string text)
        {
            string urlRegex = "(http(s)?:\\/\\/.)?(www\\.)?(wowhead)\\.[a-z]{2,6}\\b([-a-zA-Z0-9@:%_\\+.~#?&//=]*)";
            Match urlMatch = Regex.Match(text, urlRegex);
            if (!urlMatch.Success)
            {
                throw new ArgumentException();
            }
            string idRegex = "wowhead.com/item=([0-9]+)";
            string bonusRegex = "&bonus=([0-9-:]+)";
            char[] trims = {'\"', ')', '[', ']'};
            string name = text.Split(',')[1].Trim(trims);
            if (name == "nil")
            {
                return WowItem.GetNullItem();
            }
            Match idMatch = Regex.Match(text, idRegex);
            Match bonusMatch = Regex.Match(text, bonusRegex);
            string[] bonuses;
            if (bonusMatch.Groups[1].Value != "")
            {
                bonuses = bonusMatch.Groups[1].Value.Split(':');
            }
            else
            {
                bonuses = new string[] { };
            }
            int id = int.Parse(idMatch.Groups[1].Value);
            HashSet<int> bonusIds = new HashSet<int>();
            for (int i = 0; i < bonuses.Length; i++)
            {
                bonusIds.Add(int.Parse(bonuses[i]));
            }
            HashSet<BonusId> bids = new HashSet<BonusId>();
            foreach (int i in bonusIds)
            {
                bids.Add(new BonusId(i));
            }
            return new WowItem(id, name, Zone.Unknown, bids);
        }

    }
}