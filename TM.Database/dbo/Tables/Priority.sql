CREATE TABLE [dbo].[Priority] (
    [Id]    INT           IDENTITY (1, 1) NOT NULL,
    [Name]  NVARCHAR (20) NULL,
    [Color] VARCHAR (15)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

