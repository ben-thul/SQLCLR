declare @a ComplexNumber = '(2, 1)', @b ComplexNumber = '(-1, -3)';

select @a.Multiply(@b).ToString(),
	@a.[Add](@b).ToString(),
	@a.Subtract(@b).ToString(),
	@a.[Real],
	@a.Imaginary
