CREATE PROCEDURE [dbo].[sp_clients_insert]
	@is_worker INT,
	@firstname VARCHAR(50),
    @surname VARCHAR(50),
    @nif INT,
    @user_address VARCHAR(100), 
    @email VARCHAR(100), 
    @phone_number INT ,
    @password VARCHAR(20)

AS
BEGIN
    INSERT INTO dbo.Users([is_worker], [firstname], [surname], [nif], [user_address], [email], [phone_number], [password])
        VALUES (@is_worker, @firstname, @surname, @nif, @user_address, @email, @phone_number, @password)
    END