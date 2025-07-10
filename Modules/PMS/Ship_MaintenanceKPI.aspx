<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ship_MaintenanceKPI.aspx.cs" Inherits="Ship_MaintenanceKPI" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eMANAGER</title>
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <link href="CSS/tabs.css" rel="stylesheet" type="text/css" />
    <script src="JS/Common.js" type="text/javascript"></script>
    <link href="../../css/app_style.css" rel="Stylesheet" type="text/css" />
     <link href="../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <script src="JS/Common.js" type="text/javascript"></script>
    <script type="text/javascript" >
        function fncInputIntegerValuesOnly(evnt) {
            if (!(event.keyCode == 46 || event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {
                event.returnValue = false;
            }
        }
        function openprintwindow(VC, year) {
            window.open('Reports/MaintenanceKPI.aspx?VC=' + VC + '&Year=' + year, 'print', '', '');
        }

    </script>
    <style type="text/css">
    td
    {
        vertical-align:middle;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div>
        <div style="width: 99%; background-color: #d2d2d2; height: 20px; padding: 2px; text-align: center;font-weight: bold; font-size: 14px;">Maintenance KPI</div>
        <table style="background-color: #f9f9f9; border-collapse:collapse;" border="1" cellpadding="0" cellspacing="0" width="100%">
            <tr>
            <td>
                <table style="background-color: #f9f9f9; border-collapse:collapse;" border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                <td style="text-align: right; padding: 2px; width:50px;">
                    Year :
                </td>
                <td style="text-align: left; padding: 2px; width:100px;">
                    <asp:DropDownList ID="ddlYear" Width="90px" runat="server">
                    </asp:DropDownList>
                </td>
                <td style="padding: 2px; width:100px;">
                    <asp:Button ID="btnView" OnClick="btnView_Click" CssClass="btnorange" Text="Show"
                        runat="server" Width="100px" />
                </td>
                <td style="padding: 2px; text-align:left;">
                   <asp:Button ID="btnExport" OnClick="btnExport_Click" CssClass="btnorange" Text="Export" runat="server" Width="100px" />
                    <asp:Button ID="btnPrintKPI" OnClick="btnPrintKPI_Click" CssClass="btnorange" style="display:none;" Text="Print" runat="server" Width="100px" />
                </td>
                </tr>          
                </table>
             </td>      
            </tr>
            <tr>
                <td style="text-align: center">
                        <table border="1" cellpadding="2" cellspacing="0" rules="all" style="border-collapse: collapse;" width="100%">
                           
                            <tr align="center" style="background-color: #66C2FF; font-size: 11px; height: 30px;font-weight: bold;">
                                <td>
                                    Vessel
                                </td>
                                <td>
                                    Month
                                </td>
                                <td>
                                    System Jobs
                                </td>
                                <td>
                                    Due Jobs
                                </td>
                                <td>
                                    OverDue Jobs
                                </td>
                                <td>
                                    Total Jobs
                                </td>
                                <td>
                                    Outstanding Jobs <span style="font-weight: normal; font-size: 9px; color: Red;"><em>
                                        (Monthend Overdue Count)</em></span>
                                </td>
                                <td>
                                    Overdue 1 W
                                </td>
                                <td>
                                    Overdue 2 W
                                </td>
                                <td>
                                    Overdue>2 W
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <asp:Repeater ID="rptKPI" runat="server">
                                <ItemTemplate>
                                    <tr style="font-size: 11px; text-align: center;">
                                        <td align="center" style="height:30px">
                                            <%#Eval("Vesselcode") %>
                                        </td>
                                        <td align="center">
                                            <%#Eval("MonthName") %>
                                        </td>
                                        <td align="center">
                                            <%#Eval("TotalSystemJobs")%>
                                        </td>
                                        <td align="center">
                                            <%#Eval("DueJobs")%>
                                        </td>
                                        <td align="center">
                                            <%#Eval("OverDueJobs")%>
                                        </td>
                                        <td align="center">
                                            <%#Eval("TotalJobs")%>
                                        </td>
                                        <td align="center">
                                            <%#Eval("OutStandingJobs")%>
                                        </td>
                                        <td align="center">
                                            <%#Eval("OD1Week")%>
                                        </td>
                                        <td align="center">
                                            <%#Eval("OD2Week")%>
                                        </td>
                                        <td align="center">
                                            <%#Eval("ODMorethan2week")%>
                                        </td>
                                        <td>
                                            
                                            <%--Visible='<%# (DateTime.Today.Day>=1 && DateTime.Today.Day<=7) && DateTime.Today.Month==Common.CastAsInt32(Eval("Month"))%>' --%>

                                            <asp:Button ID="btnReview" Text="Review" CssClass="btn" OnClick="btnReview_Click" CommandArgument='<%#Eval("Month")%>' runat="server" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                
                </td>
            </tr>
        </table>

        <div style="position:absolute;top:0px;left:0px; height :100%;; width:100%;z-index:100;" runat="server" id="dvKPIEdit" visible="false" >
            <center>
            <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:700px; height:200px; padding :3px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:100px; opacity:1;filter:alpha(opacity=100)">
                <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                <ContentTemplate>
                    <table cellpadding="4" cellspacing="2" width="100%">
                        <tr>
                            <td colspan="4" class="text headerband">
                               Review Maintenance KPI - [<asp:Label ID="lblvessel" runat="server"></asp:Label>&nbsp;/&nbsp;<asp:Label ID="lblMonth" runat="server"></asp:Label> - <asp:Label ID="lblYear" runat="server"></asp:Label>]
                               <asp:HiddenField ID="hfmonth" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:right; ">System Jobs :</td>
                            <td style="text-align:left;">
                            <asp:TextBox ID="txtSystemJobs" onkeypress="fncInputIntegerValuesOnly(event);" runat="server"  ></asp:TextBox>
                            </td>
                            <td style="text-align:right;">Due Jobs :</td>
                            <td style="text-align:left;">
                            <asp:TextBox ID="txtDueJobs" onkeypress="fncInputIntegerValuesOnly(event);"  runat="server"></asp:TextBox>
                            </td>

                        </tr>
                        <tr>
                            <td style="text-align:right;">OverDue Jobs :</td>
                            <td style="text-align:left;">
                            <asp:Label ID="lblODJobs"  runat="server"></asp:Label>
                            </td>

                            <td style="text-align:right;">Total Jobs :</td>
                            <td style="text-align:left;">
                            <asp:Label ID="lblTotalJobs" runat="server"></asp:Label>
                            </td>

                        </tr>
                        <tr>
                            <td style="text-align:right;">Outstanding Jobs  :</td>
                            <td style="text-align:left;">
                            <asp:TextBox ID="txtOutstandingJobs" onkeypress="fncInputIntegerValuesOnly(event);"   runat="server"></asp:TextBox>
                            </td>

                            <td style="text-align:right;">Overdue 1 W  :</td>
                            <td style="text-align:left;">
                            <asp:TextBox ID="txtOD1W" onkeypress="fncInputIntegerValuesOnly(event);"  runat="server"></asp:TextBox>
                            </td>
                            

                        </tr>
                        <tr>
                            <td style="text-align:right;">Overdue 2 W  :</td>
                            <td style="text-align:left;">
                            <asp:TextBox ID="txtOD2W" onkeypress="fncInputIntegerValuesOnly(event);"  runat="server"></asp:TextBox>
                            </td>

                            <td style="text-align:right;">Overdue>2 W  :</td>
                            <td style="text-align:left;">
                            <asp:TextBox ID="txtODMore2W" onkeypress="fncInputIntegerValuesOnly(event);"  runat="server"></asp:TextBox>
                            </td>

                        </tr>
                        <tr>
                           <td colspan="4" style="text-align: center; padding-right: 5px; padding-left: 5px;">
                               <asp:Button ID="btnUpdateKPI" Text="Save" CssClass="btn" runat="server" Width="100px" onclick="btnUpdateKPI_Click"/>&nbsp;
                               <asp:Button ID="btnClose" Text="Cancel" CssClass="btn" runat="server" Width="100px" onclick="btnClose_Click" />
                           </td>
                       </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                 <asp:PostBackTrigger ControlID="btnClose" />
                 </Triggers>
                </asp:UpdatePanel>
            </div> 
            </center>
         </div>
    
    </div>
    </form>
</body>
</html>
