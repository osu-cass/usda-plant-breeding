CREATE TABLE [dbo].[Ploidies] (
    [Id]      INT            IDENTITY (1, 1) NOT NULL,
    [Name]    NVARCHAR (MAX) NULL,
    [Retired] BIT            NOT NULL,
    [Value]   NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Ploidies] PRIMARY KEY CLUSTERED ([Id] ASC)
);









