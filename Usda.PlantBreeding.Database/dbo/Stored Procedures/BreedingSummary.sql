-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[BreedingSummary]
	-- Add the parameters for the stored procedure here
	@GenusId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

  Select t.OriginalName, t.GivenName, t.FemaleParents, t.MaleParents, t.SelectionsMade from
(
	SELECT g.OriginalName, g.GivenName, f.GenusId, f.CrossNum, g.SelectionNum, f.ReciprocalString, FamOrigin.Name [FamOriginName],
	 (Select count(*) from Families where g.Id = Families.FemaleParent ) as FemaleParents,
	 (Select count(*) from Families where g.Id = Families.MaleParent ) as MaleParents,
	([dbo].[GetSelectionsFromGenotype](g.Id)) as SelectionsMade

	  FROM Genotypes g
	  Inner Join Families f
	  on g.FamilyId = f.Id
	  Inner Join Origins FamOrigin
	  on f.OriginId = FamOrigin.Id

  )t

  where (t.FemaleParents > 0 OR t.MaleParents > 0)
  and (ISNULL(@GenusId, t.GenusId) = t.GenusId)

  Order by FamOriginName, len(CrossNum), CrossNum, ReciprocalString, SelectionNum

END