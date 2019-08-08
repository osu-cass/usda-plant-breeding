CREATE TABLE [dbo].[Growers]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FirstName] VARCHAR(50) NOT NULL, 
    [LastName] VARCHAR(50) NOT NULL, 
    [Email] VARCHAR(320) NULL, 
    [WorkPhone] CHAR(10) NULL, 
    [MailingName] VARCHAR(500) NULL, 
    [MobilePhone] CHAR(10) NULL, 
    [CreatedDate] DATETIME NULL
)
