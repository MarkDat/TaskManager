CREATE TABLE [dbo].[ProjectMember] (
    [Id]        INT IDENTITY (1, 1) NOT NULL,
    [UserId]    INT NULL,
    [ProjectId] INT NULL,
    [IsActive]  BIT DEFAULT ('True') NULL,
    [IsOwner]   BIT DEFAULT ('False') NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [fk_member_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [fk_ProjectMember] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Project] ([Id])
);

