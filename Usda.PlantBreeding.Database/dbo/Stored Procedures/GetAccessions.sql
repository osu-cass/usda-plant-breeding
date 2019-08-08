-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAccessions]
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
,mcfield.PickingNumber, mcfield.PlantNum, mcfield.Row, mcfield.MapsName, mcfield.PlantingYear
  
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

--Field Status
Left Join 
(
Select mcmin.Id as mapcomponentId, mcmin.GenotypeId, mcmin.Row, mcmin.PlantNum, mcmin.PickingNumber, Maps.Name MapsName, Maps.PlantingYear
from MapComponents mcmin
Inner Join
(
	SELECT Min(Maps.PlantingYear) as minYear, [MapComponents].Id
	  FROM [MapComponents] 
	  Inner Join Maps 
	  on Maps.Id = [MapComponents].MapId
	  where GenotypeId is not null
	  and Maps.IsSeedlingMap = 1
	  and isSeedling = 0
	  and GenotypeId in (
			Select Id from Genotypes where
			(ISNULL(@GenusId, GenusId) = GenusId)
			and (ISNULL(@Year,[Year]) = [Year])
			and IsRoot = 0 
		)
	  GROUP BY [MapComponents].Id
) mcdt
	  on mcdt.Id = mcmin.Id

	  Inner join Maps
	  on Maps.Id = mcmin.MapId
	  
	)mcfield
on mcfield.GenotypeId = gen.Id

where 
(ISNULL(@GenusId, Fam.GenusId) = Fam.GenusId)
and (ISNULL(@Year, gen.[Year]) = gen.[Year] )
 and (Fam.OriginId in ( Select Id from Origins where (ISNULL(@InternalFlag, Origins.IsDefault) =  Origins.IsDefault)))
and (ISNULL(@CrossesFlag, gen.IsRoot) = gen.IsRoot)

Order by FamOrigin.Name, len(fam.CrossNum), fam.CrossNum, fam.ReciprocalString, gen.SelectionNum
END