CREATE TABLE [dbo].[Tag] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [DeletedAt] DATETIME2 (7)    NULL,
    [Name]      NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_Tag] PRIMARY KEY CLUSTERED ([Id] ASC)
);

