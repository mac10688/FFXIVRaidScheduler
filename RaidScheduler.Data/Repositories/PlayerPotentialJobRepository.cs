using RaidScheduler.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RaidScheduler.Data.Repositories
{
    public class PlayerPotentialJobRepository : IRepository<PotentialJob>
    {
        private readonly RaidSchedulerContext context;
        public PlayerPotentialJobRepository(RaidSchedulerContext context)
        {
            this.context = context;
        }

        public PotentialJob Save(PotentialJob entity)
        {
            context.Entry<PotentialJob>(entity).State = entity.PotentialJobID == 0 ? EntityState.Added : EntityState.Modified;
            context.SaveChanges();
            return entity;
        }

        public void Delete(PotentialJob entity)
        {
            context.Entry<PotentialJob>(entity).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public PotentialJob Find(int ID)
        {
            var result = context.PotentialJobs.Find(ID);
            return result;
        }

        public ICollection<PotentialJob> Get(Expression<Func<PotentialJob, bool>> where = null)
        {
            List<PotentialJob> result = null;
            if (where != null)
            {
                result = context.PotentialJobs.Where(where).ToList();

            }
            else
            {
                result = context.PotentialJobs.ToList();
            }
            return result;
        }
    }
}
