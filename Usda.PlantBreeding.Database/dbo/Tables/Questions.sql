CREATE TABLE [dbo].[Questions] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Text]       NVARCHAR (MAX) NULL,
    [GenusId]    INT            NOT NULL,
    [Required]   BIT            NOT NULL,
    [Retired]    BIT            NULL,
    [Label]      NVARCHAR (MAX) NULL,
    [Order]      INT            NOT NULL,
    [ExternalId] INT            NULL,
    [InputTypeId] INT NULL, 
    CONSTRAINT [PK_Questions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GenusQuestions] FOREIGN KEY ([GenusId]) REFERENCES [dbo].[Genus] ([Id]),
	CONSTRAINT [FK_InputTypes] FOREIGN KEY ([InputTypeId]) REFERENCES [dbo].[InputTypes] ([Id])

);






















GO
CREATE NONCLUSTERED INDEX [IX_FK_GenusQuestions]
    ON [dbo].[Questions]([GenusId] ASC);

