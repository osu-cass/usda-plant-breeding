CREATE TABLE [dbo].[Origins] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (MAX) NULL,
    [Retired]   BIT            NOT NULL,
    [IsDefault] BIT            NOT NULL,
    CONSTRAINT [PK_Origins] PRIMARY KEY CLUSTERED ([Id] ASC)
);









