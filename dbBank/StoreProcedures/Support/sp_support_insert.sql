CREATE PROCEDURE [dbo].[sp_support_insert]
	@supName VARCHAR(50),
    @email VARCHAR(100),
    @subject VARCHAR(50),
    @message VARCHAR(600)

AS
BEGIN
    INSERT INTO dbo.Support([supName], [email], [subject], [message])
        VALUES (@supName, @email, @subject, @message)
    END
