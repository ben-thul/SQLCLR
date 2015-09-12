declare @a Duration = '1:00:00', @b Duration = '1.5:30';

select @a.ToString(), @b.ToString(), @a, @b;
select dbo.AddDurations(@a, @b).ToString() as [Added],
	dbo.SubtractDurations(@b, @a).ToString() as [Subtracted],
	dbo.DivideDuration(@a, 2).ToString() as [Halved],
	dbo.MultiplyDuration(@b, 2).ToString() as [Doubled],
	dbo.NegateDuration('0:05:00.03').ToString() as [Negated],
	dbo.DurationFromDatetimes('2015-09-01', '2015-09-02 12:00').ToString() as [FromBoundaries];