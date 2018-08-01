using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace JournalResearcher.DataAccess.Data.Models
{
    public class Journal
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

        [DefaultValue(false)]
        public bool IsApproved { get; set; }

        [Required]
        public DateTime ThesisDateTime { get; set; }

        [Required]
        public string ThesisFile { get; set; }

        [Required]
        public ApplicationUser Applicant { get; set; }

        public DateTime DateSubmitted { get; set; }
    }
}
