CREATE PROCEDURE [dbo].[sp_transfers_insert]
    @id_account    INT,
    @transfer_date DATE,
    @transfer_value DECIMAL(10,2),
    @account_number INT

AS
BEGIN
    INSERT INTO dbo.Transfers([id_account],[transfer_date], [transfer_value], [account_number])
        VALUES (@id_account, @transfer_date, @transfer_value, @account_number)
    END

