using System.Data.SqlTypes;
using System.Globalization;

public partial class UserDefinedFunctions
{
    [Microsoft.SqlServer.Server.SqlFunction]
    public static SqlString FormatCurrency(SqlDouble Amount, SqlString culture)
    {
        string _culture;
        if (culture.IsNull == true)
        {
            _culture = CultureInfo.CurrentCulture.Name;
        }
        else
        {
            _culture = culture.Value;
        }
        try {
            CultureInfo ci = new CultureInfo(_culture);
            return new SqlString(Amount.Value.ToString("C", ci));
        }
        catch (CultureNotFoundException e)
        {
            return new SqlString("Invalid culture");
        }
    }
}
