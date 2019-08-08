-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[GetSelectionsFromGenotype]
(
	@GenotypeId int
)
RETURNS int
AS
BEGIN
Declare @result int

Select @result = sum(t.Selections)
		
		from (
		Select count(*) -1 [Selections] from Genotypes where Genotypes.FamilyId in ( select Id from  Families where (Families.FemaleParent = @GenotypeId or Families.MaleParent = @GenotypeId))
		GROUP By Genotypes.FamilyId
		)t
		

	RETURN @result

	END