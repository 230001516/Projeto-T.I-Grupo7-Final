CREATE TABLE [dbo].[Clients]
(
	id_user INT IDENTITY(1,1) PRIMARY KEY,
    FOREIGN KEY(id_user) REFERENCES Users(id_user)
)
