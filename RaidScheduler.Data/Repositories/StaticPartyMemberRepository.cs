using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RaidScheduler.Entities;
using System.Data.Entity;

namespace RaidScheduler.Data.Repositories
{
    public class StaticPartyMemberRepository : IRepository<StaticMember>
    {
        private readonly RaidSchedulerContext context;
        public StaticPartyMemberRepository(RaidSchedulerContext context)
        {
            this.context = context;
        }

        public StaticMember Save(StaticMember entity)
        {
            context.Entry<StaticMember>(entity).State = entity.StaticMemberID == 0 ? EntityState.Added : EntityState.Modified;
            context.SaveChanges();
            return entity;
        }

        public void Delete(StaticMember entity)
        {
            context.Entry<StaticMember>(entity).State = EntityState.Deleted;
        }

        public StaticMember Find(int ID)
        {
            var result = context.StaticPartyMember.Find(ID);
            return result;
        }

        public ICollection<StaticMember> Get(System.Linq.Expressions.Expression<Func<StaticMember, bool>> where = null)
        {
            List<StaticMember> result = null;
            if (where != null)
            {
                result = context.StaticPartyMember.Where(where).ToList();

            }
            else
            {
                result = context.StaticPartyMember.ToList();
            }
            return result;

        }
    }
}
