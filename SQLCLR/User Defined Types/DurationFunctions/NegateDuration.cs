using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class UserDefinedFunctions
{
    [SqlFunction(IsDeterministic = true, IsPrecise = true)]
    public static Duration NegateDuration(Duration d)
    {
        return new Duration(d.timeSpan.Negate());
    }
}
