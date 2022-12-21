CREATE PROCEDURE [dbo].[GetParkingCosts]
    @DateFromUtc datetime,
    @DateToUtc datetime
AS
BEGIN
  SELECT
        [CostId],
        [DateFromUtc],
        [DateToUtc],
        [PricePerDay]
  FROM [CarPark].[dbo].[Cost]
  WHERE 
        DateFromUtc >= @DateFromUtc AND DateFromUtc <= @DateToUtc
    
END
