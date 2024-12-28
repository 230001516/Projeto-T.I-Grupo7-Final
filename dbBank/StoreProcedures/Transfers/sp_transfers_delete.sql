CREATE PROCEDURE [dbo].[sp_transfers_delete]
	@id_transfers INT
AS
BEGIN
	DELETE
	FROM dbo.Transfers
	WHERE id_transfers = @id_transfers
END

