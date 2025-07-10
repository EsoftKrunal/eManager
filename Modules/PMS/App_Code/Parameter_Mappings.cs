using System;
using System.Data;
using System.Configuration;
using System.Web;
/// <summary>
/// Summary description for Parameter_Mappings
/// </summary>
public class Parameter_Mappings
{
    public string SourceProcedureName;
    public string SourceParameterName;

    public string DestProcedureName;
    public int DestRow;
    public int DestCol;

    public Parameter_Mappings(string _SourceProcedureName, string _SourceParameterName, string _DestProcedureName, int _DestRow, int _DestCol)
	{
        this.SourceProcedureName = _SourceProcedureName;
        this.SourceParameterName = _SourceParameterName;

        this.DestProcedureName = _DestProcedureName;
        this.DestRow = _DestRow;
        this.DestCol = _DestCol;
  
	}
}
