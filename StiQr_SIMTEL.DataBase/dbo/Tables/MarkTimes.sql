CREATE TABLE [dbo].[MarkTimes] (
    [Id]             INT      IDENTITY (1, 1) NOT NULL,
    [Id_Label]       INT      NOT NULL,
    [Id_Transaction] INT      NOT NULL,
    [Time_Admission] DATETIME NOT NULL,
    [Time_Departure] DATETIME NOT NULL,
    CONSTRAINT [PK_MarkTimes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MarkTimes_Labels] FOREIGN KEY ([Id_Label]) REFERENCES [dbo].[Labels] ([Id]),
    CONSTRAINT [FK_MarkTimes_Transactions] FOREIGN KEY ([Id_Transaction]) REFERENCES [dbo].[Transactions] ([Id])
);

