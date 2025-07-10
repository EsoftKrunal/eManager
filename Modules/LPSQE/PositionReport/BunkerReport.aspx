<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BunkerReport.aspx.cs" Inherits="BunkerReport"  %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
 <link rel="stylesheet" type="text/css" href="../../HRD/Styles/StyleSheet.css" />
    <script type="text/javascript" src="../js/jquery.min.js"></script>
    <script src="../js/AutoComplete/knockout-2.2.1.js" type="text/javascript"></script>
    <script type="text/javascript" src="../js/KPIScript.js"></script>

    <link rel="stylesheet" href="../js/AutoComplete/jquery-ui.css" />
    <script src="../js/AutoComplete/jquery-ui.js" type="text/javascript"></script>
    <style type="text/css">
        .auto-style1 {
            width: 30px;
            height: 21px;
        }
        .auto-style2 {
            width: 200px;
            height: 21px;
        }
        .auto-style3 {
            width: 150px;
            height: 21px;
        }
        .auto-style4 {
            width: 120px;
            height: 21px;
        }
    </style>
    </head>

 <body>
     <form id="form1" runat="server" style="font-family:Arial;font-size:12px;">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="text headerband">
        Bunker Report
    </div>
    <table width="100%" border="0" cellpadding="3" cellspacing="0" style="background-color: #F4F4F4; color:Black;">
        <tr>
            <td width="80px" style="text-align: right"> Voyage # : </td>
            <td width="110px">
                <asp:TextBox runat="server" ID="txtVoyageNo" CssClass="user-input-nopadding" Width="100px" MaxLength="50"></asp:TextBox>
            </td>
            <td style="text-align: right; width:80px;">
                From Date :
            </td>
            <td style="text-align: left;width:100px;" >
                <asp:TextBox runat="server" ID="txtFDate" CssClass="user-input-nopadding" Width="80px"
                    MaxLength="15"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtFDate" Format="dd-MMM-yyyy"
                    runat="server">
                </asp:CalendarExtender>
            </td>
            <td style="text-align: right; width:80px;">
                To Date :
            </td>
            <td style="text-align: left;width:100px;" >
                <asp:TextBox runat="server" ID="txtTDate" CssClass="user-input-nopadding" Width="80px"
                    MaxLength="15"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txtTDate" Format="dd-MMM-yyyy"
                    runat="server">
                </asp:CalendarExtender>
            </td>
            <td style="text-align: right;width:100px;" >
                Fuel Type :
            </td>
            <td style="text-align: left;width:120px;">
                <asp:DropDownList runat="server" ID="ddlType" Width="110px">
                    <asp:ListItem Text="< All >" Value=""></asp:ListItem>
                     
                </asp:DropDownList>
            </td>
            <td style="text-align: right;width:80px;">
                Location :
            </td>
            <td style="text-align: left; width:80px;">
                <asp:DropDownList runat="server" ID="ddlLocation" Width="120px">
                    <asp:ListItem Text="< All >" Value=""></asp:ListItem>
                     <asp:ListItem Value="1" Text="In Port"></asp:ListItem>
  <asp:ListItem Value="2" Text="At Anchorage"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="text-align:right">
                <asp:Button runat="server" ID="btnSearch" Text=" Search " OnClick="btnSearch_Click" CssClass="btn" Width="100px" />
                <%--<asp:Button runat="server" ID="btnNewReport" Text="Add New Report" OnClick="btnNewReport_Click" CssClass="btn" />--%>
              
            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;">
      
        <tr>
            <td style=" border-right:solid 1px black;text-align:center;height:22px; " colspan="2">
                <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label> 
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;border-right:solid 1px black;width:55%;" >
                <div style="border-bottom: none; overflow:scroll; height:25px; overflow-x:hidden;">
                    <table width="100%" cellpadding="3" border="0" style="border-collapse: collapse; " class="bordered" >
                        <thead>
                            <tr class= "headerstylegrid">

                                  <td style="width: 30px; text-align: center; color: White;">
                                    View
                                </td>
                            <%--   <td style="width: 30px; text-align: center; color: White;">
                                    Edit
                                </td>--%>
                                <td style="width: 75px; text-align: center; color: White;">
                                   Report #
                                </td>
                                <td style="text-align: center; color: White;width:200px;">
                                    Voyage#
                                </td>
                                <td style="width: 150px; color: White; text-align: center;">
                                    Bunkering Date
                                </td>
                               <%-- <td style="width: 100px; color: White; text-align: center;">
                                    Fuel Type
                                </td>
                                <td style="width: 120px; color: White; text-align: center;">
                                    Location
                                </td>--%>
                                <td style="width: 120px; color: White; text-align: center;">
                                    Port
                                </td>
                                 <td style="width: 60px; color: White; text-align: center;">
                                    Status
                                </td>
                            </tr>
                        </thead>
                    </table>
                </div>
                <div style="height: 397px; border-bottom: none; overflow-x: hidden; overflow-y: scroll;" class='ScrollAutoReset' id='dv_LFI_List'>
                    <table width="100%" cellpadding="3" border="0" style="border-collapse: collapse" class="bordered">
                        <tbody>
                            <asp:Repeater runat="server" ID="rptPR">
                                <ItemTemplate>
                                    <tr class="<%# Eval("RowCls")%>" onmouseover="this.style.historycolor=this.style.backgroundColor;this.style.backgroundColor='#FFFF66';"
                        onmouseout="this.style.backgroundColor=this.style.historycolor;">
                                         <td style="text-align:center;width:30px;">
                                             <a href='<%# "AddBunker.aspx?ReportPk=" + Eval("ReportPk") + "&Type=V&VesselId=" + Eval("VesselId") +"&FuelType="+Eval("FuelType")%>' target="_blank" title="Click to Open Banker Detail"><img src="../../HRD/Images/magnifier.png"  /></a>
                                         </td>
                                        <td style="width: 30px; text-align: center;">
                                            <asp:LinkButton runat="server" ID="btnView" OnClick="btnView_Click" CommandArgument='<%#Eval("ReportPk")%>' Style='background-color: transparent; ' > <%#Eval("ReportPk")%> </asp:LinkButton> 
                                        </td>
                                     <%--   <td style="text-align:center;width:30px;"><a href='<%# "AddBunker.aspx?ReportPk=" + Eval("ReportPk") +"&Type=E&VesselId=" + Eval("VesselId")+"&FuelType="+Eval("FuelType")%>' target="_blank" runat="server" visible='<%#(Eval("Edit_ALLOWED").ToString()=="Y")%>'><img src="../../HRD/Images/AddPencil.gif"/></a></td>--%>
                                            
                                      
                                        <td style="text-align: center;width: 200px;">
                                            <%#Eval("VoyageNumber")%>
                                        </td>
                                        <td style="width: 150px; text-align: center;">
                                            <%#Common.ToDateString(Eval("LocalDate"))%>
                                        </td>
                                       <%-- <td style="width: 100px; text-align: center;">
                                            <%#Eval("FuelType")%>
                                        </td>
                                         <td style="width: 120px; text-align: center;">
                                            <%# GetLocationText(Eval("Location")) %>
                                            
                                        </td>--%>
                                        <td style="width: 120px; text-align: center;">
                                             <%#Eval("Port")%>
                                            <%--<asp:ImageButton ID="btnExport" voyNo='<%#Eval("VoyageNumber")%>' ToolTip="Export" ImageUrl="~/Images/export.gif"
                                                CommandArgument='<%#Eval("ReportPk").ToString() + "~" + Eval("VesselId").ToString() + "~" + Eval("FuelType").ToString()%>'
                                                runat="server" OnClick="btnExport_Click" Style='background-color: transparent;
                                                height: 12px;' />--%>
                                        </td>
                                         <td style="width: 60px; color: White; text-align: center;">
                                            <img src="../../HRD/Images/lock.png" title="Bunkering Locked by Office" style='<%#(Eval("isLocked").ToString()=="Y")?"":"display:none"%>' />
                                         </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
            </td>
            <td style="vertical-align: top;border-right:solid 1px black;width:45%;">
                  <div style="border-bottom: none; overflow:scroll; height:25px; overflow-x:hidden;">
                    <table width="100%" cellpadding="3" border="0" style="border-collapse: collapse; " class="bordered" >
                        <thead>
                            <tr class= "headerstylegrid">
                              <%--  <td style="width: 30px; text-align: center; color: White;">
                                    View
                                </td>--%>
                                <td style="width: 100px; color: White; text-align: center;">
                                    Fuel Type
                                </td>
                                <td style="width: 150px; color: White; text-align: center;">
                                    Bunker Received (BDN Qty.) (MT)
                                </td>
                                <td style="width: 150px; color: White; text-align: center;">
                                    Actual Bunker (Rcvd. Qty.) (MT)
                                </td>
                            </tr>
                        </thead>
                    </table>
                </div>
                <div style="height: 397px; border-bottom: none; overflow-x: hidden; overflow-y: scroll;" class='ScrollAutoReset' id='dv_LFI_List1'>
                    <table width="100%" cellpadding="3" border="0" style="border-collapse: collapse" class="bordered">
                        <tbody>
                            <asp:Repeater runat="server" ID="rptBankingChildData" Visible="false">
                                <ItemTemplate>
                                    <tr onmouseover="">
                                      <%--  <td style="width: 25px; text-align: center;">

                                            <asp:ImageButton runat="server" ID="btnView" ImageUrl="~/Modules/HRD/Images/magnifier.png" OnClick="btnView_Click" CommandArgument='<%#Eval("ReportPk").ToString() + "~" + Eval("VesselId").ToString() + "~" + Eval("FuelType").ToString() %>' Style='background-color: transparent; height: 12px;' />
                                        </td>
                                         <td style="text-align:center;width:30px;">
                                             <a href='<%# "AddBunker.aspx?ReportPk=" + Eval("ReportPk") + "&Type=V&VesselId=" + Eval("VesselId") +"&FuelType="+Eval("FuelType")%>' target="_blank" title="Click to Open Banker Detail"><img src="../../HRD/Images/magnifier.png"  /></a>

                                         </td>--%>
                                        <td style="width: 100px; text-align: center;">
                                            <%#Eval("FuelType")%>
                                        </td>
                                         <td style="width: 120px; text-align: center;">
                                            <%#Eval("BunkerReceivedACC")%>
                                        </td>
                                        <td style="width: 120px; text-align: center;">
                                            <%#Eval("ActualBunkerReceived")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
            </td>
           
        </tr>
    </table>

        
    
         </form>
     </body>
</html>
    
  
