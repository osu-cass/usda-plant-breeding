CREATE TABLE [dbo].[MapComponentYears] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [Comments]       NVARCHAR (MAX) NULL,
    [MapComponentId] INT            NOT NULL,
    [YearsId]        INT            NOT NULL,
    CONSTRAINT [PK_MapComponentYears] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MapComponentMapComponentYears] FOREIGN KEY ([MapComponentId]) REFERENCES [dbo].[MapComponents] ([Id]),
    CONSTRAINT [FK_YearsMapComponentYears] FOREIGN KEY ([YearsId]) REFERENCES [dbo].[Years] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_YearsMapComponentYears]
    ON [dbo].[MapComponentYears]([YearsId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_MapComponentMapComponentYears]
    ON [dbo].[MapComponentYears]([MapComponentId] ASC);

