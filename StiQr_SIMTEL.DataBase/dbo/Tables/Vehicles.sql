CREATE TABLE [dbo].[Vehicles] (
    [Id]          INT           NOT NULL,
    [Id_User]     INT           NOT NULL,
    [Plate]       VARCHAR (10)  NOT NULL,
    [Alias]       VARCHAR (50)  NULL,
    [Description] VARCHAR (100) NULL,
    CONSTRAINT [PK_Vehicles] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Vehicles_Users] FOREIGN KEY ([Id_User]) REFERENCES [dbo].[Users] ([Id])
);

