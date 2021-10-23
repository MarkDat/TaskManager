CREATE TABLE [dbo].[Project] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50) NULL,
    [CreatedDate] DATETIME      DEFAULT (getdate()) NULL,
    [UpdatedDate] DATETIME      DEFAULT (getdate()) NULL,
    [CreatedBy]   NVARCHAR (25) NULL,
    [UpdatedBy]   NVARCHAR (25) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

