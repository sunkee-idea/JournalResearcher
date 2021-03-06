﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace JournalResearcher.DataAccess.Repository
{
    public interface IRepository<T> where T : class
    {
        T Get(int id);
        IEnumerable<T> GetAll();

        //        IEnumerable<T> GetObjectGraph(
        //            Expression<Func<T, bool>> filter = null,
        //            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        //            string includeProperties = "");
        IQueryable<T> Table { get; }

        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        void Insert(T entity);
        void InsertRange(IEnumerable<T> entities);

        IQueryable<T> Fetch(Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            int? page = null, int? pageSize = null, string includeProperties = "");

        int Count(Expression<Func<T, bool>> predicate);

        void Update(T entity);

        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);


    }
}
