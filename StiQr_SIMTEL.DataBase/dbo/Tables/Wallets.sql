CREATE TABLE [dbo].[Wallets] (
    [Id]             INT IDENTITY (1, 1) NOT NULL,
    [Id_User_Driver] INT NOT NULL,
    [Amount]         INT NOT NULL,
    [State]          BIT NOT NULL,
    CONSTRAINT [PK_Wallets] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Wallets_Users] FOREIGN KEY ([Id_User_Driver]) REFERENCES [dbo].[Users] ([Id])
);

