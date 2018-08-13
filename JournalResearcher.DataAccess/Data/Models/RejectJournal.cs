namespace JournalResearcher.DataAccess.Data.Models
{
    public class RejectJournal
    {
        public int Id { get; set; }
        public int RejectedJournalId { get; set; }
        public string Comment { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Journal Journal { get; set; }

    }
}
