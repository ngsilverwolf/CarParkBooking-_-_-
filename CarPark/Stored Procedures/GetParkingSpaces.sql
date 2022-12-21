CREATE PROCEDURE [dbo].[GetParkingSpaces]

AS
BEGIN
    SELECT 
        [ParkingSpaceId]
        ,[Name]
    FROM 
        [CarPark].[dbo].[ParkingSpace]
END
