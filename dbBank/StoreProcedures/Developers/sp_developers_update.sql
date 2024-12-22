CREATE PROCEDURE [dbo].[sp_developers_update]
    @id_developer INT,
	@devName VARCHAR(50),
    @devDescription VARCHAR(255),
    @twitter VARCHAR(100),
    @instagram VARCHAR(100),
    @linkedin VARCHAR(100),
    @devImage VARCHAR(100)

AS
BEGIN
    UPDATE dbo.Developers
    SET devName = @devName, [devDescription] = @devDescription, [twitter] = @twitter, [instagram] = @instagram, [linkedin] = @linkedin, [devImage] = @devImage
    WHERE [id_developer] = @id_developer
END
