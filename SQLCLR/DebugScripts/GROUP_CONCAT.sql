with cte as (
	select * from (values 
		(1, 'a'),
		(1, 'b'),
		(2, 'c')
	) as x(i, a)
)
select dbo.GROUP_CONCAT(a, ',') OVER (PARTITION BY i),
    dbo.GROUP_CONCAT(a, '') OVER (PARTITION BY i)
from cte