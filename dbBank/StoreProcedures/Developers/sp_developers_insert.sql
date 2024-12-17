CREATE PROCEDURE [dbo].[sp_developers_insert]
	@devName VARCHAR(50),
    @description VARCHAR(255),
    @twitter VARCHAR(100),
    @instagram VARCHAR(100),
    @linkedin VARCHAR(100),
    @image VARCHAR(100)

AS
BEGIN
    INSERT INTO dbo.Developers([devName], [description], [twitter], [instagram], [linkedin], [image])
        VALUES (@devName, @description, @twitter, @instagram, @linkedin, @image)
    END
