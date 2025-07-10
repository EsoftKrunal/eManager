<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Accident_List1.aspx.cs" Inherits="eReports_S115_S115_List" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../eReports/JS/jquery.min.js" type="text/javascript"></script>
    <script src="../../eReports/JS/KPIScript.js" type="text/javascript"></script>

    <link href="../../CSS/tabs.css" rel="stylesheet" type="text/css" />

    <link href="../../eReports/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="../../eReports/CSS/KPIStyle.css" rel="stylesheet" type="text/css" />

    <link rel="stylesheet" type="text/css" href="../css/jquery.datetimepicker.css"/>

</head>
<body style=" margin:0px;">
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div>
            <table border="0" cellpadding="0" cellspacing="0"  width="100%">
                    <tr>
                         <td style="text-align:center; vertical-align:middle;">
                            <table border="0" cellpadding="0" cellspacing="0"  width="100%">
                            <tr>
                                <td colspan="6" style='background-color:#CCE6FF; text-align:center; padding:4px; font-size:13px;'><asp:Label ID="lblReportName" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                            <td style="vertical-align:middle">
                             <asp:DropDownList ID="ddlFleet" runat="server" Width="105px" AutoPostBack="true" onselectedindexchanged="ddlFleet_SelectedIndexChanged" ></asp:DropDownList>
                            </td>
                            <td style="vertical-align:middle">
                             <asp:DropDownList ID="ddlVessels" runat="server" Width="108px" ></asp:DropDownList>
                            </td>
                            <td style="vertical-align:middle">
                            Period :  <asp:TextBox runat="server" ID="txtFd" MaxLength="11" Width="75px" CssClass="date_only"></asp:TextBox>
                            <asp:TextBox runat="server" ID="txtTD" MaxLength="11" Width="75px" CssClass="date_only"></asp:TextBox>
                            </td>
                            <%--<td  style="vertical-align:middle">
                            Port of Incident : <asp:TextBox runat="server" ID="txtPort" MaxLength="50"></asp:TextBox>
                            </td>--%>
                            <%--<td  style="vertical-align:middle;text-align:right;">Severity :</td>--%>
                            <td  style="vertical-align:middle">
                                <%--<asp:RadioButton Text="ALL" ID="radAll" runat="server" GroupName="sev"></asp:RadioButton>
                                <asp:RadioButton Text="Minor" ID="RadioButton1" runat="server" GroupName="sev"></asp:RadioButton>
                                <asp:RadioButton Text="Major" ID="RadioButton2" runat="server" GroupName="sev"></asp:RadioButton>
                                <asp:RadioButton Text="Severe" ID="RadioButton3" runat="server" GroupName="sev"></asp:RadioButton>--%>
                                <asp:CheckBoxList ID="cblSeverity" RepeatDirection="Horizontal" runat="server">
                                <%--<asp:ListItem Text="All" Value="0" Selected="True"></asp:ListItem>--%>
                                <asp:ListItem Text="Minor" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Major" Value="2" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Severe" Value="3" Selected="True"></asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                            <td  style="vertical-align:middle">
                           <%-- Status : --%><asp:DropDownList ID="ddlStatus" runat="server">
                                       <asp:ListItem Text="< Status >" Value="0" ></asp:ListItem>
                                       <asp:ListItem Text="Open" Value="N" Selected="True"></asp:ListItem>
                                       <asp:ListItem Text="Closed" Value="Y" ></asp:ListItem>
                                     </asp:DropDownList>
                            </td>
                            <td  style="vertical-align:middle">
                            <asp:Button runat="server" ID="btnShow" CssClass="btn" Text="Show" onclick="btnShow_Click" />
                            </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <asp:CheckBoxList ID="chkListClassificationOfAccident" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem  Value="1" Text="Injury to People" ></asp:ListItem>
                                        <asp:ListItem  Value="2" Text="Mooring" ></asp:ListItem>
                                        <asp:ListItem  Value="3" Text="Security" ></asp:ListItem>
                                        <asp:ListItem  Value="4" Text="Cargo" ></asp:ListItem>
                                        <asp:ListItem  Value="5" Text="Equipment Failure" ></asp:ListItem>
                                        <asp:ListItem  Value="6" Text="Navigation" ></asp:ListItem>
                                        <asp:ListItem  Value="7" Text="Damage to Property" ></asp:ListItem>
                                        <asp:ListItem  Value="8" Text="Pollution" ></asp:ListItem>
                                        <asp:ListItem  Value="9" Text="Fire" ></asp:ListItem>
                                    </asp:CheckBoxList>
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
                                    <col style="width:100px;" />
                                    <col />
                                    <col style="width:100px;" />
                                    <%--<col style="width:150px;" />--%>
                                    <col style="width:100px;" />
                                    <col style="width:70px;" />
                                    <col style="width:50px;" />
                                    <col style="width:17px;" />
                                </colgroup>
                                <tr>
                                    <td style="color:White;vertical-align:middle; text-align:center;">Sr#</td>
                                    <td style="color:White;vertical-align:middle; text-align:center;">View</td>
                                    <td style="color:White;vertical-align:middle; text-align:center;">Edit</td>
                                    <td style="color:White;vertical-align:middle; text-align:center;">Print</td>
                                    <td style="color:White;vertical-align:middle;">&nbsp;Report#</td>
                                    <td style="color:White;vertical-align:middle;">&nbsp;Severity</td>    
                                    <td style="color:White;vertical-align:middle;">&nbsp;Port</td>
                                    <td style="color:White;vertical-align:middle;">&nbsp;Incident Dt.</td>
                                    <%--<td style="color:White;vertical-align:middle;">&nbsp;Created By</td>--%>
                                    <td style="color:White;vertical-align:middle;">&nbsp;Report Dt.</td>
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
                                    <col style="width:100px;" />
                                    <col />
                                    <col style="width:100px;" />
                                    <%--<col style="width:150px;" />--%>
                                    <col style="width:100px;" />
                                    <col style="width:70px;" />
                                    <col style="width:50px;" />
                                    <col style="width:17px;" />
                                </colgroup>
                                <asp:Repeater ID="rptReports" runat="server">
                                    <ItemTemplate>
                                            <tr>
                                            <td style="text-align:right"><%#Eval("SrNo")%>.</td>
                                            <td style="text-align:center"><a href='<%# "eReport_S115.aspx?Key=" + Eval("ReportId") + "&Type=V&VC=" + Eval("VesselCode")%>' target="_blank"><img src="../../Images/search_magnifier_12.png" /></a></td>
                                            <td style="text-align:center"><a href='<%# "eReport_S115.aspx?Key=" + Eval("ReportId") +"&Type=E&VC=" + Eval("VesselCode")%>' target="_blank" runat="server" visible='<%#(Eval("Edit_ALLOWED").ToString()=="Y")%>'><img src="../../Images/12-em-pencil.png" /></a></td>
                                            <td style="text-align:center"><a href='<%# "ER_S115_Report.aspx?Rid=" + Eval("ReportId") + "&VC=" + Eval("VesselCode")%>' target="_blank"><img src="../../Images/printer12x12.png" /></a></td>
                                            <td align="left">&nbsp;<%#Eval("ReportNo")%></td>
                                            <td align="left">&nbsp;<%#Eval("AccidentSeverity")%></td>
                                            <td align="left">&nbsp;<%#Eval("Port")%></td>
                                            <td align="left"><%#Common.ToDateString(Eval("Incidentdate"))%></td>
                                            <%--<td align="left"><%#Eval("CreatedBy")%></td>--%>
                                            <td align="center"><%#Common.ToDateString(Eval("REPORTDATE"))%></td>
                                            <td align="center" style="text-align:center">
                                                <img id="Img1" src="../../../Images/red_circle.png" runat="server"  visible='<%#(Eval("ExportedBy").ToString().Trim()=="")%>' />
                                                <img id="Img2" src="../../../Images/green_circle.gif"  runat="server" visible='<%#(Eval("ExportedBy").ToString().Trim()!="")%>' />
                                            </td>
                                            <td style="text-align:center"><asp:ImageButton runat="server" ID="btnExport" OnClick="btnExport_Click" ImageUrl="~/Images/right_arrow.png" visible='<%#(Eval("Status").ToString()!="Open" && Eval("ExportedBy").ToString().Trim()=="")%>' CommandArgument='<%#Eval("ReportId")%>' VesselCode='<%#Eval("VesselCode")%>' /> </td>
                                            <td>&nbsp;</td>
                                           </tr>
                                    </ItemTemplate>       
                                    <AlternatingItemTemplate>
                                            <tr style="background-color:#FFF5E6">
                                            <td style="text-align:right"><%#Eval("SrNo")%>.</td>
                                            <td style="text-align:center"><a href='<%# "eReport_S115.aspx?Key=" + Eval("ReportId") + "&Type=V&VC=" + Eval("VesselCode")%>' target="_blank"><img src="../../Images/search_magnifier_12.png" /></a></td>
                                            <td style="text-align:center"><a href='<%# "eReport_S115.aspx?Key=" + Eval("ReportId") + "&Type=E&VC=" + Eval("VesselCode")%>' target="_blank" runat="server" visible='<%#(Eval("Edit_ALLOWED").ToString()=="Y")%>'><img src="../../Images/12-em-pencil.png" /></a></td>
                                            <td style="text-align:center"><a href='<%# "ER_S115_Report.aspx?Rid=" + Eval("ReportId") + "&VC=" + Eval("VesselCode")%>' target="_blank"><img src="../../Images/printer12x12.png" /></a></td>
                                            <td align="left">&nbsp;<%#Eval("ReportNo")%></td>
                                            <td align="left">&nbsp;<%#Eval("AccidentSeverity")%></td>
                                            <td align="left">&nbsp;<%#Eval("Port")%></td>
                                            <td align="left"><%#Common.ToDateString(Eval("Incidentdate"))%></td>
                                            <%--<td align="left"><%#Eval("CreatedBy")%></td>--%>
                                            <td align="center"><%#Common.ToDateString(Eval("REPORTDATE"))%></td>
                                            <td align="center" style="text-align:center">
                                                <img id="Img1" src="../../../Images/red_circle.png" runat="server" visible='<%#(Eval("ExportedBy").ToString().Trim()=="")%>' />
                                                <img id="Img2" src="../../../Images/green_circle.gif"  runat="server" visible='<%#(Eval("ExportedBy").ToString().Trim()!="")%>' />
                                            </td>
                                            <td style="text-align:center"><asp:ImageButton runat="server" ID="btnExport" OnClick="btnExport_Click" ImageUrl="~/Images/right_arrow.png" visible='<%#(Eval("Status").ToString()!="Open" && Eval("ExportedBy").ToString().Trim()=="")%>' CommandArgument='<%#Eval("ReportId")%>' VesselCode='<%#Eval("VesselCode")%>' /> </td>
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
