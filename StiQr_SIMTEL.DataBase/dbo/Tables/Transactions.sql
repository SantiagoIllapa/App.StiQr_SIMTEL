CREATE TABLE [dbo].[Transactions] (
    [Id]                  INT           IDENTITY (1, 1) NOT NULL,
    [Id_Wallet]           INT           NOT NULL,
    [Id_User_Transmitter] INT           NOT NULL,
    [Type]                CHAR (5)      NOT NULL,
    [DateTime]            DATETIME      NOT NULL,
    [Amount]              INT           NOT NULL,
    [Observations]        VARCHAR (100) NULL,
    CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Transactions_Users] FOREIGN KEY ([Id_User_Transmitter]) REFERENCES [dbo].[Users] ([Id]),
    CONSTRAINT [FK_Transactions_Wallets] FOREIGN KEY ([Id_Wallet]) REFERENCES [dbo].[Wallets] ([Id])
);

