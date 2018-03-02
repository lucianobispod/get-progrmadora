CREATE TABLE [dbo].[Enterprise] (
    [Id]              UNIQUEIDENTIFIER NOT NULL,
    [CreatedAt]       DATETIME2 (7)    NOT NULL,
    [DeletedAt]       DATETIME2 (7)    NULL,
    [Email]           NVARCHAR (MAX)   NULL,
    [EmailVerifiedAt] DATETIME2 (7)    NULL,
    [Location]        NVARCHAR (MAX)   NULL,
    [Name]            NVARCHAR (MAX)   NULL,
    [PasswordHash]    NVARCHAR (MAX)   NULL,
    [PasswordSalt]    NVARCHAR (MAX)   NULL,
    [PhoneNumber]     NVARCHAR (MAX)   NULL,
    [Picture]         NVARCHAR (MAX)   NULL,
    [State]           NVARCHAR (MAX)   NULL,
    [UpdatedAt]       DATETIME2 (7)    NOT NULL,
    CONSTRAINT [PK_Enterprise] PRIMARY KEY CLUSTERED ([Id] ASC)
);



