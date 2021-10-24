CREATE TABLE [dbo].[Tag] (
    [Id]    INT           IDENTITY (1, 1) NOT NULL,
    [Name]  NVARCHAR (20) NULL,
    [Color] NVARCHAR (20) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

