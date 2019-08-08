CREATE TABLE [dbo].[MapQuestion] (
    [Maps_Id]      INT NOT NULL,
    [Questions_Id] INT NOT NULL,
    CONSTRAINT [PK_MapQuestion] PRIMARY KEY CLUSTERED ([Maps_Id] ASC, [Questions_Id] ASC),
    CONSTRAINT [FK_MapQuestion_Map] FOREIGN KEY ([Maps_Id]) REFERENCES [dbo].[Maps] ([Id]),
    CONSTRAINT [FK_MapQuestion_Question] FOREIGN KEY ([Questions_Id]) REFERENCES [dbo].[Questions] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_MapQuestion_Question]
    ON [dbo].[MapQuestion]([Questions_Id] ASC);

