CREATE PROCEDURE [dbo].[sp_developers_get]
	@id_developer INT
AS
BEGIN
	SELECT *
	FROM Developers D
		WHERE (D.[id_developer] = @id_developer OR @id_developer IS NULL)
END

