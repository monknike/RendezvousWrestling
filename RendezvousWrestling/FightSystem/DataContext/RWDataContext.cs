﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using RendezvousWrestling.FightSystem.Achievements;
using RendezvousWrestling.FightSystem.Features;
using System;
using System.Collections.Generic;
using System.Text;

namespace RendezvousWrestling.Common.DataContext
{
    public class RWDataContext : BaseDataContext<RendezVousWrestling, RWAchievement, RWActionFactory, RWActiveAction, RWFeature, RWFeatureFactory, RWFight, RWFighterState, RWFighterStats, RWModifier, RWUser, RWFeatureParameter>
    {
        public IConfigurationRoot Configuration { get; } = new ConfigurationBuilder()
          .AddJsonFile("appsettings.json", true, true)
          .Build();

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var cnnStr = Configuration.GetConnectionString("Default");
            options.UseMySql(connectionString: cnnStr, new MariaDbServerVersion(new Version(10, 5)));
        }
    }
}