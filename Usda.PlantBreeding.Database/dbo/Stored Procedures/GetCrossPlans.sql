
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetCrossPlans]
	-- Add the parameters for the stored procedure here
	@GenusId int,
	@Year varchar(5)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

SELECT cp.*, ct.Name [CrossTypeName]
--Mother --
, mother.OriginalName [MotherOriginalName], mother.GivenName [MotherGivenName]
--Mother Mother 
, motherMother.OriginalName [MotherMotherOriginalName], motherMother.GivenName [MotherMotherGivenName]
-- Mother Father
, motherFather.OriginalName [MotherFatherOriginalName], motherFather.GivenName [MotherFatherGivenName]
--father --
, father.OriginalName [FatherOriginalName], father.GivenName [FatherGivenName]
-- Father Mother --
, fatherMother.OriginalName [FatherMotherOriginalName], fatherMother.GivenName [FatherMotherGivenName]
--Father Father --
, fatherFather.OriginalName [FatherFatherOriginalName], fatherFather.GivenName [FatherFatherGivenName]

  FROM [CrossPlans] cp

  --mother --
  Left Join Genotypes mother
  on cp.FemaleParentId = mother.Id
    Left Join Families motherFamily
	  on mother.FamilyId = motherFamily.Id
  --Mother Mother
  Left Join Genotypes motherMother
  on motherFamily.FemaleParent = motherMother.Id
  --Mother Father
  left join Genotypes motherFather
  on motherFamily.MaleParent = motherFather.Id


  --father --
  Left Join Genotypes father
  on cp.MaleParentId = father.Id
   Left Join Families fatherFamily
  on father.FamilyId = fatherFamily.Id

  --Father Father
  Left Join Genotypes fatherFather
  on fatherFamily.MaleParent = fatherFather.Id

    --Father Mother
  left join Genotypes fatherMother
  on fatherFamily.FemaleParent = fatherMother.Id

  --CrossType
 left join CrossTypes ct
 on ct.Id = cp.CrossTypeId

  Where 
  (ISNULL(@GenusId, cp.GenusId) = cp.GenusId)
  and
  (ISNULL(@Year, cp.[Year]) =cp.[Year] )
  
  Order By
  mother.OriginalName, father.OriginalName, ct.Name 
 

END