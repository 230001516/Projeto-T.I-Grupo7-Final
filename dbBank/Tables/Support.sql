CREATE TABLE [dbo].[Support]
(
	id_ticket INT PRIMARY KEY,
    id_user INT NOT NULL,
    supName VARCHAR(50)  NOT NULL,
    email VARCHAR(100) NOT NULL,
    subject VARCHAR(50) NOT NULL,
    message VARCHAR(600) NOT NULL,
    FOREIGN KEY(id_user) REFERENCES Users(id_user)
)
