<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NCR_List.aspx.cs" Inherits="eReports_G118_G118_List" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../JS/jquery.min.js" type="text/javascript"></script>
    <script src="../JS/KPIScript.js" type="text/javascript"></script>

    <link href="../../CSS/tabs.css" rel="stylesheet" type="text/css" />

    <link href="../../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/KPIStyle.css" rel="stylesheet" type="text/css" />

    <link rel="stylesheet" type="text/css" href="../css/jquery.datetimepicker.css"/>

</head>
<body style=" margin:0px;">
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div style="font-family:Arial;font-size:12px;">
            <table border="0" cellpadding="0" cellspacing="0"  width="100%">
                    <tr>
                         <td style="text-align:center; vertical-align:middle;">
                            <table border="0" cellpadding="0" cellspacing="0"  width="100%">
                            <tr>
                                <td colspan="6" style='background-color:#CCE6FF; text-align:center; padding:4px; font-size:13px;' ><b><asp:Label ID="lblReportName" runat="server"></asp:Label></b></td>
                            </tr>

                            <tr>
                            <td style="vertical-align:middle">
                             <asp:DropDownList ID="ddlFleet" runat="server" Width="105px" AutoPostBack="true" onselectedindexchanged="ddlFleet_SelectedIndexChanged" ></asp:DropDownList>
                            </td>
                            <td style="vertical-align:middle">
                             <asp:DropDownList ID="ddlVessels" runat="server" Width="108px" ></asp:DropDownList>
                            </td>
                            <td style="vertical-align:middle;">
                            Period :  <asp:TextBox runat="server" ID="txtFd" MaxLength="11" Width="75px" CssClass="date_only"></asp:TextBox>
                            <asp:TextBox runat="server" ID="txtTD" MaxLength="11" Width="75px" CssClass="date_only"></asp:TextBox>
                            </td>
                            
                            <%--<td  style="vertical-align:middle;width:80px; text-align:right;">Category :</td>--%>
                            <td  style="vertical-align:middle;">
                               <%-- <asp:CheckBoxList ID="cblCategory" RepeatDirection="Horizontal" runat="server"></asp:CheckBoxList>--%>
                            </td>
                            <td  style="vertical-align:middle;">
                            Status : <asp:DropDownList ID="ddlStatus" runat="server">
                                       <asp:ListItem Text="All" Value="0" Selected="True" ></asp:ListItem>
                                       <asp:ListItem Text="Open" Value="N" ></asp:ListItem>
                                       <asp:ListItem Text="Closed" Value="Y" ></asp:ListItem>
                                     </asp:DropDownList>
                            </td>
                            <td  style="vertical-align:middle">
                            <asp:Button runat="server" ID="btnShow" CssClass="btn" Text="Show" 
                                    onclick="btnShow_Click" />
                            </td>
                            </tr>
                            </table>
                         </td>
                    </tr>
                    <tr>
                         <td>
                           <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 25px ; text-align:center; border-bottom:none;" class="scrollbox">
                           <table cellspacing="0" rules="all" border="1" bordercolor="white" cellpadding="4" style="width:100%;border-collapse:collapse; background-color:#FF9933; color: #fff;  height:25px;" >
                                <colgroup>
                                    <col style="width:30px;" />
                                    <col style="width:40px;" />
                                    <col style="width:40px;" />
                                    <col style="width:40px;" />
                                    <col style="width:135px;"/>
                                    <col />
                                    <col style="width:100px;" />                                    
                                    <col style="width:50px;" />
                                    <col style="width:50px;" />
                                    <col style="width:17px;" />
                                </colgroup>
                                <tr class= "headerstylegrid">
                                    <td style="color:White;vertical-align:middle; text-align:center;">Sr#</td>
                                    <td style="color:White;vertical-align:middle; text-align:center;">View</td>
                                    <td style="color:White;vertical-align:middle; text-align:center;">Edit</td>
                                    <td style="color:White;vertical-align:middle; text-align:center;">Print</td>
                                    <td style="color:White;vertical-align:middle;">&nbsp;Report#</td>
                                    <td style="color:White;vertical-align:middle;">&nbsp;Audit Area</td>
                                    <td style="color:White;vertical-align:middle;">&nbsp;NCR Dt.</td>
                                    <td style="color:White;vertical-align:middle;">&nbsp;Status</td>
                                    <td style="color:White;vertical-align:middle; text-align:center">Export</td>
                                    <td style="color:White;vertical-align:middle;"></td>
                                </tr>
                                </table>
                           </div>
                           <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 395px ; text-align:center;" class="scrollbox">
                           <table cellspacing="0" rules="none" border="0" cellpadding="4" style="width:100%;border-collapse:collapse;">
                                <colgroup>
                                     <col style="width:30px;" />
                                    <col style="width:40px;" />
                                    <col style="width:40px;" />
                                    <col style="width:40px;" />
                                    <col style="width:135px;"/>
                                    <col />
                                    <col style="width:100px;" />                                    
                                    <col style="width:50px;" />
                                    <col style="width:50px;" />
                                    <col style="width:17px;" />
                                </colgroup>
                                <asp:Repeater ID="rptReports" runat="server">
                                    <ItemTemplate>
                                            <tr>
                                                <td style="text-align:right"><%#Eval("SrNo")%>.</td>
                                                <td style="text-align:center"><a href='<%# "eReport_NCR.aspx?Key=" + Eval("ReportId") + "&Type=V&VC=" + Eval("VesselCode")%>' target="_blank"><img src="../../../HRD/Images/magnifier.png" /></a></td>
                                                <td style="text-align:center"><a href='<%# "eReport_NCR.aspx?Key=" + Eval("ReportId") +"&Type=E&VC=" + Eval("VesselCode")%>' target="_blank" runat="server" visible='<%#(Eval("Edit_ALLOWED").ToString()=="Y")%>'><img src="../../../HRD/Images/AddPencil.gif"/></a></td>
                                                <td style="text-align:center"><a href='<%# "ER_NCR_Report.aspx?Rid=" + Eval("ReportId") + "&VC=" + Eval("VesselCode")%>' target="_blank"><img src="../../../HRD/Images/printer12x12.png" /></a></td>
                                                <td align="left">&nbsp;<%#Eval("ReportNo")%></td>
                                                <td align="left">&nbsp;<%#Eval("AreaAudited")%></td>
                                                <td align="center"><%#Common.ToDateString(Eval("NCRCreatedDate"))%></td>
                                                <td align="center" style="text-align:center">
                                                    <img id="Img1" src="../../../HRD/Images/red_circle.png" runat="server" visible='<%#(Eval("ExportedBy").ToString().Trim()=="")%>' />
                                                    <img id="Img2" src="../../../HRD/Images/green_circle.gif"  runat="server" visible='<%#(Eval("ExportedBy").ToString().Trim()!="")%>' />

                                                </td>
                                                <td style="text-align:center"><asp:ImageButton runat="server" ID="btnExport" OnClick="btnExport_Click" ImageUrl="~/Modules/HRD/Images/right-arrow_12.png" visible='<%#(Eval("Status").ToString()!="Open" && Eval("ExportedBy").ToString().Trim()=="")%>' CommandArgument='<%#Eval("ReportId")%>' VesselCode='<%#Eval("VesselCode")%>' /> </td>
                                                <td>&nbsp;</td>
                                           </tr>
                                    </ItemTemplate>       
                                    <AlternatingItemTemplate>
                                            <tr style="background-color:#FFF5E6">
                                                <td style="text-align:right"><%#Eval("SrNo")%>.</td>
                                                <td style="text-align:center"><a href='<%# "eReport_NCR.aspx?Key=" + Eval("ReportId") + "&Type=V&VC=" + Eval("VesselCode")%>' target="_blank"><img src="../../../HRD/Images/magnifier.png" /></a></td>
                                                <td style="text-align:center"><a id="A1" href='<%# "eReport_NCR.aspx?Key=" + Eval("ReportId") +"&Type=E&VC=" + Eval("VesselCode")%>' target="_blank" runat="server" visible='<%#(Eval("Edit_ALLOWED").ToString()=="Y")%>'><img src="../../../HRD/Images/AddPencil.gif"/></a></td>
                                                <td style="text-align:center"><a href='<%# "ER_NCR_Report.aspx?Rid=" + Eval("ReportId") + "&VC=" + Eval("VesselCode")%>' target="_blank"><img src="../../../HRD/Images/printer12x12.png" /></a></td>
                                                <td align="left">&nbsp;<%#Eval("ReportNo")%></td>
                                                <td align="left">&nbsp;<%#Eval("AreaAudited")%></td>
                                                <td align="center"><%#Common.ToDateString(Eval("NCRCreatedDate"))%></td>
                                                <td align="center" style="text-align:center">
                                                <img id="Img1" src="../../../HRD/Images/red_circle.png" runat="server" visible='<%#(Eval("ExportedBy").ToString().Trim()=="")%>' />
                                                <img id="Img2" src="../../../HRD/Images/green_circle.gif"  runat="server" visible='<%#(Eval("ExportedBy").ToString().Trim()!="")%>' />
                                                </td>
                                                <td style="text-align:center"><asp:ImageButton runat="server" ID="btnExport" OnClick="btnExport_Click" ImageUrl="~/Modules/HRD/Images/right-arrow_12.png" visible='<%#(Eval("Status").ToString()!="Open" && Eval("ExportedBy").ToString().Trim()=="")%>' CommandArgument='<%#Eval("ReportId")%>' VesselCode='<%#Eval("VesselCode")%>' /> </td>
                                                <td>&nbsp;</td>
                                           </tr>
                                    </AlternatingItemTemplate>
                                </asp:Repeater>
                            </table>
                           </div>
                         </td>
                    </tr>
                </table>
    </div>
    </form>
</body>
<script type="text/javascript" src="../JS/jquery.datetimepicker.js"></script>
<script type="text/javascript">
    $('.date_only').datetimepicker({ timepicker: false, format: 'd-M-Y', formatDate: 'd-M-Y', allowBlank: true, defaultSelect: false, validateOnBlur: false });
</script>
</html>
