-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetDistributions]
	@Year INT = NULL,
	@LocationId INT = NULL,
	@GenotypeId INT = NULL
AS BEGIN
	IF @GenotypeId IS NULL 
	(
		SELECT 
			[o].[Id] as [OrderId],
			[o].[Name] as [OrderName],
			[o].[Year] as [OrderYear],
			[o].[Note] as [OrderNote],
			[o].[MTAComplete] as [OrderMTAComplete],
			[o].[GenusId] AS [GenusId],
			[o].[LocationId] AS [LocationId],
			[l].[Name] as [LocationName],
			[l].[Retired] as [LocationRetired],
			[l].[Description] as [LocationDescription],
			[l].[MapFlag] as [LocationMapFlag],
			[l].[PrimaryContactId] as [LocationPrimaryContactId],
			[l].[Address] as [LocationAddress],
			[g].[FirstName] as [GrowerFirstName],
			[g].[LastName] as [GrowerLastName],
			[g].[Email] as [GrowerEmail],
			[g].[WorkPhone] as [GrowerWorkPhone],
			[g].[MailingName] as [GrowerMailingName],
			[g].[MobilePhone] as [GrowerMobilePhone],
			[g].[CreatedDate] as [GrowerCreatedDate],
			[ge].[Name] as [GenusName],
			[ge].[ExternalId] AS [GenusExternalId]
		FROM [dbo].[Orders] o
		LEFT JOIN [dbo].[Locations] l
		ON o.LocationId = l.Id
		LEFT JOIN [dbo].[Growers] g
		ON o.GrowerID = g.Id
		LEFT JOIN [dbo].[Genus] ge
		ON o.GenusId = ge.Id
		WHERE 
			(@Year IS NULL OR ([o].[Year] = @Year)) and
			(@LocationId IS NULL OR ([LocationId] = @LocationId))
	)
	ELSE IF @GenotypeId IS NOT NULL (
		SELECT 
			[gen].[Id] as [GenotypeId],
			[o].[Id] as [OrderId],
			[o].[Name] as [OrderName],
			[o].[Year] as [OrderYear],
			[o].[Note] as [OrderNote],
			[o].[MTAComplete] as [OrderMTAComplete],
			[o].[GenusId] AS [GenusId],
			[o].[LocationId] AS [LocationId],
			[l].[Name] as [LocationName],
			[l].[Retired] as [LocationRetired],
			[l].[Description] as [LocationDescription],
			[l].[MapFlag] as [LocationMapFlag],
			[l].[PrimaryContactId] as [LocationPrimaryContactId],
			[l].[Address] as [LocationAddress],
			[g].[FirstName] as [GrowerFirstName],
			[g].[LastName] as [GrowerLastName],
			[g].[Email] as [GrowerEmail],
			[g].[WorkPhone] as [GrowerWorkPhone],
			[g].[MailingName] as [GrowerMailingName],
			[g].[MobilePhone] as [GrowerMobilePhone],
			[g].[CreatedDate] as [GrowerCreatedDate],
			[ge].[Name] as [GenusName],
			[ge].[ExternalId] AS [GenusExternalId]
		FROM [dbo].Genotypes gen
		LEFT JOIN [dbo].[OrderProducts] op
		ON [op].GenotypeId = gen.Id
		LEFT JOIN [dbo].[Orders] o
		ON op.OrderId = o.Id
		LEFT JOIN [dbo].[Locations] l
		ON o.LocationId = l.Id
		LEFT JOIN [dbo].[Growers] g
		ON o.GrowerID = g.Id
		LEFT JOIN [dbo].[Genus] ge
		ON o.GenusId = ge.Id
		WHERE 
			(@GenotypeId IS NULL OR ([gen].[Id] = @GenotypeId)) and
			(@Year IS NULL OR ([o].[Year] = @Year)) and
			(@LocationId IS NULL OR ([LocationId] = @LocationId))
	)
END