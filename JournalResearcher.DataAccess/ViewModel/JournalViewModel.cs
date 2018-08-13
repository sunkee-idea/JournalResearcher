using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace JournalResearcher.DataAccess.ViewModel
{
    public class JournalViewModel
    {
        public int Id { get; set; }
        [Required, StringLength(255)]
        public string Title { get; set; }

        [Required]
        public string Abstract { get; set; }

        [Required, StringLength(255)]
        public string Author { get; set; }

        [Required]
        public string Reference { get; set; }

        [Required, StringLength(255)]
        public string SupervisorName { get; set; }

        public bool IsApproved { get; set; }
        [Required]
        public string ApplicantId { get; set; }

        [Required]
        public DateTime ThesisDateTime { get; set; }

        [Required]
        public IFormFile ThesisFile { get; set; }

        public string ThesisFileUrl { get; set; }


    }

    public class ApproveViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Action { get; set; }
    }

    public class JournalItem : JournalViewModel
    {
        public DateTime DateSubmitted { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        /// public UserModel Applicant { get; set; }

    }

    public class JournalFilter : JournalItem
    {
        public DateTime? DateCreatedFrom { get; set; }
        public DateTime? DateCreatedTo { get; set; }

        public static JournalFilter Deserializer(string whereCondition)
        {
            JournalFilter filter = null;
            if (!string.IsNullOrEmpty(whereCondition))
            {
                filter = JsonConvert.DeserializeObject<JournalFilter>(whereCondition);
            }

            return filter ?? new JournalFilter();
        }
    }
}
