using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Collections.Generic;
using System.IO;


/*******************************
    Warning: This is intentionally a bad example
*******************************/

[Serializable]
[SqlUserDefinedAggregate(
    Format.UserDefined,
    MaxByteSize = -1,
    IsInvariantToDuplicates = false,
    IsInvariantToOrder = true,
    IsInvariantToNulls = true,
    Name = "DumbSum"
)]
public struct DumbSum : IBinarySerialize
{
    private List<double> _list;
    public void Init()
    {
        _list = new List<double>();
    }

    public void Accumulate(SqlDouble Value)
    {
        if (Value.IsNull == false)
        {
            _list.Add(Value.Value);
        }
    }

    public void Merge (DumbSum Group)
    {
        _list.AddRange(Group._list);
    }

    public SqlDouble Terminate ()
    {
        double sum = 0;
        foreach (double v in _list)
        {
            sum += v;
        }

        return new SqlDouble(sum);
    }

    public void Write(BinaryWriter w)
    {
        w.Write(_list.Count);
        foreach(double v in _list)
        {
            w.Write(v);
        }
    }

    public void Read(BinaryReader r)
    {
        int count = r.ReadInt32();
        _list = new List<double>();
        for (int i = 0; i < count; i++)
        {
            double value = r.ReadDouble();
            _list.Add(value);
        }
    }
}
