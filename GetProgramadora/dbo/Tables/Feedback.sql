CREATE TABLE [dbo].[Feedback] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [Content]   NVARCHAR (200)   NOT NULL,
    [CreatedAt] DATETIME2 (7)    DEFAULT (getdate()) NOT NULL,
    [DeletedAt] DATETIME2 (7)    NULL,
    [Title]     NVARCHAR (100)   NOT NULL,
    [UserId]    UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Feedback] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Feedback_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE
);






GO
CREATE NONCLUSTERED INDEX [IX_Feedback_UserId]
    ON [dbo].[Feedback]([UserId] ASC);

