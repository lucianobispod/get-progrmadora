CREATE TABLE [dbo].[QuestionTag] (
    [QuestionId] UNIQUEIDENTIFIER NOT NULL,
    [TagId]      UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_QuestionTag] PRIMARY KEY CLUSTERED ([QuestionId] ASC, [TagId] ASC),
    CONSTRAINT [FK_QuestionTag_Question_QuestionId] FOREIGN KEY ([QuestionId]) REFERENCES [dbo].[Question] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_QuestionTag_Tag_TagId] FOREIGN KEY ([TagId]) REFERENCES [dbo].[Tag] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_QuestionTag_TagId]
    ON [dbo].[QuestionTag]([TagId] ASC);

