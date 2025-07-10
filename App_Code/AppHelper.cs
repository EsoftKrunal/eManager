using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for AppHelper
/// </summary>
/// namespcae
public class AppHelper
    {
        public static DataTable FilterDataTable(DataTable SrcTable, string FilterCond)
        {
            DataTable Filtered_Table = new DataTable();
            Filtered_Table = SrcTable.Clone();

            DataRow[] dataRows = SrcTable.Select(FilterCond);
            foreach (DataRow dvRow in dataRows)
            {
                Filtered_Table.ImportRow(dvRow);
            }
            return Filtered_Table;

        }
        public static DataTable SortDataTable(DataTable SrcTable, string Sort_Expr)
        {
            DataTable Sorted_Table = new DataTable();
            Sorted_Table = SrcTable.Clone();



            DataView dv;
            dv = SrcTable.DefaultView;
            dv.Sort = Sort_Expr;

            foreach (DataRowView dvRow in dv)
            {
                Sorted_Table.ImportRow(dvRow.Row);
            }
            return Sorted_Table;

        }
        public static void ExporttoExcel(DataTable table)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Reports.xls");

            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            HttpContext.Current.Response.Write(style);
            //sets font
            HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");
            //sets the table border, cell spacing, border color, font of the text, background, foreground, font height
            HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
              "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
              "style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR>");
            //am getting my grid's column headers
            int columnscount = table.Columns.Count;

            for (int j = 0; j < columnscount; j++)
            {      //write in new column
                HttpContext.Current.Response.Write("<Td>");
                //Get column headers  and make it as bold in excel columns
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write(table.Columns[j].ColumnName.ToString());
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");
            }
            HttpContext.Current.Response.Write("</TR>");
            foreach (DataRow row in table.Rows)
            {//write in new row
                //row.Attributes.Add("class", "textmode");
                HttpContext.Current.Response.Write("<TR class='textmode'>");
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    HttpContext.Current.Response.Write("<Td>");

                    HttpContext.Current.Response.Write(row[i].ToString());
                    HttpContext.Current.Response.Write("</Td>");
                }

                HttpContext.Current.Response.Write("</TR>");
            }

            HttpContext.Current.Response.Write("</Table>");
            HttpContext.Current.Response.Write("</font>");
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }


    //public static Boolean DateFormatValidation(string strDate)
    //{
    //    Regex regexDt = new Regex("(^(((([1-9])|([0][1-9])|([1-2][0-9])|(30))\\-([A,a][P,p][R,r]|[J,j][U,u][N,n]|[S,s][E,e][P,p]|[N,n][O,o][V,v]))|((([1-9])|([0][1-9])|([1-2][0-9])|([3][0-1]))\\-([J,j][A,a][N,n]|[M,m][A,a][R,r]|[M,m][A,a][Y,y]|[J,j][U,u][L,l]|[A,a][U,u][G,g]|[O,o][C,c][T,t]|[D,d][E,e][C,c])))\\-[0-9]{4}$)|(^(([1-9])|([0][1-9])|([1][0-9])|([2][0-8]))\\-([F,f][E,e][B,b])\\-[0-9]{2}(([02468][1235679])|([13579][01345789]))$)|(^(([1-9])|([0][1-9])|([1][0-9])|([2][0-9]))\\-([F,f][E,e][B,b])\\-[0-9]{2}(([02468][048])|([13579][26]))$)");

    //    Match mtStartDt = Regex.Match(strDate, regexDt.ToString());
    //    if (mtStartDt.Success)
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //      return  false;
    //    }
    //}
}
 