USE NoticeBoard;
GO

CREATE PROCEDURE sp_Announcements_Update
    @Id INT,
    @Title NVARCHAR(100),
    @Description NVARCHAR(MAX),
    @Status BIT,
    @Category NVARCHAR(50),
    @SubCategory NVARCHAR(50)
AS
BEGIN
    UPDATE Announcements
    SET Title = @Title,
        Description = @Description,
        Status = @Status,
        Category = @Category,
        SubCategory = @SubCategory
    WHERE Id = @Id;
END
GO