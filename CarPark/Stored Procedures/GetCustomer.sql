CREATE PROCEDURE [dbo].[GetCustomer]
    @CustomerId int
AS
BEGIN
    SELECT
        [CustomerId]
        ,[FirstName]
        ,[LastName]
  FROM [CarPark].[dbo].[Customer]
  WHERE
    CustomerId = @CustomerId
END
