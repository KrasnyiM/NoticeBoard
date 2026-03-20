USE NoticeBoard;
GO

CREATE PROCEDURE sp_Announcements_GetAll
AS
BEGIN
    SELECT Id, Title, Description, CreatedDate, Status, Category, SubCategory
    FROM Announcements
    ORDER BY CreatedDate DESC;
END
GO