CREATE TABLE [dbo].[ProjectPhase] (
    [Id]        INT IDENTITY (1, 1) NOT NULL,
    [ProjectId] INT NULL,
    [PhaseId]   INT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [fk_ProjectPhase_Phase] FOREIGN KEY ([PhaseId]) REFERENCES [dbo].[Phase] ([Id]),
    CONSTRAINT [fk_ProjectPhase_Project] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Project] ([Id])
);

