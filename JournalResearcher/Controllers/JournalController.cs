using JournalResearcher.DataAccess.ViewModel;
using JournalResearcher.Logic.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace JournalResearcher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JournalController : ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IJournalService _journalService;
        private readonly IUploadService _uploadService;

        public JournalController(IJournalService journalService, IHostingEnvironment hostingEnvironment,
            IUploadService uploadService)
        {
            _journalService = journalService;
            _hostingEnvironment = hostingEnvironment;
            _uploadService = uploadService;
        }

        [Route("getThesis")]
        [HttpGet("getThesis")]
        public IActionResult GetThesis()
        {
            return Ok(_journalService.GetAllThesis());
        }

        [Route("approveThesis")]
        [HttpPost("approveThesis")]
        public IActionResult ApproveThesis([FromBody] ApproveViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok();
        }

        // GET: api/Journal/userId
        [Route("get/{userId}")]
        [HttpGet("get/{userId}")]
        public IActionResult GetJournal(string userId)
        {

            var journal = _journalService.GetJournalForApplicant(userId);
            return Ok(journal);
        }

      
        // POST: api/Journal
        [HttpPost("create")]
        public async Task<IActionResult> Post([FromForm] JournalViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                if (_journalService.IfJournalAlreadyExist(model.Title))
                    throw new DuplicateNameException("Duplicate Entry");
                var fileUploadPath = Path.Combine(_hostingEnvironment.WebRootPath, "upload/thesisFile");
                model.ThesisFileUrl = await _uploadService.FileUploader(model.ThesisFile, fileUploadPath);
                await _journalService.Add(model);
                return Ok("Success");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

      
      
    }
}