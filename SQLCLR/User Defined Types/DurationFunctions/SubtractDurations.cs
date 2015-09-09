using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class UserDefinedFunctions
{
    [SqlFunction(IsDeterministic = true, IsPrecise = false)]
    public static Duration SubtractDurations(Duration a, Duration b)
    {
        return new Duration(a.timeSpan.Subtract(b.timeSpan));
    }
}
