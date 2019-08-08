-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetMapInventory] 
	@Year varchar(5),
	@MapId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select SUM(mc.PlantsInRep) [FieldCount], g.SelectionNum, g.OriginalName, g.GivenName, m.PicklistPrefix, m.Name [MapName], m.PlantingYear

from MapComponents mc
Inner Join MapComponentYears mcy
	on mc.id = mcy.MapComponentId
Inner Join Years y
	on y.Id = mcy.YearsId
Inner Join Maps m
	on m.Id = mc.MapId
Inner Join Genotypes g
	on g.id = mc.GenotypeId


Left Join Families f
on g.FamilyId = f.Id

Left Join Origins famOrigin
on f.OriginId = famOrigin.Id

Left Join Genotypes femaleGenotype
on f.FemaleParent = femaleGenotype.Id

Left Join Genotypes maleGenotype
on f.MaleParent = maleGenotype.Id


Where y.[Year] = @Year 
and m.Id = @MapId

GROUP by famOrigin.Name, len(f.CrossNum), f.CrossNum, f.ReciprocalString, g.SelectionNum, g.OriginalName, g.GivenName, m.PicklistPrefix, m.Name, m.PlantingYear

Order by famOrigin.Name, len(f.CrossNum), f.CrossNum, f.ReciprocalString, g.SelectionNum, g.OriginalName, g.GivenName, m.PicklistPrefix, m.Name, m.PlantingYear




END