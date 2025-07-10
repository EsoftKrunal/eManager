<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OfficeRunningHourPrint.aspx.cs" Inherits="OfficeRunningHourPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eMANAGER</title>
    <link href="CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="CSS/tabs.css" rel="stylesheet" type="text/css" />
    <link href="CSS/CalenderStyle.css" rel="Stylesheet" type="text/css" />
    <script src="JS/Common.js" type="text/javascript"></script>
    <script src="JS/Calender.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    
        <table width="100%">
            <tr>
                <td>
                    <center>
                    <div style="margin:auto;">
                            <b>From : </b><asp:TextBox ID="txtFrom"  runat="server" Width="80px" onfocus="showCalendar('',this,this,'','holder1',-100,22,1)" MaxLength="11" ></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;
                            <b>To :</b> <asp:TextBox ID="txtTo"  runat="server" Width="80px" onfocus="showCalendar('',this,this,'','holder1',-100,22,1)" MaxLength="11" ></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnShow" runat="server" OnClick="btnShow_OnClick"  Text="Show Report"/>
                    </div>  
                    </center>
                    <br />  
                </td>
            </tr>
            <tr>
                <td>
                    <iframe id="iframReport" runat="server" frameborder="1" style="width: 100%; height:1250px; overflow:auto"></iframe>
                </td>
            </tr>
        </table>
    
    </form>
</body>
</html>
