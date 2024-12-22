CREATE PROCEDURE [dbo].[sp_support_get]
	@id_ticket INT
AS
BEGIN
	SELECT *
	FROM Support S
		WHERE (S.[id_ticket] = @id_ticket OR @id_ticket IS NULL)
END
