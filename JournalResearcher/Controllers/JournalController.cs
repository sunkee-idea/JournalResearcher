using JournalResearcher.DataAccess.ViewModel;
using JournalResearcher.Logic.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;

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


        [HttpGet("getThesis")]
        public IActionResult Get(string filter, int page, int pageSize, string orderBy)
        {


            var filtered = JournalFilter.Deserializer(filter);
            var response = _journalService.QueryJournalCount(page, pageSize, filtered, orderBy);
            return Ok(response);

        }


        [HttpPut("approveThesis")]
        public IActionResult ApproveThesis([FromBody] ApproveViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            _journalService.ApproveJournal(model);
            return Ok();
        }

        // GET: api/Journal/userId

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
                return Ok(model);
            }
            catch (Exception e)
            {
                return BadRequest("Cannot connect to the server at the moment, try again");
            }
        }



    }
}