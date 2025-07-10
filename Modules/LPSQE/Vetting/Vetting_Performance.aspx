<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Vetting_Performance.aspx.cs" Inherits="Vetting_Vetting_Performance" Title="Untitled Page" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>EMANAGER</title>
    <script src="../js/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="../JS/Common.js" type="text/javascript"></script>
    <script src="VettingScript.js" type="text/javascript"></script>
    <link href="Vettingstyle.css" rel="stylesheet" type="text/css" />

  
 </head>
 <body>
 <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <table cellpadding="0" cellspacing="0" width="100%" border="0">
    <tr>
    <td style="width:200px; vertical-align:top;">
    <table cellpadding="3" cellspacing="0" width="100%" border="0">
    <tr><td style="background-color:#001A66; color:White; height:20px;"><b>&nbsp;&nbsp;SIRE Reports</b></td></tr>
    <tr><td><asp:RadioButton runat="server" ID="RadioButton1" GroupName="rt" Text="Total No of Inspections"/></td></tr>
    <tr><td><asp:RadioButton runat="server" ID="RadioButton2" GroupName="rt" Text="No of Inspections"/></td></tr>
    <tr><td><asp:RadioButton runat="server" ID="RadioButton3" GroupName="rt" Text="Inspection Results"/></td></tr>
    <tr><td><asp:RadioButton runat="server" ID="RadioButton4" GroupName="rt" Text="Avg. Obs. Trend Line"/></td></tr>
    <tr><td><asp:RadioButton runat="server" ID="RadioButton5" GroupName="rt" Text="No of Obs per Chapter"/></td></tr>
    <tr><td><asp:RadioButton runat="server" ID="RadioButton6" GroupName="rt" Text="Avg. Observations per Vessel"/></td></tr>
    <tr><td><asp:RadioButton runat="server" ID="RadioButton7" GroupName="rt" Text="Suptd. Attend. for Insps."/></td></tr>
    </table>
    <table cellpadding="3" cellspacing="0" width="100%" border="0">
    <tr><td style="background-color:#001A66; color:White; height:20px;"><b>&nbsp;&nbsp;CDI Reports</b></td></tr>
    <tr><td><asp:RadioButton runat="server" ID="RadioButton11" GroupName="rt" Text="Total No of Inspections"/></td></tr>
    <tr><td><asp:RadioButton runat="server" ID="RadioButton12" GroupName="rt" Text="No of Inspections"/></td></tr>
    <tr><td><asp:RadioButton runat="server" ID="RadioButton13" GroupName="rt" Text="Inspection Results"/></td></tr>
    <tr><td><asp:RadioButton runat="server" ID="RadioButton14" GroupName="rt" Text="Avg. Obs. Trend Line"/></td></tr>
    <tr><td><asp:RadioButton runat="server" ID="RadioButton15" GroupName="rt" Text="No of Obs per Chapter"/></td></tr>
    <tr><td><asp:RadioButton runat="server" ID="RadioButton16" GroupName="rt" Text="Avg. Observations per Vessel"/></td></tr>
    <tr><td><asp:RadioButton runat="server" ID="RadioButton17" GroupName="rt" Text="Suptd. Attend. for Insps."/></td></tr>
    </table>
    <td>
    <div style="text-align:center">
    <asp:UpdatePanel runat="server" ID="up1">
    <ContentTemplate>
    <div style="background-color:#001A66; height:21px;color:White; vertical-align:middle; padding-top:4px;">
    <b>Other Filters </b>
    </div>
    <div style="padding:3px; border:solid 1px grey;">
    <table cellpadding="3" cellspacing="0" width="100%" border="0" style="border-collapse:collapse; ">
    <tr>
        <td>
            Year :</td>
        <td>
             <asp:DropDownList ID="ddlYear" runat="server" Visible="true" Width="100px">
             </asp:DropDownList>
        </td>
        <td>
        &nbsp;<asp:RadioButton ID="rad_Owner" runat="server" 
                AutoPostBack="true" Checked="true" Font-Size="13px" GroupName="b" 
                OnCheckedChanged="rad_Owner_fleet_OnCheckedChanged" Text="Owner" />
            &nbsp;/<asp:RadioButton ID="rad_Fleet" runat="server" AutoPostBack="true" 
                Font-Size="13px" GroupName="b" 
                OnCheckedChanged="rad_Owner_fleet_OnCheckedChanged" Text="Fleet" />
            &nbsp;:</td>
        <td>
            <asp:DropDownList ID="ddl_owner" runat="server" AutoPostBack="true" 
                OnSelectedIndexChanged="ddl_owner_fleet_OnSelectedIndexChanged" Visible="true" 
                Width="200px">
            </asp:DropDownList>
            <asp:DropDownList ID="ddl_fleet" runat="server" AutoPostBack="true" 
                OnSelectedIndexChanged="ddl_owner_fleet_OnSelectedIndexChanged" Visible="false" 
                Width="200px">
            </asp:DropDownList>
        </td>
        <td>
            Vessel :</td>
        <td>
            <asp:DropDownList ID="ddlVessel" runat="server" Width="200px">
            </asp:DropDownList>
        </td>
        </tr>
        <tr>
         <td>
             &nbsp;<asp:Label ID="lblInspection" runat="server" Text="Inspection :"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlInspection" runat="server" Width="100px">
            </asp:DropDownList>
            </td>
        <td>
            &nbsp;Report Level :
        </td>
        <td>
            <asp:DropDownList ID="ddlReportLevel" runat="server" Width="200px">
                <asp:ListItem Text="Monthly" Value="0"></asp:ListItem>
                <asp:ListItem Text="Quarterly" Value="1"></asp:ListItem>
                <asp:ListItem Text="Half-Yearly" Value="2"></asp:ListItem>
                <asp:ListItem Text="Yearly" Value="3"></asp:ListItem>
            </asp:DropDownList>
            </td>
        <td>
            &nbsp;</td>
       <td style="text-align:center">
            <asp:Button ID="btnShow" runat="server" onclick="btnShow_Click" 
                Text="Show Report" />
            <asp:Button ID="btnDownload" runat="server" onclick="btnDownload_Click" 
                Text="Download In Excel" />
            </td>
    </tr>
    </table>
    </div>
    <%--<ajaxToolkit:CalendarExtender id="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton3" PopupPosition="TopRight" TargetControlID="txtFromDate"></ajaxToolkit:CalendarExtender> 
    <ajaxToolkit:CalendarExtender id="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton4" PopupPosition="TopRight" TargetControlID="txtToDate"></ajaxToolkit:CalendarExtender>--%>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnShow"  />
        <asp:PostBackTrigger ControlID="btnDownload"  />
    </Triggers>
    </asp:UpdatePanel>
    </div>
    <iframe runat="server" id="frmChart" src="" width="100%" height="400px"></iframe>
    </td>
    </tr>
    </table>
    
</form>
</body>
</html>

