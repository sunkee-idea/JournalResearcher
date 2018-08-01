using AutoMapper;
using JournalResearcher.DataAccess.Data.Models;
using JournalResearcher.DataAccess.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace JournalResearcher.Logic.Service
{

    public interface IApplicantService
    {
        Task<ApplicationUser> GetById(string id);
        Task<RegisterViewModel> CreateApplicant(RegisterViewModel model);
        Task<bool> IfExists(string email);
    }
    public class ApplicantService : IApplicantService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public ApplicantService(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<ApplicationUser> GetById(string id)
        {
            return await _userManager.Users.SingleAsync(x => x.Id == id);
        }

        public async Task<RegisterViewModel> CreateApplicant(RegisterViewModel model)
        {

            var user = new ApplicationUser()
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                PasswordHash = model.Password,
                UserName = model.Email,
                Role = model.Role

            };
            var result = await _userManager.CreateAsync(user, user.PasswordHash);
            if (!result.Succeeded) throw new ApplicationException("An error occurred, try again");
            if (result.Succeeded)
            {
                if (!await _userManager.IsInRoleAsync(user, model.Role))
                    await _userManager.AddToRoleAsync(user, model.Role);
                return model;
            }
            return new RegisterViewModel();
        }

        public async Task<bool> IfExists(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user != null;
        }
    }
}
