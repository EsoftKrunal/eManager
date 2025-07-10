using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Diagnostics.Contracts;

/// <summary>
/// Summary description for Opex
/// </summary>
public class Opex
{
    public Opex()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static int GetNodays(string selectedyear, int Selectedmonth)
    {
        int MonthDays = 0;
        int lengthCount = selectedyear.Length;
        int year = 0;

        if (lengthCount > 4)
        {
            year = Convert.ToInt32(selectedyear.Substring(0, 4));
            if (Selectedmonth >= 1 && Selectedmonth <= 3)
            {
                year = year + 1;
                MonthDays = DateTime.DaysInMonth(year, Selectedmonth);
            }
            else
            {
                MonthDays = DateTime.DaysInMonth(year, Selectedmonth);
            }
        }
        else
        {
            year = Convert.ToInt32(selectedyear.Substring(0, 4));
            MonthDays = DateTime.DaysInMonth(year, Selectedmonth);
        }

        return MonthDays;
    }

    public static DataTable GetYearofDaysfromBudget(int month, string year, string cocode, string vesselcode)
    {
        string procedurename = "GetNoofDaysBudget";
        DataTable dt = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@glPer", DbType.Int32, month);
        objDatabase.AddInParameter(objDbCommand, "@glYear", DbType.String, year);
        objDatabase.AddInParameter(objDbCommand, "@Cocode", DbType.String, cocode);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.String, vesselcode);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }

        return dt;
    }

    public static int GetUSFinMonth(int month)
    {
        int UsFinMonth = 0;
        if (month >=4 && month <= 12)
        {
            UsFinMonth = month - 3;
        }
        else
        {
            UsFinMonth = month + 9;
        }
        return UsFinMonth;
    }
}