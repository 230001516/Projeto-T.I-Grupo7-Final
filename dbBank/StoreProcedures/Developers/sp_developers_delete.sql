CREATE PROCEDURE [dbo].[sp_developers_delete]
	@id_developers INT
AS
BEGIN
	DELETE
	FROM dbo.Developers
	WHERE id_developer = @id_developers
END
