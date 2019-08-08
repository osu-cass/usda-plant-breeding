CREATE TABLE [dbo].[Genus] (
    [Id]                 INT            IDENTITY (1, 1) NOT NULL,
    [Name]               NVARCHAR (MAX) NULL,
    [Value]              NVARCHAR (MAX) NULL,
    [DefaultPlantsInRep] INT            NOT NULL,
    [VirusLabel1]        NVARCHAR (MAX) NULL,
    [VirusLabel2]        NVARCHAR (MAX) NULL,
    [VirusLabel3]        NVARCHAR (MAX) NULL,
    [Retired]            BIT            NOT NULL,
    [ExternalId]         INT            NULL,
    [VirusLabel4] NVARCHAR(MAX) NULL, 
    CONSTRAINT [PK_Genus] PRIMARY KEY CLUSTERED ([Id] ASC)
);



















