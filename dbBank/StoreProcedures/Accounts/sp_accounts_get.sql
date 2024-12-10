CREATE PROCEDURE [dbo].[sp_accounts_get]
	@id_account INT
AS
BEGIN
	SELECT *
	FROM Accounts A
		WHERE (A.[id_account] = @id_account OR @id_account IS NULL)
END
