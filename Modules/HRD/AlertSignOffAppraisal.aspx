<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AlertSignOffAppraisal.aspx.cs" Inherits="AlertSignOffAppraisal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" /> 
    <link href="Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="Styles/StyleSheet.css" />
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <link href="Styles/style.css" rel="stylesheet" type="text/css" />
    <title>EOC Appraisal Alert</title>
</head>
<body>
    <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center" width="100%">
            <tr style="height:23px">
                <td align="center" style="background-color:#4371a5;" class="text">
                    EOC Appraisal Alerts</td>
            </tr>
            <tr style="height:30px">
                <td>
                  <div style=" width:100%; height:500px; overflow-x:hidden; overflow-y:scroll;" > 
                   <asp:GridView ID="gv_VesselManning" runat="server" AutoGenerateColumns="false" CellPadding="0" CellSpacing="0" GridLines="horizontal" Width="98%">
                   <Columns>
                   <asp:BoundField DataField="CrewNumber" HeaderText="Emp #">
                       <ItemStyle HorizontalAlign="Center" Width="50px" />
                   </asp:BoundField>
                    <asp:BoundField DataField="FullName" HeaderText="Crew Name">
                       <ItemStyle HorizontalAlign="Left" />
                   </asp:BoundField>
                    <asp:BoundField DataField="Vessel" HeaderText="Vessel">
                       <ItemStyle HorizontalAlign="Left" />
                   </asp:BoundField>
                   <asp:BoundField DataField="RefNumber" HeaderText="Contract Ref #">
                       <ItemStyle HorizontalAlign="Left" Width="100px"/>
                   </asp:BoundField>
                    <asp:BoundField DataField="SignOffDate" HeaderText="SignOffDate">
                       <ItemStyle HorizontalAlign="Center" Width="80px"/>
                   </asp:BoundField>
                   </Columns>
                        <SelectedRowStyle CssClass="selectedtowstyle" />
                        <PagerStyle CssClass="pagerstyle" />
                        <HeaderStyle CssClass="headerstylefixedheadergrid" />
                        <RowStyle CssClass="rowstyle" />
                    </asp:GridView>
                  </div> 
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
