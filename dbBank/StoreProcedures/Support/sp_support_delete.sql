CREATE PROCEDURE [dbo].[sp_support_delete]
	@id_ticket INT
AS
BEGIN
	DELETE
	FROM dbo.Support
	WHERE id_ticket = @id_ticket
END
