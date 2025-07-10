<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowChangeRecord.aspx.cs" Inherits="ShowChangeRecord" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link rel="Stylesheet" href="CSS/style.css" />
</head>
<body>
    <form id="form1" runat="server">
    <center>
    <div style="font-size: 15px; font-weight: bold; color: White; background-color: #4371a5; padding:4px;">Change Records</div> 
    </center>
    <div style="padding:5px 30px 5px 30px; text-align:right; width:95%" >
    <!------------------------ COMMON HEADER SECTION ----------------------->
    <div>
            <div style="text-align:left;">
                <asp:Label runat="server" ID="lblManualName" Font-Bold="true" Font-Names="Arial" Font-Size="16px" style="float:left"></asp:Label>
                <asp:Label runat="server" ID="lblMVersion" Font-Bold="true" style="float:right"></asp:Label> <br />
            </div>
            <br />
    </div>
    <!----------------------------------------------->
    <div style="text-align:left">

    <asp:GridView ID="gvChangeRecords" runat="server" Width='100%' BackColor="White" 
            BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
            EnableModelValidation="True" ForeColor="Black" GridLines="Vertical" AutoGenerateColumns="false">
            <Columns>
            <asp:TemplateField HeaderText="View" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
            <ItemTemplate>
                <a href='ReadManualHistorySection.aspx?HistoryID=<%#Eval("HistoryID") %>' target="_blank" > <img src="../../HRD/Images/HourGlass.png" /> </a>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Section"  HeaderText="Section" ItemStyle-Width="50px"/>
            <asp:BoundField DataField="ManualVersion"  HeaderText="Version" ItemStyle-Width="50px"/>
            
            <asp:BoundField DataField="Revision#"  HeaderText="Revision#" ItemStyle-Width="30px"/>
            <asp:BoundField DataField="heading"  HeaderText="Heading"/>
            <asp:TemplateField HeaderText="Approved On" ItemStyle-Width="100px">
            <ItemTemplate>
                <%# Common.ToDateString(Eval("Approved On")) %>
            </ItemTemplate>
            </asp:TemplateField>
            </Columns>
        <AlternatingRowStyle BackColor="White" />
        <FooterStyle BackColor="#CCCC99" />
        <HeaderStyle CssClass="headerstylefixedheadergrid" />
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <RowStyle BackColor="#F7F7DE" />
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        </asp:GridView> 
    <hr />
    </div>
    </div>
    </form>
</body>
</html>
