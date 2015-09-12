SET STATISTICS IO, TIME ON;

SELECT dbo.[NaiveSum](n)
FROM Util.dbo.[Numbers] AS [n]
WHERE n < 1000000;

PRINT REPLICATE('-', 40);

SELECT dbo.[SmarterSum](n)
FROM Util.dbo.[Numbers] AS [n]
WHERE n < 1000000;

PRINT REPLICATE('-', 40);

SELECT SUM(CAST(n AS BIGINT))
FROM Util.dbo.[Numbers] AS [n]
WHERE n < 1000000
