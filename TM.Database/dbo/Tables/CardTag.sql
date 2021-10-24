CREATE TABLE [dbo].[CardTag] (
    [Id]     INT IDENTITY (1, 1) NOT NULL,
    [CardId] INT NULL,
    [TagId]  INT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [fk_CardTag_Card] FOREIGN KEY ([CardId]) REFERENCES [dbo].[Card] ([Id]),
    CONSTRAINT [fk_CardTag_Tag] FOREIGN KEY ([TagId]) REFERENCES [dbo].[Tag] ([Id])
);

