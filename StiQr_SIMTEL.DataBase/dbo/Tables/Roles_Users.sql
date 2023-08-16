CREATE TABLE [dbo].[Roles_Users] (
    [Id]      INT IDENTITY (1, 1) NOT NULL,
    [Id_User] INT NOT NULL,
    [Id_Role] INT NOT NULL,
    CONSTRAINT [PK_Roles_Users] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Roles_Users_Roles] FOREIGN KEY ([Id_Role]) REFERENCES [dbo].[Roles] ([Id]),
    CONSTRAINT [FK_Roles_Users_Users] FOREIGN KEY ([Id_User]) REFERENCES [dbo].[Users] ([Id])
);

