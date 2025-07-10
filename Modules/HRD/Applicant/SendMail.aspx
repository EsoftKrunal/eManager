<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendMail.aspx.cs" Inherits="SendMail" ValidateRequest="false" EnableEventValidation="false" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Send Mail</title>
    <link href="../Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="JS/Common.js"></script>
    <style type="text/css">
        .style1
        {
            text-align: left;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="font-family:Arial;font-size:12px;">
    <center>
        <div class="text headerband">  <h1><asp:Label ID="lblHeader" runat="server"></asp:Label></h1> </div>
    
     
     

        <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:800px;border-collapse:collapse;" >
            <col style="text-align:right" />
            <col style="text-align:right" />
            <col style="text-align:left" />
            <col style="text-align:left" />
            <tr>
                <td >&nbsp;</td>
                <td style="text-align :left">From :</td>
                <td style="text-align :left"><asp:TextBox runat="server" ID="txtFrom" Width="500px"  Enabled="false"></asp:TextBox> </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td style="text-align :left">To :</td>
                <td style="text-align :left"><asp:TextBox runat="server" ID="txtTo" Width="500px"></asp:TextBox></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td style="text-align :left">CC :</td>
                <td style="text-align :left">
                    <asp:TextBox runat="server" ID="txtCC" Width="500px" ></asp:TextBox>
                   <%-- <asp:DropDownList ID="ddlCC" runat="server" Width="210px"></asp:DropDownList>--%>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td style="text-align :left">Bcc :</td>
                <td style="text-align :left"><asp:TextBox runat="server" ID="txtBCC" Width="500px"></asp:TextBox> </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td style="text-align :left">Subject :</td>
                <td style="text-align :left"><asp:TextBox runat="server" ID="txtSubject" Width="500px"></asp:TextBox> </td>
                <td>&nbsp;</td>
            </tr>
              <%--<tr>
                <td>&nbsp;</td>
                <td style="text-align :left">Attachment :</td>
                <td style="text-align :left"> <a id="aFile" runat="server" target="_blank"> <img src="../Images/paperclip.gif" style="border:solid 0px red;"/></a> </td>
                <td>&nbsp;</td>
            </tr>--%>
            <tr>
                <td colspan="4" class="style1" ><b>Mail Message :</b></td>
            </tr>
            <tr>
                <td colspan="4" style=" text-align:left" >
                    <div id="dvApplicantDetails" runat="server">
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="4"  style="text-align:left;">
                <asp:HiddenField runat="server" ID="hfdMessage" /> 
                    Enter your comments here :- 
                    <div runat="server" id="dvContent" contenteditable="true" style="text-align:left; border:solid 1px black;width:100%;height:100px">
                        <asp:Literal runat="server" ID="litMessage" ></asp:Literal> 
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <%--<asp:ImageButton runat="server" ID="btnSend" ImageUrl="~/Modules/HRD/Images/sendmail.jpg" onclick="btnSend_Click" OnClientClick="hfdMessage.value=dvContent.innerHTML;" />--%>
                    
                </td>
            </tr>
        </table>
          <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <asp:Button ID="btnSend"  runat="server" onclick="btnSend_Click" OnClientClick="hfdMessage.value=dvContent.innerHTML;"  Text=" Send Mail"  CssClass="btn"  />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Font-Bold="true" ></asp:Label> &nbsp;
                </td>
            </tr>
        </table>
    </center>
    </div>
    </form>
</body>
</html>
