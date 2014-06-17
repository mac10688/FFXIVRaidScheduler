using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RaidScheduler.Entities;
using System.Data.Entity;

namespace RaidScheduler.Data.Repositories
{
    public class RaidRepository : IRepository<Raid>
    {
        private readonly RaidSchedulerContext context;

        public RaidRepository(RaidSchedulerContext context)
        {
            this.context = context;
        }

        public ICollection<Raid> Query()
        {
            var result = context.Raids.ToList();
            return result;
        }

        public Raid Find(int raidID)
        {
            var result = context.Raids.Find(raidID);
            return result;
        }

        public Raid Save(Raid entity)
        {
            context.Entry<Raid>(entity).State = entity.RaidID == 0 ? EntityState.Added : EntityState.Modified;
            context.SaveChanges();
            return entity;
        }

        public void Delete(Raid entity)
        {
            context.Entry<Raid>(entity).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public ICollection<Raid> Get(System.Linq.Expressions.Expression<Func<Raid, bool>> where = null)
        {
            List<Raid> result = null;
            if (where != null)
            {
                result = context.Raids.Where(where).ToList();

            }
            else
            {
                result = context.Raids.ToList();
            }
            return result;

        }
    }
}
