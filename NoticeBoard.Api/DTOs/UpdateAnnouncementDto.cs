namespace NoticeBoard.Api.DTOs
{
    public class UpdateAnnouncementDto : CreateAnnouncementDto
    {
        public bool Status { get; set; }
    }
}
