using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class UserDefinedFunctions
{
    [SqlFunction]
    public static Duration DurationFromDatetimes(SqlDateTime dt1, SqlDateTime dt2)
    {
        TimeSpan ts = dt2.Value - dt1.Value;
        return new Duration(ts);
    }
}
