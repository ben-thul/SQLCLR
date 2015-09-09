using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Text.RegularExpressions;

public partial class UserDefinedFunctions
{
    [SqlFunction(
        IsDeterministic = true, 
        IsPrecise = true, 
        DataAccess = DataAccessKind.None
    )]
    public static SqlBoolean RegexMatch(SqlString regex, SqlString target)
    {
        Regex r;
        try
        {
            r = new Regex(regex.Value);
        }
        catch
        {
            string message = "'" + regex.Value + "' does not appear to be a valid regex.";
            throw new Exception(message);
        }
        if (r.IsMatch(target.Value) == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
