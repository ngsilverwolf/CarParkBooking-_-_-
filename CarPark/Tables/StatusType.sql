CREATE TABLE [dbo].[StatusType]
(
	[StatusTypeId]       int NOT NULL               CONSTRAINT PK_StatusType PRIMARY KEY IDENTITY, 
    [Description]        nvarchar(50) NOT NULL ,              
    [DateCreatedUtc]     datetime NOT NULL ,    
);