CREATE TABLE [dbo].[MapComponentYearsFate] (
    [MapComponentYears_Id] INT NOT NULL,
    [Fates_Id]             INT NOT NULL,
    CONSTRAINT [PK_MapComponentYearsFate] PRIMARY KEY CLUSTERED ([MapComponentYears_Id] ASC, [Fates_Id] ASC),
    CONSTRAINT [FK_MapComponentYearsFate_Fate] FOREIGN KEY ([Fates_Id]) REFERENCES [dbo].[Fates] ([Id]),
    CONSTRAINT [FK_MapComponentYearsFate_MapComponentYears] FOREIGN KEY ([MapComponentYears_Id]) REFERENCES [dbo].[MapComponentYears] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_MapComponentYearsFate_Fate]
    ON [dbo].[MapComponentYearsFate]([Fates_Id] ASC);

