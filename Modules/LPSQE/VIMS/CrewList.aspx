<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewList.aspx.cs" Inherits="VIMS_CrewList" %>
<%@ Register Src="~/Modules/PMS/UserControls/Left.ascx" TagName="Left" TagPrefix="uc2" %>
<%--<%@ Register Src="UserControls/HeaderMenu.ascx" TagName="HMenu" TagPrefix="hm" %>--%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="~/Modules/PMS/UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>
<%@ Register Src="~/Modules/PMS/UserControls/Footer.ascx" TagName="footer" TagPrefix="mtm" %>
<%@ Register src="~/Modules/LPSQE/VIMS/VIMSMenu.ascx" tagname="VIMSMenu" tagprefix="mtm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eMANAGER</title>
    <link href="../CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/tabs.css" rel="stylesheet" type="text/css" />
    <script src="../JS/Common.js" type="text/javascript"></script>
    </head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <mtm:VIMSMenu runat="server" ID="VIMSMenu1" />
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <%--<tr><td>
        <hm:HMenu runat="server" ID="menu2" />  
        </td></tr>--%>
        <tr>
        <td style=" text-align :left; vertical-align : top;" >
        <asp:UpdatePanel runat="server" ID="ss">
        <ContentTemplate>
        <table border="0" cellpadding="0" cellspacing="0" style="border: #4371a5 1px solid;" width="100%">
            <tr>
                <td style=" background-color:#4371a5; color:White; font-size:12px; padding:3px;text-align:center ">
                    Crew List
                </td>
            </tr>
        </table>

        <table border="0" cellpadding="4" cellspacing="0" style="border: #e6dcdc 1px solid; text-align: center" width="100%">
                <col width="80px" />
                <col width="200px" />
                <col width="80px" />
                <col width="200px" />
                <col  />
                <tr>
                    <td>
                        Year
                    </td>
                    <td style="text-align:left;">
                        <asp:DropDownList ID="ddlyear" runat="server" Width="80px"></asp:DropDownList>
                    </td>
                    <td>
                        Month
                    </td>
                    <td style="text-align:left;">
                        <asp:DropDownList ID="ddlMonth" runat="server" Width="80px">
                            <asp:ListItem Value="" Text="All"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                            <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                            <asp:ListItem Value="5" Text="May"></asp:ListItem>
                            <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                            <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                            <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                            <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                            <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                            <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                            <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                        </asp:DropDownList>

                    </td>
                    
                    <td style="text-align:right;padding-right:20px;">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_OnClick" />
                    </td>
                </tr>
            </table>

        <div style="height: 25px; overflow-y: scroll; overflow-x: hidden; border: solid 1px #00ABE1;">
            <table width="100%" border="1" cellpadding="3" cellspacing="0" style="background-color: #00ABE1; border-collapse: collapse;" bordercolor="white">
                <thead>
                    <tr style='color: White; height: 25px;'>
                        <td style="width: 100px; color: White;"><b>Vessel</b></td>
                        <td style="width: 100px; color: White;"><b>Crew#</b></td>
                        <td style="color: White;"><b>Crew name</b></td>
                        <td style="width: 100px; color: White;"><b>Sing On</b></td>
                        <td style="width: 100px; color: White;"><b>Sign Off</b></td>
                    </tr>
                </thead>
            </table>
        </div>
        <div style="height: 500px; border-bottom: none; border: solid 1px #00ABE1; overflow-x: hidden; overflow-y: scroll;" class='ScrollAutoReset' id='dv_FocusCamp_List'>
            <table width="100%" border="1" cellpadding="3" cellspacing="0" style="background-color: #F5FCFE; border-collapse: collapse;" class='newformat'>
                <tbody>
                    <asp:Repeater runat="server" ID="rptCrewList">
                        <ItemTemplate>
                            <tr>
                                <td style="width: 100px;"><%#Eval("VesselCode")%></td>
                                <td style="width: 100px; text-align: left;"><%#Eval("CrewNumber")%></td>
                                <td style=""><%#Eval("CrewName")%></td>
                                <td style="width: 100px; text-align: left;"><%#Common.ToDateString(Eval("SignOnDate"))%></td>
                                <td style="width: 100px;"><%#Common.ToDateString(Eval("SignOffDate"))%></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>

      

        </ContentTemplate>
        
        </asp:UpdatePanel>
        </td>
        </tr>
        </table>  
     </div>
     <mtm:footer ID="footer1" runat ="server" />
    </form>
</body>
</html>
