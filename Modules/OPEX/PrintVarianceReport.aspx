<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintVarianceReport.aspx.cs" Inherits="PrintVarianceReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER </title>
    <link href="../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" >
        function check() 
        {
            obj = document.getElementById('ddlReportLevel');
            if (obj.selectedIndex == 0) 
            {
                alert('Please Select Report Level');
                return false;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" style="font-family:Arial;font-size:12px;">
        <table cellpadding="0" cellspacing="0" width="100%" border="1" style="border-collapse:collapse;">
            <tr class="text headerband" >
                <td colspan="6" style="font-weight:15px;padding:4px;">
                    Variance Report
                </td>
            </tr>
            <tr style="text-align:center;" >
                <td>
                    Current Finance Year</td>
                <td>
                    Month
                </td>
                <td>
                    Company
                </td>
                <td>
                    Vessel
                </td>
                <td>
                    Report Level
                </td>
                <td>
                </td>
            </tr>
            <tr style="text-align:center;">    
                <td>
                    <asp:Label ID="lblYear" runat="server" ></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblMonth" runat="server" ></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblCompany" runat="server" ></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblVessel" runat="server" ></asp:Label>                    
                </td>
                <td>
                    <%--<asp:DropDownList ID="ddlReportLevel" runat="server" 
                        onselectedindexchanged="ddlReportLevel_SelectedIndexChanged"  AutoPostBack="true"  >
                    <asp:ListItem  Value="0"> Select</asp:ListItem>
                    <asp:ListItem  Value="1"> General Summary</asp:ListItem>
                    <asp:ListItem  Value="2"> Budget Summary</asp:ListItem>
                    <asp:ListItem  Value="3"> Account Summary</asp:ListItem>
                    <asp:ListItem  Value="4"> Account Details</asp:ListItem>
                    </asp:DropDownList>--%>
                    <%--<asp:Label ID="lblReportLevel" runat="server" ></asp:Label>--%>
                    <asp:DropDownList ID="ddlReportLevel" runat="server" Width="160px" AutoPostBack="true" OnSelectedIndexChanged="ddlReportLevel_OnSelectedIndexChanged" >
                            <asp:ListItem  Value="" Selected="True"> Select</asp:ListItem>
                            <asp:ListItem  Value="1"> General Summary</asp:ListItem>
                            <asp:ListItem  Value="2"> Budget Summary</asp:ListItem>
                            <asp:ListItem  Value="3"> Account Summary</asp:ListItem>
                            <asp:ListItem  Value="4"> Account Details</asp:ListItem>
                            <%--<asp:ListItem  Value="5"> Detail Activity Report</asp:ListItem>
                            <asp:ListItem  Value="6"> CLS Report</asp:ListItem>--%>
                            <%--<asp:ListItem  Value="7"> Budget Comment Report</asp:ListItem>--%>
                            </asp:DropDownList>
                </td>
                <td>
                    
                </td>
            </tr>
            <tr>    
                <td colspan="6" style="text-align:left;">
                         <iframe id="IFRAME1" frameborder="1" style="width: 100%; height:500px; overflow:auto" runat="server"  ></iframe>

                </td>
            </tr>
        </table>
    </form>
</body>
</html>
