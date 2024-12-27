CREATE PROCEDURE [dbo].[sp_transfers_get]
	@id_transfers INT
AS
BEGIN
	SELECT *
	FROM Transfers T
		WHERE (T.[id_transfers] = @id_transfers OR @id_transfers IS NULL)
END