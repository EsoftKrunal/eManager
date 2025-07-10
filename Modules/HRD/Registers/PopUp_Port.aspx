<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PopUp_Port.aspx.cs" Inherits="Registers_PopUp_Port" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Shipsoft : Port Entry </title>
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
<body style="text-align: center">
    <form id="form1" runat="server">
    <%-- <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
        <table cellpadding="10" cellspacing="0">
            <tr><td><asp:Label ID="lbl_Port_Message" runat="server" ForeColor="#C00000"></asp:Label></td></tr>
          <tr>
            <td style="text-align: center;">
            <FIELDSET style="BORDER-RIGHT: #8fafdb 1px solid; BORDER-TOP: #8fafdb 1px solid; BORDER-LEFT: #8fafdb 1px solid; BORDER-BOTTOM: #8fafdb 1px solid; width: 368px; height: 140px;" class="">
            <LEGEND><strong>Port Details</strong></LEGEND>
            <TABLE style="TEXT-ALIGN: center" cellSpacing=0 cellPadding=0 width="100%" border=0>
            <TBODY><TR><TD colSpan=3>
                </TD></TR><TR>
                <TD style="PADDING-RIGHT: 15px; TEXT-ALIGN: right" align=right></TD><TD style="TEXT-ALIGN: left">&nbsp; </TD><TD style="TEXT-ALIGN: left"></TD></TR><TR><TD style="PADDING-RIGHT: 15px; TEXT-ALIGN: right" align=right>Country:</TD><TD style="TEXT-ALIGN: left"><asp:DropDownList id="ddl_P_Country" tabIndex=2 runat="server" CssClass="required_box" Width="245px">
                                                                    </asp:DropDownList></TD><TD style="TEXT-ALIGN: left"></TD></TR><TR><TD style="PADDING-RIGHT: 15px; TEXT-ALIGN: right" align=right></TD><TD style="TEXT-ALIGN: left"><asp:RangeValidator id="RangeValidator1" runat="server" ErrorMessage="Required." ControlToValidate="ddl_P_Country" Type="Integer" MinimumValue="1" MaximumValue="5000"></asp:RangeValidator></TD><TD style="TEXT-ALIGN: left"></TD></TR><TR><TD style="PADDING-RIGHT: 15px; TEXT-ALIGN: right" align=right>Port:</TD><TD style="TEXT-ALIGN: left"><asp:TextBox id="txtPortName" tabIndex=1 runat="server" CssClass="required_box" Width="240px" MaxLength="49"></asp:TextBox></TD><TD style="TEXT-ALIGN: left"></TD></TR>
                <tr>
                    <td align="right" style="padding-right: 15px; text-align: right">
                    </td>
                    <td style="text-align: left">
                        <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ErrorMessage="Required" ControlToValidate="txtPortName"></asp:RequiredFieldValidator></td>
                    <td style="text-align: left">
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center" colspan="3">
                        <asp:Button ID="btn_Save_Port" runat="server" CssClass="btn" OnClick="btn_Save_Port_Click" TabIndex="5" Text="Save" Width="59px" />
                        <asp:Button ID="Button1" runat="server" CausesValidation="False" CssClass="btn" OnClientClick="return reload();" TabIndex="4" Text="Close" Width="59px" />
                    </td>
                </tr>
            </TBODY></TABLE>
            </FIELDSET>
            </td>
         </tr>
        </table>
    </form>
</body>
</html>
