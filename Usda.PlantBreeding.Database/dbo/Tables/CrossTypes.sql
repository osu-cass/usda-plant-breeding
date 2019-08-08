CREATE TABLE [dbo].[CrossTypes] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (MAX) NULL,
    [Value]      NVARCHAR (MAX) NULL,
    [Retired]    BIT            NOT NULL,
    [GenusId]    INT            NOT NULL,
    [ExternalId] INT            NULL,
    CONSTRAINT [PK_CrossTypes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CrossTypeGenus] FOREIGN KEY ([GenusId]) REFERENCES [dbo].[Genus] ([Id])
);














GO
CREATE NONCLUSTERED INDEX [IX_FK_CrossTypeGenus]
    ON [dbo].[CrossTypes]([GenusId] ASC);

