<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FinalWage.aspx.cs" Inherits="Reporting_FinalWage" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
      <style type="text/css" >
    .fixedbar
    {
        position:fixed;
        margin:80px 0px 0px 120px;   
        background-color:#f0f0f0;  
        z-index:100;
        border:solid 1px #5c5c5c;
    }
    </style>
</head>
<body>
<form id="form1" runat="server" defaultbutton="btnsearch">
 <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
FilterMode="validChars" FilterType="Custom" TargetControlID="txtempno" ValidChars="0123456789sSyY">
</ajaxToolkit:FilteredTextBoxExtender>

<table style="width :100%;font-family:Arial;font-size:12px;" cellpadding="0" cellspacing="0">
<tr>
<td style=" text-align :left; vertical-align : top;" >  
        <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
<tr>
<td align="center" valign="top" style="height: 224px" >
        <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;
            border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid;
            text-align: center" width="100%">
            <tr>
                <td align="center" class="text headerband" style="width: 100%; ">
                    Monthly Wage Pay To Crew</td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <table border="0" cellpadding="0" cellspacing="0" style="background-color: #ffffff" width="100%">
                        <tr>
                            <td style="padding-right: 10px; width: 100%; color: red; text-align: center">
                                <asp:Label ID="lblMessage" runat="Server" Font-Size="12px" ForeColor="Red" meta:resourcekey="lblMessageResource1"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                 <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr style="padding :3px" >
                                        <td style="height: 17px; width: 91px; text-align: right; padding-right: 5px;">
                                            Emp No:</td>
                                            <td style="height: 17px">
                                                <asp:TextBox ID="txtempno" runat="server" style="text-transform:uppercase" CssClass="required_box" Width="137px" MaxLength="6"></asp:TextBox>
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtempno"
                                                 ErrorMessage="Required"></asp:RequiredFieldValidator>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                &nbsp; &nbsp;
                                            <asp:Button ID="btnsearch" runat="server" CssClass="btn" Text="Show Report" OnClick="btnsearch_Click"   /></td>
                                            
                                      
                                    </tr>
                                     <tr>
                                         <td style="height: 4px" colspan="2">
                                                    <iframe runat="server" id="IFRAME1" frameborder="1" style="width: 100%; height:430px; overflow:auto"></iframe>
                                         </td>
                                     </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
            </td>
        </tr>
    </table>
    </td>
    </tr>
    </table>
    <div>
</div>
</form>
</body>
</html>
