CREATE TABLE [dbo].[CardAssign] (
    [Id]         INT IDENTITY (1, 1) NOT NULL,
    [UserId]     INT NULL,
    [CardId]     INT NULL,
    [IsAssigned] BIT DEFAULT ('True') NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [fk_assign_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [fk_CardAssign] FOREIGN KEY ([CardId]) REFERENCES [dbo].[Card] ([Id])
);

