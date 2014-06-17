using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RaidScheduler.Entities;
using System.Data.Entity;

namespace RaidScheduler.Data.Repositories
{
    public class StaticPartyDayAndTimeScheduleRepository : IRepository<StaticPartyDayAndTimeSchedule>
    {
        private readonly RaidSchedulerContext context;

        public StaticPartyDayAndTimeScheduleRepository(RaidSchedulerContext context)
        {
            this.context = context;
        }

        public StaticPartyDayAndTimeSchedule Save(StaticPartyDayAndTimeSchedule entity)
        {
            var result = context.Entry<StaticPartyDayAndTimeSchedule>(entity).State = entity.StaticPartyDayAndTimeScheduleID == 0 ? EntityState.Added : EntityState.Modified;
            context.SaveChanges();
            return entity;
        }

        public void Delete(StaticPartyDayAndTimeSchedule entity)
        {
            context.Entry<StaticPartyDayAndTimeSchedule>(entity).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public StaticPartyDayAndTimeSchedule Find(int ID)
        {
            var result = context.StaticPartyDayAndTimesAvailable.Find(ID);
            return result;
        }

        public ICollection<StaticPartyDayAndTimeSchedule> Get(System.Linq.Expressions.Expression<Func<StaticPartyDayAndTimeSchedule, bool>> where = null)
        {
            List<StaticPartyDayAndTimeSchedule> result = null;
            if (where != null)
            {
                result = context.StaticPartyDayAndTimesAvailable.Where(where).ToList();

            }
            else
            {
                result = context.StaticPartyDayAndTimesAvailable.ToList();
            }
            return result;
        }
    }
}
