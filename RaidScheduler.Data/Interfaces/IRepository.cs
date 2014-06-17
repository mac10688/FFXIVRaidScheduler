using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RaidScheduler.Data
{
    public interface IRepository<T>
    {
        T Save(T entity);
        void Delete(T entity);
        T Find(int ID);
        ICollection<T> Get(Expression<Func<T,bool>> where = null);
    }
}
