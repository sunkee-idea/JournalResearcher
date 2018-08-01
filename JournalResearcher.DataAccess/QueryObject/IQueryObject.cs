using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace JournalResearcher.DataAccess.QueryObject
{
    public interface IQueryObject<TEntity>
    {
        Expression<Func<TEntity,bool>> Expression { get; }
        IQueryObject<TEntity> And(Expression<Func<TEntity, bool>> query);
        IQueryObject<TEntity> Or(Expression<Func<TEntity, bool>> query);
        IQueryObject<TEntity> And(IQueryObject<TEntity> queryObject);
        IQueryObject<TEntity> Or(IQueryObject<TEntity> queryObject);

    }
}
