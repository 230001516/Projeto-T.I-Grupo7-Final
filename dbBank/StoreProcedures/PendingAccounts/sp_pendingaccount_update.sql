﻿CREATE PROCEDURE [dbo].[sp_pendingaccount_update]
	@id_accountPending INT,
    @id_user Nvarchar(450),
    @id_worker Nvarchar(450),
    @motive NVARCHAR(100),
    @account_state INT  
AS
BEGIN
    UPDATE PendingAccounts
    SET [id_user] = @id_user, [id_worker] =  @id_worker, [account_state] = @account_state, [motive] = @motive
    WHERE id_accountPending = id_accountPending;
END;
