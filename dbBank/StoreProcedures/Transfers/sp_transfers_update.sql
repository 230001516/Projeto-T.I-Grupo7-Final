CREATE PROCEDURE [dbo].[sp_transfers_update]
    @id_transfers INT,
	@id_account INT,
    @transfer_value DECIMAL(10,2),
    @account_number INT

AS
BEGIN
    UPDATE dbo.Transfers
    SET [id_account] = @id_account, [transfer_value] =  @transfer_value, [account_number] = @account_number
    WHERE [id_transfers] = @id_transfers
END
