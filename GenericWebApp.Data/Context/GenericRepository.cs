using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GenericWebApp.Data.Context
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        private DataDbContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(DataDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }


        #region Sync

        public virtual IEnumerable<TEntity> GetAll()
        {
            return Get();
        }

        protected internal IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "", int fetchTotal = -1, int skip = 0)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }


            if (fetchTotal >= 0)
            {
                if (orderBy != null)
                {
                    return skip > 0 ?
                        orderBy(query).Skip(skip).Take(fetchTotal).ToList() :
                        orderBy(query).Take(fetchTotal).ToList();
                }
                return skip > 0 ?
                    query.Skip(skip).Take(fetchTotal).ToList() :
                    query.Take(fetchTotal).ToList();
            }

            if (orderBy != null)
            {
                return skip > 0
                    ? orderBy(query).Skip(skip).ToList()
                    : orderBy(query).ToList();
            }

            return skip > 0 ?
                query.Skip(skip).ToList() :
                query.ToList();
        }

        public virtual TEntity GetById(int id)
        {
            return dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            //dbSet.AddOrUpdate(entity);
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Added;
        }

        protected internal virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        #endregion


        #region Async

        public async virtual Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await GetAsync();
        }

        protected internal async Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "", int fetchTotal = -1, int skip = 0)
        {
            IQueryable<TEntity> query = dbSet;//.AsExpandable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }


            if (fetchTotal >= 0)
            {
                if (orderBy != null)
                {
                    if (skip > 0)
                    {
                        var theQuery = orderBy(query).Skip(skip).Take(fetchTotal);
                        return await theQuery.ToListAsync();
                    }
                    else
                    {
                        var theQuery = orderBy(query).Take(fetchTotal);

                        if (true)
                        {
                            // var sql = theQuery.ToTraceString();
                            //  var sql = ((System.Data.Entity.Core.Objects.ObjectQuery)theQuery).ToTraceString();
                        }

                        return await orderBy(query).Take(fetchTotal).ToListAsync();
                    }

                }
                return skip > 0 ?
                    await query.Skip(skip).Take(fetchTotal).ToListAsync() :
                    await query.Take(fetchTotal).ToListAsync();
            }

            if (orderBy != null)
            {
                return skip > 0
                    ? await orderBy(query).Skip(skip).ToListAsync()
                    : await orderBy(query).ToListAsync();
            }

            if (skip > 0)
            {
                return await query.Skip(skip).ToListAsync();
            }




            return await query.ToListAsync();

        }

        public async virtual Task<TEntity> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual void InsertAsync(TEntity entity)
        {
            //dbSet.AddOrUpdate(entity);
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Added;
        }

        protected async internal virtual void DeleteAsync(object id)
        {
            TEntity entityToDelete = await dbSet.FindAsync(id);
            DeleteAsync(entityToDelete);
        }

        public virtual void DeleteAsync(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void UpdateAsync(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        #endregion
    }
}