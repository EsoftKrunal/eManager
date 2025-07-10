<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PopUp_NearestAirPort.aspx.cs" Inherits="Registers_PopUp_NearestAirPort" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
      <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
<script language="javascript">
    function reload()
    {
    opener.location.reload(true);
    self.close();
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: left">
   <%-- <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
    
        <table cellpadding="10" cellspacing="0" style="width: 376px; height: 79px">
            <tr>
                <td style="text-align: center">
            </td>
            </tr>
        <tr>
            <td style="text-align: left; width: 100%;">
            <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; width: 365px; height: 114px;" class="">
                <legend><strong>Nearest Airport Details</strong></legend>
                  <table border="0" cellpadding="0" cellspacing="0" style="text-align: center; width: 100%;" >
                    <tr>
                      <td colspan="2"><asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label></td>
                    </tr>
                    <tr style="color: #0e64a0">
                      <td align="right" style="padding-right: 15px; text-align: right"></td>
                      <td style="text-align: left"></td>
                    </tr>
                    <tr>
                       <td align="right" style="text-align: right; padding-right:15px; height: 18px;">Country:</td>
                       <td style="text-align: left; height: 18px;"><asp:DropDownList ID="ddlCountryName" runat="server" CssClass="required_box" Width="205px" TabIndex="1"></asp:DropDownList></td>
                    </tr>
                    <tr>
                          <td align="right" style="padding-right: 15px; text-align: right; height: 2px;"></td>
                          <td style="text-align: left; height: 2px;"><asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="ddlCountryName" ErrorMessage="Required" MaximumValue="1000" MinimumValue="1" Type="Integer"></asp:RangeValidator></td>
                    </tr>
                    <tr>
                          <td align="right" style="padding-right: 15px; text-align: right">Nearest Airport:</td>
                          <td style="text-align: left"><asp:TextBox ID="txtNearestAirportName" runat="server" CssClass="required_box" Width="200px" TabIndex="2" MaxLength="49"></asp:TextBox></td>
                    </tr>
                    <tr>
                          <td align="right" style="padding-right: 15px; text-align: right"></td>
                          <td style="text-align: left"><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNearestAirportName" ErrorMessage="Required"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                          <td align="right" style="padding-right: 15px; text-align: center" colspan="2"><asp:Button ID="btn_Add_NearestAirport" runat="server" CssClass="btn" Text="Save" Width="59px" OnClick="btn_Add_NearestAirport_Click" CausesValidation="False" TabIndex="4" /><asp:Button ID="Button1" runat="server" CssClass="btn" Text="Close" Width="59px"  OnClientClick="return reload();" CausesValidation="False" TabIndex="4" /></td>
                    </tr>
               </table>
               </fieldset>
          </td>
         </tr>
        </table>
      
    </div>
    </form>
</body>
</html>
