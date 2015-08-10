using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Collections.Generic;
using System.IO;

[Serializable]
[Microsoft.SqlServer.Server.SqlUserDefinedAggregate(
    Format.UserDefined,
    IsInvariantToDuplicates = false,
    IsInvariantToNulls = true,
    IsInvariantToOrder = true,
    IsNullIfEmpty = true,
    MaxByteSize = -1    
)]
public struct Median : IBinarySerialize
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

    public void Merge (Median Group)
    {
        _list.AddRange(Group._list);
    }

    public SqlDouble Terminate ()
    {
        _list.Sort();
        int size = _list.Count;
        if(size % 2 == 0) 
        {
            //An even number of elements were accumulated
            //so pick the two in the middle and return the mean

            int second = size / 2;
            int first = second - 1;

            return new SqlDouble((_list[first] + _list[second]) / 2);

        }
        else
        {
            //Return the middle element
            return new SqlDouble(_list[(size - 1) / 2]);
        }
    }

    public void Read(BinaryReader r)
    {
        int size = r.ReadInt32();
        _list = new List<double>();
        for (int i = 0; i < size; i++)
        {
            _list.Add(r.ReadDouble());
        }
    }

    public void Write(BinaryWriter w)
    {
        w.Write(_list.Count);
        foreach (double v in _list)
        {
            w.Write(v);
        }
    }
}
