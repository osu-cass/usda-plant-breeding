-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetDistributionAccessions]
	@OrderId int
AS
BEGIN
	SELECT [p].[Id] AS [ProductId],
		[p].[GenotypeId] AS [GenotypeId],
		[p].[OrderId] AS [OrderId],
		[p].[Quantity] AS [ProductQuantity],
		[p].[MaterialId] AS [ProductMaterialId],
		[p].[VirusTested] AS [ProductVirusTested],
		[p].[Note] AS [ProductNote],
		[p].[CreatedDate] AS [ProductCreatedDate],
		[p].[DateSent] AS [ProductDateSent],
		[g].[CreatedDate] AS [GenotypeCreatedDate],
		[g].[CrossPlanId] AS [CrossPlanId],
		[g].[ExternalId] AS [GenotypeExternalId],
		[g].[FamilyId] AS [FamilyId],
		[g].[Fate] AS [GenotypeFate],
		[g].[GivenName] AS [GenotypeGivenName],
		[g].[IsPopulation] AS [GenotypeIsPopulation],
		[g].[IsRoot] AS [GenotypeIsRoot],
		[g].[Note] AS [GenotypeNote],
		[g].[OriginalName] AS [GenotypeOriginalName],
		[g].[Patent] AS [GenotypePatent],
		[g].[PatentApp] AS [GenotypePatentApp],
		[g].[PatentYear] AS [GenotypePatentYear],
		[g].[PloidyName] AS [GenotypePloidyName],
		[g].[SelectionNum] AS [GenotypeSelectionNum],
		[g].[Year] AS [GenotypeYear],
		[m].[Name] AS [MaterialName],
		[m].[Value] AS [MaterialValue],
		[f].[CrossTypeId] AS [FamilyCrossTypeId],
		[f].[BaseGenotypeId] AS [FamilyBaseGenotypeId],
		[f].[CrossNum] AS [FamilyCrossNum],
		[f].[FemaleParent] AS [FamilyFemaleParent],
		[f].[FemaleParentOriginalName] AS [FamilyFemaleParentOriginalName],
		[f].[FieldPlantedNum] AS [FamilyFemalePlantedNum],
		[f].[GenusId] AS [FamilyGenusId],
		[f].[IsReciprocal] AS [FamilyIsReciprocal],
		[f].[MaleParent] AS [FamilyMaleParent],
		[f].[MaleParentOriginalName] AS [FamilyMaleParentOriginalName],
		[f].[OriginId] AS [FamilyOriginId],
		[f].[Purpose] AS [FamilyPurpose],
		[f].[ReciprocalString] AS [FamilyReciprocalString],
		[f].[SeedNum] AS [FamilySeedNum],
		[f].[TransplantedNum] AS [FamilyTransplantedNum],
		[f].[Unsuccessful] AS [FamilyUnsuccessful],
		[ct].[Name] AS [CrossTypeName],
		[ct].[Retired] AS [CrossTypeRetired],
		[ct].[Value] AS [CrossTypeValue]
	FROM [dbo].[OrderProducts] p
	LEFT JOIN [dbo].[Genotypes] g
	ON [p].[GenotypeId] = [g].[Id]
	LEFT JOIN [dbo].[Materials] m
	ON [p].[MaterialId] = [m].[Id]
	LEFT JOIN [dbo].[Families] f
	ON [g].[FamilyId] = [f].[Id]
	LEFT JOIN [dbo].[CrossTypes] ct
	ON [f].[CrossTypeId] = [ct].[Id]
	WHERE [p].[OrderId] = @OrderId
END
