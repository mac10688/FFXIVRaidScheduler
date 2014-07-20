using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RaidScheduler.Domain.DomainModels;
using RaidScheduler.Domain.Data;
using System.Data.Entity;

namespace RaidScheduler.Domain.Repositories
{
    public class RaidCriteriaRepository : IRepository<RaidCriteria>
    {
        private readonly RaidSchedulerContext context;
        public RaidCriteriaRepository(RaidSchedulerContext context)
        {
            this.context = context;
        }

        public RaidCriteria Save(RaidCriteria entity)
        {
            var result = context.Entry<RaidCriteria>(entity).State = entity.RaidCriteriaId == 0 ? EntityState.Added : EntityState.Modified;
            context.SaveChanges();
            return entity;
        }

        public void Delete(RaidCriteria entity)
        {
            context.Entry<RaidCriteria>(entity).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public RaidCriteria Find(int ID)
        {
            var result = context.RaidCriteria.Find(ID);
            return result;
        }

        public ICollection<RaidCriteria> Get(System.Linq.Expressions.Expression<Func<RaidCriteria, bool>> where = null)
        {
            List<RaidCriteria> result = null;
            if (where != null)
            {
                result = context.RaidCriteria.Where(where).ToList();

            }
            else
            {
                result = context.RaidCriteria.ToList();
            }
            return result;

        }
    }
}
