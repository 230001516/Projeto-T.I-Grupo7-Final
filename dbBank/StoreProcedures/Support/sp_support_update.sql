CREATE PROCEDURE [dbo].[sp_support_update]
    @id_ticket INT ,
    @id_user INT, 
    @supName VARCHAR(50),
    @email VARCHAR(100),
    @subject VARCHAR(50),
    @message VARCHAR(600)

AS
BEGIN
    UPDATE dbo.Support
    SET [id_user] = @id_user, [supName] = @supName, [email] = @email, [subject] = @subject, [message] = @message
    WHERE [id_ticket] = @id_ticket
END
