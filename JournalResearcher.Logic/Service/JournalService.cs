using AutoMapper;
using JournalResearcher.DataAccess.Cores;
using JournalResearcher.DataAccess.Data.Models;
using JournalResearcher.DataAccess.Repository;
using JournalResearcher.DataAccess.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JournalResearcher.Logic.Service
{
    public interface IJournalService
    {
        Task Add(JournalViewModel model);

        IEnumerable<JournalItem> QueryThesis(int page, int count, JournalFilter filterExpression = null,
            string orderByExpression = null);

        JournalItem ApproveJournal(ApproveViewModel model);
        JournalItem RejectJournalWithComment();
        IEnumerable<JournalItem> GetJournalForApplicant(string userId);
        IEnumerable<JournalItem> GetAllThesis();
        bool IfJournalAlreadyExist(string title);
        CounterModel<JournalItem> QueryJournalCount(int page, int count, JournalFilter filter = null, string expression = null);
    }

    public class JournalService : IJournalService
    {
        private readonly IApplicantService _applicantService;

        private readonly IJournalRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public JournalService(IJournalRepository repository, IUnitOfWork unitOfWork,
            IApplicantService applicantService)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;

            _applicantService = applicantService;
        }

        public async Task Add(JournalViewModel model)
        {
            try
            {
                var entity = Mapper.Map<JournalViewModel, Journal>(model);
                entity.DateSubmitted = DateTime.UtcNow;
                entity.Applicant = await _applicantService.GetById(model.ApplicantId);
                entity.IsApproved = false;
                entity.ThesisFile = model.ThesisFileUrl;
                _repository.Insert(entity);
                model.Id = entity.Id;
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new ApplicationException("An error occurred " + e.Message);
            }
        }

        public IEnumerable<JournalItem> QueryThesis(int page, int count, JournalFilter filterExpression = null,
            string orderByExpression = null)
        {
            var orderBy = OrderExpression.Deserializer(orderByExpression);
            //var entities = _repository.GetJournalPaged(page, count, filterExpression, orderBy).Where(x => x.Applicant.Id == orderByExpression);
            var entities = _repository.GetJournalPaged(page, count, filterExpression, orderBy).ToList();
            return ProcessQuery(entities);
        }

        public JournalItem ApproveJournal(ApproveViewModel model)
        {
            var entity = GetJournal(model.Id);
            if (model.Action == "Approve") entity.IsApproved = true;
            else if (model.Action == "Reject") entity.IsApproved = false;
            _repository.Update(entity);
            _unitOfWork.SaveChanges();
            return Mapper.Map<Journal, JournalItem>(entity);
        }



        public IEnumerable<JournalItem> GetJournalForApplicant(string userId)
        {
            //  var entities = _repository.Table.Include(x => x.Applicant).Where(x => x.Applicant.Id == userId).ToList();
            var entities = _repository.Fetch(x => x.Applicant.Id == userId, includeProperties: "Applicant").ToList();
            return ProcessQuery(entities);
        }

        public IEnumerable<JournalItem> GetAllThesis()
        {
            var entities = _repository.GetAll();
            return Mapper.Map<IEnumerable<Journal>, IEnumerable<JournalItem>>(entities);
        }

        public bool IfJournalAlreadyExist(string title)
        {
            return _repository.Table.Any(x => x.Title == title);
        }

        public CounterModel<JournalItem> QueryJournalCount(int page, int count, JournalFilter filter = null, string expression = null)
        {
            int totalCount;
            var orderBy = OrderExpression.Deserializer(expression);
            var entities = _repository.GetJournalPaged(page, count, out totalCount, filter, orderBy).ToList();
            return new CounterModel<JournalItem>()
            {
                Total = totalCount,
                Items = ProcessQuery(entities)
            };
        }

        private IEnumerable<JournalItem> ProcessQuery(IEnumerable<Journal> entities)
        {
            return entities.Where(x => x.Id > 0).Select(x => new JournalItem
            {
                Title = x.Title,
                Id = x.Id,
                Abstract = x.Abstract,
                Author = x.Author,
                IsApproved = x.IsApproved,
                Reference = x.Reference,
                ThesisFileUrl = x.ThesisFile,
                SupervisorName = x.SupervisorName ?? "none"
            });
        }

        private Journal GetJournal(int id)
        {
            var entity = _repository.Get(id);
            if (entity == null) throw new NullReferenceException("Data Not Found");
            return entity;
        }

        public JournalItem RejectJournalWithComment()
        {
            throw new NotImplementedException();
        }
    }
}