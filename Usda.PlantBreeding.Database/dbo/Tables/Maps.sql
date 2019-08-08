CREATE TABLE [dbo].[Maps] (
    [Id]                 INT            IDENTITY (1, 1) NOT NULL,
    [Name]               NVARCHAR (MAX) NULL,
    [PlantingYear]       NVARCHAR (MAX) NULL,
    [GenusId]            INT            NOT NULL,
    [IsSeedlingMap]      BIT            NOT NULL,
    [PicklistPrefix]    AS             ([dbo].[GetMapPrefix]([Id])),
    [Retired]            BIT            NOT NULL,
    [DefaultPlantsInRep] INT            NOT NULL,
    [LocationId]         INT            NULL,
    [ExternalId]         INT            NULL,
    [StartCorner] NCHAR(50) NULL, 
    [LocationSuffix] NVARCHAR(50) NULL, 
    [CrossTypeId] INT NULL, 
    [Note] NVARCHAR(MAX) NULL, 
    CONSTRAINT [PK_Maps] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MapGenus] FOREIGN KEY ([GenusId]) REFERENCES [dbo].[Genus] ([Id]),
    CONSTRAINT [FK_MapLocation] FOREIGN KEY ([LocationId]) REFERENCES [dbo].[Locations] ([Id]), 
    CONSTRAINT [FK_Maps_ToTab] FOREIGN KEY (CrossTypeId) REFERENCES [dbo].[CrossTypes] ([Id])
);




























GO
CREATE NONCLUSTERED INDEX [IX_FK_MapGenus]
    ON [dbo].[Maps]([GenusId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_MapLocation]
    ON [dbo].[Maps]([LocationId] ASC);

	Go
	CREATE NONCLUSTERED INDEX [IX_FK_MapCrossType]
    ON [dbo].[Maps]([CrossTypeId] ASC);

