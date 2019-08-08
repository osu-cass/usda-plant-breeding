CREATE TABLE [dbo].[MapComponents] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [Row]           INT            NULL,
    [MapId]         INT            NOT NULL,
    [Rep]           INT            NOT NULL,
    [PlantsInRep]   INT            NOT NULL,
    [GenotypeId]    INT            NULL,
    [PlantNum]      INT            NULL,
    [isSeedling]    BIT            NULL,
    [Virus1]        NVARCHAR(50)            NULL,
    [Virus2]        NVARCHAR(50)            NULL,
    [Virus3]        NVARCHAR(50)            NULL,
    [ExternalId]    INT            NULL,
    [Virus4] NVARCHAR(50) NULL, 
    [isRemoved] BIT NOT NULL, 
    [CreatedYear] NVARCHAR(4) NULL, 
	    [PickingNumber]  as ([dbo].[GetPickingNumber]([Id])),

    CONSTRAINT [PK_MapComponents] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MapComponentGenotype] FOREIGN KEY ([GenotypeId]) REFERENCES [dbo].[Genotypes] ([Id]),
    CONSTRAINT [FK_MapMapComponent] FOREIGN KEY ([MapId]) REFERENCES [dbo].[Maps] ([Id]) ON DELETE CASCADE
);






















GO
CREATE NONCLUSTERED INDEX [IX_FK_MapMapComponent]
    ON [dbo].[MapComponents]([MapId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_MapComponentGenotype]
    ON [dbo].[MapComponents]([GenotypeId] ASC);

