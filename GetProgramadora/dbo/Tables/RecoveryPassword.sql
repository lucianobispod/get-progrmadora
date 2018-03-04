CREATE TABLE [dbo].[RecoveryPassword] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [AcessedAt] DATETIME2 (7)    NULL,
    [CreatedAt] DATETIME2 (7)    DEFAULT (getdate()) NOT NULL,
    [UserId]    UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_RecoveryPassword] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RecoveryPassword_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE
);






GO
CREATE NONCLUSTERED INDEX [IX_RecoveryPassword_UserId]
    ON [dbo].[RecoveryPassword]([UserId] ASC);

