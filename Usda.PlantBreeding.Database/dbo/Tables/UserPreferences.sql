CREATE TABLE [dbo].[UserPreferences] (
    [UserId]  VARCHAR(50) NOT NULL,
    [GenusId] INT           NOT NULL,
    CONSTRAINT [PK_UserPreferences] PRIMARY KEY CLUSTERED ([UserId] ASC),
    CONSTRAINT [FK_UserPreferenceGenus] FOREIGN KEY ([GenusId]) REFERENCES [dbo].[Genus] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_UserPreferenceGenus]
    ON [dbo].[UserPreferences]([GenusId] ASC);


GO
