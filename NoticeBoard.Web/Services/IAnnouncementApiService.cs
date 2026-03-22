using NoticeBoard.Web.Models;

namespace NoticeBoard.Web.Services
{
    public interface IAnnouncementApiService
    {
        Task<IEnumerable<AnnouncementViewModel>> GetAllAsync();
        Task<AnnouncementViewModel?> GetByIdAsync(int id);
        Task<bool> CreateAsync(AnnouncementCreateViewModel model);
        Task<bool> UpdateAsync(int id, AnnouncementUpdateViewModel model);
        Task<bool> DeleteAsync(int id);
    }
}
