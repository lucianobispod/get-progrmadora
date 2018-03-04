CREATE TABLE [dbo].[Enterprise] (
    [Id]              UNIQUEIDENTIFIER NOT NULL,
    [CreatedAt]       DATETIME2 (7)    DEFAULT (getdate()) NOT NULL,
    [DeletedAt]       DATETIME2 (7)    NULL,
    [Email]           NVARCHAR (100)   NOT NULL,
    [EmailVerifiedAt] DATETIME2 (7)    NULL,
    [Location]        NVARCHAR (50)    NULL,
    [Name]            NVARCHAR (200)   NOT NULL,
    [PasswordHash]    NVARCHAR (MAX)   NULL,
    [PasswordSalt]    NVARCHAR (MAX)   NULL,
    [PhoneNumber]     NVARCHAR (14)    NULL,
    [Picture]         NVARCHAR (MAX)   NULL,
    [State]           NVARCHAR (2)     NULL,
    [UpdatedAt]       DATETIME2 (7)    DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Enterprise] PRIMARY KEY CLUSTERED ([Id] ASC)
);






GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Enterprise_Name]
    ON [dbo].[Enterprise]([Name] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Enterprise_Email]
    ON [dbo].[Enterprise]([Email] ASC);

