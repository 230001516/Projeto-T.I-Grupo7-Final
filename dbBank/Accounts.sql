CREATE TABLE [dbo].[Accounts]
(
	id_account INT PRIMARY KEY,
    id_pendingAccount INT NOT NULL,
    balance DECIMAL(10, 2) NOT NULL,
    account_number INT NOT NULL,
    FOREIGN KEY(id_pendingAccount) REFERENCES PendingAccounts(id_accountPending)
)
