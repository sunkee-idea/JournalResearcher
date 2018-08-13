using JournalResearcher.DataAccess.Cores;
using JournalResearcher.DataAccess.Data;
using JournalResearcher.DataAccess.Data.Models;
using JournalResearcher.DataAccess.QueryObject;
using JournalResearcher.DataAccess.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace JournalResearcher.DataAccess.Repository
{
    public interface IJournalRepository : IRepository<Journal>
    {
        IEnumerable<Journal> GetJournalPaged(int page, int count, out int totalCount, JournalFilter filter = null,
            OrderExpression orderExpression = null);

        IEnumerable<Journal> GetJournalPaged(int page, int count, JournalFilter filter = null,
            OrderExpression orderExpression = null);
    }
    public class JournalRepository : Repository<Journal>, IJournalRepository
    {
        public JournalRepository(ApplicationDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }

        public IEnumerable<Journal> GetJournalPaged(int page, int count, out int totalCount, JournalFilter filter = null,
            OrderExpression orderExpression = null)
        {
            var expression = new JournalQueryObject(filter).Expression;
            totalCount = Count(expression);
            return JournalPaged(page, count, expression, orderExpression);

        }

        public IEnumerable<Journal> GetJournalPaged(int page, int count, JournalFilter filter = null, OrderExpression orderExpression = null)
        {
            var expression = new JournalQueryObject(filter).Expression;
            return JournalPaged(page, count, expression, orderExpression);
        }

        IEnumerable<Journal> JournalPaged(int page, int count,
            Expression<Func<Journal, bool>> expression, OrderExpression orderExpression)
        {
            var order = ProcessOrderFunc(orderExpression);
            return Fetch(expression, order, page, count, includeProperties: "Applicant");
        }
        public static Func<IQueryable<Journal>, IOrderedQueryable<Journal>> ProcessOrderFunc(
            OrderExpression orderDeserilizer = null)
        {
            IOrderedQueryable<Journal> Orderfunc(IQueryable<Journal> queryable)
            {
                var orderQueryable = queryable.OrderBy(x => x.Title).ThenBy(x => x.Id).ThenBy(x => x.Title);
                return orderQueryable;
            }

            return Orderfunc;
        }



    }

    public class JournalQueryObject : QueryObject<Journal>
    {
        public JournalQueryObject(JournalFilter filter)
        {
            if (filter != null)
            {
                if (filter.Id > 0)
                    And(c => c.Id == filter.Id);
                if (!string.IsNullOrWhiteSpace(filter.Title))
                    And(c => c.Title.Contains(filter.Title));
                if (!string.IsNullOrWhiteSpace(filter.SupervisorName))
                    And(c => c.SupervisorName.Contains(filter.SupervisorName));
                if (!string.IsNullOrWhiteSpace(filter.Author))
                    And(c => c.Author == filter.Author);
                if (!string.IsNullOrWhiteSpace(filter.FirstName))
                    And(c => c.Applicant.FirstName == filter.FirstName);
                if (!string.IsNullOrWhiteSpace(filter.LastName))
                    And(c => c.Applicant.LastName == filter.LastName);
                if (filter.DateCreatedFrom.HasValue)
                    And(c => c.DateSubmitted >= filter.DateCreatedFrom.Value);
                if (filter.DateCreatedTo.HasValue)
                    And(c => c.DateSubmitted <= filter.DateCreatedTo.Value);
            }

        }
    }
}
