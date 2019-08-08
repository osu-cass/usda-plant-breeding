
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetCrossPlanLocations]
	-- Add the parameters for the stored procedure here
		@GenusId int,
	@Year varchar(5)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   select distinct 
   --Genotype info
   g.OriginalName [OriginalName], g.GivenName [GivenName]
--CrossPlan info
, cp.[Year] [CrossYear]

--Map info
, m.Name [MapName], m.PlantingYear [MapPlantYear], m.IsSeedlingMap [MapSeedlingField], m.PicklistPrefix [MapPicklistPrefix]
--Map location
 ,l.Name [MapLocation]  
--Map component info

, mc.PickingNumber [PickingNumber], mc.[Row], mc.PlantNum, mc.ExternalId, mc.Virus1, mc.Virus2, mc.Virus3, mc.Virus4

from 
	(select cp.Id, cp.FemaleParentId [GenotypeId], 'Mother' [M/f]
	from
	 CrossPlans cp
	 where cp.FemaleParentId is not null
 
	 union 

	 select cp.Id, cp.MaleParentId [GenotypeId], 'Father' [M/f]
	from
	 CrossPlans cp
	 where cp.MaleParentId is not null ) dt

Inner Join CrossPlans cp
on cp.Id = dt.Id
 Inner Join Genotypes g
 on g.Id = dt.GenotypeId
 Inner Join MapComponents mc
 on mc.GenotypeId = dt.GenotypeId
 Inner Join Maps m
 on m.Id = mc.MapId
 Left Join Locations l
 on m.LocationId = l.Id

 where m.Retired <> 1
and 
  (ISNULL(@GenusId, cp.GenusId) = cp.GenusId)
  and
  (ISNULL(@Year, cp.[Year]) =cp.[Year] )
  
  Order by g.OriginalName, m.PlantingYear, l.Name
  
END