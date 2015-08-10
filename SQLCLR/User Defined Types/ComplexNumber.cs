using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Numerics;


[Serializable]
[Microsoft.SqlServer.Server.SqlUserDefinedType(Format.Native)]
public struct ComplexNumber: INullable
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
        return string.Format("({0}, {1})", _real, _imaginary);
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
        ComplexNumber u = new ComplexNumber();
        // Put your code here
        return u;
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
        return new ComplexNumber(Complex.Divide(_complex, other._complex)));
    }
}