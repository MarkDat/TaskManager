CREATE TABLE [dbo].[CardHistory] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Content]     NVARCHAR (200) NULL,
    [CardId]      INT            NULL,
    [CreatedDate] DATETIME       DEFAULT (getdate()) NULL,
    [UpdatedDate] DATETIME       DEFAULT (getdate()) NULL,
    [CreatedBy]   NVARCHAR (25)  NULL,
    [ActionType]  NVARCHAR (25)  NULL,
    [UpdatedBy]   NVARCHAR (25)  NULL,
    [UserId]      INT            NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [fk_CardHistory_Card] FOREIGN KEY ([CardId]) REFERENCES [dbo].[Card] ([Id]),
    CONSTRAINT [fk_CardHistory_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id])
);

