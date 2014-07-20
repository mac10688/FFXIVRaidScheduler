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
    public class JobRepository : IRepository<Job>
    {
        private readonly RaidSchedulerContext context;

        public JobRepository(RaidSchedulerContext context)
        {
            this.context = context;
        }

        public ICollection<Job> Query()
        {
            var result = context.Jobs.ToList();
            return result;
        }

        public Job Find(int ID)
        {
            var result = context.Jobs.Find(ID);
            return result;
        }

        public Job Save(Job job)
        {
            context.Entry<Job>(job).State = job.JobId == 0 ? EntityState.Added : EntityState.Modified;
            context.SaveChanges();
            return job;
        }

        public void Delete(Job job)
        {
            context.Entry<Job>(job).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public ICollection<Job> Get(System.Linq.Expressions.Expression<Func<Job, bool>> where = null)
        {
            List<Job> result = null;
            if (where != null)
            {
                result = context.Jobs.Where(where).ToList();

            }
            else
            {
                result = context.Jobs.ToList();
            }
            return result;

        }
    }
}
