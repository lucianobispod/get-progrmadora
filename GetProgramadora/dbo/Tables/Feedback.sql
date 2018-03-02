CREATE TABLE [dbo].[Feedback] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [Content]   NVARCHAR (MAX)   NULL,
    [CreatedAt] DATETIME2 (7)    NOT NULL,
    [DeletedAt] DATETIME2 (7)    NULL,
    [Title]     NVARCHAR (MAX)   NULL,
    [UserId]    UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Feedback] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Feedback_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE
);




GO
CREATE NONCLUSTERED INDEX [IX_Feedback_UserId]
    ON [dbo].[Feedback]([UserId] ASC);

