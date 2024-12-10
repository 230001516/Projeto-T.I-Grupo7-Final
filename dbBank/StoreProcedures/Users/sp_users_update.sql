CREATE PROCEDURE [dbo].[sp_users_update]
    @id_user INT,
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
    UPDATE dbo.Users
    SET [is_worker] = @is_worker, [firstname] = @firstname, [surname] = @surname, 
        [nif] = @nif, [user_address] = @user_address, [email] = @email, 
        [phone_number] = @phone_number, [password] = @password
    WHERE [id_user] = @id_user
END