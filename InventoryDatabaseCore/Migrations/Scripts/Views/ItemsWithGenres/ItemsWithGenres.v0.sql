CREATE OR ALTER VIEW ItemsWithGenres AS
SELECT i.Id, i.[Name], i.[Description], i.IsActive, i.IsDeleted
, g.Id GenreId, g.[Name] Genre, g.IsActive [GenreIsActive],
g.IsDeleted [GenreIsDeleted]
FROM items i
LEFT JOIN ItemGenre ig on i.Id = ig.ItemId
LEFT JOIN Genres g on ig.GenreId = g.Id