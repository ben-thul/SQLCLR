using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

[Serializable]
[Microsoft.SqlServer.Server.SqlUserDefinedAggregate(
    Format.Native,
    IsInvariantToDuplicates = false,
    IsInvariantToNulls = true,
    IsInvariantToOrder = true,
    //MaxByteSize = -1,
    Name = "SmarterSum"
)]
public struct SmarterSum
{
    private double _sum;
    public void Init()
    {
        _sum = 0;
    }

    public void Accumulate(SqlDouble Value)
    {
        _sum += Value.Value;
    }

    public void Merge (SmarterSum Group)
    {
        _sum += Group._sum;
    }

    public SqlDouble Terminate ()
    {
        // Put your code here
        return new SqlDouble (_sum);
    }

    // This is a place-holder member field
    public int _var1;
}
