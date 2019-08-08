-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE dbo.UpdateSelectionYearForGenotypes
	-- Add the parameters for the stored procedure here
	@mapId int,
	@isSeedling bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    Update Genotypes
set Year = results.SelectionYear

from (
SELECT g.Id [GenotypeId], (	
	Select Years.[Year]
	from MapComponentYears 
	Inner Join(
	Select min(YearsId) [YearsId], MapComponentId
	from MapComponentYears 
	group by MapComponentId 
	)dt
	on dt.MapComponentId =  MapComponentYears.MapComponentId 
	and dt.YearsId = MapComponentYears.YearsId	
	Inner Join Years 
	on Years.Id = MapComponentYears.YearsId
	where dt.MapComponentId = mc.Id
	) [SelectionYear]
From Genotypes g
  Inner Join MapComponents mc
 on mc.GenotypeId = g.Id

 Inner Join MapComponentYears mcy
 on mcy.MapComponentId = mc.Id

 where mc.isSeedling = @isSeedling
 and mc.MapId = @mapId
 )results

 where Id = results.GenotypeId

END