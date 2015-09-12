using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.IO;


[Serializable]
[SqlUserDefinedType(
    Format.UserDefined,
    MaxByteSize = sizeof(long) + sizeof(bool),
    IsFixedLength = true,
    IsByteOrdered = true
)]
public class Duration: INullable, IBinarySerialize, IComparable
{
    private TimeSpan _ts;
    private bool _null;

    public Duration(TimeSpan ts)
    {
        _ts = ts;
        _null = false;
    }

    public Duration()
    {
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
        w.Write(_null);
        w.Write(_ts.Ticks);
    }

    public void Read(BinaryReader r)
    {
        _null = r.ReadBoolean();
        _ts = new TimeSpan(r.ReadInt64());
    }

    public int CompareTo(object obj)
    {
        Duration otherDuration = (Duration)obj;
        return _ts.CompareTo(otherDuration._ts);
    }
}