CREATE TABLE [dbo].[Question] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [CreatedAt]     DATETIME2 (7)    NOT NULL,
    [DeletedAt]     DATETIME2 (7)    NULL,
    [Message]       NVARCHAR (MAX)   NULL,
    [QuestionTagId] UNIQUEIDENTIFIER NOT NULL,
    [Title]         NVARCHAR (MAX)   NULL,
    [UpdatedAt]     DATETIME2 (7)    NOT NULL,
    [UserId]        UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Question] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Question_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE
);




GO
CREATE NONCLUSTERED INDEX [IX_Question_UserId]
    ON [dbo].[Question]([UserId] ASC);

