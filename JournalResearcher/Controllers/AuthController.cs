using JournalResearcher.DataAccess.ViewModel;
using JournalResearcher.Logic.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Threading.Tasks;

namespace JournalResearcher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IApplicantService _applicantService;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthController(IApplicantService applicantService, RoleManager<IdentityRole> roleManager)
        {
            _applicantService = applicantService;
            _roleManager = roleManager;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (await _applicantService.IfExists(model.Email))
                throw new DuplicateNameException("Account Already Exist");
            if (!await _roleManager.RoleExistsAsync(model.Role))
                throw new ApplicationException("Sorry,something went wrong,try again");
            var user = await _applicantService.CreateApplicant(model);
            return Ok(user);
        }
    }
}