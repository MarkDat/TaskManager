CREATE TABLE [dbo].[CardMovement] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [CardId]      INT           NULL,
    [PhaseId]     INT           NULL,
    [CreatedDate] DATETIME      DEFAULT (getdate()) NULL,
    [UpdatedDate] DATETIME      DEFAULT (getdate()) NULL,
    [CreatedBy]   NVARCHAR (25) NULL,
    [UpdatedBy]   NVARCHAR (25) NULL,
    [IsCurrent]   BIT           DEFAULT ('True') NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [fk_CardMovement_Card] FOREIGN KEY ([CardId]) REFERENCES [dbo].[Card] ([Id]),
    CONSTRAINT [fk_CardMovement_Phase] FOREIGN KEY ([PhaseId]) REFERENCES [dbo].[Phase] ([Id])
);

