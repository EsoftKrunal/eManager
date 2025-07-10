<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="FIR_Report.aspx.cs" Inherits="FIR_Report" Title="FIR Report" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692FBEA5521E1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>EMANAGER</title>
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
     <link rel="stylesheet" type="text/css" href="../../HRD/Styles/StyleSheet.css" />  
<script language="javascript" type="text/javascript">
     function FIR_Report(file)
     {
        window.open('FIR_Report_PopUp.aspx?File=' + file);  
     }
    </script>
    </head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%" style="font-family:Arial;font-size:12px;">
         <tr>
                        <td align="center" class="text headerband" style="height: 23px; " >
                            FIR Report</td>
                    </tr>
        <tr>
            <td align="center" valign="top" >
            <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 0px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid" class="">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="padding-right: 10px; color: red; text-align: center">
                                        <asp:Label ID="lblMessage" runat="Server" Font-Size="12px" ForeColor="Red" meta:resourcekey="lblMessageResource1"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="text-align: left">
                                            <table cellpadding="3" cellspacing="0" width="100%">
                                            <tr>
                                                <td style="text-align: right" valign="bottom">
                                                    Fleet : </td>
                                                <td valign="bottom">
                                                    <asp:DropDownList runat="server" ID="ddlFleet" CssClass="input_box" ></asp:DropDownList> 
                                                    </td>
                                                <td style="text-align: right" valign="bottom">
                                                    From Date :</td>
                                                <td style="text-align: left" valign="bottom">
                                                    <asp:TextBox ID="txtfromdate" runat="server" CssClass="input_box" Width="93px"></asp:TextBox>
                                                    <asp:ImageButton ID="imgfrom" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Calendar.gif"/>
                                                </td>
                                                <td style="text-align: right" valign="bottom">
                                                    To Date :</td>
                                                <td style="text-align: left; padding-left: 4px;" valign="top">
                                                    <asp:TextBox ID="txttodate" runat="server" CssClass="input_box" Width="93px"></asp:TextBox><asp:ImageButton
                                                    ID="imgto" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Calendar.gif"/>
                                                </td>
                                                <td style="text-align: left" valign="top">
                                                    <asp:Button ID="btn_Show" runat="server" CssClass="btn" Text="Show Report" OnClick="btn_Show_Click"  />
                                                    <asp:Button ID="btn_Clear" runat="server" CssClass="btn" Text="Clear" OnClick="btn_Clear_Click"  />
                                                </td>
                                            </tr>
                                            </table>
                                            <asp:GridView style="TEXT-ALIGN: center" id="Grd_FIR" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False" GridLines="Horizontal" HeaderStyle-Wrap="false" PageSize="15" OnPageIndexChanging="Grd_NearMiss_PageIndexChanging">
<RowStyle CssClass="rowstyle"></RowStyle>
<Columns>
<asp:TemplateField HeaderText="View" Visible="false" >
<ItemTemplate>
<img id="img_ViewDetail" src="../../HRD/Images/HourGlass.gif" style="cursor: hand; display:<%# (Eval("TableId").ToString().Trim()=="")?"None":"Block" %>" onclick='return FIR_Report("<%#Eval("FileName") %>");' title="View Details" />                                      
</ItemTemplate>
<ItemStyle Width="40px"></ItemStyle>
</asp:TemplateField>
<asp:TemplateField HeaderText="Fleet">
<ItemTemplate>
<asp:Label ID="lblVesselName" runat="server" Text='<%#Eval("FleetNo") %>'></asp:Label>
<asp:HiddenField ID="hfd_VesselId" runat="server" Value='<%#Eval("TableId") %>' />            
</ItemTemplate>
<ItemStyle HorizontalAlign="Left"></ItemStyle>
</asp:TemplateField>
<asp:BoundField DataField="FIRDate" HeaderText="Report Date">
<ItemStyle HorizontalAlign="Center" Wrap="False" Width="150px"></ItemStyle>
</asp:BoundField>
<asp:TemplateField HeaderText="View">
<ItemTemplate>
<img id="img_ViewDetail" src="../../HRD/Images/paperclip.gif" style="cursor: hand; display:<%# (Eval("TableId").ToString().Trim()=="")?"None":"Block" %>" onclick='<%#"return FIR_Report(\"" + Eval("FileName") + "\");" %>' title="View Details" />                                      
</ItemTemplate>
<ItemStyle Width="40px"></ItemStyle>
</asp:TemplateField>
</Columns>
<FooterStyle HorizontalAlign="Center"></FooterStyle>
<PagerStyle HorizontalAlign="Center"></PagerStyle>
<SelectedRowStyle CssClass="selectedtowstyle"></SelectedRowStyle>
<HeaderStyle Wrap="False" CssClass="headerstylefixedheadergrid" ></HeaderStyle>
</asp:GridView>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy" PopupButtonID="imgfrom" PopupPosition="TopRight" TargetControlID="txtfromdate"></ajaxToolkit:CalendarExtender>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupButtonID="imgto" PopupPosition="TopRight" TargetControlID="txttodate"></ajaxToolkit:CalendarExtender>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </fieldset>
            </td>
        </tr>
    </table>
</form>
</body>
</html>

