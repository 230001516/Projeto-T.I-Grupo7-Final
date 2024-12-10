CREATE TABLE [dbo].[Transfers]
(
	id_transfers INT PRIMARY KEY,
    id_account INT NOT NULL,
    transfer_date DATE NOT NULL,
    transfer_value DECIMAL(10,2) NOT NULL,
    account_number INT NOT NULL,
    FOREIGN KEY (id_account) REFERENCES Accounts(id_account)
)

