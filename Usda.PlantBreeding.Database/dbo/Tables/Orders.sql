CREATE TABLE [dbo].[Orders]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] VARCHAR(50) NOT NULL, 
    [Year] INT NOT NULL, 
    [LocationId] INT NULL, 
    [GenusId] INT NOT NULL, 
    [GrowerId] INT NOT NULL, 
    [Note] NVARCHAR(MAX) NULL, 
    [MTARequestedProp] BIT NOT NULL DEFAULT 0, 
    [MTARequestedTest] BIT NOT NULL DEFAULT 0, 
    [MTAComplete] DATE NULL, 
    CONSTRAINT [FK_Orders_ToGenus] FOREIGN KEY ([GenusId]) REFERENCES [Genus]([Id]),
	CONSTRAINT [FK_Orders_ToLocations] FOREIGN KEY ([LocationId]) REFERENCES [Locations]([Id]),
	CONSTRAINT [FK_Orders_ToGrowers] FOREIGN KEY ([GrowerId]) REFERENCES [Growers]([Id])
)
