using Microsoft.SqlServer.Server;
using System;
using System.Collections;
using System.Data.SqlTypes;
using System.Text.RegularExpressions;

public partial class UserDefinedFunctions
{
    [Microsoft.SqlServer.Server.SqlFunction(
            DataAccess=DataAccessKind.None,
            SystemDataAccess=SystemDataAccessKind.None,
            FillRowMethodName="NextToken",
            TableDefinition="token nvarchar(max)"
        )
    ]
    public static IEnumerable RegexSplit(SqlString toBesplit, SqlString regex)
    {
        ArrayList tokens = new ArrayList();
        try
        {
            Regex r = new Regex(regex.Value);
        }
        catch
        {
            String message = "'" + regex.Value + "' does not appear to be a valid regex.";
            throw new Exception(message);
        }
        foreach (String t in Regex.Split(toBesplit.Value, regex.Value))
        {
            tokens.Add(t);
        }
        return tokens;
    }
}
