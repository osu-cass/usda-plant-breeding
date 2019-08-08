/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

USE [PlantBreeding]
GO

--Intellisense is not correct here
IF ($(defaultValues) = 1)
BEGIN
	INSERT INTO [dbo].[Genus]
				([Name]
				,[Value]
				,[DefaultPlantsInRep]
				,[VirusLabel1]
				,[VirusLabel2]
				,[VirusLabel3]
				,[Retired]
				,[ExternalId]
				,[VirusLabel4])
			VALUES
				('A','Actinidia',3,NULL,NULL,NULL,0,NULL,NULL),
				('F','Fragaria',8,NULL,NULL,NULL,0,NULL,NULL),
				('R','Rubus',3,NULL,NULL,NULL,0,NULL,NULL),
				('V','Vaccinium',3,NULL,NULL,NULL,0,NULL,NULL)

	INSERT INTO [dbo].[Origins]
				([Name]
				,[Retired]
				,[IsDefault])
			VALUES
				('DEFAULT', 0, 1)

	INSERT INTO [dbo].[Families]
				([CrossNum]
				,[FieldPlantedNum]
				,[TransplantedNum]
				,[GenusId]
				,[IsReciprocal]
				,[OriginId]
				,[SeedNum]
				,[CrossTypeId]
				,[Purpose]
				,[FemaleParent]
				,[MaleParent]
				,[ReciprocalString]
				,[BaseGenotypeId]
				,[Unsuccessful])
			VALUES
				('1',0,NULL,1,NULL,1,100,NULL,'Default Value',NULL,NULL,'N',NULL,0),
				('1',0,NULL,2,NULL,1,100,NULL,'Default Value',NULL,NULL,'N',NULL,0),
				('1',0,NULL,3,NULL,1,100,NULL,'Default Value',NULL,NULL,'N',NULL,0),
				('1',0,NULL,4,NULL,1,100,NULL,'Default Value',NULL,NULL,'N',NULL,0)
END
GO
