CREATE TABLE [dbo].[Candidates] (
    [Id]         INT IDENTITY (1, 1) NOT NULL,
    [Year]       INT NULL,
    [GenotypeId] INT NOT NULL,
    CONSTRAINT [PK_Candidates] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CandidatesGenotype] FOREIGN KEY ([GenotypeId]) REFERENCES [dbo].[Genotypes] ([Id])
);














GO
CREATE NONCLUSTERED INDEX [IX_FK_CandidatesGenotype]
    ON [dbo].[Candidates]([GenotypeId] ASC);





