﻿CREATE TABLE [dbo].[Utilisateur]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Username] NVARCHAR(50) NOT NULL,
	[Nom] NVARCHAR(50),
	[Prenom] NVARCHAR(50),
	[Email] NVARCHAR(100) NOT NULL,
	[Password] VARBINARY(64) NOT NULL,
	[Salt] VARCHAR(60) NOT NULL,

	CONSTRAINT CK_Email CHECK ([Email] LIKE '%@%.%'),
	CONSTRAINT UK_Username UNIQUE ([Username]),
	CONSTRAINT UK_Email UNIQUE ([Email]),

)
