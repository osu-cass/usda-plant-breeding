

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetMapViewComponents] 
	@Year varchar(5),
	@MapId int,
	@ShowPicking bit,
	@MapComponentSeedlingFlag bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here



SELECT   
      mc.[Row], mc.PlantNum,
	   GenotypeName = 
			Case
				When @ShowPicking = 1 THEN CONCAT( ISNULL(NULLIF(g.GivenName, ' '), g.OriginalName), CHAR(13)+CHAR(10), '(', mc.PickingNumber, ')')
				else ISNULL(NULLIF(g.GivenName, ' '), g.OriginalName)
			End
		
FROM MapComponents mc

Inner join MapComponentYears mcy
on mc.Id = mcy.MapComponentId
Inner join Maps m
on m.Id = mc.MapId
Inner Join Years
on mcy.YearsId = Years.Id
Left Join Genotypes g
on g.Id = mc.GenotypeId 
where mc.MapId = @MapId 
and Years.[Year] = @Year 
and (ISNULL(@MapComponentSeedlingFlag, mc.isSeedling) = mc.isSeedling)

END