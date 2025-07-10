using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for YearAndMonthLoader
/// </summary>
public class EoffYearAndMonthLoader
{
    static int _Year, _Month;

    public EoffYearAndMonthLoader()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static int SelectedYear
    {
        get
        {
            return Common.CastAsInt32(_Year);
        }
        set
        {
            _Year = value;
        }
    }
    public static int SelectedMonth
    {
        get
        {
            return Common.CastAsInt32(_Month);
        }
        set
        {
            _Month = value;

        }
    }
}
