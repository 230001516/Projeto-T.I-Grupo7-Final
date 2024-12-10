CREATE PROCEDURE [dbo].[sp_accounts_insert]
    @id_pendingAccount INT,
    @balance DECIMAL(10, 2),
    @account_number INT

AS
BEGIN
    INSERT INTO dbo.Accounts([id_pendingAccount], [balance], [account_number])
        VALUES (@id_pendingAccount, @balance, @account_number)
    END