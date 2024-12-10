CREATE PROCEDURE [dbo].[sp_accounts_delete]
	@id_account INT
AS
BEGIN
	DELETE
	FROM dbo.Accounts
	WHERE id_account = @id_account
END
