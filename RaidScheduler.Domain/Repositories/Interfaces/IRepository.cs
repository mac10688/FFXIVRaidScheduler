using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RaidScheduler.Domain.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        /// <summary>
        /// Save a given Entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        T Save(T entity);

        /// <summary>
        /// Delete a givn entity.
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);

        /// <summary>
        /// Find a given entity.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        T Find(string ID);

        /// <summary>
        /// Get all the entities or apply an Expression to filter.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        ICollection<T> Get(Expression<Func<T,bool>> where = null);
    }
}
