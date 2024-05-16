CREATE TABLE [dbo].[BlogPost]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Titre] NVARCHAR(50) NOT NULL,
	[Contenu] TEXT NOT NULL,
	[Utilisateur_Id] INT NOT NULL,

	CONSTRAINT FK_Blog_Post FOREIGN KEY ([Utilisateur_Id]) REFERENCES [Utilisateur]([Id]),





)
