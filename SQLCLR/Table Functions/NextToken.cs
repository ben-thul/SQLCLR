using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class UserDefinedFunctions
{

    public static void NextToken(object objToken, out SqlString token)
    {
        String t = (String)objToken;
        token = (SqlString)t;
    }
}