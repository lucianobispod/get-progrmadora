CREATE TABLE [dbo].[AcademicQualification] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [Course]      NVARCHAR (100)   NOT NULL,
    [CreatedAt]   DATETIME2 (7)    DEFAULT (getdate()) NOT NULL,
    [FinishedAt]  DATETIME2 (7)    NOT NULL,
    [Institution] NVARCHAR (100)   NOT NULL,
    [Period]      NVARCHAR (50)    NOT NULL,
    [StartedAt]   DATETIME2 (7)    NOT NULL,
    [UpdatedAt]   DATETIME2 (7)    DEFAULT (getdate()) NOT NULL,
    [UserId]      UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_AcademicQualification] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AcademicQualification_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_AcademicQualification_UserId]
    ON [dbo].[AcademicQualification]([UserId] ASC);

