CREATE PROCEDURE [dbo].[sp_pendingaccount_get]
	@id_accountPending INT
AS
BEGIN
	SELECT *
	FROM PendingAccounts PA
		WHERE (PA.[id_accountPending] = @id_accountPending OR @id_accountPending IS NULL)
END
