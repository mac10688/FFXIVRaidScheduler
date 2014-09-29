using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RaidScheduler.Domain.DomainModels;
using RaidScheduler.Domain.Data;
using System.Data.Entity;
using RaidScheduler.Domain.DomainModels.RaidDomain;

namespace RaidScheduler.Domain.Repositories
{
    //public class RaidRepository : IRepository<Raid>
    //{
    //    private readonly RaidSchedulerContext context;

    //    public RaidRepository(RaidSchedulerContext context)
    //    {
    //        this.context = context;
    //    }

    //    public ICollection<Raid> Query()
    //    {
    //        var result = context.Raids.ToList();
    //        return result;
    //    }

    //    public Raid Find(string raidID)
    //    {
    //        var result = context.Raids.Find(raidID);
    //        return result;
    //    }

    //    public Raid Save(Raid entity)
    //    {
    //        context.Entry<Raid>(entity).State = entity.RaidId == 0 ? EntityState.Added : EntityState.Modified;
    //        context.SaveChanges();
    //        return entity;
    //    }

    //    public void Delete(Raid entity)
    //    {
    //        context.Entry<Raid>(entity).State = EntityState.Deleted;
    //        context.SaveChanges();
    //    }

    //    public ICollection<Raid> Get(System.Linq.Expressions.Expression<Func<Raid, bool>> where = null)
    //    {
    //        List<Raid> result = null;
    //        if (where != null)
    //        {
    //            result = context.Raids.Where(where).ToList();

    //        }
    //        else
    //        {
    //            result = context.Raids.ToList();
    //        }
    //        return result;

    //    }
    //}
}
