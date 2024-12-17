CREATE PROCEDURE [dbo].[sp_developers_update]
    @id_developer INT,
	@devName VARCHAR(50),
    @description VARCHAR(255),
    @twitter VARCHAR(100),
    @instagram VARCHAR(100),
    @linkedin VARCHAR(100),
    @image VARCHAR(100)

AS
BEGIN
    UPDATE dbo.Developers
    SET devName = @devName, [description] = @description, [twitter] = @twitter, [instagram] = @instagram, [linkedin] = @linkedin, [image] = @image
    WHERE [id_developer] = @id_developer
END
