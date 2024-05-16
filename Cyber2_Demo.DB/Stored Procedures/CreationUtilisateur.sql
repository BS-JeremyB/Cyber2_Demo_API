CREATE PROCEDURE [dbo].[CreationUtilisateur]
	@Username NVARCHAR(50),
	@Nom NVARCHAR(50),
	@Prenom NVARCHAR(50),
	@Email NVARCHAR(100),
	@Password VARCHAR(30)

AS
BEGIN

	DECLARE @Salt VARCHAR(60)
	DECLARE @HashedPassword VARBINARY(64)
	DECLARE @Pepper NVARCHAR(100)

	
	SET @Pepper = [dbo].GetPepper()
	SET @Salt = CONVERT(VARCHAR(60),NEWID())
	SET @HashedPassword = HASHBYTES('SHA2_512', CONCAT(@Password, @Salt, @Pepper))

	INSERT INTO [Utilisateur] (Username, Email, Nom, Prenom, Password, Salt)
	OUTPUT inserted.Id
	VALUES (@Username, @Email, @Nom, @Prenom, @HashedPassword, @Salt)

END