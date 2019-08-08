


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetPhenotypeEntry] 
	@Year varchar(5),
	@MapId int,
	@GenotypeId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
 Select mcy.Id [MapCompYearId], mc.GenotypeId, a.Text, q.Label, mc.Row, mc.PlantNum, a.Id [answerId], q.[Order], Maps.PicklistPrefix, mc.PickingNumber PickingNumber, mc.ExternalId, mc.PlantsInRep, mc.Rep
 ,ISNULL(NULLIF(femaleGenotype.GivenName, ' '), femaleGenotype.OriginalName) as [FemaleGenotypeName], ISNULL(NULLIF(maleGenotype.GivenName, ' '), maleGenotype.OriginalName) as [MaleGenotypeName]
 , ISNULL(NULLIF(g.GivenName, ' '), g.OriginalName) as [GenotypeName]
 , mcy.Comments
 , fct.Name [CrossTypeName]
 , Maps.Name [MapName]
 , Years.[Year] [YearName]
 , STUFF((
  Select ', ' + Fates.Label
	From MapComponentYearsFate
	Inner Join Fates 
	on Fates.Id = MapComponentYearsFate.Fates_Id
	where MapComponentYearsFate.MapComponentYears_Id = mcy.Id
	order by 1
	for xml path('')
	   ), 1, 1, '' ) AS [Fates]

from Maps
Inner Join MapQuestion mq
on mq.Maps_Id = Maps.Id
Inner Join Questions q
on q.Id = mq.Questions_Id

Inner Join MapComponents mc
on mc.MapId = Maps.Id
Inner Join MapComponentYears mcy
on mcy.MapComponentId = mc.Id


Inner Join Years 
on Years.Id = mcy.YearsId
Left Join Answers a 
on a.MapComponentYearsId = mcy.Id 
and  a.QuestionId = q.Id

Left Join Genotypes g
on mc.GenotypeId = g.Id

Left Join Families f
on g.FamilyId = f.Id

Left Join CrossTypes fct
on fct.Id = f.CrossTypeId

Left Join Genotypes femaleGenotype
on f.FemaleParent = femaleGenotype.Id

Left Join Genotypes maleGenotype
on f.MaleParent = maleGenotype.Id


where 
ISNULL(@GenotypeId, g.Id) = g.Id 
and ISNULL(@MapId, mc.MapId) = mc.MapId 
and ISNULL(@Year, Years.[Year]) = Years.[Year]
and mc.isSeedling = 0

Order By Years.[Year] Desc, mc.GenotypeId, Row, PlantNum, q.Label

END