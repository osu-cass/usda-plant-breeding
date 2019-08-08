CREATE TABLE [dbo].[Years] (
    [Id]    INT            IDENTITY (1, 1) NOT NULL,
    [MapId] INT            NOT NULL,
    [Year]  NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Years] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MapYears] FOREIGN KEY ([MapId]) REFERENCES [dbo].[Maps] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_MapYears]
    ON [dbo].[Years]([MapId] ASC);

