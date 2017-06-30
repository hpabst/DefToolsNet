using Microsoft.VisualStudio.TestTools.UnitTesting;
using DefToolsNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefToolsNet.Models.Tests
{
    [TestClass()]
    public class TsvParserTests
    {
        [TestMethod()]
        public void ParseImportTextTest()
        {
            string sampleLines =
                "player\tdate\ttime\titem\titemID\titemString\tresponse\tvotes\tclass\tinstance\tboss\tgear1\tgear2\tresponseID\tisAwardReason\r" +
                "\nMarath-Shadowsong\t01/06/17\t20:46:13\t=HYPERLINK(\"https://www.wowhead.com/item=143575&bonus=570\",\"[Helm of the Foreseen Protector]\")\t143575\t143575::::::::110:264::5:1:570:::\tSlight Upgrade\t2\tSHAMAN\tThe Nighthold-Heroic\tnil\t=HYPERLINK(\"https://www.wowhead.com/item=138343&bonus=3516:1487:3528\",\"[Helm of Shackled Elements]\")\t=HYPERLINK(\"https://www.wowhead.com/item=0\",\"nil\")\t4\tfalse\r" +
                "\nBerrimond-Shadowsong\t01/06/17\t20:57:11\t=HYPERLINK(\"https://www.wowhead.com/item=143564&bonus=570\",\"[Leggings of the Foreseen Conqueror]\")\t143564\t143564::::::::110:264::5:1:570:::\tGives me 2/4 set\t2\tPALADIN\tThe Nighthold-Heroic\tnil\t=HYPERLINK(\"https://www.wowhead.com/item=134506&bonus=3573:43:1552:3337\",\"[Legplates of the Swarm]\")\t=HYPERLINK(\"https://www.wowhead.com/item=0\",\"nil\")\t1\tfalse\r" +
                "\nShamydavisjr-Shadowsong\t01/06/17\t20:57:21\t=HYPERLINK(\"https://www.wowhead.com/item=140917&bonus=3517:41:1492:3528\",\"[Netherbranded Shoulderpads]\")\t140917\t140917::::::::110:264::5:4:3517:41:1492:3528:::\tGreed/OS\t3\tSHAMAN\tThe Nighthold-Heroic\tnil\t=HYPERLINK(\"https://www.wowhead.com/item=134480&bonus=3418:1557:3337\",\"[Epaulets of Deceitful Intent]\")\t=HYPERLINK(\"https://www.wowhead.com/item=0\",\"nil\")\t5\tfalse\r";
            TsvParser tp = new TsvParser();
            WowPlayer playerMarath = new WowPlayer("Marath", "Shadowsong", WowClass.Shaman);
            WowPlayer playerBerrimond = new WowPlayer("Berrimond", "Shadowsong", WowClass.Paladin);
            WowPlayer playerShamydavisjr = new WowPlayer("Shamydavisjr", "Shadowsong", WowClass.Shaman);

            HashSet<BonusId> bids1 = new HashSet<BonusId>();
            bids1.Add(new BonusId(570));
            WowItem helmProtector = new WowItem(143575, "Helm of the Foreseen Protector", Zone.Unknown, bids1);

            HashSet<BonusId> bids2 = new HashSet<BonusId>();
            bids2.Add(new BonusId(3516));
            bids2.Add(new BonusId(1487));
            bids2.Add(new BonusId(3528));
            WowItem helmShackledElements = new WowItem(138343, "Helm of Shackled Elements", Zone.Unknown, bids2);

            HashSet<BonusId> bids3 = new HashSet<BonusId>();
            bids3.Add(new BonusId(570));
            WowItem leggingsConqueror = new WowItem(143564, "Leggings of the Foreseen Conqueror", Zone.Unknown, bids3);

            HashSet<BonusId> bids4 = new HashSet<BonusId>();
            bids4.Add(new BonusId(3573));
            bids4.Add(new BonusId(43));
            bids4.Add(new BonusId(1552));
            bids4.Add(new BonusId(3337));
            WowItem legplatesSwarm = new WowItem(134506, "Legplates of the Swarm", Zone.Unknown, bids4);

            HashSet<BonusId> bids5 = new HashSet<BonusId>();
            bids5.Add(new BonusId(3517));
            bids5.Add(new BonusId(41));
            bids5.Add(new BonusId(1492));
            bids5.Add(new BonusId(3528));
            WowItem netherbrandedShoulderpads = new WowItem(140917, "Netherbranded Shoulderpads", Zone.Unknown, bids5);

            HashSet<BonusId> bids6 = new HashSet<BonusId>();
            bids6.Add(new BonusId(3418));
            bids6.Add(new BonusId(1557));
            bids6.Add(new BonusId(3337));
            WowItem epauletsDIntent = new WowItem(134480, "Epaulets of Deceitful Intent", Zone.Unknown, bids6);
            LootAward award1 = new LootAward("Slight Upgrade", new DateTime(2017, 06, 01), helmProtector,
                helmShackledElements, WowItem.GetNullItem(), playerMarath);
            LootAward award2 = new LootAward("Gives me 2/4 set", new DateTime(2017, 06, 01), leggingsConqueror, legplatesSwarm, WowItem.GetNullItem(), playerBerrimond);
            LootAward award3 = new LootAward("Greed/OS", new DateTime(2017, 06, 01), netherbrandedShoulderpads, epauletsDIntent, WowItem.GetNullItem(), playerShamydavisjr);
            List<LootAward> testAwards = new List<LootAward>();
            testAwards.Add(award1);
            testAwards.Add(award2);
            testAwards.Add(award3);

            ICollection<LootAward> awards = tp.ParseImportText(sampleLines);
            Assert.IsTrue(awards.Count == 3);
            foreach (LootAward testAward in testAwards)
            {
                bool present = false;
                foreach (LootAward award in awards)
                {
                    if (testAward.Matches(award))
                        present = true;
                }
                Assert.IsTrue(present);
            }
        }

        [TestMethod()]
        public void ParseLineTest()
        {
            TsvParser tp = new TsvParser();
            string testStr1 =
                "Vt-Shadowsong\t01/06/17\t20:57:16\t=HYPERLINK(\"https://www.wowhead.com/item=140820&bonus=3517:1492:3528\",\"[Phial of Fel Blood]\")\t140820\t140820::::::::110:264::5:3:3517:1492:3528:::\tGreed/OS\t2\tDEATHKNIGHT\tThe Nighthold-Heroic\tnil\t=HYPERLINK(\"https://www.wowhead.com/item=0\",\"nil\")\t=HYPERLINK(\"https://www.wowhead.com/item=0\",\"nil\")\t5\tfalse";
            LootAward la = tp.ParseLine(testStr1);
            Assert.IsTrue(la.Player.Name == "Vt");
            Assert.IsTrue(la.Player.Realm == "Shadowsong");
            Assert.IsTrue(la.Player.PlayerClass == WowClass.DeathKnight);
            Assert.IsTrue(la.AwardReason == "Greed/OS");
            Assert.IsTrue(la.AwardDate.Equals(new DateTime(2017, 06, 01, 20, 57, 16)));
            Assert.IsTrue(la.Item.Name == "Phial of Fel Blood");
            Assert.IsTrue(la.Item.ItemId == 140820);
            Assert.IsTrue(la.Item.Instance == Zone.TheNighthold);
            Assert.IsTrue(la.Item.BonusIds.Count == 3);
            HashSet<int> bids = new HashSet<int>();
            foreach (BonusId bid in la.Item.BonusIds)
            {
                bids.Add(bid.Id);
            }
            Assert.IsTrue(bids.Contains(3517));
            Assert.IsTrue(bids.Contains(1492));
            Assert.IsTrue(bids.Contains(3528));
            Assert.IsTrue(la.Replacement1 == WowItem.GetNullItem());
            Assert.IsTrue(la.Replacement2 == WowItem.GetNullItem());
        }
    }
}