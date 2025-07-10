<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VerifyJobsPopUp.aspx.cs" Inherits="VerifyJobsPopUp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Verify Jobs</title>
    
    <link href="CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="CSS/CalenderStyle.css" rel="Stylesheet" type="text/css" />
    
    <script type="text/javascript">
    window.resizeTo(650,350);            
    </script>
    <script type="text/javascript">
        function RefereshBackPage()
        {
            window.opener.ReloadPage();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="height: 20px; text-align :center; padding-top :4px;" class="orangeheader"  >
           <asp:Label ID="lblCompName" runat="server" style="font-weight:bold; font-size:14px;" ></asp:Label>
        </div>
        <table cellpadding="2" cellspacing="0" width="100%">
            <tr style="color:Blue;" valign="middle">
                <td colspan="2" align="center">
                    
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <b> Office Comments </b><br />
                    <asp:TextBox ID="txtComment" runat="server"  TextMode="MultiLine" Width="100%" Height="120px"></asp:TextBox>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    Verified By :
                    <asp:Label ID="lblVerifiedBy" runat="server" style="font-weight:bold;" ></asp:Label>
                </td>
                <td>
                    Verified On :
                    <asp:Label ID="lblVerifiedOn" runat="server" style="font-weight:bold;"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_OnClick" Text="Save" CssClass="btn"  />                    
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
