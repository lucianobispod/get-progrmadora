CREATE TABLE [dbo].[Comment] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [CommentText] NVARCHAR (MAX)   NULL,
    [CreatedAt]   DATETIME2 (7)    NOT NULL,
    [DeletedAt]   DATETIME2 (7)    NULL,
    [QuestionId]  UNIQUEIDENTIFIER NOT NULL,
    [UpdatedAt]   DATETIME2 (7)    NOT NULL,
    [UserId]      UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Comment_Question] FOREIGN KEY ([QuestionId]) REFERENCES [dbo].[Question] ([Id]),
    CONSTRAINT [FK_Comment_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id])
);



