using RaidScheduler.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RaidScheduler.Data.Repositories
{
    public class PlayerDayAndTimeAvailableRepository : IRepository<PlayerDayAndTimeAvailable>
    {
        private readonly RaidSchedulerContext context;
        public PlayerDayAndTimeAvailableRepository(RaidSchedulerContext context)
        {
            this.context = context;
        }

        public PlayerDayAndTimeAvailable Save(PlayerDayAndTimeAvailable entity)
        {
            context.Entry<PlayerDayAndTimeAvailable>(entity).State = entity.PlayerDayAndTimeAvailableID == 0 ? EntityState.Added : EntityState.Modified;
            context.SaveChanges();
            return entity;
        }

        public void Delete(PlayerDayAndTimeAvailable entity)
        {
            context.Entry<PlayerDayAndTimeAvailable>(entity).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public PlayerDayAndTimeAvailable Find(int ID)
        {
            var result = context.PlayerDayAndTimesAvailable.Find(ID);
            return result;
        }

        public ICollection<PlayerDayAndTimeAvailable> Get(System.Linq.Expressions.Expression<Func<PlayerDayAndTimeAvailable, bool>> where = null)
        {
            List<PlayerDayAndTimeAvailable> result = null;
            if(where != null)
            { 
                result = context.PlayerDayAndTimesAvailable.Where(where).ToList();                
            }
            else
            {
                result = context.PlayerDayAndTimesAvailable.ToList();
            }
            return result;
        }
    }
}
