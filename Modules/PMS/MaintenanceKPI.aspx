<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MaintenanceKPI.aspx.cs" Inherits="MaintenanceKPI" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%--<link href="CSS/style.css" rel="stylesheet" type="text/css" />--%>
    <link href="CSS/tabs.css" rel="stylesheet" type="text/css" />
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
      <link href="../../css/app_style.css" rel="Stylesheet" type="text/css" />
    <link href="../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
     <link href="../HRD/Styles/style.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div>
        <div class="text headerband">Maintenance KPI</div>
        <div style="width: 99%; background-color: #e2e2e2; padding: 3px; text-align: center;font-weight: bold; font-size: 14px;">
        <asp:RadioButtonList runat="server" Visible="false" ID="Rad_officeship" OnSelectedIndexChanged="Rad_officeship_OnSelectedIndexChanged" AutoPostBack="true" RepeatDirection="Horizontal">
            <asp:ListItem Text="Office Data" Value="O" Selected="True"></asp:ListItem> 
            <asp:ListItem Text="Ship Data" Value="S"></asp:ListItem> 
        </asp:RadioButtonList>
        </div>
            <table style="background-color: #f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="text-align: right; padding: 2px;">
                    Select Vessel :
                </td>
                <td style="text-align: left; padding: 2px;">
                    <asp:DropDownList ID="ddlVessels" runat="server">
                    </asp:DropDownList>
                </td>
                <td style="text-align: right; padding: 2px;">
                    Year :
                </td>
                <td style="text-align: left; padding: 2px;">
                    <asp:DropDownList ID="ddlYear" Width="90px" runat="server">
                    </asp:DropDownList>
                </td>
                <td style="text-align: right;">
                    Month :
                </td>
                <td style="text-align: left;">
                    <asp:DropDownList ID="ddlMonth" Width="90px" runat="server">
                        <asp:ListItem Text="Jan" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Feb" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Mar" Value="3"></asp:ListItem>
                        <asp:ListItem Text="Apr" Value="4"></asp:ListItem>
                        <asp:ListItem Text="May" Value="5"></asp:ListItem>
                        <asp:ListItem Text="Jun" Value="6"></asp:ListItem>
                        <asp:ListItem Text="Jul" Value="7"></asp:ListItem>
                        <asp:ListItem Text="Aug" Value="8"></asp:ListItem>
                        <asp:ListItem Text="Sep" Value="9"></asp:ListItem>
                        <asp:ListItem Text="Oct" Value="10"></asp:ListItem>
                        <asp:ListItem Text="Nov" Value="11"></asp:ListItem>
                        <asp:ListItem Text="Dec" Value="12"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="padding: 2px;">
                    <asp:Button ID="btnView" OnClick="btnView_Click" ForeColor="White" BackColor="Red" Text="Show" runat="server" Width="100px" />
                    <asp:Button ID="btnAdd" OnClick="btnAdd_Click" ForeColor="White" BackColor="Red" Text="Add Missing KPI ( Last Month)" runat="server" Width="130px" />
                </td>
                <td style="padding: 2px;">
                    <%--<asp:Button ID="btnPrintKPI" OnClick="btnPrintKPI_Click" CssClass="btnorange" Text="Print"
                        runat="server" Width="100px" />--%>
                    <asp:Button ID="btnImport" OnClick="btnImport_Click" CssClass="btnorange" Text="Import"
                        runat="server" Width="100px" Visible="false" />
                </td>
            </tr>
            </table>
        <asp:Panel runat="server" ID="pnlOffice">
            <table style="background-color: #f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="text-align: center">
                    <div id="Div1" onscroll="SetScrollPos(this)" style="overflow-y: scroll; overflow-x: hidden;
                        width: 99%; height: 31px; text-align: center;">
                        <table border="1" cellpadding="2" cellspacing="0" rules="all" style="border-collapse: collapse;"
                            width="1200">
                            <colgroup>
                                <col style="width: 100px;" />
                                <col style="width: 100px;" />
                                <col style="width: 100px;" />
                                <col style="width: 100px;" />
                                <col style="width: 100px;" />
                                <col style="width: 100px;" />
                                <col style="width: 140px;" />
                                <%--<col style="width: 100px;" />
                                <col style="width: 100px;" />--%>
                                <col style="width: 150px;" />
                                <col />
                            </colgroup>
                            <tr align="center" class= "headerstylegrid">
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
                                <%--<td>
                                    Overdue 1 W
                                </td>
                                <td>
                                    Overdue 2 W
                                </td>--%>
                                <td>
                                     Outstanding % 
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="dvKPI" onscroll="SetScrollPos(this)" class="scrollbox" style="overflow-y: scroll;
                        overflow-x: hidden; width: 99%; height: 300px; text-align: center;">
                        <table border="1" cellpadding="2" cellspacing="0" rules="all" style="border-collapse: collapse;"
                            width="1200">
                            <colgroup>
                                <col style="width: 100px;" />
                                <col style="width: 100px;" />
                                <col style="width: 100px;" />
                                <col style="width: 100px;" />
                                <col style="width: 100px;" />
                                <col style="width: 100px;" />
                                <col style="width: 140px;" />
                                <%--<col style="width: 100px;" />
                                <col style="width: 100px;" />--%>
                                <col style="width: 150px;" />
                                <col />
                            </colgroup>
                            <asp:Repeater ID="rptKPI" runat="server">
                                <ItemTemplate>
                                    <tr style="font-size: 11px; text-align: center;">
                                        <td align="center">
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
                                        <%--<td align="center">
                                            <%#Eval("OD1Week")%>
                                        </td>
                                        <td align="center">
                                            <%#Eval("OD2Week")%>
                                        </td>--%>
                                        <td align="center">
                                            <%#Eval("OutstandingPer")%>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnEditKPI" Text="Edit" ForeColor="White" BackColor="Red" OnClick="btnEditKPI_Click"
                                                CommandArgument='<%# Eval("Vesselcode").ToString() + "@" + Eval("Month").ToString()  %>'
                                                runat="server" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </td>
            </tr>
        </table>

        <div style="position:absolute;top:0px;left:0px; height :510px; width:100%;z-index:100;" runat="server" id="dvKPIEdit" visible="false" >
            <center>
            <div style="position:absolute;top:0px;left:0px; height :700px; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:550px; height:200px; padding :3px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:100px; opacity:1;filter:alpha(opacity=100)">
                <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                <ContentTemplate>
                    <table cellpadding="4" cellspacing="2" width="100%">
                        <tr>
                            <td colspan="4" class="text headerband">
                               Edit Maintenance KPI - [<asp:Label ID="lblvessel" runat="server"></asp:Label>&nbsp;/&nbsp;<asp:Label ID="lblMonth" runat="server"></asp:Label> - <asp:Label ID="lblYear" runat="server"></asp:Label>]
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:right; ">System Jobs :</td>
                            <td style="text-align:left;">
                            <asp:TextBox ID="txtSystemJobs" onkeypress="fncInputIntegerValuesOnly(event)" runat="server" required='yes' ></asp:TextBox>
                            </td>
                            <td style="text-align:right;">Due Jobs :</td>
                            <td style="text-align:left;">
                            <asp:TextBox ID="txtDueJobs" onkeypress="fncInputIntegerValuesOnly(event)" required='yes'  runat="server"></asp:TextBox>
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
                            <asp:TextBox ID="txtOutstandingJobs" onkeypress="fncInputIntegerValuesOnly(event)" required='yes'  runat="server"></asp:TextBox>
                            </td>

                            <td style="text-align:right;">Overdue 1 W  :</td>
                            <td style="text-align:left;">
                            <asp:TextBox ID="txtOD1W" onkeypress="fncInputIntegerValuesOnly(event)" required='yes'  runat="server"></asp:TextBox>
                            </td>
                            

                        </tr>
                        <tr>
                            <td style="text-align:right;">Overdue 2 W  :</td>
                            <td style="text-align:left;">
                            <asp:TextBox ID="txtOD2W" onkeypress="fncInputIntegerValuesOnly(event)" required='yes'  runat="server"></asp:TextBox>
                            </td>

                            <td style="text-align:right;">Overdue>2 W  :</td>
                            <td style="text-align:left;">
                            <asp:TextBox ID="txtODMore2W" onkeypress="fncInputIntegerValuesOnly(event)" required='yes'  runat="server"></asp:TextBox>
                            </td>

                        </tr>
                        <tr>
                           <td colspan="4" style="text-align: right; padding-right: 5px; padding-left: 5px;">
                               <asp:Button ID="btnUpdateKPI" Text="Update" ForeColor="White" BackColor="Red" runat="server" 
                                   Width="100px" onclick="btnUpdateKPI_Click"/>&nbsp;
                               <asp:Button ID="btnClose" Text="Cancel"  runat="server" ForeColor="White" BackColor="Red"
                                   onclick="btnClose_Click" />
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
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlShip" Visible="false">
        <table style="background-color: #f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="text-align: center">
                    <div id="Div2" onscroll="SetScrollPos(this)" style="overflow-y: scroll; overflow-x: hidden;width: 99%; height: 31px; text-align: center;">
                        <table border="1" cellpadding="2" cellspacing="0" rules="all" style="border-collapse: collapse;"width="1100">
                               <colgroup>
                                <col style="width: 100px;" />
                                <col style="width: 100px;" />
                                <col style="width: 100px;" />
                                <col style="width: 100px;" />
                                <col style="width: 100px;" />
                                <col style="width: 100px;" />
                                <col style="width: 140px;" />
                                <col style="width: 100px;" />
                                <col style="width: 100px;" />
                                <col style="width: 100px;" />
                                <col />
                            </colgroup>
                            <tr align="center" style="background-color: #66C2FF; font-size: 11px; height: 25px;
                                font-weight: bold;">
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
                              
                            </tr>
                        </table>
                    </div>
                    <div id="Div3" onscroll="SetScrollPos(this)" class="scrollbox" style="overflow-y: scroll; overflow-x: hidden; width: 99%; height: 300px; text-align: center;">
                        <table border="1" cellpadding="2" cellspacing="0" rules="all" style="border-collapse: collapse;" width="1100">
                            <colgroup>
                                <col style="width: 100px;" />
                                <col style="width: 100px;" />
                                <col style="width: 100px;" />
                                <col style="width: 100px;" />
                                <col style="width: 100px;" />
                                <col style="width: 100px;" />
                                <col style="width: 140px;" />
                                <col style="width: 100px;" />
                                <col style="width: 100px;" />
                                <col style="width: 100px;" />
                                <col />
                            </colgroup>
                            <asp:Repeater ID="rpt_ShipData" runat="server">
                                <ItemTemplate>
                                    <tr style="font-size: 11px; text-align: center;">
                                        <td align="center">
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
                                       
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
