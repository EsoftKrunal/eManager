using System;
using System.Data;
using System.Configuration;
using System.Web;

/// <summary>
/// Summary description for MyParameter
/// </summary>
public class MyParameter
{
    public string Name;
    public object Value;
    public MyParameter(string _Name, object _Value)
    {
        this.Name = _Name;
        if (_Value ==null )
            this.Value = DBNull.Value;
        else
            this.Value = _Value;
    }
}
