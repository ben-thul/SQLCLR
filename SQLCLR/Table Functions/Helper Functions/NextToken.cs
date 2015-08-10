using System;
using System.Data.SqlTypes;

public partial class UserDefinedFunctions
{

    public static void NextToken(object objToken, out SqlString token)
    {
        String t = (String)objToken;
        token = (SqlString)t;
    }
}