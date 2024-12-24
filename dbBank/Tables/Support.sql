CREATE TABLE [dbo].[Support]
(
	id_ticket INT IDENTITY(1,1) PRIMARY KEY,
    supName VARCHAR(50)  NOT NULL,
    email VARCHAR(100) NOT NULL,
    subject VARCHAR(50) NOT NULL,
    message VARCHAR(600) NOT NULL
)
