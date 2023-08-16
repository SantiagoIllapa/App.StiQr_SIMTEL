CREATE TABLE [dbo].[Labels] (
    [Id]           INT      IDENTITY (1, 1) NOT NULL,
    [Id_Vehicle]   INT      NOT NULL,
    [CreationDate] DATETIME NOT NULL,
    CONSTRAINT [PK_Labels] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Labels_Vehicles] FOREIGN KEY ([Id_Vehicle]) REFERENCES [dbo].[Vehicles] ([Id])
);

