using System;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Text.RegularExpressions;
using System.Collections;

public partial class UserDefinedFunctions
{
    [SqlFunction(
            DataAccess = DataAccessKind.None,
            SystemDataAccess = SystemDataAccessKind.None,
            FillRowMethodName = "NextMatch",
            TableDefinition = "n int, token nvarchar(max)"
    )]
    public static IEnumerable RegexCapture(SqlString toBeSplit, SqlString regex)
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
        Match m = r.Match(toBeSplit.Value);
        if (m.Success == true)
        {
            GroupCollection groups = m.Groups;
            if (groups.Count > 0)
            {
                for(int i = 0; i < groups.Count; i++)
                {
                    yield return new MyMatch(i, groups[i].Value);
                }
            }
        }
    }

    public static void NextMatch(object match, out SqlInt32 n, out SqlString token)
    {
        MyMatch m = (MyMatch)match;
        n = m.index;
        token = m.text;
    }
    private class MyMatch
    {
        public int index { get; set; }
        public string text { get; set; }
        public MyMatch(int index, string text)
        {
            this.index = index;
            this.text = text;
        }
    }
}

