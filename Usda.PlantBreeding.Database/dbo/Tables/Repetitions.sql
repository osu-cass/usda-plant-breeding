CREATE TABLE [dbo].[Repetitions] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [MapComponentId] INT            NOT NULL,
    [Note]           NVARCHAR (MAX) NOT NULL,
    [RepNumber]      INT            NOT NULL,
    [RBD]            BIT            NOT NULL,
    [RSV]            BIT            NOT NULL,
    [TSV]            BIT            NOT NULL,
    CONSTRAINT [PK_Repetitions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MapComponentRepetition] FOREIGN KEY ([MapComponentId]) REFERENCES [dbo].[MapComponents] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_FK_MapComponentRepetition]
    ON [dbo].[Repetitions]([MapComponentId] ASC);

