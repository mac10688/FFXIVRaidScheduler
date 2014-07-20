using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaidScheduler.DTO;
using System.Data.Entity;
using System.Linq.Expressions;

namespace RaidScheduler.Data.Repositories
{
    public class PlayerRepository : IRepository<Player>
    {
        private readonly RaidSchedulerContext context;
        public PlayerRepository(RaidSchedulerContext context)
        {
            this.context = context;
        }

        public Player Find(int ID)
        {
            return context.Player.Where(p => p.PlayerID == ID).SingleOrDefault();
        }

        public ICollection<Player> Get(Expression<Func<Player, bool>> where = null)
        {
            List<Player> result = null;
            if (where != null)
            {
                result = context.Player.Where(where).ToList();

            }
            else
            {
                result = context.Player.ToList();
            }
            return result;

        }

        public Player Save(Player entity)
        {
            context.Entry<Player>(entity).State = entity.PlayerID == 0? EntityState.Added : EntityState.Modified;
            context.SaveChanges();
            return entity;
        }

        public void Delete(Player entity)
        {
            context.Entry<Player>(entity).State = EntityState.Deleted;
            context.SaveChanges();
        }        
    }
}
