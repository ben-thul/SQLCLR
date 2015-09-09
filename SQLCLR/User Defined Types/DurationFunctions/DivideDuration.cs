using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class UserDefinedFunctions
{
    [SqlFunction(IsDeterministic = true, IsPrecise = false)]
    public static Duration DivideDuration(Duration d, SqlDouble m)
    {
        long ticks = d.timeSpan.Ticks;
        ticks = (long)(ticks * m.Value);
        return new Duration(TimeSpan.FromTicks(ticks));
    }
}
