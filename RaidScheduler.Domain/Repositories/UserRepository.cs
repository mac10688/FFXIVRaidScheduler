using RaidScheduler.Domain.Data;
using RaidScheduler.Domain.DomainModels.UserDomain;
using RaidScheduler.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidScheduler.Domain.Repositories
{
    public class UserRepository : IRepository<User>
    {

        private readonly RaidSchedulerContext context;
        public UserRepository(RaidSchedulerContext context)
        {
            this.context = context;
        }

        public User Save(User entity)
        {
            var user = context.Users.Where(u => u.Id == entity.Id).SingleOrDefault();
            context.Entry<User>(user).State = user == null ? EntityState.Added : EntityState.Modified;
            context.SaveChanges();
            return user;
        }

        public void Delete(User entity)
        {
            context.Entry<User>(entity).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public User Find(string Id)
        {
            return context.Users.Find(Id);
        }

        public ICollection<User> Get(System.Linq.Expressions.Expression<Func<User, bool>> where = null)
        {
            List<User> result = null;
            if (where != null)
            {
                result = context.Users.Where(where).ToList();
            }
            else
            {
                result = context.Users.ToList();
            }
            return result;
        }
    }
}
