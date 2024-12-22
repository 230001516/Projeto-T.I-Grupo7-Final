CREATE PROCEDURE [dbo].[sp_developers_insert]
	@devName VARCHAR(50),
    @devDescription VARCHAR(255),
    @twitter VARCHAR(100),
    @instagram VARCHAR(100),
    @linkedin VARCHAR(100),
    @devImage VARCHAR(100)

AS
BEGIN
    INSERT INTO dbo.Developers([devName], [devDescription], [twitter], [instagram], [linkedin], [devImage])
        VALUES (@devName, @devDescription, @twitter, @instagram, @linkedin, @devImage)
    END
