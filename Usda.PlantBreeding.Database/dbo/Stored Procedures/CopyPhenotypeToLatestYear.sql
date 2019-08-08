
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CopyPhenotypeToLatestYear]
	-- Add the parameters for the stored procedure here
	@MapId int,
	@FromYear varchar(5),
	@isSeedling bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   


Insert into Answers (text, QuestionId, GenotypeId, [MapComponentYearsId])
Select a.Text, a.QuestionId, a.GenotypeId, (	
	Select MapComponentYears.Id
	from MapComponentYears 
	Inner Join(
	Select max(YearsId) [YearsId], MapComponentId
	from MapComponentYears 
	group by MapComponentId 
	)dt
	on dt.MapComponentId =  MapComponentYears.MapComponentId 
	and dt.YearsId = MapComponentYears.YearsId
	where dt.MapComponentId = mc.Id
	
	) [MapComponentYearsId] from Answers a
inner join MapComponentYears mcy
on mcy.Id = a.MapComponentYearsId
inner Join MapComponents mc
on mc.Id = mcy.MapComponentId
Inner Join Years
on mcy.YearsId = Years.Id

where mc.isSeedling = @isSeedling and  mc.MapId = @MapId
and Years.[Year] = @FromYear



Insert into MapComponentYearsFate (Fates_Id, MapComponentYears_Id)
Select Fates_Id, (	
					Select MapComponentYears.Id
					from MapComponentYears 
					Inner Join(
						Select max(YearsId) [YearsId], MapComponentId
						from MapComponentYears 
						group by MapComponentId 
					)dt
					on dt.MapComponentId =  MapComponentYears.MapComponentId 
					and dt.YearsId = MapComponentYears.YearsId
					where dt.MapComponentId = mc.Id
	
					) [MapComponentYearsId] 

from MapComponentYearsFate mcyf
inner join MapComponentYears mcy
on mcy.Id = mcyf.MapComponentYears_Id
inner Join MapComponents mc
on mc.Id = mcy.MapComponentId
Inner Join Years
on mcy.YearsId = Years.Id

where mc.isSeedling = 0 and  mc.MapId = @MapId
and Years.[Year] = @FromYear

Update MapComponentYears
set Comments = results.Comments
from (
Select mcy.Comments, (	
					Select MapComponentYears.Id
					from MapComponentYears 
					Inner Join(
						Select max(YearsId) [YearsId], MapComponentId
						from MapComponentYears 
						group by MapComponentId 
					)dt
					on dt.MapComponentId =  MapComponentYears.MapComponentId 
					and dt.YearsId = MapComponentYears.YearsId
					where dt.MapComponentId = mc.Id
	
					) [MapComponentYearsId] 

from MapComponentYears mcy
inner Join MapComponents mc
on mc.Id = mcy.MapComponentId
Inner Join Years
on mcy.YearsId = Years.Id

where mc.isSeedling = 0 and  mc.MapId = @MapId
and Years.[Year] = @FromYear
)results

where results.MapComponentYearsId = Id



END