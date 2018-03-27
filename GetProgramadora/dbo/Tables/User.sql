CREATE TABLE [dbo].[User] (
    [Id]              UNIQUEIDENTIFIER NOT NULL,
    [CreatedAt]       DATETIME2 (7)    DEFAULT (getdate()) NOT NULL,
    [DeletedAt]       DATETIME2 (7)    NULL,
    [Description]     NVARCHAR (100)   NULL,
    [Email]           NVARCHAR (70)    NOT NULL,
    [EmailConfirmed]  BIT              NOT NULL,
    [EmailVerifiedAt] DATETIME2 (7)    NULL,
    [LastName]        NVARCHAR (50)    NOT NULL,
    [Location]        NVARCHAR (100)   NULL,
    [Name]            NVARCHAR (50)    NOT NULL,
    [PasswordHash]    NVARCHAR (MAX)   NOT NULL,
    [PasswordSalt]    NVARCHAR (MAX)   NOT NULL,
    [PhoneNumber]     NVARCHAR (14)    NULL,
    [Picture]         NVARCHAR (MAX)   NULL,
    [Points]          INT              NOT NULL,
    [State]           NVARCHAR (2)     NULL,
    [UpdatedAt]       DATETIME2 (7)    DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC)
);








GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_User_Email]
    ON [dbo].[User]([Email] ASC);

