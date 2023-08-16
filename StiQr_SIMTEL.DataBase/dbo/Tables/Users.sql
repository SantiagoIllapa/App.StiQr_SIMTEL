CREATE TABLE [dbo].[Users] (
    [Id]        INT          IDENTITY (1, 1) NOT NULL,
    [Name]      VARCHAR (50) NULL,
    [LastName]  VARCHAR (50) NULL,
    [CIdentity] CHAR (10)    NOT NULL,
    [Password]  VARCHAR (50) NOT NULL,
    [Email]     VARCHAR (50) NULL,
    [Phone]     VARCHAR (50) NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
);

