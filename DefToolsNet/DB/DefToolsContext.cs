﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DefToolsNet.Models;

namespace DefToolsNet.DB
{
    public class DefToolsContext : DbContext
    {

        public DefToolsContext() : base()
        {

        }

        public DbSet<BonusId> BonusIds { get; set; }
        public DbSet<LootAward> LootAwards { get; set; }
        public DbSet<WowItem> WowItems { get; set; }

        public DbSet<WowPlayer> WowPlayers { get; set; }

    }
}