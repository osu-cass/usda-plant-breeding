CREATE TABLE [dbo].[FlatTypes] (
    [Id]      INT            IDENTITY (1, 1) NOT NULL,
    [Name]    NVARCHAR (MAX) NOT NULL,
    [Value]   NVARCHAR (MAX) NULL,
    [Retired] BIT            NOT NULL,
    CONSTRAINT [PK_FlatTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);





