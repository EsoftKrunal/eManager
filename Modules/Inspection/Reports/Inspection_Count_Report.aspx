<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="Inspection_Count_Report.aspx.cs" Inherits="Inspection_Count_Report" Title="Inspection_Count_Report" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692FBEA5521E1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>EMANAGER</title>
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
     <link rel="stylesheet" type="text/css" href="../../HRD/Styles/StyleSheet.css" /> 
     </head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%" style="font-family:Arial;font-size:12px;">
         <tr>
                        <td align="center" class="text headerband" style="height: 23px; " >
                            Inspection Count Report</td>
                    </tr>
            <tr>
                <td align="center" valign="top" style="height: 235px">
                <table width="99%" border="0" cellspacing="0" cellpadding="2">
                <tr>
                    <td>
                    <div style="height: 353px; overflow-y: auto; overflow-x: hidden;">
                        <asp:GridView ID="Gv_TotalInspCurrYear" runat="server" AutoGenerateColumns="false" GridLines="Both" Width="98%">
                            <Columns>
                                <asp:BoundField DataField="Code" HeaderText="Inspection Group">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Jan" HeaderText="JAN">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Feb" HeaderText="FEB">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Mar" HeaderText="MAR">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Apr" HeaderText="APR">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="May" HeaderText="MAY">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Jun" HeaderText="JUN">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Jul" HeaderText="JUL">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Aug" HeaderText="AUG">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Sep" HeaderText="SEP">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Oct" HeaderText="OCT">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Nov" HeaderText="NOV">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Dec" HeaderText="DEC">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                            </Columns>
                            <RowStyle CssClass="rowstyle" Font-Bold="False" />
                            <SelectedRowStyle CssClass="selectedtowstyle" />
                            <PagerStyle CssClass="pagerstyle" />
                            <HeaderStyle CssClass="headerstylefixedheadergrid" />
                        </asp:GridView>
                    </div>
                    </td>
                </tr>
                <tr>
                  <td colspan="2" align="right" >
                  <%--<a href="SiteFrame.aspx"><img src="images/btn.jpg" name="Image5" width="86" height="30" border="0" id="Image5" /></a>--%>
                  <asp:ImageButton ID="Image5" runat="server" Visible="false" ImageUrl="~/Modules/HRD/Images/btn.jpg" Width="86px" Height="30px" BorderWidth="0px" OnClick="Image5_Click"/>
                  </td>
                </tr>
            </table>
                </td>
            </tr>
        </table>
</form>
</body>
</html>