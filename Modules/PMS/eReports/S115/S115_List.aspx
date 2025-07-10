<%@ Page Language="C#" AutoEventWireup="true" CodeFile="S115_List.aspx.cs" Inherits="eReports_S115_S115_List" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="~/Modules/PMS/HSSQE/mainmene.ascx" tagname="mrvmenu" tagprefix="uc1" %>

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
    <link rel="stylesheet" type="text/css" href="~/HSSQE/style.css"/>
</head>
<body style=" margin:0px;">
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

       <%-- <div class="menuframe">                
        <uc1:mrvmenu ID="mrvmenu1" runat="server" />                
    </div>
    <div style="border-bottom:solid 5px #4371a5;"></div>--%>
    <%--------------------------------------------------------------------------------------%>

    <div>
            <table border="0" cellpadding="0" cellspacing="0"  width="100%">
                    <tr>
                         <td style="text-align:center; vertical-align:middle;">
                            <table border="0" cellpadding="0" cellspacing="0"  width="100%">
                            <tr>
                                <td colspan="5" style='background-color:#CCE6FF; text-align:center; padding:4px; font-size:13px;'><asp:Label ID="lblReportName" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                            <td style="vertical-align:middle; width:300px;;">
                            Period :  <asp:TextBox runat="server" ID="txtFd" MaxLength="11" Width="85px" CssClass="date_only"></asp:TextBox>
                            <asp:TextBox runat="server" ID="txtTD" MaxLength="11" Width="85px" CssClass="date_only"></asp:TextBox>
                            </td>
                            <%--<td  style="vertical-align:middle">
                            Port of Incident : <asp:TextBox runat="server" ID="txtPort" MaxLength="50"></asp:TextBox>
                            </td>--%>
                            <td  style="vertical-align:middle;width:80px; text-align:right;">Severity :</td>
                            <td  style="vertical-align:middle;width:250px;">
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
                            <td  style="vertical-align:middle;width:150px;">
                            Status : <asp:DropDownList ID="ddlStatus" runat="server">
                                       <asp:ListItem Text="All" Value="0" Selected="True" ></asp:ListItem>
                                       <asp:ListItem Text="Open" Value="N" ></asp:ListItem>
                                       <asp:ListItem Text="Closed" Value="Y" ></asp:ListItem>
                                     </asp:DropDownList>
                            </td>
                            <td  style="vertical-align:middle">
                            <asp:Button runat="server" ID="btnShow" CssClass="btn" Text="Show" onclick="btnShow_Click" />
                                
                                <%--<a href="\eReports\S115\eReport_S115.aspx?Type=E" class="btn" target="_blank">Add New Report</a>--%>

                                <%--<asp:ImageButton ID="btnAddNewReport" runat="server" ImageUrl="~/Modules/PMS/Images/add.png" OnClick="btnAddNewReport_OnClick" Visible='<%# (Eval("FormNo").ToString()=="G113") %>' />--%>
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
                                    <col style="width:135px;"/>
                                    <col style="width:60px;" />
                                    <col />
                                    <col style="width:100px;" />
                                    <%--<col style="width:150px;" />--%>
                                    <col style="width:100px;" />
                                    <col style="width:50px;" />
                                    <col style="width:50px;" />
                                    <col style="width:17px;" />
                                </colgroup>
                                <tr>
                                    <td style="color:White;vertical-align:middle; text-align:center;">Sr#</td>
                                    <td style="color:White;vertical-align:middle; text-align:center;">View</td>
                                    <td style="color:White;vertical-align:middle; text-align:center;">Print</td>
                                    <td style="color:White;vertical-align:middle;">&nbsp;Report#</td>
                                    <td style="color:White;vertical-align:middle;">&nbsp;Severity</td>    
                                    <td style="color:White;vertical-align:middle;">&nbsp;Port</td>
                                    <td style="color:White;vertical-align:middle;">&nbsp;Incident Dt.</td>
                                    <%--<td style="color:White;vertical-align:middle;">&nbsp;Created By</td>--%>
                                    <td style="color:White;vertical-align:middle;">&nbsp;Report Dt.</td>
                                    <td style="color:White;vertical-align:middle;">&nbsp;Status</td>
                                    <td style="color:White;vertical-align:middle;text-align:center;">&nbsp;RCA</td>
                                    <td style="color:White;vertical-align:middle;"></td>
                                </tr>
                                </table>
                           </div>
                           <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 400px ; text-align:center;" class="scrollbox">
                           <table cellspacing="0" rules="none" border="0" cellpadding="4" style="width:100%;border-collapse:collapse;">
                                <colgroup>
                                    <col style="width:30px;" />
                                    <col style="width:40px;" />
                                    <col style="width:40px;" />
                                    <col style="width:135px;"/>
                                    <col style="width:60px;" />
                                    <col />
                                    <col style="width:100px;" />
                                    <%--<col style="width:150px;" />--%>
                                    <col style="width:100px;" />
                                    <col style="width:50px;" />
                                    <col style="width:50px;" />
                                    <col style="width:17px;" />
                                </colgroup>
                                <asp:Repeater ID="rptReports" runat="server">
                                    <ItemTemplate>
                                            <tr>
                                            <td style="text-align:right"><%#Eval("SrNo")%>.</td>
                                            <td style="text-align:center">
                                                <a href='<%# "eReport_S115.aspx?Key=" + Eval("ReportId") + "&Type=V"%>' target="_blank" runat="server" visible='<%#(Eval("Edit_ALLOWED").ToString()=="N")%>'><img src="../../Images/search_magnifier_12.png" /></a>
                                                <a id="A1" href='<%# "eReport_S115.aspx?Key=" + Eval("ReportId") +"&Type=E"%>' target="_blank" runat="server" visible='<%#(Eval("Edit_ALLOWED").ToString()=="Y")%>'><img src="../../Images/12-em-pencil.png"/></a>
                                            </td>
                                            
                                            <td style="text-align:center"><a href='<%# "ER_S115_Report.aspx?Rid=" + Eval("ReportId") +"&VesselCode="+ Eval("VesselCode")%>' target="_blank"><img src="../../Images/printer12x12.png" /></a></td>
                                            <td align="left">&nbsp;<%#Eval("ReportNo")%></td>
                                            <td align="left">&nbsp;<%#Eval("AccidentSeverity")%></td>
                                            <td align="left">&nbsp;<%#Eval("Port")%></td>
                                            <td align="left"><%#Common.ToDateString(Eval("Incidentdate"))%></td>
                                            <%--<td align="left"><%#Eval("CreatedBy")%></td>--%>
                                            <td align="center"><%#Common.ToDateString(Eval("REPORTDATE"))%></td>
                                            <td align="center"><%#Eval("Status")%></td>
                                            <td align="center" style="text-align:center;">
                                                <asp:ImageButton ID="btnDownloadRCA" runat="server" ImageUrl="~/Modules/PMS/Images/paperclip.gif" OnClick="btnDownloadRCA_OnClick" CommandArgument='<%#Eval("ReportId")  %>' Visible='<%#Common.CastAsInt32( Eval("RcaRecordCount"))>0 %>' />
                                            </td>
                                            <td>&nbsp;</td>
                                           </tr>
                                    </ItemTemplate>       
                                    <AlternatingItemTemplate>
                                            <tr style="background-color:#FFF5E6">
                                            <td style="text-align:right"><%#Eval("SrNo")%>.</td>
                                            <td style="text-align:center">
                                                <a id="A2" href='<%# "eReport_S115.aspx?Key=" + Eval("ReportId") + "&Type=V"%>' target="_blank" runat="server" visible='<%#(Eval("Edit_ALLOWED").ToString()=="N")%>'><img src="../../Images/search_magnifier_12.png" /></a>
                                                <a id="A1" href='<%# "eReport_S115.aspx?Key=" + Eval("ReportId") +"&Type=E"%>' target="_blank" runat="server" visible='<%#(Eval("Edit_ALLOWED").ToString()=="Y")%>'><img src="../../Images/12-em-pencil.png"/></a>
                                            </td>
                                            <td style="text-align:center"><a href='<%# "ER_S115_Report.aspx?Rid=" + Eval("ReportId")+"&VesselCode="+ Eval("VesselCode")%>' target="_blank"><img src="../../Images/printer12x12.png" /></a></td>
                                            <td align="left">&nbsp;<%#Eval("ReportNo")%></td>
                                            <td align="left">&nbsp;<%#Eval("AccidentSeverity")%></td>
                                            <td align="left">&nbsp;<%#Eval("Port")%></td>
                                            <td align="left"><%#Common.ToDateString(Eval("Incidentdate"))%></td>
                                            <%--<td align="left"><%#Eval("CreatedBy")%></td>--%>
                                            <td align="center"><%#Common.ToDateString(Eval("REPORTDATE"))%></td>
                                            <td align="center"><%#Eval("Status")%></td>
                                             <td align="center" style="text-align:center;">
                                                 <asp:ImageButton ID="btnDownloadRCA" runat="server" ImageUrl="~/Modules/PMS/Images/paperclip.gif" OnClick="btnDownloadRCA_OnClick" CommandArgument='<%#Eval("ReportId")  %>' Visible='<%#Common.CastAsInt32( Eval("RcaRecordCount"))>0 %>' />
                                             </td>
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
