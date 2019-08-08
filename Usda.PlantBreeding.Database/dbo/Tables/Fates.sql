CREATE TABLE [dbo].[Fates] (
    [Id]      INT            IDENTITY (1, 1) NOT NULL,
    [Name]    NVARCHAR (MAX) NOT NULL,
    [Retired] BIT            NOT NULL,
    [Order]   INT            NULL,
    [Label] NVARCHAR(50) NULL, 
    CONSTRAINT [PK_Fates] PRIMARY KEY CLUSTERED ([Id] ASC)
);







