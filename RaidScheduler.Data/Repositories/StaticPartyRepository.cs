using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RaidScheduler.DTO;
using System.Data.Entity;

namespace RaidScheduler.Data.Repositories
{
    public class StaticPartyRepository : IRepository<StaticParty>
    {
        private readonly RaidSchedulerContext context;
        public StaticPartyRepository(RaidSchedulerContext context)
        {
            this.context = context;
        }

        public StaticParty Save(StaticParty entity)
        {
            context.Entry<StaticParty>(entity).State = entity.StaticPartyID == 0 ? EntityState.Added : EntityState.Modified;
            context.SaveChanges();
            return entity;
        }

        public void Delete(StaticParty entity)
        {
            context.Entry<StaticParty>(entity).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public StaticParty Find(int ID)
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
