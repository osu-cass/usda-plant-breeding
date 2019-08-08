CREATE TABLE [dbo].[CrossPlans] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [Year]            NVARCHAR (MAX) NULL,
    [MaleParentId]    INT            NULL,
    [FemaleParentId]  INT            NULL,
    [Purpose]         NVARCHAR (MAX) NULL,
    [GenusId]         INT            NOT NULL,
    [Unsuccessful]    BIT            NOT NULL,
	[DateCreated]	  DATETIME2		 NOT NULL		DEFAULT '2017-01-01T00:00:00',
    [Reciprocal]      BIT            NOT NULL,
    [CrossTypeId]     INT            NULL,
    [OriginId]        INT            NULL,
    [SeedNum]         INT            NULL,
    [FieldPlantedNum] INT            NULL,
    [TransplantedNum] INT            NULL,
    [Note]            NVARCHAR (MAX) NULL,
    [CrossNum]        NVARCHAR (MAX) NULL,
    [GenotypeId] INT NULL, 
    CONSTRAINT [PK_CrossPlans] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CrossPlanCrossType] FOREIGN KEY ([CrossTypeId]) REFERENCES [dbo].[CrossTypes] ([Id]),
    CONSTRAINT [FK_CrossPlanGenotype] FOREIGN KEY ([MaleParentId]) REFERENCES [dbo].[Genotypes] ([Id]),
    CONSTRAINT [FK_CrossPlanGenotype1] FOREIGN KEY ([FemaleParentId]) REFERENCES [dbo].[Genotypes] ([Id]),
    CONSTRAINT [FK_CrossPlanGenus] FOREIGN KEY ([GenusId]) REFERENCES [dbo].[Genus] ([Id]),
    CONSTRAINT [FK_CrossPlanOrigin] FOREIGN KEY ([OriginId]) REFERENCES [dbo].[Origins] ([Id]) 
);




















GO



GO
CREATE NONCLUSTERED INDEX [IX_FK_CrossPlanGenotype1]
    ON [dbo].[CrossPlans]([FemaleParentId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_CrossPlanGenotype]
    ON [dbo].[CrossPlans]([MaleParentId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_CrossPlanGenus]
    ON [dbo].[CrossPlans]([GenusId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_CrossPlanOrigin]
    ON [dbo].[CrossPlans]([OriginId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_CrossPlanCrossType]
    ON [dbo].[CrossPlans]([CrossTypeId] ASC);


GO
