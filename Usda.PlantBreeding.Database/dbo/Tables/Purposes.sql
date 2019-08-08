CREATE TABLE [dbo].[Purposes] (
    [Id]      INT            NOT NULL IDENTITY,
    [Name]    NVARCHAR (255) NOT NULL,
    [Value]   NCHAR (255)    NULL,
    [Retired] BIT            DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


