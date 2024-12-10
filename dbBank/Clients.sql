CREATE TABLE [dbo].[Clients]
(
	id_user INT PRIMARY KEY,
    FOREIGN KEY(id_user) REFERENCES Users(id_user)
)
