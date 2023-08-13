using BigBang.Order.Domain;
using BigBang.Order.Domain.Aggregates;
using BigBang.Order.Domain.Repositories;
using BigBang.Order.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Collections;
using System.Linq.Expressions;
using System.Reflection;

namespace BigBang.Order.Persistence.Repositories
{
    public abstract class Repository<T> : IRepository<T>, IRepository where T : Entity,  IAggregateRoot
    {
        protected readonly OrderDbContext dbContext;
        private readonly DbSet<T> _entities;

        protected Repository(OrderDbContext dbContext)
        {
            this.dbContext = dbContext;
            _entities = dbContext.Set<T>();
        }

        public virtual async Task<T> GetAsync(Expression<Func<T, bool>> filter)
        {
            return await _entities.SingleOrDefaultAsync(filter);
        }

        public virtual async Task<IList<T>> GetListAsync(Expression<Func<T, bool>> predicate)
        {
            return await EntityFrameworkQueryableExtensions.ToListAsync(ConvertToAggregate(dbContext.Set<T>()).Where(predicate));
        }

        public virtual async Task<IList<T>> GetAllAsync()
        {
            return await EntityFrameworkQueryableExtensions.ToListAsync(ConvertToAggregate(dbContext.Set<T>()));
        }

        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await EntityFrameworkQueryableExtensions.CountAsync(EntityFrameworkQueryableExtensions.AsNoTracking(dbContext.Set<T>()), predicate);
        }

        public virtual async Task<bool> IsExistAsync(Expression<Func<T, bool>> predicate)
        {
            return await EntityFrameworkQueryableExtensions.AnyAsync(dbContext.Set<T>(), predicate);
        }

        public virtual async Task AddAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            await dbContext.Set<T>().AddAsync(entity);
            SetEnumerationMembersAsUnTrackedRecurse(entity);
            dbContext.SaveChanges();
        }

        public virtual async Task AddRangeAsync(IEnumerable<T> collection)
        {
            T[] aggregateRoots = (collection as T[]) ?? collection.ToArray();
            await dbContext.Set<T>().AddRangeAsync(aggregateRoots);
            T[] array = aggregateRoots;
            foreach (T enumerationMembersAsUnTrackedRecurse in array)
            {
                SetEnumerationMembersAsUnTrackedRecurse(enumerationMembersAsUnTrackedRecurse);
            }
            await dbContext.SaveChangesAsync();
        }

        public virtual Task UpdateAsync(T entity)
        {
            dbContext.Set<T>().Update(entity);
            dbContext.SaveChanges();
            return Task.CompletedTask;
        }

        public virtual Task UpdateRangeAsync(IEnumerable<T> collection)
        {
            T[] entities = (collection as T[]) ?? collection.ToArray();
            dbContext.Set<T>().UpdateRange(entities);
            dbContext.SaveChanges();
            return Task.CompletedTask;
        }

        public virtual Task DeleteAsync(T aggregate)
        {
            dbContext.Set<T>().Remove(aggregate);
            dbContext.SaveChanges();
            return Task.CompletedTask;
        }

        public virtual Task DeleteRangeAsync(IEnumerable<T> collection)
        {
            T[] entities = (collection as T[]) ?? collection.ToArray();
            dbContext.Set<T>().RemoveRange(entities);
            return Task.CompletedTask;
        }

        private IQueryable<T> ConvertToAggregate(IQueryable<T> set)
        {
            IQueryable<T> queryable = set;
            foreach (INavigation item in dbContext.Model.FindEntityType(typeof(T))!.GetDerivedTypesInclusive().SelectMany((IEntityType type) => type.GetNavigations()).Distinct())
            {
                queryable = EntityFrameworkQueryableExtensions.Include(queryable, item.Name);
            }

            return queryable;
        }

        protected virtual void SetEnumerationMembersAsUnTrackedRecurse(object obj)
        {
            if (obj == null)
            {
                return;
            }

            Type type = obj.GetType();
            if (!type.IsClass || type.IsValueType || type.IsPrimitive || type.FullName == "System.String")
            {
                return;
            }

            try
            {
                EntityEntry entityEntry = dbContext.Entry(obj);
                if (entityEntry == null || entityEntry.State != EntityState.Added)
                {
                    return;
                }
            }
            catch
            {
                return;
            }

            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo propertyInfo in properties)
            {
                Type propertyType = propertyInfo.PropertyType;
                if (!propertyType.IsClass || propertyType.IsValueType || propertyType.IsPrimitive || propertyType.FullName == "System.String")
                {
                    continue;
                }

                object value = propertyInfo.GetValue(obj, null);
                if (propertyType.IsSubclassOf(typeof(Enumeration)))
                {
                    try
                    {
                        EntityEntry entityEntry2 = dbContext.Entry(value);
                        if (entityEntry2 != null && entityEntry2.State == EntityState.Added)
                        {
                            dbContext.Entry(value).State = EntityState.Unchanged;
                            continue;
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }

                IList list = value as IList;
                if (list != null)
                {
                    foreach (object item in list)
                    {
                        SetEnumerationMembersAsUnTrackedRecurse(item);
                    }
                }
                else
                {
                    SetEnumerationMembersAsUnTrackedRecurse(value);
                }
            }
        }
    }
}
