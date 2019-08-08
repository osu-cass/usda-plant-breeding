-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[GetOriginalName] 
(
	-- Add the parameters for the function here
	@genotypeId int
)
RETURNS nvarchar(100)
as
BEGIN
Declare @result nvarchar(100)

	Select @result =
	CASE
		when (f.ReciprocalString is not null AND LOWER(f.ReciprocalString) <> 'n') 
			THEN CASE
				When (g.SelectionNum is not null AND g.SelectionNum > 0)
					then o.Name + ' ' + f.CrossNum + f.ReciprocalString + '-' + CONVERT(varchar(3), g.SelectionNum)
				ELSE o.Name + ' ' + f.CrossNum + f.ReciprocalString
			END
	
		ELSE
			CASE
				When (g.SelectionNum is not null AND g.SelectionNum > 0)
					then o.Name + ' ' + f.CrossNum + '-' + CONVERT(varchar(3), g.SelectionNum)
				ELSE o.Name + ' ' + f.CrossNum
			END
	
					
	END
	from Genotypes g
	Inner Join Families f
	on f.Id = g.FamilyId
	Left Join Origins o
	on o.Id = f.OriginId
	where g.Id = @genotypeId

	RETURN @result

	END