using BigBang.Order.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BigBang.Order.Domain
{
    public interface IRepository<TEntity> : IRepository
    {
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);

        Task<IList<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate);

        Task<IList<TEntity>> GetAllAsync();

        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);

        Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> predicate);

        Task AddAsync(TEntity entity);

        Task AddRangeAsync(IEnumerable<TEntity> entity);

        Task UpdateAsync(TEntity entity);

        Task UpdateRangeAsync(IEnumerable<TEntity> entity);

        Task DeleteAsync(TEntity aggregate);

        Task DeleteRangeAsync(IEnumerable<TEntity> collection);
    }
}
