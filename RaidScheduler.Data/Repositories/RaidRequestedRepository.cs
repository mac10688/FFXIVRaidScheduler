using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RaidScheduler.Entities;
using System.Data.Entity;

namespace RaidScheduler.Data.Repositories
{
    public class RaidRequestedRepository : IRepository<RaidRequested>
    {

        private readonly RaidSchedulerContext context;
        public RaidRequestedRepository(RaidSchedulerContext context)
        {
            this.context = context;
        }

        public RaidRequested Save(RaidRequested entity)
        {
            context.Entry<RaidRequested>(entity).State = entity.RaidRequestedID == 0 ? EntityState.Added : EntityState.Modified;
            context.SaveChanges();
            return entity;
        }

        public void Delete(RaidRequested entity)
        {
            context.Entry<RaidRequested>(entity).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public RaidRequested Find(int ID)
        {
            var result = context.RaidsRequested.Find(ID);
            return result;
        }

        public ICollection<RaidRequested> Get(System.Linq.Expressions.Expression<Func<RaidRequested, bool>> where = null)
        {
            List<RaidRequested> result = null;
            if (where != null)
            {
                result = context.RaidsRequested.Where(where).ToList();
            }
            else
            {
                result = context.RaidsRequested.ToList();
            }
            return result;
        }
    }
}
