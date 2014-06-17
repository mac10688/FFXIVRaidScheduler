﻿using RaidScheduler.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidScheduler.Domain
{
    public class RaidDomain : IRaidDomain
    {
        public ICollection<Raid> CommonRaidsRequested(ICollection<Player> playerCollection)
        {
            var raidsRequested = new List<Raid>();
            foreach (var sMember in playerCollection)
            {
                var raids = sMember.RaidsRequested.Select(r => r.Raid);
                if (!raidsRequested.Any())
                {
                    raidsRequested.AddRange(raids);
                }
                else
                {
                    var raidsToRemove = new List<Raid>();
                    foreach (var raid in raidsRequested)
                    {
                        if (!raids.Any(r => r.RaidID == raid.RaidID))
                        {
                            raidsToRemove.Add(raid);
                        }
                    }
                    foreach (var raid in raidsToRemove)
                    {
                        raidsRequested.Remove(raid);
                    }
                }
            }
            return raidsRequested;
        }
    }
}
