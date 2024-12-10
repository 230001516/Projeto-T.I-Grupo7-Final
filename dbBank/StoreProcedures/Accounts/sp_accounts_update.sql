CREATE PROCEDURE [dbo].[sp_accounts_update]
    @id_account INT,
	@id_pendingAccount INT,
    @balance DECIMAL(10, 2),
    @account_number INT

AS
BEGIN
    UPDATE dbo.Accounts
    SET [id_pendingAccount] = @id_pendingAccount, [balance] = @balance, [account_number] = @account_number
    WHERE [id_account] = @id_account
END