CREATE PROCEDURE [dbo].[GetReservations]
    @ParkingSpaceId int
AS
BEGIN
    SELECT
        [ReservationId]
        ,[ParkingSpaceId]
        ,[CustomerId]
        ,[DateFromUtc]
        ,[DateToUtc]
        ,[Status]
        ,[Cost]
  FROM [CarPark].[dbo].[Reservation]
  WHERE
    ParkingSpaceId = @ParkingSpaceId
END
