CREATE TABLE [dbo].[User] (
    [Id]              UNIQUEIDENTIFIER NOT NULL,
    [CreatedAt]       DATETIME2 (7)    NOT NULL,
    [DeletedAt]       DATETIME2 (7)    NULL,
    [Description]     NVARCHAR (MAX)   NULL,
    [Email]           NVARCHAR (MAX)   NULL,
    [EmailConfirmed]  BIT              NOT NULL,
    [EmailVerifiedAt] DATETIME2 (7)    NULL,
    [Name]            NVARCHAR (MAX)   NULL,
    [PasswordHash]    NVARCHAR (MAX)   NULL,
    [PasswordSalt]    NVARCHAR (MAX)   NULL,
    [PhoneNumber]     NVARCHAR (MAX)   NULL,
    [Picture]         NVARCHAR (MAX)   NULL,
    [UpdatedAt]       DATETIME2 (7)    NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC)
);



