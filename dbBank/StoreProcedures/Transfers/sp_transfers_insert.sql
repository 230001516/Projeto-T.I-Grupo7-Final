CREATE PROCEDURE [dbo].[sp_transfers_insert]
    @transfer_date DATE,
    @transfer_value DECIMAL(10,2),
    @account_number INT

AS
BEGIN
    INSERT INTO dbo.Transfers([transfer_date], [transfer_value], [account_number])
        VALUES (@transfer_date, @transfer_value, @account_number)
    END

