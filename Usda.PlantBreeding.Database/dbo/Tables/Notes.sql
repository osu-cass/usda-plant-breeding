CREATE TABLE [dbo].[Notes] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Date]       DATETIME       NOT NULL,
    [GenotypeId] INT            NOT NULL,
    [Text]       NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Notes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_NoteGenotype] FOREIGN KEY ([GenotypeId]) REFERENCES [dbo].[Genotypes] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_FK_NoteGenotype]
    ON [dbo].[Notes]([GenotypeId] ASC);

