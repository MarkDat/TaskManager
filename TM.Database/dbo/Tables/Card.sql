CREATE TABLE [dbo].[Card] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50)  NULL,
    [Description] NVARCHAR (500) NULL,
    [CreatedDate] DATETIME       DEFAULT (getdate()) NULL,
    [UpdatedDate] DATETIME       DEFAULT (getdate()) NULL,
    [CreatedBy]   NVARCHAR (25)  NULL,
    [UpdatedBy]   NVARCHAR (25)  NULL,
    [DueDate]     DATETIME       NULL,
    [ProjectId]   INT            NULL,
    [PriorityId]  INT            NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [fk_Card_Priority] FOREIGN KEY ([PriorityId]) REFERENCES [dbo].[Priority] ([Id]),
    CONSTRAINT [fk_Card_Project] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Project] ([Id])
);

