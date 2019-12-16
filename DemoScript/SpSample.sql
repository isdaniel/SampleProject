CREATE PROCEDURE dbo.Sample_Procedure 
	@UserName VARCHAR(100)
AS 
BEGIN 
	SELECT 
		[UserID],
		[UserName],
		[PassWord]
	FROM dbo.UserAccount
	WHERE [UserName] = @UserName
END