CREATE PROCEDURE [dbo].[sp_accounts_insert]
    @balance DECIMAL(10, 2),
    @account_number INT

AS
BEGIN
    INSERT INTO dbo.Accounts([balance], [account_number])
        VALUES (@balance, @account_number)
    END