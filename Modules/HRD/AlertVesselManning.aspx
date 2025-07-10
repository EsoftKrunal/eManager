<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AlertVesselManning.aspx.cs" Inherits="AlertVesselManning" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" /> 
     <link href="Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="Styles/StyleSheet.css" />
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>Vessel Manning Alert</title>
<link href="Styles/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center" width="100%">
            <tr style="height:23px">
                <td align="center" style="background-color:#4371a5;" class="text">
                    Vessel Manning Alerts</td>
            </tr>
            <tr style="height:30px">
                <td>
                    <br />
                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td style="padding-right: 15px; text-align: right">
                                <asp:TextBox ID="TextBox1" runat="server" BackColor="#99FF33" CssClass="input_box"
                                    Width="37px" ReadOnly="True"></asp:TextBox>
                                :</td>
                            <td style="text-align: left">
                                Actual Manning less than Safe Manning</td>
                            <td style="padding-right: 15px; text-align: right">
                                <asp:TextBox ID="TextBox2" runat="server" BackColor="#FCC2BC" CssClass="input_box"
                                    Width="37px" ReadOnly="True"></asp:TextBox>
                                :</td>
                            <td style="text-align: left">
                                Actual Manning greater than Bugeted Manning</td>
                        </tr>
                    </table>
                  <br />
                  <div style=" width:100%; height:450px; overflow-x:hidden; overflow-y:scroll;" > 
                   <asp:GridView ID="gv_VesselManning" runat="server" OnRowDataBound="gv_VesselManning_RowDataBound" AutoGenerateColumns="false" CellPadding="0" CellSpacing="0" GridLines="horizontal" Width="98%">
                   <Columns>
                   <asp:TemplateField Visible="False">
                   <ItemTemplate>
                   <asp:HiddenField ID="hiddenAvalue" runat="server" Value='<%# Eval("AValue") %>' />
                   </ItemTemplate>
                   </asp:TemplateField>
                   <asp:BoundField DataField="VesselCode" HeaderText="VSL">
                       <ItemStyle HorizontalAlign="Left" Width="100px" />
                   </asp:BoundField>
                    <asp:BoundField DataField="RankCode" HeaderText="Rank">
                       <ItemStyle HorizontalAlign="Left" Width="100px" />
                   </asp:BoundField>
                    <asp:BoundField DataField="Safe" HeaderText="Safe Manning">
                       <ItemStyle HorizontalAlign="Center" Width="80px"/>
                   </asp:BoundField>
                  
                   <asp:BoundField DataField="Budget" HeaderText="Budget Manning">
                       <ItemStyle HorizontalAlign="Center" Width="80px"/>
                   </asp:BoundField>
                    <asp:BoundField DataField="Actual" HeaderText="Actual Manning">
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
