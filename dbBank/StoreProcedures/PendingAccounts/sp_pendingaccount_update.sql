CREATE PROCEDURE [dbo].[sp_pendingaccount_update]
	@id_accountPending INT,
    @account_state INT
AS
BEGIN
    UPDATE PendingAccounts
    SET account_state = @account_state
    WHERE id_accountPending = id_accountPending;
END;
