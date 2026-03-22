using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoticeBoard.Api.DTOs;
using NoticeBoard.Core.Entities;
using NoticeBoard.Core.Interfaces;

namespace NoticeBoard.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnnouncementsController : ControllerBase
    {
        private readonly IAnnouncementRepository _repository;

        public AnnouncementsController(IAnnouncementRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var announcements = await _repository.GetAllAsync();
            return Ok(announcements); 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var announcement = await _repository.GetByIdAsync(id);
            if (announcement == null)
                return NotFound(); 

            return Ok(announcement);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAnnouncementDto dto)
        {
            var announcement = new Announcement
            {
                Title = dto.Title,
                Description = dto.Description,
                Status = true, 
                Category = dto.Category,
                SubCategory = dto.SubCategory
            };

            var newId = await _repository.CreateAsync(announcement);

            return CreatedAtAction(nameof(GetById), new { id = newId }, newId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAnnouncementDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            existing.Title = dto.Title;
            existing.Description = dto.Description;
            existing.Status = dto.Status;
            existing.Category = dto.Category;
            existing.SubCategory = dto.SubCategory;

            await _repository.UpdateAsync(existing);
            return NoContent(); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
