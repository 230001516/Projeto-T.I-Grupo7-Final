CREATE TABLE [dbo].[PendingAccounts]
(
	id_accountPending INT PRIMARY KEY,
    id_user INT NOT NULL,
    account_state INT NOT NULL,
    id_worker INT NOT NULL,
    motive VARCHAR(45) NOT NULL,
    FOREIGN KEY (id_user) REFERENCES Users(id_user),
    FOREIGN KEY (id_worker) REFERENCES Workers(id_user)
)
