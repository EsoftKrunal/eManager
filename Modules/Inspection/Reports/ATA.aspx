<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="ATA.aspx.cs" Inherits="Reports_ATA"  %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692FBEA5521E1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
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
        <div class="text headerband" style="font-family:Arial;">
             Audit Trend Analysis Report
        </div>
<table style="font-family:Arial;font-size:12px;">
       
<tr>
<td><asp:RadioButton runat="server" id="radRCA" GroupName="red" Text="RCA" Checked="true" AutoPostBack="true" OnCheckedChanged="radtype_OnCheckedChanged" /></td>
<td><asp:RadioButton runat="server" id="radSC" GroupName="red" Text="Sire Chapter" AutoPostBack="true" OnCheckedChanged="radtype_OnCheckedChanged" /> </td>
<%--<td><asp:RadioButton runat="server" id="radReport" GroupName="red" Text="Report" AutoPostBack="true" OnCheckedChanged="radtype_OnCheckedChanged" /> </td>--%>
<%--<td><asp:RadioButton runat="server" id="radAnalysis" GroupName="red" Text="Analysis" AutoPostBack="true" OnCheckedChanged="radtype_OnCheckedChanged" /> </td>--%>
</tr>
</table>
<div>
<table width='100%' style="font-family:Arial;font-size:12px;">
<tr>
<td style="width:230px; vertical-align:top;">
<table border="0" cellpadding="0" cellspacing="0" width="100%">
<tr>
<td style="text-align: left">
    <table cellpadding="2" cellspacing="0" width="100%" border="1" style="border-collapse:collapse" >
    <tr>
    <td style="text-align: left" valign="bottom">
                    <strong>From Date :</strong></td>
    </tr>
        <tr>
            <td style="text-align: center" valign="bottom">
                <asp:TextBox ID="txtfromdate" runat="server" CssClass="input_box" Width="100px" style="text-align:center"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" 
                    Format="dd-MMM-yyyy" PopupButtonID="imgfrom" PopupPosition="TopRight" 
                    TargetControlID="txtfromdate">
                </ajaxToolkit:CalendarExtender>
                <asp:ImageButton ID="imgfrom" runat="server" CausesValidation="False" 
                    ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
              
            </td>
        </tr>
        <tr>
            <td style="text-align: left" valign="bottom">
                <strong>To Date :</strong></td>
        </tr>
        <tr>
            <td style="text-align: center" valign="bottom">
                
                <asp:TextBox ID="txttodate" runat="server" CssClass="input_box" Width="100px" style="text-align:center"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" 
                    Format="dd-MMM-yyyy" PopupButtonID="imgto" PopupPosition="TopRight" 
                    TargetControlID="txttodate">
                </ajaxToolkit:CalendarExtender>
                <asp:ImageButton ID="imgto" runat="server" CausesValidation="False" 
                    ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
        </tr>
        <tr>
            <td style="text-align: left" valign="bottom">
                <strong>Status :</strong></td>
        </tr>
    <tr>
     <td style="text-align: center" valign="bottom">
                    <asp:RadioButton ID="rad_All" runat="server" Checked="true" GroupName="ab" Text="All" />
                    <asp:RadioButton ID="rad_O" runat="server" GroupName="ab" Text="Open" />
                    <asp:RadioButton ID="rad_C" runat="server" GroupName="ab" Text="Closed" />
                </td>
    </tr>
    
    <tr>
      <td style="text-align: center; padding:5px" valign="bottom">
                    <asp:Button ID="btn_Analyse" runat="server" CssClass="btn" Text="Analyse" OnClientClick="DisableMe(this);" OnClick="btn_Analyse_Click" Width="90px" style='padding:5px;' />
                    <asp:Button ID="btn_Show" runat="server" CssClass="btn" Text="Show Report" OnClientClick="DisableMe(this);" OnClick="btn_Show_Click" Width="90px" style=' padding:5px;'/>
                    <%--&nbsp;<asp:Button ID="btn_Clear" runat="server" CssClass="btn" OnClick="btn_Clear_Click" Text="Clear" Width="80px"  style='background-color:#FFA319;color:White; padding:5px;' />--%>
      </td>
    </tr>
    </table>
</td>
</tr>
</table>
</td>
<td>
<iframe runat="server" id="frm1" width="100%" height="425px" frameborder="1" scrolling="yes">
</iframe>
</td>
    </tr>
</table>
</div>
<script type="text/javascript">
    function CallAfterRefresh()
    { }
    function DisableMe() {
        DisableAfter();
    }
    function DisableAfter() {
        window.setTimeout(function () {
            
            document.getElementById('btn_Show').setAttribute('value', 'loading...');
            document.getElementById('btn_Show').style.backgroundColor = 'grey';
            document.getElementById('btn_Show').onclick = function () { return false; };

            document.getElementById('btn_Analyse').setAttribute('value', 'loading...');
            document.getElementById('btn_Analyse').style.backgroundColor = 'grey';
            document.getElementById('btn_Analyse').onclick = function () { return false; };

        }, 20);
    }
</script>
</form>
</body>
</html>
