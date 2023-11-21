CREATE TABLE [dbo].[UserAccounts]
(
	[UserName] NVARCHAR(200) NOT NULL PRIMARY KEY,
	[PasswordHash] NVARCHAR(200),
	[Email] NVARCHAR(200),
)
