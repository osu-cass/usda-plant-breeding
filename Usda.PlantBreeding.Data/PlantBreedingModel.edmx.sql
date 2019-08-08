
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 08/06/2019 10:44:39
-- Generated from EDMX file: Usda.PlantBreeding.Data\PlantBreedingModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [PlantBreeding];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_AnswerGenotype]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Answers] DROP CONSTRAINT [FK_AnswerGenotype];
GO
IF OBJECT_ID(N'[dbo].[FK_AnswersQuestions]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Answers] DROP CONSTRAINT [FK_AnswersQuestions];
GO
IF OBJECT_ID(N'[dbo].[FK_CandidatesGenotype]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Candidates] DROP CONSTRAINT [FK_CandidatesGenotype];
GO
IF OBJECT_ID(N'[dbo].[FK_CrossPlanCrossType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CrossPlans] DROP CONSTRAINT [FK_CrossPlanCrossType];
GO
IF OBJECT_ID(N'[dbo].[FK_CrossPlanGenotype]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CrossPlans] DROP CONSTRAINT [FK_CrossPlanGenotype];
GO
IF OBJECT_ID(N'[dbo].[FK_CrossPlanGenotype1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CrossPlans] DROP CONSTRAINT [FK_CrossPlanGenotype1];
GO
IF OBJECT_ID(N'[dbo].[FK_CrossPlanGenus]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CrossPlans] DROP CONSTRAINT [FK_CrossPlanGenus];
GO
IF OBJECT_ID(N'[dbo].[FK_CrossPlanOrigin]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CrossPlans] DROP CONSTRAINT [FK_CrossPlanOrigin];
GO
IF OBJECT_ID(N'[dbo].[FK_CrossTypeFamily]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Families] DROP CONSTRAINT [FK_CrossTypeFamily];
GO
IF OBJECT_ID(N'[dbo].[FK_CrossTypeGenus]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CrossTypes] DROP CONSTRAINT [FK_CrossTypeGenus];
GO
IF OBJECT_ID(N'[dbo].[FK_FamilyGenotype]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Families] DROP CONSTRAINT [FK_FamilyGenotype];
GO
IF OBJECT_ID(N'[dbo].[FK_FamilyOrigin]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Families] DROP CONSTRAINT [FK_FamilyOrigin];
GO
IF OBJECT_ID(N'[dbo].[FK_FemaleFamilyGenotype]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Families] DROP CONSTRAINT [FK_FemaleFamilyGenotype];
GO
IF OBJECT_ID(N'[dbo].[FK_FlatNoteFlatType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FlatNotes] DROP CONSTRAINT [FK_FlatNoteFlatType];
GO
IF OBJECT_ID(N'[dbo].[FK_GenotypeFlatNote]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FlatNotes] DROP CONSTRAINT [FK_GenotypeFlatNote];
GO
IF OBJECT_ID(N'[dbo].[FK_GenusFamily]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Families] DROP CONSTRAINT [FK_GenusFamily];
GO
IF OBJECT_ID(N'[dbo].[FK_GenusQuestions]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Questions] DROP CONSTRAINT [FK_GenusQuestions];
GO
IF OBJECT_ID(N'[dbo].[FK_InputTypes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Questions] DROP CONSTRAINT [FK_InputTypes];
GO
IF OBJECT_ID(N'[dbo].[FK_Locations_ToGrowers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Locations] DROP CONSTRAINT [FK_Locations_ToGrowers];
GO
IF OBJECT_ID(N'[dbo].[FK_MaleFamilyGenotype]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Families] DROP CONSTRAINT [FK_MaleFamilyGenotype];
GO
IF OBJECT_ID(N'[dbo].[FK_MapComponentGenotype]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MapComponents] DROP CONSTRAINT [FK_MapComponentGenotype];
GO
IF OBJECT_ID(N'[dbo].[FK_MapComponentMapComponentYears]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MapComponentYears] DROP CONSTRAINT [FK_MapComponentMapComponentYears];
GO
IF OBJECT_ID(N'[dbo].[FK_MapComponentRepetition]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Repetitions] DROP CONSTRAINT [FK_MapComponentRepetition];
GO
IF OBJECT_ID(N'[dbo].[FK_MapComponentYearsAnswer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Answers] DROP CONSTRAINT [FK_MapComponentYearsAnswer];
GO
IF OBJECT_ID(N'[dbo].[FK_MapComponentYearsFate_Fate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MapComponentYearsFate] DROP CONSTRAINT [FK_MapComponentYearsFate_Fate];
GO
IF OBJECT_ID(N'[dbo].[FK_MapComponentYearsFate_MapComponentYears]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MapComponentYearsFate] DROP CONSTRAINT [FK_MapComponentYearsFate_MapComponentYears];
GO
IF OBJECT_ID(N'[dbo].[FK_MapGenus]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Maps] DROP CONSTRAINT [FK_MapGenus];
GO
IF OBJECT_ID(N'[dbo].[FK_MapLocation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Maps] DROP CONSTRAINT [FK_MapLocation];
GO
IF OBJECT_ID(N'[dbo].[FK_MapMapComponent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MapComponents] DROP CONSTRAINT [FK_MapMapComponent];
GO
IF OBJECT_ID(N'[dbo].[FK_MapQuestion_Map]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MapQuestion] DROP CONSTRAINT [FK_MapQuestion_Map];
GO
IF OBJECT_ID(N'[dbo].[FK_MapQuestion_Question]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MapQuestion] DROP CONSTRAINT [FK_MapQuestion_Question];
GO
IF OBJECT_ID(N'[dbo].[FK_Maps_ToTab]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Maps] DROP CONSTRAINT [FK_Maps_ToTab];
GO
IF OBJECT_ID(N'[dbo].[FK_MapYears]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Years] DROP CONSTRAINT [FK_MapYears];
GO
IF OBJECT_ID(N'[dbo].[FK_NoteGenotype]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Notes] DROP CONSTRAINT [FK_NoteGenotype];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderProducts_ToGenotypes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderProducts] DROP CONSTRAINT [FK_OrderProducts_ToGenotypes];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderProducts_ToMaterials]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderProducts] DROP CONSTRAINT [FK_OrderProducts_ToMaterials];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderProducts_ToOrders]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderProducts] DROP CONSTRAINT [FK_OrderProducts_ToOrders];
GO
IF OBJECT_ID(N'[dbo].[FK_Orders_ToGenus]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [FK_Orders_ToGenus];
GO
IF OBJECT_ID(N'[dbo].[FK_Orders_ToGrowers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [FK_Orders_ToGrowers];
GO
IF OBJECT_ID(N'[dbo].[FK_Orders_ToLocations]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [FK_Orders_ToLocations];
GO
IF OBJECT_ID(N'[dbo].[FK_PlantFamily]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Genotypes] DROP CONSTRAINT [FK_PlantFamily];
GO
IF OBJECT_ID(N'[dbo].[FK_UserPreferenceGenus]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserPreferences] DROP CONSTRAINT [FK_UserPreferenceGenus];
GO
IF OBJECT_ID(N'[dbo].[FK_YearsMapComponentYears]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MapComponentYears] DROP CONSTRAINT [FK_YearsMapComponentYears];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[__RefactorLog]', 'U') IS NOT NULL
    DROP TABLE [dbo].[__RefactorLog];
GO
IF OBJECT_ID(N'[dbo].[Answers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Answers];
GO
IF OBJECT_ID(N'[dbo].[Candidates]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Candidates];
GO
IF OBJECT_ID(N'[dbo].[CrossPlans]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CrossPlans];
GO
IF OBJECT_ID(N'[dbo].[CrossTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CrossTypes];
GO
IF OBJECT_ID(N'[dbo].[Families]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Families];
GO
IF OBJECT_ID(N'[dbo].[Fates]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Fates];
GO
IF OBJECT_ID(N'[dbo].[FlatNotes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FlatNotes];
GO
IF OBJECT_ID(N'[dbo].[FlatTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FlatTypes];
GO
IF OBJECT_ID(N'[dbo].[Genotypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Genotypes];
GO
IF OBJECT_ID(N'[dbo].[Genus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Genus];
GO
IF OBJECT_ID(N'[dbo].[Growers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Growers];
GO
IF OBJECT_ID(N'[dbo].[InputTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InputTypes];
GO
IF OBJECT_ID(N'[dbo].[Locations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Locations];
GO
IF OBJECT_ID(N'[dbo].[MapComponents]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MapComponents];
GO
IF OBJECT_ID(N'[dbo].[MapComponentYears]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MapComponentYears];
GO
IF OBJECT_ID(N'[dbo].[MapComponentYearsFate]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MapComponentYearsFate];
GO
IF OBJECT_ID(N'[dbo].[MapQuestion]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MapQuestion];
GO
IF OBJECT_ID(N'[dbo].[Maps]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Maps];
GO
IF OBJECT_ID(N'[dbo].[Materials]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Materials];
GO
IF OBJECT_ID(N'[dbo].[Notes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Notes];
GO
IF OBJECT_ID(N'[dbo].[OrderProducts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderProducts];
GO
IF OBJECT_ID(N'[dbo].[Orders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Orders];
GO
IF OBJECT_ID(N'[dbo].[Origins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Origins];
GO
IF OBJECT_ID(N'[dbo].[Ploidies]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Ploidies];
GO
IF OBJECT_ID(N'[dbo].[Purposes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Purposes];
GO
IF OBJECT_ID(N'[dbo].[Questions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Questions];
GO
IF OBJECT_ID(N'[dbo].[Repetitions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Repetitions];
GO
IF OBJECT_ID(N'[dbo].[UserPreferences]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserPreferences];
GO
IF OBJECT_ID(N'[dbo].[Years]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Years];
GO
IF OBJECT_ID(N'[USDAFruitsModelStoreContainer].[QuestionsCPY]', 'U') IS NOT NULL
    DROP TABLE [USDAFruitsModelStoreContainer].[QuestionsCPY];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Families'
CREATE TABLE [dbo].[Families] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CrossNum] nvarchar(max)  NULL,
    [GenusId] int  NOT NULL,
    [IsReciprocal] bit  NULL,
    [OriginId] int  NULL,
    [SeedNum] int  NULL,
    [CrossTypeId] int  NULL,
    [Purpose] nvarchar(max)  NULL,
    [FemaleParent] int  NULL,
    [MaleParent] int  NULL,
    [ReciprocalString] nvarchar(max)  NULL,
    [BaseGenotypeId] int  NULL,
    [MaleParentOriginalName] nvarchar(100)  NULL,
    [FemaleParentOriginalName] nvarchar(100)  NULL,
    [Unsuccessful] bit  NOT NULL,
    [FieldPlantedNum] int  NULL,
    [TransplantedNum] int  NULL
);
GO

-- Creating table 'Genotypes'
CREATE TABLE [dbo].[Genotypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SelectionNum] int  NULL,
    [GivenName] nvarchar(max)  NULL,
    [FamilyId] int  NOT NULL,
    [Year] nvarchar(max)  NULL,
    [Note] nvarchar(max)  NULL,
    [Fate] nvarchar(max)  NULL,
    [ExternalId] int  NULL,
    [IsRoot] bit  NULL,
    [Alias1] nvarchar(max)  NULL,
    [Alias2] nvarchar(max)  NULL,
    [OriginalName] nvarchar(100)  NULL,
    [PatentApp] nvarchar(max)  NULL,
    [Patent] nvarchar(max)  NULL,
    [PatentYear] nvarchar(max)  NULL,
    [PloidyName] nvarchar(255)  NULL,
    [CrossPlanId] int  NULL,
    [CreatedDate] datetime  NULL,
    [IsPopulation] bit  NOT NULL
);
GO

-- Creating table 'CrossTypes'
CREATE TABLE [dbo].[CrossTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NULL,
    [Value] nvarchar(max)  NULL,
    [Retired] bit  NOT NULL,
    [GenusId] int  NOT NULL,
    [ExternalId] int  NULL
);
GO

-- Creating table 'Genus'
CREATE TABLE [dbo].[Genus] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NULL,
    [Value] nvarchar(max)  NULL,
    [DefaultPlantsInRep] int  NOT NULL,
    [VirusLabel1] nvarchar(max)  NULL,
    [VirusLabel2] nvarchar(max)  NULL,
    [VirusLabel3] nvarchar(max)  NULL,
    [Retired] bit  NOT NULL,
    [ExternalId] int  NULL,
    [VirusLabel4] nvarchar(max)  NULL
);
GO

-- Creating table 'Origins'
CREATE TABLE [dbo].[Origins] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NULL,
    [Retired] bit  NOT NULL,
    [IsDefault] bit  NOT NULL
);
GO

-- Creating table 'Ploidies'
CREATE TABLE [dbo].[Ploidies] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NULL,
    [Retired] bit  NOT NULL,
    [Value] nvarchar(max)  NULL
);
GO

-- Creating table 'Questions'
CREATE TABLE [dbo].[Questions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Text] nvarchar(max)  NULL,
    [GenusId] int  NOT NULL,
    [Required] bit  NOT NULL,
    [Retired] bit  NULL,
    [Label] nvarchar(max)  NULL,
    [Order] int  NOT NULL,
    [ExternalId] int  NULL,
    [InputTypeId] int  NULL
);
GO

-- Creating table 'Answers'
CREATE TABLE [dbo].[Answers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Text] nvarchar(max)  NULL,
    [QuestionId] int  NOT NULL,
    [GenotypeId] int  NOT NULL,
    [MapComponentYearsId] int  NOT NULL
);
GO

-- Creating table 'MapComponents'
CREATE TABLE [dbo].[MapComponents] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Row] int  NULL,
    [MapId] int  NOT NULL,
    [Rep] int  NOT NULL,
    [PlantsInRep] int  NOT NULL,
    [GenotypeId] int  NULL,
    [PlantNum] int  NULL,
    [isSeedling] bit  NULL,
    [ExternalId] int  NULL,
    [Virus4] nvarchar(50)  NULL,
    [Virus1] nvarchar(50)  NULL,
    [Virus2] nvarchar(50)  NULL,
    [Virus3] nvarchar(50)  NULL,
    [isRemoved] bit  NOT NULL,
    [PickingNumber] nvarchar(150)  NULL,
    [CreatedYear] nvarchar(4)  NULL
);
GO

-- Creating table 'Candidates'
CREATE TABLE [dbo].[Candidates] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Year] int  NULL,
    [GenotypeId] int  NOT NULL
);
GO

-- Creating table 'Maps'
CREATE TABLE [dbo].[Maps] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NULL,
    [PlantingYear] nvarchar(max)  NULL,
    [GenusId] int  NOT NULL,
    [IsSeedlingMap] bit  NOT NULL,
    [PicklistPrefix] nvarchar(100)  NULL,
    [Retired] bit  NOT NULL,
    [DefaultPlantsInRep] int  NOT NULL,
    [LocationId] int  NULL,
    [ExternalId] int  NULL,
    [StartCorner] nchar(50)  NULL,
    [LocationSuffix] nvarchar(50)  NULL,
    [CrossTypeId] int  NULL,
    [Note] nvarchar(max)  NULL
);
GO

-- Creating table 'Repetitions'
CREATE TABLE [dbo].[Repetitions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [MapComponentId] int  NOT NULL,
    [Note] nvarchar(max)  NOT NULL,
    [RepNumber] int  NOT NULL,
    [RBD] bit  NOT NULL,
    [RSV] bit  NOT NULL,
    [TSV] bit  NOT NULL
);
GO

-- Creating table 'CrossPlans'
CREATE TABLE [dbo].[CrossPlans] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Year] nvarchar(max)  NULL,
    [MaleParentId] int  NULL,
    [FemaleParentId] int  NULL,
    [Purpose] nvarchar(max)  NULL,
    [GenusId] int  NOT NULL,
    [Unsuccessful] bit  NOT NULL,
    [Reciprocal] bit  NOT NULL,
    [CrossTypeId] int  NULL,
    [OriginId] int  NULL,
    [SeedNum] int  NULL,
    [FieldPlantedNum] int  NULL,
    [TransplantedNum] int  NULL,
    [Note] nvarchar(max)  NULL,
    [CrossNum] nvarchar(max)  NULL,
    [GenotypeId] int  NULL,
    [DateCreated] datetime  NOT NULL
);
GO

-- Creating table 'Fates'
CREATE TABLE [dbo].[Fates] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Retired] bit  NOT NULL,
    [Order] int  NULL,
    [Label] nvarchar(50)  NULL
);
GO

-- Creating table 'Locations'
CREATE TABLE [dbo].[Locations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Retired] bit  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [MapFlag] bit  NOT NULL,
    [PrimaryContactId] int  NULL,
    [Address] nvarchar(255)  NULL
);
GO

-- Creating table 'Notes'
CREATE TABLE [dbo].[Notes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [GenotypeId] int  NOT NULL,
    [Text] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'FlatTypes'
CREATE TABLE [dbo].[FlatTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Value] nvarchar(max)  NULL,
    [Retired] bit  NOT NULL
);
GO

-- Creating table 'FlatNotes'
CREATE TABLE [dbo].[FlatNotes] (
    [Other] nvarchar(max)  NULL,
    [FlatTypeId] int  NOT NULL,
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [Text] nvarchar(max)  NOT NULL,
    [GenotypeId] int  NOT NULL
);
GO

-- Creating table 'Years'
CREATE TABLE [dbo].[Years] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [MapId] int  NOT NULL,
    [Year] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'MapComponentYears'
CREATE TABLE [dbo].[MapComponentYears] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Comments] nvarchar(max)  NULL,
    [MapComponentId] int  NOT NULL,
    [YearsId] int  NOT NULL
);
GO

-- Creating table 'UserPreferences'
CREATE TABLE [dbo].[UserPreferences] (
    [UserId] varchar(50)  NOT NULL,
    [GenusId] int  NOT NULL
);
GO

-- Creating table 'Purposes'
CREATE TABLE [dbo].[Purposes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [Value] nchar(255)  NULL,
    [Retired] bit  NOT NULL
);
GO

-- Creating table 'InputTypes'
CREATE TABLE [dbo].[InputTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Text] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Growers'
CREATE TABLE [dbo].[Growers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] varchar(50)  NOT NULL,
    [LastName] varchar(50)  NOT NULL,
    [Email] varchar(320)  NULL,
    [WorkPhone] char(10)  NULL,
    [MailingName] varchar(500)  NULL,
    [MobilePhone] char(10)  NULL,
    [CreatedDate] datetime  NULL
);
GO

-- Creating table 'Materials'
CREATE TABLE [dbo].[Materials] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [Value] varchar(255)  NULL
);
GO

-- Creating table 'OrderProducts'
CREATE TABLE [dbo].[OrderProducts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [GenotypeId] int  NOT NULL,
    [Quantity] int  NULL,
    [MaterialId] int  NOT NULL,
    [OrderId] int  NOT NULL,
    [VirusTested] datetime  NULL,
    [Note] nvarchar(255)  NULL,
    [CreatedDate] datetime  NOT NULL,
    [DateSent] datetime  NULL
);
GO

-- Creating table 'Orders'
CREATE TABLE [dbo].[Orders] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [Year] int  NOT NULL,
    [GenusId] int  NOT NULL,
    [GrowerId] int  NOT NULL,
    [LocationId] int  NULL,
    [Note] nvarchar(max)  NULL,
    [MTARequestedProp] bit  NOT NULL,
    [MTARequestedTest] bit  NOT NULL,
    [MTAComplete] datetime  NULL
);
GO

-- Creating table 'C__RefactorLog'
CREATE TABLE [dbo].[C__RefactorLog] (
    [OperationKey] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'QuestionsCPies'
CREATE TABLE [dbo].[QuestionsCPies] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Text] nvarchar(max)  NULL,
    [GenusId] int  NOT NULL,
    [Required] bit  NOT NULL,
    [Retired] bit  NULL,
    [Label] nvarchar(max)  NULL,
    [Order] int  NOT NULL,
    [ExternalId] int  NULL,
    [InputTypeId] int  NULL,
    [CurId] int  NULL
);
GO

-- Creating table 'MapQuestion'
CREATE TABLE [dbo].[MapQuestion] (
    [Maps_Id] int  NOT NULL,
    [Questions_Id] int  NOT NULL
);
GO

-- Creating table 'MapComponentYearsFate'
CREATE TABLE [dbo].[MapComponentYearsFate] (
    [MapComponentYears_Id] int  NOT NULL,
    [Fates_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Families'
ALTER TABLE [dbo].[Families]
ADD CONSTRAINT [PK_Families]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Genotypes'
ALTER TABLE [dbo].[Genotypes]
ADD CONSTRAINT [PK_Genotypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CrossTypes'
ALTER TABLE [dbo].[CrossTypes]
ADD CONSTRAINT [PK_CrossTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Genus'
ALTER TABLE [dbo].[Genus]
ADD CONSTRAINT [PK_Genus]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Origins'
ALTER TABLE [dbo].[Origins]
ADD CONSTRAINT [PK_Origins]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Ploidies'
ALTER TABLE [dbo].[Ploidies]
ADD CONSTRAINT [PK_Ploidies]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Questions'
ALTER TABLE [dbo].[Questions]
ADD CONSTRAINT [PK_Questions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Answers'
ALTER TABLE [dbo].[Answers]
ADD CONSTRAINT [PK_Answers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MapComponents'
ALTER TABLE [dbo].[MapComponents]
ADD CONSTRAINT [PK_MapComponents]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Candidates'
ALTER TABLE [dbo].[Candidates]
ADD CONSTRAINT [PK_Candidates]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Maps'
ALTER TABLE [dbo].[Maps]
ADD CONSTRAINT [PK_Maps]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Repetitions'
ALTER TABLE [dbo].[Repetitions]
ADD CONSTRAINT [PK_Repetitions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CrossPlans'
ALTER TABLE [dbo].[CrossPlans]
ADD CONSTRAINT [PK_CrossPlans]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Fates'
ALTER TABLE [dbo].[Fates]
ADD CONSTRAINT [PK_Fates]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Locations'
ALTER TABLE [dbo].[Locations]
ADD CONSTRAINT [PK_Locations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Notes'
ALTER TABLE [dbo].[Notes]
ADD CONSTRAINT [PK_Notes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FlatTypes'
ALTER TABLE [dbo].[FlatTypes]
ADD CONSTRAINT [PK_FlatTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FlatNotes'
ALTER TABLE [dbo].[FlatNotes]
ADD CONSTRAINT [PK_FlatNotes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Years'
ALTER TABLE [dbo].[Years]
ADD CONSTRAINT [PK_Years]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MapComponentYears'
ALTER TABLE [dbo].[MapComponentYears]
ADD CONSTRAINT [PK_MapComponentYears]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [UserId] in table 'UserPreferences'
ALTER TABLE [dbo].[UserPreferences]
ADD CONSTRAINT [PK_UserPreferences]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [Id] in table 'Purposes'
ALTER TABLE [dbo].[Purposes]
ADD CONSTRAINT [PK_Purposes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'InputTypes'
ALTER TABLE [dbo].[InputTypes]
ADD CONSTRAINT [PK_InputTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Growers'
ALTER TABLE [dbo].[Growers]
ADD CONSTRAINT [PK_Growers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Materials'
ALTER TABLE [dbo].[Materials]
ADD CONSTRAINT [PK_Materials]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrderProducts'
ALTER TABLE [dbo].[OrderProducts]
ADD CONSTRAINT [PK_OrderProducts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [PK_Orders]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [OperationKey] in table 'C__RefactorLog'
ALTER TABLE [dbo].[C__RefactorLog]
ADD CONSTRAINT [PK_C__RefactorLog]
    PRIMARY KEY CLUSTERED ([OperationKey] ASC);
GO

-- Creating primary key on [Id], [GenusId], [Required], [Order] in table 'QuestionsCPies'
ALTER TABLE [dbo].[QuestionsCPies]
ADD CONSTRAINT [PK_QuestionsCPies]
    PRIMARY KEY CLUSTERED ([Id], [GenusId], [Required], [Order] ASC);
GO

-- Creating primary key on [Maps_Id], [Questions_Id] in table 'MapQuestion'
ALTER TABLE [dbo].[MapQuestion]
ADD CONSTRAINT [PK_MapQuestion]
    PRIMARY KEY CLUSTERED ([Maps_Id], [Questions_Id] ASC);
GO

-- Creating primary key on [MapComponentYears_Id], [Fates_Id] in table 'MapComponentYearsFate'
ALTER TABLE [dbo].[MapComponentYearsFate]
ADD CONSTRAINT [PK_MapComponentYearsFate]
    PRIMARY KEY CLUSTERED ([MapComponentYears_Id], [Fates_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [GenusId] in table 'Families'
ALTER TABLE [dbo].[Families]
ADD CONSTRAINT [FK_GenusFamily]
    FOREIGN KEY ([GenusId])
    REFERENCES [dbo].[Genus]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GenusFamily'
CREATE INDEX [IX_FK_GenusFamily]
ON [dbo].[Families]
    ([GenusId]);
GO

-- Creating foreign key on [OriginId] in table 'Families'
ALTER TABLE [dbo].[Families]
ADD CONSTRAINT [FK_FamilyOrigin]
    FOREIGN KEY ([OriginId])
    REFERENCES [dbo].[Origins]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FamilyOrigin'
CREATE INDEX [IX_FK_FamilyOrigin]
ON [dbo].[Families]
    ([OriginId]);
GO

-- Creating foreign key on [CrossTypeId] in table 'Families'
ALTER TABLE [dbo].[Families]
ADD CONSTRAINT [FK_CrossTypeFamily]
    FOREIGN KEY ([CrossTypeId])
    REFERENCES [dbo].[CrossTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CrossTypeFamily'
CREATE INDEX [IX_FK_CrossTypeFamily]
ON [dbo].[Families]
    ([CrossTypeId]);
GO

-- Creating foreign key on [FamilyId] in table 'Genotypes'
ALTER TABLE [dbo].[Genotypes]
ADD CONSTRAINT [FK_PlantFamily]
    FOREIGN KEY ([FamilyId])
    REFERENCES [dbo].[Families]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PlantFamily'
CREATE INDEX [IX_FK_PlantFamily]
ON [dbo].[Genotypes]
    ([FamilyId]);
GO

-- Creating foreign key on [FemaleParent] in table 'Families'
ALTER TABLE [dbo].[Families]
ADD CONSTRAINT [FK_FemaleFamilyGenotype]
    FOREIGN KEY ([FemaleParent])
    REFERENCES [dbo].[Genotypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FemaleFamilyGenotype'
CREATE INDEX [IX_FK_FemaleFamilyGenotype]
ON [dbo].[Families]
    ([FemaleParent]);
GO

-- Creating foreign key on [MaleParent] in table 'Families'
ALTER TABLE [dbo].[Families]
ADD CONSTRAINT [FK_MaleFamilyGenotype]
    FOREIGN KEY ([MaleParent])
    REFERENCES [dbo].[Genotypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MaleFamilyGenotype'
CREATE INDEX [IX_FK_MaleFamilyGenotype]
ON [dbo].[Families]
    ([MaleParent]);
GO

-- Creating foreign key on [GenusId] in table 'Questions'
ALTER TABLE [dbo].[Questions]
ADD CONSTRAINT [FK_GenusQuestions]
    FOREIGN KEY ([GenusId])
    REFERENCES [dbo].[Genus]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GenusQuestions'
CREATE INDEX [IX_FK_GenusQuestions]
ON [dbo].[Questions]
    ([GenusId]);
GO

-- Creating foreign key on [QuestionId] in table 'Answers'
ALTER TABLE [dbo].[Answers]
ADD CONSTRAINT [FK_AnswersQuestions]
    FOREIGN KEY ([QuestionId])
    REFERENCES [dbo].[Questions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AnswersQuestions'
CREATE INDEX [IX_FK_AnswersQuestions]
ON [dbo].[Answers]
    ([QuestionId]);
GO

-- Creating foreign key on [GenotypeId] in table 'Candidates'
ALTER TABLE [dbo].[Candidates]
ADD CONSTRAINT [FK_CandidatesGenotype]
    FOREIGN KEY ([GenotypeId])
    REFERENCES [dbo].[Genotypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CandidatesGenotype'
CREATE INDEX [IX_FK_CandidatesGenotype]
ON [dbo].[Candidates]
    ([GenotypeId]);
GO

-- Creating foreign key on [MapId] in table 'MapComponents'
ALTER TABLE [dbo].[MapComponents]
ADD CONSTRAINT [FK_MapMapComponent]
    FOREIGN KEY ([MapId])
    REFERENCES [dbo].[Maps]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MapMapComponent'
CREATE INDEX [IX_FK_MapMapComponent]
ON [dbo].[MapComponents]
    ([MapId]);
GO

-- Creating foreign key on [MapComponentId] in table 'Repetitions'
ALTER TABLE [dbo].[Repetitions]
ADD CONSTRAINT [FK_MapComponentRepetition]
    FOREIGN KEY ([MapComponentId])
    REFERENCES [dbo].[MapComponents]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MapComponentRepetition'
CREATE INDEX [IX_FK_MapComponentRepetition]
ON [dbo].[Repetitions]
    ([MapComponentId]);
GO

-- Creating foreign key on [Maps_Id] in table 'MapQuestion'
ALTER TABLE [dbo].[MapQuestion]
ADD CONSTRAINT [FK_MapQuestion_Map]
    FOREIGN KEY ([Maps_Id])
    REFERENCES [dbo].[Maps]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Questions_Id] in table 'MapQuestion'
ALTER TABLE [dbo].[MapQuestion]
ADD CONSTRAINT [FK_MapQuestion_Question]
    FOREIGN KEY ([Questions_Id])
    REFERENCES [dbo].[Questions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MapQuestion_Question'
CREATE INDEX [IX_FK_MapQuestion_Question]
ON [dbo].[MapQuestion]
    ([Questions_Id]);
GO

-- Creating foreign key on [GenusId] in table 'Maps'
ALTER TABLE [dbo].[Maps]
ADD CONSTRAINT [FK_MapGenus]
    FOREIGN KEY ([GenusId])
    REFERENCES [dbo].[Genus]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MapGenus'
CREATE INDEX [IX_FK_MapGenus]
ON [dbo].[Maps]
    ([GenusId]);
GO

-- Creating foreign key on [GenotypeId] in table 'Answers'
ALTER TABLE [dbo].[Answers]
ADD CONSTRAINT [FK_AnswerGenotype]
    FOREIGN KEY ([GenotypeId])
    REFERENCES [dbo].[Genotypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AnswerGenotype'
CREATE INDEX [IX_FK_AnswerGenotype]
ON [dbo].[Answers]
    ([GenotypeId]);
GO

-- Creating foreign key on [MaleParentId] in table 'CrossPlans'
ALTER TABLE [dbo].[CrossPlans]
ADD CONSTRAINT [FK_CrossPlanGenotype]
    FOREIGN KEY ([MaleParentId])
    REFERENCES [dbo].[Genotypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CrossPlanGenotype'
CREATE INDEX [IX_FK_CrossPlanGenotype]
ON [dbo].[CrossPlans]
    ([MaleParentId]);
GO

-- Creating foreign key on [FemaleParentId] in table 'CrossPlans'
ALTER TABLE [dbo].[CrossPlans]
ADD CONSTRAINT [FK_CrossPlanGenotype1]
    FOREIGN KEY ([FemaleParentId])
    REFERENCES [dbo].[Genotypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CrossPlanGenotype1'
CREATE INDEX [IX_FK_CrossPlanGenotype1]
ON [dbo].[CrossPlans]
    ([FemaleParentId]);
GO

-- Creating foreign key on [GenotypeId] in table 'MapComponents'
ALTER TABLE [dbo].[MapComponents]
ADD CONSTRAINT [FK_MapComponentGenotype]
    FOREIGN KEY ([GenotypeId])
    REFERENCES [dbo].[Genotypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MapComponentGenotype'
CREATE INDEX [IX_FK_MapComponentGenotype]
ON [dbo].[MapComponents]
    ([GenotypeId]);
GO

-- Creating foreign key on [LocationId] in table 'Maps'
ALTER TABLE [dbo].[Maps]
ADD CONSTRAINT [FK_MapLocation]
    FOREIGN KEY ([LocationId])
    REFERENCES [dbo].[Locations]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MapLocation'
CREATE INDEX [IX_FK_MapLocation]
ON [dbo].[Maps]
    ([LocationId]);
GO

-- Creating foreign key on [GenusId] in table 'CrossPlans'
ALTER TABLE [dbo].[CrossPlans]
ADD CONSTRAINT [FK_CrossPlanGenus]
    FOREIGN KEY ([GenusId])
    REFERENCES [dbo].[Genus]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CrossPlanGenus'
CREATE INDEX [IX_FK_CrossPlanGenus]
ON [dbo].[CrossPlans]
    ([GenusId]);
GO

-- Creating foreign key on [GenotypeId] in table 'Notes'
ALTER TABLE [dbo].[Notes]
ADD CONSTRAINT [FK_NoteGenotype]
    FOREIGN KEY ([GenotypeId])
    REFERENCES [dbo].[Genotypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_NoteGenotype'
CREATE INDEX [IX_FK_NoteGenotype]
ON [dbo].[Notes]
    ([GenotypeId]);
GO

-- Creating foreign key on [FlatTypeId] in table 'FlatNotes'
ALTER TABLE [dbo].[FlatNotes]
ADD CONSTRAINT [FK_FlatNoteFlatType]
    FOREIGN KEY ([FlatTypeId])
    REFERENCES [dbo].[FlatTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FlatNoteFlatType'
CREATE INDEX [IX_FK_FlatNoteFlatType]
ON [dbo].[FlatNotes]
    ([FlatTypeId]);
GO

-- Creating foreign key on [GenotypeId] in table 'FlatNotes'
ALTER TABLE [dbo].[FlatNotes]
ADD CONSTRAINT [FK_GenotypeFlatNote]
    FOREIGN KEY ([GenotypeId])
    REFERENCES [dbo].[Genotypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GenotypeFlatNote'
CREATE INDEX [IX_FK_GenotypeFlatNote]
ON [dbo].[FlatNotes]
    ([GenotypeId]);
GO

-- Creating foreign key on [BaseGenotypeId] in table 'Families'
ALTER TABLE [dbo].[Families]
ADD CONSTRAINT [FK_FamilyGenotype]
    FOREIGN KEY ([BaseGenotypeId])
    REFERENCES [dbo].[Genotypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FamilyGenotype'
CREATE INDEX [IX_FK_FamilyGenotype]
ON [dbo].[Families]
    ([BaseGenotypeId]);
GO

-- Creating foreign key on [GenusId] in table 'CrossTypes'
ALTER TABLE [dbo].[CrossTypes]
ADD CONSTRAINT [FK_CrossTypeGenus]
    FOREIGN KEY ([GenusId])
    REFERENCES [dbo].[Genus]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CrossTypeGenus'
CREATE INDEX [IX_FK_CrossTypeGenus]
ON [dbo].[CrossTypes]
    ([GenusId]);
GO

-- Creating foreign key on [CrossTypeId] in table 'CrossPlans'
ALTER TABLE [dbo].[CrossPlans]
ADD CONSTRAINT [FK_CrossPlanCrossType]
    FOREIGN KEY ([CrossTypeId])
    REFERENCES [dbo].[CrossTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CrossPlanCrossType'
CREATE INDEX [IX_FK_CrossPlanCrossType]
ON [dbo].[CrossPlans]
    ([CrossTypeId]);
GO

-- Creating foreign key on [OriginId] in table 'CrossPlans'
ALTER TABLE [dbo].[CrossPlans]
ADD CONSTRAINT [FK_CrossPlanOrigin]
    FOREIGN KEY ([OriginId])
    REFERENCES [dbo].[Origins]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CrossPlanOrigin'
CREATE INDEX [IX_FK_CrossPlanOrigin]
ON [dbo].[CrossPlans]
    ([OriginId]);
GO

-- Creating foreign key on [MapId] in table 'Years'
ALTER TABLE [dbo].[Years]
ADD CONSTRAINT [FK_MapYears]
    FOREIGN KEY ([MapId])
    REFERENCES [dbo].[Maps]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MapYears'
CREATE INDEX [IX_FK_MapYears]
ON [dbo].[Years]
    ([MapId]);
GO

-- Creating foreign key on [MapComponentId] in table 'MapComponentYears'
ALTER TABLE [dbo].[MapComponentYears]
ADD CONSTRAINT [FK_MapComponentMapComponentYears]
    FOREIGN KEY ([MapComponentId])
    REFERENCES [dbo].[MapComponents]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MapComponentMapComponentYears'
CREATE INDEX [IX_FK_MapComponentMapComponentYears]
ON [dbo].[MapComponentYears]
    ([MapComponentId]);
GO

-- Creating foreign key on [YearsId] in table 'MapComponentYears'
ALTER TABLE [dbo].[MapComponentYears]
ADD CONSTRAINT [FK_YearsMapComponentYears]
    FOREIGN KEY ([YearsId])
    REFERENCES [dbo].[Years]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_YearsMapComponentYears'
CREATE INDEX [IX_FK_YearsMapComponentYears]
ON [dbo].[MapComponentYears]
    ([YearsId]);
GO

-- Creating foreign key on [MapComponentYears_Id] in table 'MapComponentYearsFate'
ALTER TABLE [dbo].[MapComponentYearsFate]
ADD CONSTRAINT [FK_MapComponentYearsFate_MapComponentYears]
    FOREIGN KEY ([MapComponentYears_Id])
    REFERENCES [dbo].[MapComponentYears]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Fates_Id] in table 'MapComponentYearsFate'
ALTER TABLE [dbo].[MapComponentYearsFate]
ADD CONSTRAINT [FK_MapComponentYearsFate_Fate]
    FOREIGN KEY ([Fates_Id])
    REFERENCES [dbo].[Fates]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MapComponentYearsFate_Fate'
CREATE INDEX [IX_FK_MapComponentYearsFate_Fate]
ON [dbo].[MapComponentYearsFate]
    ([Fates_Id]);
GO

-- Creating foreign key on [MapComponentYearsId] in table 'Answers'
ALTER TABLE [dbo].[Answers]
ADD CONSTRAINT [FK_MapComponentYearsAnswer]
    FOREIGN KEY ([MapComponentYearsId])
    REFERENCES [dbo].[MapComponentYears]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MapComponentYearsAnswer'
CREATE INDEX [IX_FK_MapComponentYearsAnswer]
ON [dbo].[Answers]
    ([MapComponentYearsId]);
GO

-- Creating foreign key on [CrossTypeId] in table 'Maps'
ALTER TABLE [dbo].[Maps]
ADD CONSTRAINT [FK_Maps_ToTab]
    FOREIGN KEY ([CrossTypeId])
    REFERENCES [dbo].[CrossTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Maps_ToTab'
CREATE INDEX [IX_FK_Maps_ToTab]
ON [dbo].[Maps]
    ([CrossTypeId]);
GO

-- Creating foreign key on [GenusId] in table 'UserPreferences'
ALTER TABLE [dbo].[UserPreferences]
ADD CONSTRAINT [FK_UserPreferenceGenus]
    FOREIGN KEY ([GenusId])
    REFERENCES [dbo].[Genus]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserPreferenceGenus'
CREATE INDEX [IX_FK_UserPreferenceGenus]
ON [dbo].[UserPreferences]
    ([GenusId]);
GO

-- Creating foreign key on [InputTypeId] in table 'Questions'
ALTER TABLE [dbo].[Questions]
ADD CONSTRAINT [FK_InputTypes]
    FOREIGN KEY ([InputTypeId])
    REFERENCES [dbo].[InputTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InputTypes'
CREATE INDEX [IX_FK_InputTypes]
ON [dbo].[Questions]
    ([InputTypeId]);
GO

-- Creating foreign key on [GenotypeId] in table 'OrderProducts'
ALTER TABLE [dbo].[OrderProducts]
ADD CONSTRAINT [FK_OrderProducts_ToGenotypes]
    FOREIGN KEY ([GenotypeId])
    REFERENCES [dbo].[Genotypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderProducts_ToGenotypes'
CREATE INDEX [IX_FK_OrderProducts_ToGenotypes]
ON [dbo].[OrderProducts]
    ([GenotypeId]);
GO

-- Creating foreign key on [GenusId] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_Orders_ToGenus]
    FOREIGN KEY ([GenusId])
    REFERENCES [dbo].[Genus]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Orders_ToGenus'
CREATE INDEX [IX_FK_Orders_ToGenus]
ON [dbo].[Orders]
    ([GenusId]);
GO

-- Creating foreign key on [GrowerId] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_Orders_ToGrowers]
    FOREIGN KEY ([GrowerId])
    REFERENCES [dbo].[Growers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Orders_ToGrowers'
CREATE INDEX [IX_FK_Orders_ToGrowers]
ON [dbo].[Orders]
    ([GrowerId]);
GO

-- Creating foreign key on [MaterialId] in table 'OrderProducts'
ALTER TABLE [dbo].[OrderProducts]
ADD CONSTRAINT [FK_OrderProducts_ToMaterials]
    FOREIGN KEY ([MaterialId])
    REFERENCES [dbo].[Materials]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderProducts_ToMaterials'
CREATE INDEX [IX_FK_OrderProducts_ToMaterials]
ON [dbo].[OrderProducts]
    ([MaterialId]);
GO

-- Creating foreign key on [OrderId] in table 'OrderProducts'
ALTER TABLE [dbo].[OrderProducts]
ADD CONSTRAINT [FK_OrderProducts_ToOrders]
    FOREIGN KEY ([OrderId])
    REFERENCES [dbo].[Orders]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderProducts_ToOrders'
CREATE INDEX [IX_FK_OrderProducts_ToOrders]
ON [dbo].[OrderProducts]
    ([OrderId]);
GO

-- Creating foreign key on [LocationId] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_Orders_ToLocations]
    FOREIGN KEY ([LocationId])
    REFERENCES [dbo].[Locations]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Orders_ToLocations'
CREATE INDEX [IX_FK_Orders_ToLocations]
ON [dbo].[Orders]
    ([LocationId]);
GO

-- Creating foreign key on [PrimaryContactId] in table 'Locations'
ALTER TABLE [dbo].[Locations]
ADD CONSTRAINT [FK_Locations_ToGrowers]
    FOREIGN KEY ([PrimaryContactId])
    REFERENCES [dbo].[Growers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Locations_ToGrowers'
CREATE INDEX [IX_FK_Locations_ToGrowers]
ON [dbo].[Locations]
    ([PrimaryContactId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------