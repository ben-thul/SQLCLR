using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.IO;


[Serializable]
[SqlUserDefinedType(Format.UserDefined,
    MaxByteSize = sizeof(long),
    IsFixedLength = true,
    IsByteOrdered = true
)]
public struct Duration: INullable, IBinarySerialize
{
    private TimeSpan _ts;
    private bool _null;

    public Duration(TimeSpan ts)
    {
        _ts = ts;
        _null = false;
    }

    public override string ToString()
    {
        return _ts.ToString();
    }
    
    public bool IsNull
    {
        get
        {
            return _null;
        }
    }
    
    public static Duration Null
    {
        get
        {
            Duration h = new Duration();
            h._null = true;
            return h;
        }
    }
    
    public static Duration Parse(SqlString s)
    {
        if (s.IsNull)
            return Null;
        Duration u = new Duration();
        u._ts = TimeSpan.Parse(s.Value);
        return u;
    }

    public TimeSpan timeSpan
    {
        get
        {
            return _ts;
        }
    }
        
    public void Write(BinaryWriter w)
    {
        w.Write(_ts.Ticks);
    }

    public void Read(BinaryReader r)
    {
        _ts = new TimeSpan(r.ReadInt64());
    }
}