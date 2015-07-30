using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Collections;

public partial class UserDefinedFunctions
{
    [Microsoft.SqlServer.Server.SqlFunction(
        DataAccess=DataAccessKind.None,
        Name="StringSplit",
        SystemDataAccess=SystemDataAccessKind.None,
        TableDefinition="token nvarchar(max)",
        FillRowMethodName="NextToken"
    )]

    public static IEnumerable StringSplit(SqlString toBeSplit, SqlString delimiter)
    {
        String d = delimiter.Value;
        if (d.Length > 1)
        {
            throw new Exception("Delimiter longer than one character");
        }
        char[] delimiters = d.ToCharArray();
        ArrayList tokens = new ArrayList();
        foreach (String t in toBeSplit.Value.Split(delimiters)) 
        {
            tokens.Add(t);
        }
        return tokens;
    }
}
