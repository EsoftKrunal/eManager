<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WageScalePopupCopy.aspx.cs" Inherits="Registers_WageScalePopupCopy" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" >
    function BindHistory(id)
    {
        window.parent.frames[0].document.getElementById('txtHistory').value=id;    
        window.parent.frames[0].document.getElementById('btn_Show_History').click();
        parent.tb_remove();
        return false;
    }
    function BindCopy()
    {   
        var WSId=document.getElementById('<%=ddcopywage.ClientID%>').value;
        var Sen=document.getElementById('<%=txtcopyseniority.ClientID%>').value;
        var Nat=document.getElementById('<%=ddcopynationality.ClientID%>').value;
        window.parent.frames[0].document.getElementById('txtCopy').value="WCId=" + WSId + "&Sen=" + Sen + "&Nat=" + Nat;;    
        window.parent.frames[0].document.getElementById('btn_Show_Copy').click();
        parent.tb_remove();
        return false;
    }
    </script> 
    <style type="text/css">
        .style1
        {
            width: 281px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align :center " >
     <div runat="server" id="dvCopy" style=" text-align :center " >
     <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid;padding-bottom:5px;width:90%">
         <legend><strong>Copy From</strong></legend>
            <table cellpadding="5" cellspacing="5" style="width:85%"  >
                <tr>
                    <td style=" text-align: right;">Wage Scale:</td>
                    <td style="text-align: left; " class="style1"><asp:DropDownList ID="ddcopywage" ValidationGroup="aa" runat="server" CssClass="required_box" Width="274px"></asp:DropDownList></td>
                    <td style="text-align: left; "><asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="ddcopywage" ErrorMessage="Required." MaximumValue="5000" MinimumValue="1" Type="Integer" ValidationGroup="aa"></asp:RangeValidator></td>
                    <td style=" text-align: right">&nbsp;</td>
                </tr>
                
                <tr>
                    <td style=" text-align: right">Nationality:</td>
                    <td style=" text-align: left" valign="top" class="style1">
                        <asp:DropDownList ID="ddcopynationality" runat="server" CssClass="required_box" ValidationGroup="aa" Width="272px"></asp:DropDownList></td>
                    <td style=" text-align: left"><asp:RangeValidator ID="RangeValidator5" runat="server" ControlToValidate="ddcopynationality" ErrorMessage="Required." MaximumValue="5000" MinimumValue="1" Type="Integer" ValidationGroup="aa"></asp:RangeValidator></td>
                    <td style=" text-align: left" valign="top">&nbsp;</td>
                </tr>
                
                <tr>
                    <td style=" text-align: right">Seniority(Year):</td>
                    <td style=" text-align: left" valign="top" class="style1">
                        <asp:TextBox ID="txtcopyseniority" runat="server" CssClass="required_box" ValidationGroup="aa" MaxLength="2" Width="57px"></asp:TextBox></td>
                    <td style=" text-align: left"><asp:RangeValidator ID="RangeValidator6" runat="server" ErrorMessage="Required." MaximumValue="10" MinimumValue="1" Type="Integer" ControlToValidate="txtcopyseniority" Display="Dynamic" ValidationGroup="aa"></asp:RangeValidator> <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required" ControlToValidate="txtcopyseniority" Display="Dynamic"  ValidationGroup="aa"></asp:RequiredFieldValidator></td>
                    <td style=" text-align: left" valign="top">&nbsp;</td>
                </tr>
                
                <tr>
                    <td style=" text-align: right">&nbsp;</td>
                    <td style=" text-align: center" valign="top">
                         <input runat="server" id="b1" validationgroup="aa" type="button" class="btn" onclick='return BindCopy();' value="Copy to create New" style="width :150px" />
                    </td>
                    <td style=" text-align: left">&nbsp;</td>
                    <td style=" text-align: left" valign="top">&nbsp;</td>
                </tr>
            </table>
        </fieldset>
     </div>
     <div runat="server" id="dvHistory" >
        <table width="100%" cellpadding="3" cellspacing="0">
            <tr>
                <td> 
                <asp:Label runat="server" ID="lblHistory"></asp:Label> 
                <asp:Repeater runat="server" id="rpt_History">
                <ItemTemplate >
                <div style=" display:block;" style=" text-decoration:underline; cursor:pointer; font-size:14px;" onclick='BindHistory(<%# Eval("wagescalerankid")%>)'>  <%# Eval("wefdate")%> </div>
                </ItemTemplate>
                </asp:Repeater>   
                </td>
            </tr> 
        </table> 
     </div> 
    </div>
    </form>
</body>
</html>
