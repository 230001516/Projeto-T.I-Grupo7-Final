CREATE PROCEDURE [dbo].[sp_pendingaccount_delete]
    @id_accountPending INT
AS
BEGIN
    DELETE FROM PendingAccounts
    WHERE id_accountPending = @id_accountPending;
END;
