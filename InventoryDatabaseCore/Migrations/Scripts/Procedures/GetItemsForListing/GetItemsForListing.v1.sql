CREATE OR ALTER PROCEDURE dbo.GetItemsForListing
@minDate DATETIME = null,
@maxDate DATETIME = null
AS
BEGIN
SET NOCOUNT ON;
SELECT item.Name, item.Description, item.Notes
, item.IsActive, item.IsDeleted, cat.Name
FROM dbo.Items item
LEFT JOIN dbo.Categories cat on item.CategoryId = cat.Id
WHERE(@minDate IS NULL OR item.CreatedDate >= @minDate)
AND(@maxDate IS NULL OR item.CreatedDate <= @maxDate)
END