using System;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Collections.Generic;
using System.Text;
using System.IO;

[Serializable]
[SqlUserDefinedAggregate(Format.UserDefined,
    IsInvariantToDuplicates = false,
    IsInvariantToOrder = false, 
    IsInvariantToNulls = false,
    IsNullIfEmpty = true,
    MaxByteSize = -1,
    Name = "GROUP_CONCAT"
)]
public class Concatenate : IBinarySerialize
{
    private List<string> _elements;
    private string _delimiter;

    public void Init()
    {
        _elements = new List<string>();
    }

    public void Accumulate(SqlString Value, SqlString delimiter)
    {
        _delimiter = delimiter.Value;
        if (Value.IsNull == false)
        {
            _elements.Add(Value.ToString());
        }
    }

    public void Merge (Concatenate Group)
    {
        _elements.AddRange(Group._elements);
    }

    public SqlString Terminate ()
    {
        StringBuilder sb = new StringBuilder();
        if (_elements.Count > 1)
        {
            for (int i = 0; i < _elements.Count - 1; i++)
            {
                sb.Append(_elements[i]);
                sb.Append(_delimiter);
            }
            sb.Append(_elements[_elements.Count - 1]);
        }
        else if (_elements.Count == 1)
        {
            sb.Append(_elements[0]);
        }
        else //this group has no elements
        {
            return SqlString.Null;
        }
        return new SqlString (sb.ToString());
    }

    public void Write(BinaryWriter w)
    {
        w.Write(_delimiter);
        w.Write(_elements.Count);
        foreach (string s in _elements)
        {
            w.Write(s);
        }
    }

    public void Read(BinaryReader r)
    {
        _delimiter = r.ReadString();
        int count = r.ReadInt32();
        _elements = new List<string>();
        for(int i=0; i < count; i++)
        {
            _elements.Add(r.ReadString());
        }
    }
}
