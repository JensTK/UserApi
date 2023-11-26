CREATE TABLE [dbo].[UserAccounts]
(
	[UserId] INTEGER IDENTITY,
	[UserName] NVARCHAR(200) NOT NULL UNIQUE,
	[Email] NVARCHAR(200), 
    CONSTRAINT [PK_UserAccounts] PRIMARY KEY ([UserId]),
)
