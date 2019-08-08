CREATE TABLE [dbo].[Locations] (
    [Id]      INT            IDENTITY (1, 1) NOT NULL,
    [Name]    NVARCHAR (MAX) NOT NULL,
    [Retired] BIT            NOT NULL,
    [Description] NVARCHAR(MAX) NULL, 
    [MapFlag] BIT NOT NULL DEFAULT 1, 
    [PrimaryContactId] INT NULL,
	[Address] NVARCHAR(MAX) NULL,
	CONSTRAINT [FK_Locations_ToGrowers] FOREIGN KEY ([PrimaryContactId]) REFERENCES [Growers]([Id]),
    CONSTRAINT [PK_Locations] PRIMARY KEY CLUSTERED ([Id] ASC)
);





