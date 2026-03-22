using System.ComponentModel.DataAnnotations;

namespace NoticeBoard.Web.Models
{
    public class AnnouncementCreateViewModel
    {
        [Required(ErrorMessage = "Заголовок є обов'язковим")]
        [StringLength(100, ErrorMessage = "Заголовок не може перевищувати 100 символів")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Опис є обов'язковим")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Оберіть категорію")]
        public string Category { get; set; } = string.Empty;

        [Required(ErrorMessage = "Оберіть підкатегорію")]
        public string SubCategory { get; set; } = string.Empty;
    }
}
