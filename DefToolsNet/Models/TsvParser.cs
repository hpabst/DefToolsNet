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
            throw new NotImplementedException();
        }
    }
}
