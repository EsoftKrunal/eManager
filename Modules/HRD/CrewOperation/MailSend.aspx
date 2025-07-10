<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" EnableEventValidation="false" CodeFile="MailSend.aspx.cs" Inherits="CrewOperation_MailSend" %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Mail Send</title>
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <script type="text/javascript" >
    function SaveState()
    {
        document.getElementById('txta').value=document.getElementById('txtbody').innerHTML;
        return true;
    }
    </script> 
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <center>
    <asp:TextBox Runat="server" ID="txta" Height="125px" Width="541px" style="display : none"></asp:TextBox>   
    <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center;background-color:#f9f9f9; width: 800px; height: 446px;">
            <tr>
                <td align="center" style="background-color:#4371a5; height: 23px; width: 875px;" class="text" >Send Mail</td>
            </tr>
                <tr>
                    <td style=" text-align:center " >
                    <table cellpadding="0" cellspacing="0" width="100%" >
                    <tr>
                        <td style="height: 13px">
                            <asp:Label ID="Label1" runat="server" Width="303px" ForeColor="Red"></asp:Label>
                        </td></tr>
                    <tr>
                    <tr>
                        <td><input type="button" onclick="history.go(-1);" value="Go Back" />   </td>
                    </tr>
                    <td style="text-align :center" >
                    <asp:UpdatePanel >
                    <ContentTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; ">
                        <tr>
                            <td align="center" style="width: 100%;" valign="top">
                            
                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 550px;">
                                                <tr>
                                                    <td align="right" style="width: 104px; height: 1px">
                                                    </td>
                                                    <td style="padding-left: 5px; width: 151px; height: 1px; text-align: left">&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" style="width: 104px; height: 1px" >
                                                        To:</td>
                                                    <td style="padding-left: 5px; height: 1px; text-align: left" >
                                                        <asp:TextBox ID="txtTo" runat="server" CssClass="input_box"
                                                            MaxLength="200" meta:resourcekey="txtheightResource1" 
                                                            TabIndex="11" Width="450px"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTo"
                                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                            </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" style="width: 104px; height: 1px" >
                                                    </td>
                                                    <td align="right" colspan="1" style="padding-left: 5px; color: #0e64a0; height: 1px;
                                                        text-align: left">
                                                      <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                                            ControlToValidate="txtTo" ErrorMessage="Invalid Email Id" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>--%></td>
                                                </tr>
                                                <tr style="color: #0e64a0" id="row1" runat="server"  >
                                                    <td align="right" style="width: 104px; height: 1px" >
                                                        Attn:</td>
                                                    <td style="padding-left: 5px; width: 151px; height: 1px; text-align: left" >
                                                        <asp:TextBox ID="txtAttn" runat="server" CssClass="input_box"
                                                            MaxLength="200" meta:resourcekey="txtheightResource1" 
                                                            TabIndex="11" Width="450px"></asp:TextBox></td>
                                                </tr>
                                        <tr id="row2" runat="server"  >
                                                    <td align="right" style="width: 104px; height: 1px" >
                                                    </td>
                                                    <td align="right" colspan="1" style="padding-left: 5px; color: #0e64a0; height: 1px;
                                                        text-align: left">
                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAttn"
                                                            ErrorMessage="Required" Width="61px"></asp:RequiredFieldValidator>--%>
                                                        <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                            ControlToValidate="txtAttn" ErrorMessage="Invalid Email Id" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>--%></td>
                                                </tr>
                                                 <tr>
                                                    <td  align="right" style="width: 104px; height: 1px">
                                                        CC:</td>
                                                    <td  style="padding-left: 5px; width: 151px; height: 1px; text-align: left">
                                                        <asp:TextBox ID="txtCC" runat="server" CssClass="input_box"
                                                            MaxLength="200" meta:resourcekey="txtheightResource1" 
                                                            TabIndex="11" Width="450px"></asp:TextBox></td>
                                                </tr>
                                                 <tr>
                                                    <td align="right" style="width: 104px; height: 1px" >
                                                    </td>
                                                    <td align="right" colspan="1" style="padding-left: 5px; color: #0e64a0; height: 1px;
                                                        text-align: left">
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td  align="right" style="width: 104px; height: 1px">
                                                        Subject:</td>
                                                    <td  style="padding-left: 5px; height: 1px; text-align: left">
                                                        <asp:TextBox ID="txtSubject" runat="server" CssClass="input_box"
                                                            MaxLength="200" meta:resourcekey="txtheightResource1" 
                                                            TabIndex="11" Width="450px"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSubject"
                                                            ErrorMessage="Required"></asp:RequiredFieldValidator></td>
                                                </tr>
                                                 <tr>
                                                    <td align="right" style="width: 104px; height: 1px" >
                                                    </td>
                                                    <td align="right" colspan="1" style="padding-left: 5px; color: #0e64a0; height: 1px;
                                                        text-align: left">
                                                        &nbsp;</td>
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
                                                    <td align="center" colspan="1" style="padding-left: 5px; height: 10px;">
<asp:Button  ID="bin_sendmail" runat="server" Width="70px" Text="Send Mail" CssClass="btn" OnClientClick="return SaveState();" OnClick="bin_sendmail_Click"    />
                                                        <asp:HiddenField ID="hd1" runat="server" /> 
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" style="width: 104px; hight: 13px;">
                                                        </td>
                                                    <td style="padding-left: 5px; width: 151px; text-align: left; height: 13px;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td align="right" style="width: 104px; height: 117px">
                                                        </td>
                                                    <td style="padding-left: 5px; width: 151px; text-align: left;border :solid 2px black;background-color :White">
                                                   <div id="txtbody" runat="server" style=" width:602px;" contenteditable="true">
                                                   <FTB:FreeTextBox ID="ftb1" runat="server" ></FTB:FreeTextBox>
                                                   </div>
                                                        </td>
                                                        
                                                </tr>
                                                <tr>
                                                    <td align="right" style="width: 104px; height: 10px">
                                                    </td>
                                                    <td style="padding-left: 5px; width: 151px; height: 10px; text-align: left">
                                                    </td>
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
                    </ContentTemplate> 
                    </asp:UpdatePanel>
                    </td></tr>
            </table></td></tr></table>
    </center>
    </div>
    </form>
</body>
</html>
