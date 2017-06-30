using System;
using System.Collections;
using System.Collections.Generic;

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

    }
}