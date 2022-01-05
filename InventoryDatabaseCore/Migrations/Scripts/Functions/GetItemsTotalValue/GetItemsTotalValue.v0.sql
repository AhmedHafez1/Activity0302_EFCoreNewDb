Create or alter function dbo.GetItemsTotalValue (@IsActive BIT = true)
RETURNS TABLE
AS
RETURN (
SELECT Id, [Name], [Description], Quantity, PurchasePrice, Quantity * PurchasePrice as TotalValue
From Items
Where IsActive = @IsActive
)
