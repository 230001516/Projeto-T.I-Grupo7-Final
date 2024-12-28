CREATE PROCEDURE [dbo].[sp_pendingaccount_insert]
	@id_user INT,
    @id_worker INT,
    @motive NVARCHAR(100),
    @account_state INT  
AS
BEGIN
    INSERT INTO PendingAccounts (id_user, id_worker, motive, account_state)
    VALUES (@id_user, @id_worker, @motive, @account_state);
END;
