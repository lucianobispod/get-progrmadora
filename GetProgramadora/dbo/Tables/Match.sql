CREATE TABLE [dbo].[Match] (
    [UserId]       UNIQUEIDENTIFIER NOT NULL,
    [EnterpriseId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Match] PRIMARY KEY CLUSTERED ([UserId] ASC, [EnterpriseId] ASC),
    CONSTRAINT [FK_Match_Enterprise_EnterpriseId] FOREIGN KEY ([EnterpriseId]) REFERENCES [dbo].[Enterprise] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Match_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Match_EnterpriseId]
    ON [dbo].[Match]([EnterpriseId] ASC);

