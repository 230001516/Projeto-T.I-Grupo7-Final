﻿CREATE TABLE [dbo].[Users]
(
	id_user INT IDENTITY(1,1) PRIMARY KEY,
    is_worker INT,
    firstname VARCHAR(50) NOT NULL,
    surname VARCHAR(50) NOT NULL,
    nif INT UNIQUE NOT NULL,
    user_address VARCHAR(100) NOT NULL, 
    email VARCHAR(100) NOT NULL, 
    phone_number INT UNIQUE NOT NULL,
    password VARCHAR(20) NOT NULL
)
