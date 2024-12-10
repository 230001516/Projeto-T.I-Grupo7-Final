CREATE TABLE [dbo].[Workers]
(
	id_user INT PRIMARY KEY,
    work_hours INT,
    FOREIGN KEY(id_user) REFERENCES Users(id_user)
)
