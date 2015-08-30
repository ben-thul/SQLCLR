using System;
using System.Data.SqlTypes;

public partial class UserDefinedFunctions
{

    public static void NextToken(object objToken, out SqlString token)
    {
        token = (String)objToken;
    }
}