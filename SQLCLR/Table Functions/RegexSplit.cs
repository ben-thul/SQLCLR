using Microsoft.SqlServer.Server;
using System;
using System.Collections;
using System.Data.SqlTypes;
using System.Text.RegularExpressions;

public partial class UserDefinedFunctions
{
    [SqlFunction(
            DataAccess=DataAccessKind.None,
            SystemDataAccess=SystemDataAccessKind.None,
            FillRowMethodName="NextToken",
            TableDefinition="token nvarchar(max)"
        )
    ]
    public static IEnumerable RegexSplit(SqlString toBesplit, SqlString regex)
    {
        try
        {
            Regex r = new Regex(regex.Value);
        }
        catch
        {
            string message = "'" + regex.Value + "' does not appear to be a valid regex.";
            throw new Exception(message);
        }
        foreach (string t in Regex.Split(toBesplit.Value, regex.Value))
        {
            yield return t;
        }
    }
}
