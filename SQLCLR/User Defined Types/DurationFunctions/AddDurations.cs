using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class UserDefinedFunctions
{
    [SqlFunction(IsDeterministic = true, IsPrecise = true)]
    public static Duration AddDurations(Duration a, Duration b)
    {
        if (a.IsNull || b.IsNull)
        {
            return Duration.Null;
        }
        else
        {
            return new Duration(a.timeSpan + b.timeSpan);
        }
    }
}
