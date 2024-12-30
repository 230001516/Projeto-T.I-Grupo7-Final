CREATE TABLE [dbo].[PendingAccounts]
(
	id_accountPending INT IDENTITY(1,1) PRIMARY KEY,
    id_user nvarchar(450) NOT NULL,
    account_state INT NOT NULL,
    id_worker nvarchar(450) NOT NULL,
    motive VARCHAR(45) NOT NULL,
    FOREIGN KEY (id_worker) REFERENCES Workers(id_user)
)
