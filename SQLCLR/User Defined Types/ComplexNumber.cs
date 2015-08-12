using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Numerics;
using System.Text.RegularExpressions;
using System.IO;


[Serializable]
[SqlUserDefinedType(
    Format.UserDefined,
    MaxByteSize = 2 * sizeof(double) + sizeof(bool)
)]
public struct ComplexNumber: INullable, IBinarySerialize
{
    private Complex _complex;
    private bool _null;

    public ComplexNumber(Complex c)
    {
        _complex = c;
        _null = false;
    }

    public override string ToString()
    {
        // Replace with your own code
        return _complex.ToString();
    }
    
    public bool IsNull
    {
        get
        {
            // Put your code here
            return _null;
        }
    }
    
    public static ComplexNumber Null
    {
        get
        {
            ComplexNumber h = new ComplexNumber();
            h._null = true;
            return h;
        }
    }
    
    public static ComplexNumber Parse(SqlString s)
    {
        if (s.IsNull)
            return Null;
        ComplexNumber cn = new ComplexNumber();
        Regex r = new Regex(
            @"^\(([0-9+-.]+), ([0-9+-.]+)\)$"
        );
        MatchCollection matches = r.Matches(s.Value);
        GroupCollection groups = matches[0].Groups;
        double real = double.Parse(groups[1].Value);
        double imaginary = double.Parse(groups[2].Value);
        cn._complex = new Complex(real, imaginary);
        return cn;
    }
    
    // This is a place-holder method
    public ComplexNumber Add(ComplexNumber other)
    {
        return new ComplexNumber(Complex.Add(_complex, other._complex));
    }

    public ComplexNumber Subract(ComplexNumber other)
    {
        return new ComplexNumber(Complex.Subtract(_complex, other._complex));
    }

    public ComplexNumber Multiply(ComplexNumber other)
    {
        return new ComplexNumber(Complex.Multiply(_complex, other._complex));
    }

    public ComplexNumber Divide(ComplexNumber other)
    {
        return new ComplexNumber(Complex.Divide(_complex, other._complex));
    }

    public SqlDouble Real
    {
        get
        {
            return new SqlDouble(_complex.Real);
        }
    }

    public SqlDouble Imaginary
    {
        get
        {
            return new SqlDouble(_complex.Imaginary);
        }
    }

    public SqlDouble Magnitude
    {
        get
        {
            return new SqlDouble(_complex.Magnitude);
        }
    }

    public void Read(BinaryReader r)
    {
        _null = r.ReadBoolean();
        double real = r.ReadDouble();
        double imaginary = r.ReadDouble();
        _complex = new Complex(real, imaginary);
    }

    public void Write(BinaryWriter w)
    {
        w.Write(_null);
        w.Write(_complex.Real);
        w.Write(_complex.Imaginary);
    }
}