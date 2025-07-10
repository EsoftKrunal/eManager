<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JobTrackingHistory.aspx.cs" Inherits="Docket_JobTrackingHistory" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
 <link href="../CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/tabs.css" rel="stylesheet" type="text/css" />
    <title>DryDock : Job Tracking </title>
    <script src="../JS/JQuery.js" type="text/javascript"></script>
    <script src="../JS/Common.js" type="text/javascript"></script>
    <script src="../JS/JQScript.js" type="text/javascript"></script>
    <style type="text/css">
    .tdleft
    {
        height:20px;
        vertical-align:middle;
        padding-left:5px;
        font-size:11px;
    }
    .tdDatacell
    {
         width:50px;
         height:20px;
         text-align:center;
         vertical-align:middle;
         font-size:11px;
    }
    .tdDataHeadercell
    {
        width:50px;
        height:23px;
        vertical-align:middle;
        text-align:center;
        font-size:11px;
    }
    
    .tdDatacellP
    {
         width:50px;
         height:20px;
         text-align:center;
         background-color:#E6E600;
         vertical-align:middle;
         font-size:11px;
    }
    
    </style>
</head>
<body style="font-family:calibri; font-size:14px;">
    <form id="form1" runat="server">
    <div>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div style="background-color:#1947A3;color:White; text-align:center; padding:3px; font-size:15px; "><b>JOB TRACKING HISTORY</b></div>
    <div>
    <table style="width:100%;border-collapse:collapse;" cellpadding="0" cellspacing="0" border="1">
        <tr>
            <td>
                <div style="border:none; background-color : #ADD6FF; padding:5px; font-size:13px; ">
                <table style="width :100%;">
                <tr>
                <td style="text-align:right; width:">Docket # :</td>
                <td style="text-align:left"><asp:Label runat="server" ID="lblDocketNo"></asp:Label>
                </td>
                <td style="text-align:right">Vessel :</td>
                <td style="text-align:left"><asp:Label runat="server" ID="lblVessel"></asp:Label></td>
                <td style="text-align:right">Type :</td>
                <td style="text-align:left"><asp:Label runat="server" ID="lblType"></asp:Label></td>
                <td style="text-align:right">DD Duration :</td>
                <td style="text-align:left"><asp:Label runat="server" ID="lblPlanDuration"></asp:Label>&nbsp;</td>
                </tr>
                </table>
                <table style="width :100%;">
                <tr>
                    <td style="text-align:right; width:150px;">Job Category : </td>
                    <td> <asp:Label runat="server" ID="lblJobCatName"></asp:Label> </td>
                </tr>
                <tr>
                    <td style="text-align:right">Job Name : </td>
                    <td> <asp:Label runat="server" ID="lblJobName"></asp:Label> </td>
                </tr>
                </table>
                </div>
            </td>
        </tr>
        <tr>
        <td>
        <asp:Repeater runat="server" ID="RPT1">
        <ItemTemplate>
            <div style=" border-bottom:solid 1px #c2c2c2; display:table; width:100%;">
            <div style="padding:3px; display:table-cell; width:90px; text-align:center; color:#444444; float:left;"><b><%#Common.ToDateString(Eval("For_Date"))%></b> |</div> 
            <%--<div style="padding:3px; display:table-cell; width:50px; text-align:right; color:Purple;float:left; "><%#Eval("Per")%>% | </div>--%>
            <div style="padding:3px; display:table-cell;font-style: italic; color:#3399FF; float:left;"><%#Eval("Remark")%></div>
            </div>
        </ItemTemplate>
        </asp:Repeater>
        
        </td>
        </tr>

       <%-- <tr>        
            <td>
                <table style="width:100%;border-collapse:collapse;" cellpadding="0" cellspacing="0" border="1">
                <tr>
                <td style="width:100px; height:25px; vertical-align:middle; background-color:#c2c2c2; font-size:12px;">
                &nbsp;Date Updated
                </td>
                <td style="height:25px; vertical-align:middle; font-size:12px;">
                   <div style="width:1200px; height:25px; overflow-x:hidden;overflow-y:scroll;border-right:solid 1px grey;" id="dv_dataheader">
                    <asp:Literal runat="server" ID="lit_Dates"></asp:Literal>
                   </div>
                </td>
                </tr>
                <tr>
                <td>
                <div>
                    <div style="height:500px; overflow:hidden" id="dv_header">
                    <asp:Literal runat="server" ID="litHead"></asp:Literal>
                    </div>
                </div>
                </td>
                <td>
                <div>
                    <div style="width:1200px; height:500px; overflow:scroll; border-right:solid 1px grey;" id="dv_data" onscroll="Data_Scroll();">
                    <asp:Literal runat="server" ID="litData"></asp:Literal>
                    </div>
                </div>
                </td>
                </tr>
                </table>
            </td> 
        </tr>--%>
    </table>
    </div>
    </div>
    <script type="text/javascript">
        function Data_Scroll() {
            $("#dv_header").scrollTop($("#dv_data").scrollTop());
            $("#dv_dataheader").scrollLeft($("#dv_data").scrollLeft());
        }

        $(document).ready(function () {

            var docWidth = document.documentElement.clientWidth || document.body.clientWidth;
            $("#dv_dataheader").width(docWidth - 100 + 'px');
            $("#dv_data").width(docWidth - 100 + 'px');
        });
    </script>
    </form>
</body>
</html>
