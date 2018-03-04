CREATE TABLE [dbo].[Comment] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [CommentText] NVARCHAR (200)   NOT NULL,
    [CreatedAt]   DATETIME2 (7)    DEFAULT (getdate()) NOT NULL,
    [DeletedAt]   DATETIME2 (7)    NULL,
    [QuestionId]  UNIQUEIDENTIFIER NOT NULL,
    [UpdatedAt]   DATETIME2 (7)    DEFAULT (getdate()) NOT NULL,
    [UserId]      UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Comment_Question_QuestionId] FOREIGN KEY ([QuestionId]) REFERENCES [dbo].[Question] ([Id]),
    CONSTRAINT [FK_Comment_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE
);






GO
CREATE NONCLUSTERED INDEX [IX_Comment_UserId]
    ON [dbo].[Comment]([UserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Comment_QuestionId]
    ON [dbo].[Comment]([QuestionId] ASC);

