USE NoticeBoard;
GO

CREATE PROCEDURE sp_Announcements_Delete
    @Id INT
AS
BEGIN
    DELETE FROM Announcements
    WHERE Id = @Id;
END
GO