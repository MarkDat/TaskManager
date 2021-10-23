CREATE TABLE [dbo].[Phase] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (50) NULL,
    [AcceptMoveId] INT           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

