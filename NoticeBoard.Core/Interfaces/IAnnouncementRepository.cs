using NoticeBoard.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoticeBoard.Core.Interfaces
{
    public interface IAnnouncementRepository
    {
        Task<int> CreateAsync(Announcement announcement);
        Task<IEnumerable<Announcement>> GetAllAsync();
        Task<Announcement?> GetByIdAsync(int id); 
        Task UpdateAsync(Announcement announcement);
        Task DeleteAsync(int id);
    }
}
