using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RaidScheduler.Domain.DomainModels;

using System.Data.Entity;
using RaidScheduler.Domain.Data;
using RaidScheduler.Domain.DomainModels.StaticPartyDomain;

namespace RaidScheduler.Domain.Repositories
{
    public class StaticPartyRepository : IRepository<StaticParty>
    {
        private readonly RaidSchedulerContext context;
        public StaticPartyRepository(RaidSchedulerContext context)
        {
            this.context = context;
        }

        public StaticParty Save(StaticParty staticParty)
        {
            var cParty = context.StaticParties.Where(sp => sp.StaticPartyId == staticParty.StaticPartyId).SingleOrDefault();
            context.Entry<StaticParty>(staticParty).State = cParty == null ? EntityState.Added : EntityState.Modified;
            context.SaveChanges();
            return staticParty;
        }

        public void Delete(StaticParty entity)
        {
            context.Entry<StaticParty>(entity).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public StaticParty Find(string ID)
        {
            var result = context.StaticParties.Find(ID);
            return result;
        }

        public ICollection<StaticParty> Get(System.Linq.Expressions.Expression<Func<StaticParty, bool>> where = null)
        {
            List<StaticParty> result = null;
            if (where != null)
            {
                result = context.StaticParties.Where(where).ToList();

            }
            else
            {
                result = context.StaticParties.ToList();
            }
            return result;

        }
    }
}
