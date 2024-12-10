CREATE PROCEDURE [dbo].[sp_users_get]
	@id_user INT
AS
BEGIN
	SELECT *
	FROM Users U
		WHERE (U.[id_user] = @id_user OR @id_user IS NULL)
END
