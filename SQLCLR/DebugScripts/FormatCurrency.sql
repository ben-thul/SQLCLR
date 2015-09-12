WITH cte AS (
    SELECT * FROM (VALUES
        (1.235),
        (2.00)
    ) AS x(a)
)
SELECT a, 
    dbo.[FormatCurrency](a, 'en-US'),
    dbo.[FormatCurrency](a, 'ja-JP'),
    dbo.[FormatCurrency](a, 'garbage')
FROM cte