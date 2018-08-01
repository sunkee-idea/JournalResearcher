using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using JournalResearcher.DataAccess.Data;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace JournalResearcher.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext Context;
        private readonly IUnitOfWork _unitOfWork;
        protected readonly DbSet<T> DbSet;


        public Repository(ApplicationDbContext context,IUnitOfWork unitOfWork)
        {
            Context = context;
            _unitOfWork = unitOfWork;
            DbSet = context.Set<T>();
        }

        public T Get(int id)
        {
            return Context.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return Context.Set<T>().ToList();
        }

        public IQueryable<T> Table => Queryable();

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Where(predicate);
        }

        public void Insert(T entity)
        {
            Context.Set<T>().Add(entity);
        }

        public void InsertRange(IEnumerable<T> entities)
        {
            Context.Set<T>().AddRange(entities);
        }

        public IQueryable<T> Fetch(Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int? page = null, int? pageSize = null)
        {
            IQueryable<T> query = DbSet;
            if (orderBy != null)
                query = orderBy(query);
            if (predicate != null)
                query = query.AsExpandable().Where(predicate);
            if (page == null || pageSize == null) return query;
            if (page <= 0) page = 1;

            if (pageSize <= 0) pageSize = 10;
            query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);


            return query;
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            return Fetch(predicate).Count();
        }

        public void Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(T entity)
        {
            Context.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            Context.Set<T>().RemoveRange(entities);
        }

        public virtual IQueryable<T> Queryable()
        {
            return DbSet;
        }

        //Asynchronous Tasks


       

        public virtual async Task<IEnumerable<T>> GetAllEntity()
        {
            return await Context.Set<T>().ToListAsync();
        }


        public virtual Task<T> FindAsync(params object[] keyValues)
        {
            return FindAsync(CancellationToken.None, keyValues);
        }

        public virtual Task<T> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return DbSet.FindAsync(cancellationToken, keyValues);
        }

        public virtual Task<bool> DeleteAsync(params object[] keyValues)
        {
            return DeleteAsync(CancellationToken.None, keyValues);
        }

        public virtual async Task<T> GetAsync(params object[] keyValues)
        {
            return await DbSet.FindAsync(keyValues);
        }

        public virtual async Task<T> GetAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return await DbSet.FindAsync(cancellationToken, keyValues);
        }

        public virtual async Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            var entity = await GetAsync(cancellationToken, keyValues);

            if (entity == null)
                return false;
            Context.Entry(entity).State = EntityState.Deleted;

            return true;
        }
    }
}