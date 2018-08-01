using AutoMapper;
using JournalResearcher.DataAccess.Cores;
using JournalResearcher.DataAccess.Data.Models;
using JournalResearcher.DataAccess.Repository;
using JournalResearcher.DataAccess.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JournalResearcher.Logic.Service
{
    public interface IJournalService
    {
        Task Add(JournalViewModel model);

        IEnumerable<JournalItem> QueryProduct(int page, int count, JournalFilter filterExpression = null,
            string orderByExpression = null);

        JournalViewModel ApproveJournal(int id);
        IEnumerable<JournalItem> GetJournalForApplicant(string userId);
        bool IfJournalAlreadyExist(string title);
    }

    public class JournalService : IJournalService
    {
        private readonly IApplicantService _applicantService;
        private readonly IMapper _mapper;
        private readonly IJournalRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public JournalService(IJournalRepository repository, IUnitOfWork unitOfWork, IMapper mapper,
            IApplicantService applicantService)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _applicantService = applicantService;
        }

        public async Task Add(JournalViewModel model)
        {
            try
            {
                var entity = _mapper.Map<JournalViewModel, Journal>(model);
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

        public IEnumerable<JournalItem> QueryProduct(int page, int count, JournalFilter filterExpression = null,
            string orderByExpression = null)
        {
            var orderBy = OrderExpression.Deserializer(orderByExpression);
            var entities = _repository.GetJournalPaged(page, count, filterExpression, orderBy).Where(x => x.Applicant.Id == orderByExpression);
            return ProcessQuery(entities);
        }

        public JournalViewModel ApproveJournal(int id)
        {
            var entity = GetJournal(id);
            entity.IsApproved = true;
            _repository.Update(entity);
            _unitOfWork.SaveChanges();
            return _mapper.Map<Journal, JournalViewModel>(entity);
        }

        public IEnumerable<JournalItem> GetJournalForApplicant(string userId)
        {
            var entities = _repository.Table.Include(x => x.Applicant).Where(x => x.Applicant.Id == userId).ToList();
            // var entities = _repository.Fetch(x => x.Applicant.Id == userId).Include(x => x.Applicant).ToList();
            return ProcessQuery(entities);
        }

        public bool IfJournalAlreadyExist(string title)
        {
            return _repository.Table.Any(x => x.Title == title);
        }

        private IEnumerable<JournalItem> ProcessQuery(IEnumerable<Journal> entities)
        {
            return entities.Where(x => x.Id > 0).Select(x => new JournalItem
            {
                Title = x.Title,
                Abstract = x.Abstract,
                Author = x.Author,
                Applicant = new UserModel
                {
                    FirstName = x.Applicant.FirstName,
                    LastName = x.Applicant.LastName,
                    Email = x.Applicant.Email
                },
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
    }
}