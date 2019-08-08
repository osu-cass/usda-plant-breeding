CREATE TABLE [dbo].[Answers] (
    [Id]                  INT            IDENTITY (1, 1) NOT NULL,
    [Text]                NVARCHAR (MAX) NULL,
    [QuestionId]          INT            NOT NULL,
    [GenotypeId]          INT            NOT NULL,
    [MapComponentYearsId] INT            NOT NULL,
    CONSTRAINT [PK_Answers] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AnswerGenotype] FOREIGN KEY ([GenotypeId]) REFERENCES [dbo].[Genotypes] ([Id]),
    CONSTRAINT [FK_AnswersQuestions] FOREIGN KEY ([QuestionId]) REFERENCES [dbo].[Questions] ([Id]),
    CONSTRAINT [FK_MapComponentYearsAnswer] FOREIGN KEY ([MapComponentYearsId]) REFERENCES [dbo].[MapComponentYears] ([Id])
);







GO
 
GO


GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_FK_AnswersQuestions]
    ON [dbo].[Answers]([QuestionId] ASC);




GO



GO
CREATE NONCLUSTERED INDEX [IX_FK_AnswerGenotype]
    ON [dbo].[Answers]([GenotypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_MapComponentYearsAnswer]
    ON [dbo].[Answers]([MapComponentYearsId] ASC);

