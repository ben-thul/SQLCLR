WITH cte AS (
    SELECT * FROM (VALUES
        (2.15),
        (2.25)
    ) AS x(a)
)
SELECT a, dbo.[BankersRound](a, 1), round(a, 1)
FROM cte;