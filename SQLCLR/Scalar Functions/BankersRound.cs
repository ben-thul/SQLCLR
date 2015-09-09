using System;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class UserDefinedFunctions
{
    [SqlFunction(DataAccess=DataAccessKind.None)]
    public static SqlDouble BankersRound(SqlDouble value, SqlInt16 places)
    {
        Double myValue = value.Value;

        return Math.Round(myValue, places.Value, MidpointRounding.ToEven);
    }
}
