CREATE TABLE [dbo].[Tag] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [CreatedAt] DATETIME2 (7)    DEFAULT (getdate()) NOT NULL,
    [DeletedAt] DATETIME2 (7)    NULL,
    [Name]      NVARCHAR (100)   NOT NULL,
    [TagType]   INT              NOT NULL,
    CONSTRAINT [PK_Tag] PRIMARY KEY CLUSTERED ([Id] ASC)
);





