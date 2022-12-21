CREATE TABLE [dbo].[ParkingSpace]
(
	[ParkingSpaceId]                  int NOT NULL               CONSTRAINT PK_ParkingSpace PRIMARY KEY IDENTITY, 
    [Name]					          nvarchar(50) NOT NULL 
);