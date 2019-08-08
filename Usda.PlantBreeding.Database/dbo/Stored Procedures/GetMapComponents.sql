-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetMapComponents] 
	@Year varchar(5),
	@MapId int,
	@MapComponentSeedlingFlag bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select mc.[Row], mc.PlantNum, g.OriginalName, g.GivenName, mc.PickingNumber PickingNumber, mc.CreatedYear [CreatedYear], mc.PlantsInRep [PlantsInRep]
, mc.ExternalId, m.PicklistPrefix, m.Name, m.PlantingYear, y.[Year], mc.Virus1, mc.Virus2, mc.Virus3, mc.Virus4, mc.PlantsInRep, mc.Rep
,ISNULL(NULLIF(femaleGenotype.GivenName, ' '), femaleGenotype.OriginalName) as [FemaleGenotypeName], ISNULL(NULLIF(maleGenotype.GivenName, ' '), maleGenotype.OriginalName) as [MaleGenotypeName]

from MapComponents mc
Inner Join MapComponentYears mcy
	on mc.id = mcy.MapComponentId
Inner Join Years y
	on y.Id = mcy.YearsId
Inner Join Maps m
	on m.Id = mc.MapId
Left Join Genotypes g
	on g.id = mc.GenotypeId


Left Join Families f
on g.FamilyId = f.Id

Left Join Genotypes femaleGenotype
on f.FemaleParent = femaleGenotype.Id

Left Join Genotypes maleGenotype
on f.MaleParent = maleGenotype.Id


Where y.[Year] = @Year 
and m.Id = @MapId
and (ISNULL(@MapComponentSeedlingFlag, mc.isSeedling) = mc.isSeedling )

Order by mc.[Row], PlantNum


END