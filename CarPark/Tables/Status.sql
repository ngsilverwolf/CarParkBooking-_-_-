CREATE TABLE [dbo].[Status]
(  
	[StatusId]         int NOT NULL               CONSTRAINT PK_Status PRIMARY KEY IDENTITY, 
    [StatusTypeId]     int NOT NULL               CONSTRAINT FK_Status_StatusType FOREIGN KEY REFERENCES StatusType(StatusTypeId),          
    [DateCreatedUtc]   datetime NOT NULL
);