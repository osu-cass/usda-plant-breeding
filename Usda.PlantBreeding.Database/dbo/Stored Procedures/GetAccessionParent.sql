-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAccessionParent]
	@GenusId int,
	@Year varchar(5),
	@InternalFlag bit,
	@CrossesFlag bit
AS
BEGIN

	
	SET NOCOUNT ON;

	SELECT gen.Id, gen.SelectionNum, gen.GivenName, gen.OriginalName, gen.Year, gen.Note, gen.PloidyName [Ploidy], gen.Fate , 
	FamOrigin.Name [Origin], fam.TransplantedNum, fam.SeedNum, fam.FieldPlantedNum
,Fam.Purpose [FamilyPurpose], ct.Name [CrossTypeName]
,FamMale.OriginalName [FamilyMaleOriginalName], FamMale.GivenName [FamilyMaleGivenName]
,FamFemale.OriginalName [FamilyFemaleOriginalName], FamFemale.GivenName [FamilyFemaleGivenName]
  
  FROM [Genotypes] gen

  --Family
  Left Join Families Fam
  on Fam.Id = gen.FamilyId
  Left Join Origins FamOrigin
  on FamOrigin.Id = Fam.OriginId
  Left Join CrossTypes ct
  on fam.CrossTypeId = ct.Id

 --Family Female Parent
Left Join Genotypes FamFemale
on FamFemale.Id = Fam.FemaleParent

 --Family Male Parent
Left Join Genotypes FamMale
on FamMale.Id = Fam.MaleParent


where 
(ISNULL(@GenusId, Fam.GenusId) = Fam.GenusId)
and (ISNULL(@Year, gen.[Year]) = gen.[Year] )
 and (Fam.OriginId in ( Select Id from Origins where (ISNULL(@InternalFlag, Origins.IsDefault) =  Origins.IsDefault)))
and (ISNULL(@CrossesFlag, gen.IsRoot) = gen.IsRoot)

Order by FamOrigin.Name, len(fam.CrossNum), fam.CrossNum, fam.ReciprocalString, gen.SelectionNum
END