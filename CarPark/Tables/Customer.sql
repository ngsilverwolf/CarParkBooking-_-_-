CREATE TABLE [dbo].[Customer]
(
	[CustomerId]                int NOT NULL               CONSTRAINT PK_Customer PRIMARY KEY IDENTITY, 
    [FirstName]                 nvarchar(100) NOT NULL ,              
    [LastName]                  nvarchar(100) NOT NULL ,       
);  