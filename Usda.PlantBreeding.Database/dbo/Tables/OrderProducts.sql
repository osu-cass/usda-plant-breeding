CREATE TABLE [dbo].[OrderProducts]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [GenotypeId] INT NOT NULL, 
    [Quantity] INT NULL, 
    [MaterialId] INT NOT NULL, 
    [OrderId] INT NOT NULL, 
    [VirusTested] DATE NULL, 
    [Note] NVARCHAR(255) NULL, 
    [CreatedDate] DATETIME NOT NULL, 
    [DateSent] DATE NULL, 
    CONSTRAINT [FK_OrderProducts_ToGenotypes] FOREIGN KEY ([GenotypeId]) REFERENCES [Genotypes]([Id]),
	CONSTRAINT [FK_OrderProducts_ToMaterials] FOREIGN KEY ([MaterialId]) REFERENCES [Materials]([Id]),
	CONSTRAINT [FK_OrderProducts_ToOrders] FOREIGN KEY ([OrderId]) REFERENCES [Orders]([Id])
)
