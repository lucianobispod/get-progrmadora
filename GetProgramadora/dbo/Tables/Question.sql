CREATE TABLE [dbo].[Question] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [Content]   NVARCHAR (500)   NOT NULL,
    [CreatedAt] DATETIME2 (7)    DEFAULT (getdate()) NOT NULL,
    [DeletedAt] DATETIME2 (7)    NULL,
    [Title]     NVARCHAR (150)   NOT NULL,
    [UpdatedAt] DATETIME2 (7)    DEFAULT (getdate()) NOT NULL,
    [UserId]    UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Question] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Question_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE
);








GO
CREATE NONCLUSTERED INDEX [IX_Question_UserId]
    ON [dbo].[Question]([UserId] ASC);

