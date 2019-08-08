-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[GetMapPrefix] 
(
	-- Add the parameters for the function here
	@mapId int
)
RETURNS nvarchar(100)
as
BEGIN
Declare @result nvarchar(100)

Select @result = 
	CASE
		when ISNULL(l.Description, '') != '' OR ISNULL(m.LocationSuffix, '') != ''
			then ISNULL(l.Description, '')  + ISNULL(m.LocationSuffix, '') + '-' + ct.Name
		else
			ct.Name

	END
		
	from Maps m
	Inner Join CrossTypes ct
	on m.CrossTypeId = ct.Id
	inner join locations l
	on m.LocationId = l.Id
	where m.Id = @mapId

	RETURN @result

	END