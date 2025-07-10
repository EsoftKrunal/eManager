<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendCrewDocuments.aspx.cs" Inherits="CrewRecord_SendCrewDocuments" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
     <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
</head>
<body>
    <form id="form1" runat="server">
  <div style="text-align: center" >
         <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center;background-color:#f9f9f9; width: 100%; height: 446px;">
                         <tr>
                <td align="center" style="background-color:#4371a5; height: 23px; width: 875px;" class="text" >
                    Send Mail</td>
            </tr>
                <tr>
                    <td style="height: 432px; width: 875px;" >
                    <table cellpadding="0" cellspacing="0">
                    <tr><td style="height: 13px">  
                       
                        <asp:Label ID="Label1" runat="server" Width="303px" ForeColor="Red"></asp:Label></td></tr>
                    <tr><td style="padding-right: 10px; padding-left: 10px;text-align: center;" align="center">
                  
                           <fieldset style="BORDER-RIGHT: #8fafdb 1px solid; BORDER-TOP: #8fafdb 1px solid; BORDER-LEFT: #8fafdb 1px solid; BORDER-BOTTOM: #8fafdb 1px solid; width: 697px; height: 356px;">
                                                <table border="0" cellpadding="0" cellspacing="0" style="width: 95%; height: 312px">
                                                    <tr>
                                                        <td align="right" style="width: 66%;" valign="top">
                                                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                                            <tr>
                                                                                <td align="right" style="width: 104px; height: 1px">
                                                                                </td>
                                                                                <td style="padding-left: 5px; width: 151px; height: 1px; text-align: left">&nbsp;
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="width: 104px; height: 1px" >
                                                                                    From:</td>
                                                                                <td style="padding-left: 5px; width: 151px; height: 1px; text-align: left" >
                                                                                    <asp:TextBox ID="txtSender" runat="server" CssClass="input_box"
                                                                                        MaxLength="200" meta:resourcekey="txtheightResource1" 
                                                                                        TabIndex="11" Width="598px"></asp:TextBox></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="width: 104px; height: 1px" >
                                                                                </td>
                                                                                <td align="right" colspan="1" style="padding-left: 5px; color: #0e64a0; height: 1px;
                                                                                    text-align: left">
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtSender"
                                                                                        ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                                                                        ControlToValidate="txtReceiver" ErrorMessage="Invalid Email Id" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>
                                                                            </tr>
                                                                            <tr style="color: #0e64a0">
                                                                                <td align="right" style="width: 104px; height: 1px" >
                                                                                    To:</td>
                                                                                <td style="padding-left: 5px; width: 151px; height: 1px; text-align: left" >
                                                                                    <asp:TextBox ID="txtReceiver" runat="server" CssClass="input_box"
                                                                                        MaxLength="200" meta:resourcekey="txtheightResource1" 
                                                                                        TabIndex="11" Width="598px"></asp:TextBox></td>
                                                                            </tr>
                                                                    <tr>
                                                                                <td align="right" style="width: 104px; height: 1px" >
                                                                                </td>
                                                                                <td align="right" colspan="1" style="padding-left: 5px; color: #0e64a0; height: 1px;
                                                                                    text-align: left">
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtReceiver"
                                                                                        ErrorMessage="Required" Width="61px"></asp:RequiredFieldValidator>
                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                                                        ControlToValidate="txtReceiver" ErrorMessage="Invalid Email Id" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td  align="right" style="width: 104px; height: 1px">
                                                                                    Subject:</td>
                                                                                <td  style="padding-left: 5px; width: 151px; height: 1px; text-align: left">
                                                                                    <asp:TextBox ID="txtSubject" runat="server" CssClass="input_box"
                                                                                        MaxLength="200" meta:resourcekey="txtheightResource1" 
                                                                                        TabIndex="11" Width="598px"></asp:TextBox></td>
                                                                            </tr>
                                                                             <tr>
                                                                                <td align="right" style="width: 104px; height: 1px" >
                                                                                </td>
                                                                                <td align="right" colspan="1" style="padding-left: 5px; color: #0e64a0; height: 1px;
                                                                                    text-align: left">
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSubject"
                                                                                        ErrorMessage="Required"></asp:RequiredFieldValidator></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="width: 104px; height: 1px">
                                                                                    &nbsp;Attachments:</td>
                                                                                <td style="padding-left: 5px; height: 1px; width: 151px; text-align: left;">
                                                                                    <asp:ListBox ID="lstattachment" runat="server" Width="603px" CssClass="input_box"></asp:ListBox></td>
                                                                                    
                                                                            </tr>
                                                                              <tr>
                                                                                <td align="right" style="width: 104px; height: 1px" >
                                                                                </td>
                                                                                <td align="right" colspan="1" style="padding-left: 5px; color: #0e64a0; height: 1px;
                                                                                    text-align: left">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="width: 104px; height: 10px">
                                                                                    Body:</td>
                                                                                <td style="padding-left: 5px; width: 151px; height: 10px; text-align: left">
                                                                                    <asp:TextBox ID="txtBody" runat="server" CssClass="input_box" Width="598px" MaxLength="9" meta:resourcekey="txtwaistResource1" TabIndex="14" Height="123px" TextMode="MultiLine"></asp:TextBox></td>
                                                                                    
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="width: 104px; height: 10px">
                                                                                </td>
                                                                                <td style="padding-left: 5px; width: 151px; height: 10px; text-align: left">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="width: 104px; height: 13px;">
                                                                                    </td>
                                                                                <td style="padding-left: 5px; width: 151px; text-align: left; height: 13px;">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="width: 104px; height: 10px">
                                                                                </td>
                                                                                <td align="right" colspan="1" style="padding-left: 5px; height: 10px;">
                        <asp:Button  ID="bin_sendmail" runat="server" Width="70px" Text="Send Mail" CssClass="btn" OnClick="bin_sendmail_Click"  />
                                                                                    <asp:Button  ID="btnback" runat="server" Width="70px" Text="Back" CssClass="btn"  CausesValidation="False" OnClick="btnback_Click"  /></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="width: 104px; height: 3px">
                                                                                    </td>
                                                                                <td align="right" colspan="1" style="height: 3px">
                                                                                    &nbsp; &nbsp;
                                                                                    
                                                                                </td>

                                                                            </tr>
                                                                        </table>
                                                            </td>
                                                    </tr>
                                                </table>
                                                </fieldset>
                  
</td></tr></table></td></tr></table>
         <table width="100%" style="background-color:#f9f9f9;" cellpadding="0" cellspacing="0"  >
        
            </table>
        </div> 

        

        

        

        

        

        

        

    </form>
</body>
</html>
