-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[GetPickingNumber] 
(
	-- Add the parameters for the function here
	@mapComponentId int
)
RETURNS nvarchar(150)
as
BEGIN
Declare @result nvarchar(150)

Select @result = 

	Case 
		when (mc.ExternalId is not null)
			then CONVERT(nvarchar(150),mc.ExternalId)
		else
			Case
				When(m.IsSeedlingMap = 1)
					then CONCAT(m.PicklistPrefix, '-', RIGHT(ISNULL(m.PlantingYear, ''), 2), 'R', mc.Row, 'P', mc.PlantNum)
				else
					CONCAT(m.PicklistPrefix, '-', RIGHT(ISNULL(mc.CreatedYear, ''), 2), 'R', mc.Row, 'P', mc.PlantNum)
			End
	End
		
	from MapComponents mc
	Inner Join Maps m
	on mc.MapId = m.Id
	where mc.Id = @mapComponentId

	RETURN @result

	END