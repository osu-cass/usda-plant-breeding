-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeletePhenotypeFromYear]
	-- Add the parameters for the stored procedure here
	@MapId int,
	@Year varchar(5),
	@isSeedling bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

DECLARE @toDelete TABLE (Id int)
INSERT INTO @toDelete

SELECT mcy.Id
  FROM [MapComponentYears] mcy
  inner join MapComponents mc 
  on mc.Id = mcy.MapComponentId
  Inner Join Years y
  on mcy.YearsId = y.Id

  Where mc.MapId = @MapId and y.[Year] = @year
  and mc.isSeedling = @isSeedling
  

Delete from MapComponentYearsFate
where MapComponentYears_Id in (Select * from @toDelete)

Delete from Answers 
where MapComponentYearsId in (Select * from @toDelete)


Delete from MapComponentYears
where Id in (Select * from @toDelete)


END