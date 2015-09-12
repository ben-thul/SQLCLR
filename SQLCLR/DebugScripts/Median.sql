WITH cte AS (
    SELECT * FROM (VALUES
        (1, 1),
        (1, 2),
        (1, 3),
        (2, 4)
    ) AS x(i, a)
)
SELECT i, dbo.Median(a)
FROM cte
GROUP BY GROUPING SETS ((), (i))