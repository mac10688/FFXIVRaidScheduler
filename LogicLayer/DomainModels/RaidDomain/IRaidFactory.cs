using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidScheduler.Domain.DomainModels.RaidDomain
{
    public interface IRaidFactory
    {
        Raid CreateRaid(RaidType raid);
        IEnumerable<Raid> GetAllRaids();
    }
}
