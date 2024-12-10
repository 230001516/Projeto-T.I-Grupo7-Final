CREATE PROCEDURE [dbo].[sp_users_delete]
	@id_user INT
AS
BEGIN
	DELETE
	FROM dbo.Users
	WHERE id_user = @id_user
END