USE NoticeBoard;
GO

CREATE PROCEDURE sp_Announcements_GetById
    @Id INT
AS
BEGIN
    SELECT Id, Title, Description, CreatedDate, Status, Category, SubCategory
    FROM Announcements
    WHERE Id = @Id;
END
GO