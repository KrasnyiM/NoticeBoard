namespace NoticeBoard.Web.Models
{
    public class AnnouncementUpdateViewModel : AnnouncementCreateViewModel
    {
        public int Id { get; set; }
        public bool Status { get; set; }
    }
}
