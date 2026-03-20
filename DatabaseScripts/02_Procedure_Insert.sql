USE NoticeBoard;
GO

CREATE PROCEDURE sp_Announcements_Insert
    @Title NVARCHAR(100),
    @Description NVARCHAR(MAX),
    @Status BIT,
    @Category NVARCHAR(50),
    @SubCategory NVARCHAR(50),
    @NewId INT OUTPUT
AS
BEGIN
    INSERT INTO Announcements (Title, Description, Status, Category, SubCategory)
    VALUES (@Title, @Description, @Status, @Category, @SubCategory);

    SET @NewId = SCOPE_IDENTITY();
END
GO