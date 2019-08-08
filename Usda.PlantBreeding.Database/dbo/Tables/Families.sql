CREATE TABLE [dbo].[Families] (
    [Id]                       INT            IDENTITY (1, 1) NOT NULL,
    [CrossNum]                 NVARCHAR (MAX) NULL,
    [FieldPlantedNum]          INT NULL,
    [TransplantedNum]          INT NULL,
    [GenusId]                  INT            NOT NULL,
    [IsReciprocal]             BIT            NULL,
    [OriginId]                 INT            NULL,
    [SeedNum]                  INT            NULL,
    [CrossTypeId]              INT            NULL,
    [Purpose]                  NVARCHAR (MAX) NULL,
    [FemaleParent]             INT            NULL,
    [MaleParent]               INT            NULL,
    [ReciprocalString]         NVARCHAR (MAX) NULL,
    [BaseGenotypeId]           INT            NULL,
     [FemaleParentOriginalName] AS             ([dbo].[GetOriginalName]([FemaleParent])),
    [MaleParentOriginalName]   AS             ([dbo].[GetOriginalName]([MaleParent])),
    [Unsuccessful]             BIT            NOT NULL,
    CONSTRAINT [PK_Families] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CrossTypeFamily] FOREIGN KEY ([CrossTypeId]) REFERENCES [dbo].[CrossTypes] ([Id]),
    CONSTRAINT [FK_FamilyGenotype] FOREIGN KEY ([BaseGenotypeId]) REFERENCES [dbo].[Genotypes] ([Id]),
    CONSTRAINT [FK_FamilyOrigin] FOREIGN KEY ([OriginId]) REFERENCES [dbo].[Origins] ([Id]),
    CONSTRAINT [FK_FemaleFamilyGenotype] FOREIGN KEY ([FemaleParent]) REFERENCES [dbo].[Genotypes] ([Id]),
    CONSTRAINT [FK_GenusFamily] FOREIGN KEY ([GenusId]) REFERENCES [dbo].[Genus] ([Id]),
    CONSTRAINT [FK_MaleFamilyGenotype] FOREIGN KEY ([MaleParent]) REFERENCES [dbo].[Genotypes] ([Id])
);


































GO
CREATE NONCLUSTERED INDEX [IX_FK_GenusFamily]
    ON [dbo].[Families]([GenusId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_FamilyOrigin]
    ON [dbo].[Families]([OriginId] ASC);




GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_FK_CrossTypeFamily]
    ON [dbo].[Families]([CrossTypeId] ASC);


GO





GO
CREATE NONCLUSTERED INDEX [IX_FK_MaleFamilyGenotype]
    ON [dbo].[Families]([MaleParent] ASC);




GO
CREATE NONCLUSTERED INDEX [IX_FK_FemaleFamilyGenotype]
    ON [dbo].[Families]([FemaleParent] ASC);




GO
CREATE NONCLUSTERED INDEX [IX_FK_FamilyGenotype]
    ON [dbo].[Families]([BaseGenotypeId] ASC);

