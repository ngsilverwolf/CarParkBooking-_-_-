CREATE TABLE [dbo].[Cost]
(
	[CostId]                      int NOT NULL               CONSTRAINT PK_Cost PRIMARY KEY IDENTITY, 
    [DateFromUtc]                 datetime NOT NULL ,              
    [DateToUtc]                   datetime NOT NULL ,       
    [PricePerDay]                 numeric(8,2) NOT NULL ,
);