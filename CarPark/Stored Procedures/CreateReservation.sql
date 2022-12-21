CREATE PROCEDURE [dbo].[CreateReservation]
    @ParkingSpaceId int,
    @CustomerId int,
    @DateFromUtc datetime,
    @DateToUtc datetime,
    @Status nvarchar(100),
    @Cost numeric
AS
BEGIN

IF EXISTS (SELECT  ReservationId 
           FROM [CarPark].[dbo].[Reservation]
           WHERE
                ParkingSpaceId = @ParkingSpaceId AND
                DateFromUtc = @DateFromUtc AND
                DateToUtc = @DateToUtc)

    RAISERROR('Reservation ALready Exists', 16, 1)
Else

	INSERT INTO [CarPark].[dbo].[Reservation] ([ParkingSpaceId],[CustomerId],[DateFromUtc],[DateToUtc],[Status],[Cost])
	OUTPUT INSERTED.ReservationId 
	VALUES(@ParkingSpaceId,@CustomerId,@DateFromUtc, @DateToUtc,@Status,@Cost)
END
