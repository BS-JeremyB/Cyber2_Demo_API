CREATE PROCEDURE [dbo].[Login]
	@Username NVARCHAR(50),
	@Password NVARCHAR(30)
AS
BEGIN

	IF EXISTS( SELECT * FROM [Utilisateur] WHERE Username = @Username)
		BEGIN

			DECLARE @Pepper NVARCHAR(100)

			SET @Pepper = [dbo].GetPepper();

			SELECT *
			FROM [Utilisateur]
			WHERE Username = @Username AND Password = HASHBYTES('SHA2_512', CONCAT(@Password, Salt, @Pepper))
		END

END
