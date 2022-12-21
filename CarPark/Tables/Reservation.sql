CREATE TABLE [dbo].[Reservation]
(
	[ReservationId]               int NOT NULL              CONSTRAINT PK_Reservation PRIMARY KEY IDENTITY, 
    [ParkingSpaceId]              int NOT NULL              CONSTRAINT FK_Reservation_ParkingSpace FOREIGN KEY REFERENCES ParkingSpace(ParkingSpaceId),
    [CustomerId]                  int NOT NULL              CONSTRAINT FK_Reservation_Customer FOREIGN KEY REFERENCES Customer(CustomerId),
    [DateFromUtc]                 datetime NOT NULL ,              
    [DateToUtc]                   datetime NOT NULL ,       
    [Status]                      nvarchar(50) NOT NULL ,
    [Cost]                        numeric(8,2) NOT NULL ,
);