CREATE TABLE [dbo].[Todo] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50) NULL,
    [IsCheck]     BIT           DEFAULT ('False') NULL,
    [CreatedDate] DATETIME      DEFAULT (getdate()) NULL,
    [UpdatedDate] DATETIME      DEFAULT (getdate()) NULL,
    [CreatedBy]   NVARCHAR (25) NULL,
    [UpdatedBy]   NVARCHAR (25) NULL,
    [ParentId]    INT           NULL,
    [CardId]      INT           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [fk_Todo_Card] FOREIGN KEY ([CardId]) REFERENCES [dbo].[Card] ([Id]),
    CONSTRAINT [fk_Todo_Todo] FOREIGN KEY ([ParentId]) REFERENCES [dbo].[Todo] ([Id])
);

