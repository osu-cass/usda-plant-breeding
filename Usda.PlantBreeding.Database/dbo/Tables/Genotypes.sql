CREATE TABLE [dbo].[Genotypes] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [SelectionNum] INT            NULL,
    [GivenName]    NVARCHAR (MAX) NULL,
    [FamilyId]     INT            NOT NULL,
    [Year]         NVARCHAR (MAX) NULL,
    [Note]         NVARCHAR (MAX) NULL,
    [Fate]         NVARCHAR (MAX) NULL,
    [ExternalId]   INT            NULL,
    [IsRoot]       BIT            NULL,
    [Alias1]       NVARCHAR (MAX) NULL,
    [Alias2]       NVARCHAR (MAX) NULL,
    [OriginalName] AS             ([dbo].[GetOriginalName]([Id])),
    [PatentApp]    NVARCHAR (MAX) NULL,
    [Patent]       NVARCHAR (MAX) NULL,
    [PatentYear]   NVARCHAR (MAX) NULL,
    [PloidyName]   NVARCHAR (255) NULL,
    [CrossPlanId]  INT			  NULL, 
    [CreatedDate]  DATE			  NULL, 
	[IsPopulation] BIT			  NOT NULL,
    CONSTRAINT [PK_Genotypes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PlantFamily] FOREIGN KEY ([FamilyId]) REFERENCES [dbo].[Families] ([Id])
);




































GO
CREATE NONCLUSTERED INDEX [IX_FK_PlantFamily]
    ON [dbo].[Genotypes]([FamilyId] ASC);


GO


