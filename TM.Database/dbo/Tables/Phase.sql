CREATE TABLE [dbo].[Phase] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (50) NULL,
    [AcceptMoveId] INT           NULL,
    [Code]         VARCHAR (5)   NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

