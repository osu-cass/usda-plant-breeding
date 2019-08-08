CREATE TABLE [dbo].[FlatNotes] (
    [Other]      NVARCHAR (MAX) NULL,
    [FlatTypeId] INT            NOT NULL,
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Date]       DATETIME       NOT NULL,
    [Text]       NVARCHAR (MAX) NOT NULL,
    [GenotypeId] INT            NOT NULL,
    CONSTRAINT [PK_FlatNotes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FlatNoteFlatType] FOREIGN KEY ([FlatTypeId]) REFERENCES [dbo].[FlatTypes] ([Id]),
    CONSTRAINT [FK_GenotypeFlatNote] FOREIGN KEY ([GenotypeId]) REFERENCES [dbo].[Genotypes] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_GenotypeFlatNote]
    ON [dbo].[FlatNotes]([GenotypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_FlatNoteFlatType]
    ON [dbo].[FlatNotes]([FlatTypeId] ASC);

