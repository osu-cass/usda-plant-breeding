-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetMapComponentFates] 
	@Year varchar(5),
	@MapIsSeedling bit,
	@GenusId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select mc.[Row], mc.PlantNum, g.OriginalName, g.GivenName, mc.PickingNumber PickingNumber
, mc.ExternalId, m.PicklistPrefix, m.Name, m.PlantingYear, y.[Year], mc.Virus1, mc.Virus2, mc.Virus3, mc.Virus4, mc.PlantsInRep, mc.Rep, mcy.Id [mapComponentYearId]
,ISNULL(NULLIF(femaleGenotype.GivenName, ' '), femaleGenotype.OriginalName) as [FemaleGenotypeName], ISNULL(NULLIF(maleGenotype.GivenName, ' '), maleGenotype.OriginalName) as [MaleGenotypeName], mcyearfate.Name [Fate], mcy.Comments, m.Name [MapName]

from MapComponents mc
Inner Join MapComponentYears mcy
	on mc.id = mcy.MapComponentId
Inner Join Years y
	on y.Id = mcy.YearsId
Inner Join Maps m
	on m.Id = mc.MapId
Left Join Genotypes g
	on g.id = mc.GenotypeId
Inner Join MapComponentYearsFate mcyf
	on mcy.Id = mcyf.MapComponentYears_Id
Inner Join Fates mcyearfate
	on mcyf.Fates_Id = mcyearfate.Id

Left Join Families f
on g.FamilyId = f.Id

Left Join Genotypes femaleGenotype
on f.FemaleParent = femaleGenotype.Id

Left Join Genotypes maleGenotype
on f.MaleParent = maleGenotype.Id


Where y.[Year] = @Year 
and (ISNULL(@MapIsSeedling,m.IsSeedlingMap) = m.IsSeedlingMap)
and (m.GenusId = @GenusId)

Order by m.Name, mc.[Row], PlantNum

END