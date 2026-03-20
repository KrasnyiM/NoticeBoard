using Dapper;
using Microsoft.Data.SqlClient;
using NoticeBoard.Core.Entities;
using NoticeBoard.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace NoticeBoard.Infrastructure.Repositories
{
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly string _connectionString;

        
        public AnnouncementRepository(IConfiguration configuration) //configure later
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentNullException(nameof(configuration), "Connection string is missing");
        }

        public async Task<int> CreateAsync(Announcement announcement)
        {
            using var connection = new SqlConnection(_connectionString);

            var parameters = new DynamicParameters();
            parameters.Add("@Title", announcement.Title);
            parameters.Add("@Description", announcement.Description);
            parameters.Add("@Status", announcement.Status);
            parameters.Add("@Category", announcement.Category);
            parameters.Add("@SubCategory", announcement.SubCategory);
            parameters.Add("@NewId", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await connection.ExecuteAsync(
                "sp_Announcements_Insert",
                parameters,
                commandType: CommandType.StoredProcedure);

            return parameters.Get<int>("@NewId");
        }

        public async Task<IEnumerable<Announcement>> GetAllAsync()
        {
            using var connection = new SqlConnection(_connectionString);

            return await connection.QueryAsync<Announcement>(
                "sp_Announcements_GetAll",
                commandType: CommandType.StoredProcedure);
        }

        public async Task<Announcement?> GetByIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);

            return await connection.QuerySingleOrDefaultAsync<Announcement>(
                "sp_Announcements_GetById",
                new { Id = id },
                commandType: CommandType.StoredProcedure);
        }

        public async Task UpdateAsync(Announcement announcement)
        {
            using var connection = new SqlConnection(_connectionString);

            await connection.ExecuteAsync(
                "sp_Announcements_Update",
                new
                {
                    announcement.Id,
                    announcement.Title,
                    announcement.Description,
                    announcement.Status,
                    announcement.Category,
                    announcement.SubCategory
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);

            await connection.ExecuteAsync(
                "sp_Announcements_Delete",
                new { Id = id },
                commandType: CommandType.StoredProcedure);
        }

    }
}
