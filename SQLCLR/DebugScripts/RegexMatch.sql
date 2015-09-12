select dbo.RegexMatch('^\d{5}$', '12343') as [ZipCodeMatch],
	dbo.RegexMatch('^\(?\d{3}\)?-\d{3}-\d{4}$', '650-867-5309') as [PhoneNumberMatch];