CREATE TABLE [dbo].[Roles_Permissions] (
    [Id]            INT IDENTITY (1, 1) NOT NULL,
    [Id_Role]       INT NOT NULL,
    [Id_Permission] INT NOT NULL,
    CONSTRAINT [PK_Roles_Permissions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Roles_Permissions_Permissions] FOREIGN KEY ([Id_Permission]) REFERENCES [dbo].[Permissions] ([Id]),
    CONSTRAINT [FK_Roles_Permissions_Roles] FOREIGN KEY ([Id_Role]) REFERENCES [dbo].[Roles] ([Id])
);

