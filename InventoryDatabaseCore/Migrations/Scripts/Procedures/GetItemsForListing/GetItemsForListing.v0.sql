CREATE OR ALTER PROCEDURE dbo.GetItemsForListing
                @minDate DATETIME = null,
                @maxDate DATETIME = null
                AS
                BEGIN
                SET NOCOUNT ON;
                SELECT item.Name, item.Description, item.Notes
                , item.IsActive, item.IsDeleted, g.Name, cat.Name
                FROM dbo.Items item
                LEFT JOIN dbo.ItemGenre ig on item.Id = ig.ItemId
                LEFT JOIN dbo.Genres g on ig.GenreId = g.Id
                LEFT JOIN dbo.Categories cat on item.CategoryId = cat.Id
                WHERE(@minDate IS NULL OR item.CreatedDate >= @minDate)
                AND(@maxDate IS NULL OR item.CreatedDate <= @maxDate)
                AND(@maxDate IS NULL OR item.CreatedDate <= @maxDate)
                END