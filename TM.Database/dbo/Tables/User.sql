CREATE TABLE [dbo].[User] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [UserName]  VARCHAR (20)  NULL,
    [Password]  VARCHAR (20)  NULL,
    [Image]     TEXT          NULL,
    [FirstName] NVARCHAR (25) NULL,
    [LastName]  NVARCHAR (25) NULL,
    [IsActive]  BIT           DEFAULT ('True') NULL,
    [Phone]     VARCHAR (15)  NULL,
    [Birthday]  DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

